using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    /// <summary>
    /// This interface contains the functions that necessary fro communiction with server.
    /// </summary>
    interface ITCPClient
    {
        // Connection function.
        void Connect(string ip, int port);
        // Write to server.
        void Write(string command);
        // Read from server.
        string Read(); 
        // Disconnect function.
        void Disconnect();
    }
}
