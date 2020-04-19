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
    /// This class is ViewModel of the exception messages in SubMainWindow class and inherit the AbstractViewModel.
    /// </summary>
    public class ExceptionVM : AbstractViewModel
    {
        public ExceptionVM(AbstractFlightSimulatorModel modelCreated) : base(modelCreated)
        {
            // Notify to view from model.
            model.GetMessageOfException().PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        // Returns a string that contains the appropriate message from model.
        public string VM_Msg_of_exception
        {
            get 
            { return model.messageOfException.Msg_of_exception; }
        }
        // Function that returns the TimeOut message.
        public string GetTimeOutMessage()
        {
            return MessageOfException.ReadTimeoutException;
        }
    }
}
