using FlightSimulator.Models;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public WheelVM WheelViewModel { get; internal set; }
        public DashboardVM DashboardViewModel { get; internal set; }
        public GameMapVM GameMapViewModel { get; internal set; }
        public ExceptionVM ExceptionViewModel { get; internal set; }

        public AbstractFlightSimulatorModel SimulatorModel = new MyFlightSimulatorModel();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            WheelViewModel = new WheelVM(SimulatorModel);
            DashboardViewModel = new DashboardVM(SimulatorModel);
            GameMapViewModel = new GameMapVM(SimulatorModel);
            ExceptionViewModel = new ExceptionVM(SimulatorModel);
        }
    }
}
