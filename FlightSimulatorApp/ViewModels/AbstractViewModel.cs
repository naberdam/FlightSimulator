using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// Abstract class for all ViewModel classes and it implementes INotifyPropertyChanged for notify the views.
    /// </summary>
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AbstractFlightSimulatorModel model;
        // Constructor for all ViewModel that inherit this abstract class.
        public AbstractViewModel(AbstractFlightSimulatorModel modelCreated)
        {
            model = modelCreated;
        }
        // Function that tells to the model to stop connection.
        public void StopConnection()
        {
            this.model.Disconnect();
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
