using FlightSimulator.Models.PropertiesOfDataServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    /// <summary>
    /// This class is the model for our project.
    /// </summary>
    class MyFlightSimulatorModel : AbstractFlightSimulatorModel
    {
        // Mutex variable for locking the server.
        readonly Mutex mutex = new Mutex();
        // Thread that responsible to read from server.
        Thread threadOfRead;
        // Thread that responsible to write to server.
        Thread threadOfWrite;
        Thread stopAllThreads;
        volatile bool timeout = false;
        
        // Activate threads.
        public override void Start()
        {
            threadOfRead = new Thread(StartRead);
            threadOfRead.Start();
            threadOfWrite = new Thread(StartWrite);
            threadOfWrite.Start();
        }

        // The function of threadOfRead.
        private void StartRead()
        {
            // While we dont disconnect or something got wrong, then continue to read data from server.
            while (!stop)
            {
                // Check if there is timeout now.
                ReadTillTimeOutStop();
                UpdateVariablesFromServer("get /instrumentation/heading-indicator/indicated-heading-deg\r\n", "Indicated_heading_deg");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n", "Airspeed_indicator_indicated_speed_kt");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /instrumentation/altimeter/indicated-altitude-ft\r\n", "Altimeter_indicated_altitude_ft");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n", "Attitude_indicator_internal_pitch_deg");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /instrumentation/attitude-indicator/internal-roll-deg\r\n", "Attitude_indicator_internal_roll_deg");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /instrumentation/gps/indicated-altitude-ft\r\n", "Gps_indicated_altitude_ft");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /instrumentation/gps/indicated-ground-speed-kt\r\n", "Gps_indicated_ground_speed_kt");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /instrumentation/gps/indicated-vertical-speed\r\n", "Gps_indicated_vertical_speed");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /position/latitude-deg\r\n", "Latitude");
                if (timeout || stop) { continue; }
                UpdateVariablesFromServer("get /position/longitude-deg\r\n", "Longitude");
                if (timeout || stop) { continue; }
                Thread.Sleep(250);
            }
        }

        // The function of threadOWrite.
        private void StartWrite()
        {
            // While we dont disconnect or something got wrong, then continue to read data from server.
            while (!stop)
            {
                string message_to_server;
                // While there is a message in updateDataFromWheel queue then send it to server.
                while (this.dataFromWheel.GetQueueOfSetupdateDataFromWheel().Count > 0)
                {
                    // Take the message from the queue.
                    message_to_server = this.dataFromWheel.GetQueueOfSetupdateDataFromWheel().Dequeue();
                    mutex.WaitOne();
                    // Write it to the server.
                    WriteToServer(message_to_server);
                    message_to_server = ReadFromServer();
                    mutex.ReleaseMutex();
                    message_to_server = "";
                }
                Thread.Sleep(250);
            }
        }

        // Function that call to write function of tcpClient and catches all the exceptions that can happen.
        private void WriteToServer(string variable)
        {
            try
            {
                tcpClient.Write(variable);
            }
            catch (ObjectDisposedException)
            {
                SetStringException(MessageOfException.ExceptionTypes.WriteObjectDisposedException);
                stop = true;
                return;
            }
            catch (InvalidOperationException)
            {
                SetStringException(MessageOfException.ExceptionTypes.WriteInvalidOperationException);
                stop = true;
                return;
            }
            catch (IOException)
            {
                SetStringException(MessageOfException.ExceptionTypes.WriteIOException);
                stop = true;
                return;
            }
            catch (Exception)
            {
                SetStringException(MessageOfException.ExceptionTypes.RegularException);
                stop = true;
                return;
            }
        }

        // Function that call to read function of tcpClient and catches all the exceptions that can happen.
        private string ReadFromServer()
        {
            try
            {
                string strFromServer = tcpClient.Read();
                if (strFromServer != "ERR") { messageOfException.String_exception = MessageOfException.ExceptionTypes.Nothing; }
                return strFromServer;
            }
            catch (ObjectDisposedException)
            {
                SetStringException(MessageOfException.ExceptionTypes.ReadObjectDisposedException);
                stop = true;
                return "ERR";
            }
            catch (InvalidOperationException)
            {
                SetStringException(MessageOfException.ExceptionTypes.ReadInvalidOperationException);
                stop = true;
                return "ERR";
            }
            catch (TimeoutException)
            {
                SetStringException(MessageOfException.ExceptionTypes.ReadTimeoutException);
                timeout = true;
                return "ERR";
            }
            catch (IOException e)
            {
                mutex.WaitOne();
                // Sometimes there is timeout but this exception belongs to IOException.
                if (e.Message == "Unable to read data from the transport connection: A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.")
                {
                    messageOfException.String_exception = MessageOfException.ExceptionTypes.ReadTimeoutException;
                    timeout = true;
                }
                else
                {
                    // Regular IOException.
                    messageOfException.String_exception = MessageOfException.ExceptionTypes.ReadIOException;
                    stop = true;
                }
                mutex.ReleaseMutex();
                return "ERR";
            }
            catch (Exception)
            {
                SetStringException(MessageOfException.ExceptionTypes.RegularException);
                stop = true;
                return "ERR";
            }
        }
        // Function that check when the timeout has stopped and we can read data drom server.
        private void ReadTillTimeOutStop()
        {
            if (timeout)
            {
                // While we have ReadTimeoutException then keep try reading.
                while (messageOfException.Msg_of_exception == MessageOfException.ReadTimeoutException) { string msg = ReadFromServer(); }
                // The timeout stopped.
                timeout = false;
            }
        }

        
        // Function that change the messageOfException.String_exception according to nameOfException.
        private void SetStringException(MessageOfException.ExceptionTypes nameOfException)
        {
            mutex.WaitOne();
            messageOfException.String_exception = nameOfException;
            mutex.ReleaseMutex();
        }

        // Function that read data of variable_to_get from the server and set it in propertyName.
        private void UpdateVariablesFromServer(string variable_to_get, string propertyName)
        {
            string[] splittedData;
            mutex.WaitOne();
            WriteToServer(variable_to_get);
            splittedData = System.Text.RegularExpressions.Regex.Split(ReadFromServer(), "\n");
            mutex.ReleaseMutex();
            if (splittedData[0] != "ERR")
            {
                try
                {
                    double x = double.Parse(splittedData[0]);
                    var prop = varaiblesFromSimulator.GetType().GetProperty(propertyName);
                    prop.SetValue(varaiblesFromSimulator, x);
                }
                catch (Exception) { }
            }
        }
    }
}
