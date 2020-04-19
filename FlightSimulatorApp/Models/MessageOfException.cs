using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    /// <summary>
    /// Class that responsible of sending messages to his view model which is ExceptionVM.
    /// </summary>
    public class MessageOfException : INotifyPropertyChanged
    {
        public const string WriteObjectDisposedException = "The server has been closed. Please check your connection";
        public const string WriteInvalidOperationException = "The server is not connected to a remote host. Please check your connection";
        public const string WriteIOException = "An error occurred when accessing the socket. Please check your connection";
        public const string ReadObjectDisposedException = "The NetworkStream is closed. Please check your connection";
        public const string ReadInvalidOperationException = "The NetworkStream does not support reading. Please check your connection";
        public const string ReadTimeoutException = "The operation has timed-out. Please wait few seconds";
        public const string ReadIOException = "An error occurred when accessing the socket. Please check your connection";
        public const string RegularException = "Something got wrong. Please check your connection";

        // Enum type of the exceptions that we can get.
        public enum ExceptionTypes
        {
            WriteObjectDisposedException,
            WriteInvalidOperationException,
            WriteIOException,
            ReadObjectDisposedException,
            ReadInvalidOperationException,
            ReadTimeoutException,
            ReadIOException,
            RegularException,
            Nothing
        }

        public MessageOfException()
        {
            // Empty message.
            Msg_of_exception = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string msg_Of_Exception;
        // Property of message of exception.
        public string Msg_of_exception
        {
            get { return msg_Of_Exception; }
            set
            {
                if(msg_Of_Exception != value)
                {
                    msg_Of_Exception = value;
                    NotifyPropertyChanged("Msg_of_exception");
                }                
            }
        }

        private ExceptionTypes string_exception;
        // Property of ExceptionTypes type which responsible to set Msg_of_exception with the appropriate message.
        public ExceptionTypes String_exception
        {
            get { return string_exception; }
            set 
            {
                if(string_exception != value)
                {
                    string_exception = value;
                    // Set the Msg_of_exception according to string_exception.
                    switch (string_exception)
                    {
                        case ExceptionTypes.WriteObjectDisposedException:
                            Msg_of_exception = WriteObjectDisposedException;
                            break;
                        case ExceptionTypes.WriteInvalidOperationException:
                            Msg_of_exception = WriteInvalidOperationException;
                            break;
                        case ExceptionTypes.WriteIOException:
                            Msg_of_exception = WriteIOException;
                            break;
                        case ExceptionTypes.ReadObjectDisposedException:
                            Msg_of_exception = ReadObjectDisposedException;
                            break;
                        case ExceptionTypes.ReadInvalidOperationException:
                            Msg_of_exception = ReadInvalidOperationException;
                            break;
                        case ExceptionTypes.ReadTimeoutException:
                            Msg_of_exception = ReadTimeoutException;
                            break;
                        case ExceptionTypes.ReadIOException:
                            Msg_of_exception = ReadIOException;
                            break;
                        case ExceptionTypes.RegularException:
                            Msg_of_exception = RegularException;
                            break;
                        case ExceptionTypes.Nothing:
                            Msg_of_exception = "";
                            break;
                        default:
                            Msg_of_exception = "";
                            break;
                    }
                }
            }
        }


    }
}
