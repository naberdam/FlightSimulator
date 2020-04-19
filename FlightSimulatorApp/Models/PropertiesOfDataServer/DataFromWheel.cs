using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models.PropertiesOfDataServer
{
    /// <summary>
    /// This class contains the properties of the wheel that we need to send to the server.
    /// </summary>
    public class DataFromWheel : INotifyPropertyChanged
    {
        // Queue for know if we need to send values to server.
        readonly Queue<string> updateDataFromWheel = new Queue<string>();
        public DataFromWheel() { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set
            {
                // Checking if we need to update rudder.
                if (value != rudder)
                {
                    rudder = value;
                    this.updateDataFromWheel.Enqueue("set /controls/flight/rudder " + rudder + "\n");
                }
                
            }
        }

        private double elevator;

        public double Elevator
        {
            get { return elevator; }
            set
            {
                // Checking if we need to update elevator.
                if (value != elevator)
                {
                    elevator = value;
                    this.updateDataFromWheel.Enqueue("set /controls/flight/elevator " + elevator + "\n");
                }
                
            }
        }
        private double throttle;
        public double Throttle
        {
            get { return throttle; }
            set
            {
                // Checking if we need to update throttle.
                if (value != throttle)
                {
                    throttle = value;
                    this.updateDataFromWheel.Enqueue("set /controls/engines/current-engine/throttle " + throttle + "\n");
                }
                
            }
        }
        private double aileron;
        public double Aileron
        {
            get { return aileron; }
            set
            {
                // Checking if we need to update aileron.
                if (value != aileron)
                {
                    aileron = value;
                    this.updateDataFromWheel.Enqueue("set /controls/flight/aileron " + aileron + "\n");
                }
                
            }
        }

        // Function for getting the queue of updateDataFromWheel.
        public Queue<string> GetQueueOfSetupdateDataFromWheel()
        {
            return this.updateDataFromWheel;
        }
    }
}
