using FlightSimulator.Models.PropertiesOfDataServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    /// <summary>
    /// Abstract class of all models that want to read or write from/to server.
    /// </summary>
    public abstract class AbstractFlightSimulatorModel
    {
        protected volatile Boolean stop;
        protected Client tcpClient;
        // Class that contains the property of Dashboard and GameMap.
        public VariablesFromSimulator varaiblesFromSimulator = new VariablesFromSimulator();
        // Class that contains the property of wheel.
        public DataFromWheel dataFromWheel = new DataFromWheel();
        // Class that contains the exceptions.
        public MessageOfException messageOfException = new MessageOfException();
        // Connect to server.
        public void Connect(string ip, int port)
        {
            tcpClient = new Client();
            tcpClient.Connect(ip, port);
            stop = false;
            messageOfException.Msg_of_exception = "";
            Start();
        }
        // Function that check if server is connect.
        public bool IsConnect()
        {
            return !stop;
        }
        // Function for disconnect from derver.
        public void Disconnect()
        {
            if(stop == true) { return; }
            stop = true;
            tcpClient.Disconnect();
            varaiblesFromSimulator.ResetAllVariables();
        }
        // Start to read or/and write from/to server.
        public abstract void Start();
        public VariablesFromSimulator GetVariablesFromSimulator() { return this.varaiblesFromSimulator; }
        public DataFromWheel GetDataFromWheel() { return this.dataFromWheel; }
        public MessageOfException GetMessageOfException() { return this.messageOfException; }

    }
}
