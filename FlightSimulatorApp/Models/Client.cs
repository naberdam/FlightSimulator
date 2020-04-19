using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    /// <summary>
    /// Class that contains the fields that responsible of the communication with the server and implements ITCPClient.
    /// </summary>
    public class Client : ITCPClient
    {
        TcpClient tcpclnt;
        NetworkStream stm;
        public Client()
        {
            this.tcpclnt = new TcpClient();
        }

        // Connection function.
        public void Connect(string ip, int port)
        {
            tcpclnt.Connect(ip, port);
            this.stm = this.tcpclnt.GetStream();
        }
        
        // Disconnection function.
        public void Disconnect()
        {
            try
            {
                // Close networkStream.
                tcpclnt.GetStream().Close();
            }// The networkStream was already closed or something else happen.
            catch (Exception) { }
            try
            {
                // Close tcpClient.
                tcpclnt.Close();
            }// The tcpclnt was already closed or something else happen.
            catch (Exception) {}
            tcpclnt = null;
        }

        // Function that read from server and returns string of the message.
        public string Read()
        {
            if (tcpclnt != null)
            {
                // Time out of 10 seconds.
                tcpclnt.ReceiveTimeout = 10000;
                this.stm.ReadTimeout = 10000;
                // Only if the ReceiveBufferSize not empty so we want to convert the message to string and return it.
                if (tcpclnt.ReceiveBufferSize > 0)
                {
                    byte[] bb = new byte[tcpclnt.ReceiveBufferSize];
                    int k = this.stm.Read(bb, 0, 100);
                    string massage = "";
                    for (int i = 0; i < k; i++)
                    {
                        massage += (Convert.ToChar(bb[i]));
                    }
                    return massage;
                }
            }
            return "ERR";
        }

        // Function that write to server.
        public void Write(string command)
        {
            if (tcpclnt != null)
            {
                this.stm = this.tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(command);
                stm.Write(ba, 0, ba.Length);
            }
        }
    }
}
