using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models.PropertiesOfDataServer
{
	/// <summary>
	/// This class contains all the properties of the Dashboard and the GameMap.
	/// </summary>
	public class VariablesFromSimulator : INotifyPropertyChanged
    {
		// Const values for coordinates of Natbag.
		const double Natbag_Latitude = 32.002644;
		const double Natbag_Longitude = 34.888781;
		public VariablesFromSimulator()
		{
			this.location = new Location(latitude, longitude);
		}

		// Reset the properties in this class.
		public void ResetAllVariables()
		{
			Indicated_heading_deg = 0;
			Gps_indicated_vertical_speed = 0;
			Gps_indicated_altitude_ft = 0;
			Gps_indicated_ground_speed_kt = 0;
			Altimeter_indicated_altitude_ft = 0;
			Attitude_indicator_internal_pitch_deg = 0;
			Attitude_indicator_internal_roll_deg = 0;
			Airspeed_indicator_indicated_speed_kt = 0;
			Longitude = Natbag_Longitude;
			Latitude = Natbag_Latitude;
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyPropertyChanged(string propName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}


		private double indicated_heading_deg;
		public double Indicated_heading_deg
		{
			get { return indicated_heading_deg; }
			set
			{
				// Checking if we need to update Indicated_heading_deg.
				if (gps_indicated_vertical_speed != value)
				{
					indicated_heading_deg = value;
					NotifyPropertyChanged("Indicated_heading_deg");
				}

			}
		}

		private double gps_indicated_vertical_speed;

		public double Gps_indicated_vertical_speed
		{
			get { return gps_indicated_vertical_speed; }
			set
			{
				// Checking if we need to update Gps_indicated_vertical_speed.
				if (gps_indicated_vertical_speed != value)
				{
					gps_indicated_vertical_speed = value;
					NotifyPropertyChanged("Gps_indicated_vertical_speed");
				}

			}
		}


		private double gps_indicated_ground_speed_kt;

		public double Gps_indicated_ground_speed_kt
		{
			get { return gps_indicated_ground_speed_kt; }
			set
			{
				// Checking if we need to update Gps_indicated_ground_speed_kt.
				if (gps_indicated_ground_speed_kt != value)
				{
					gps_indicated_ground_speed_kt = value;
					NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
				}

			}
		}


		private double airspeed_indicator_indicated_speed_kt;

		public double Airspeed_indicator_indicated_speed_kt
		{
			get { return airspeed_indicator_indicated_speed_kt; }
			set
			{
				// Checking if we need to update Airspeed_indicator_indicated_speed_kt.
				if (airspeed_indicator_indicated_speed_kt != value)
				{
					airspeed_indicator_indicated_speed_kt = value;
					NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
				}

			}
		}


		private double gps_indicated_altitude_ft;

		public double Gps_indicated_altitude_ft
		{
			get { return gps_indicated_altitude_ft; }
			set
			{
				// Checking if we need to update Gps_indicated_altitude_ft.
				if (gps_indicated_altitude_ft != value)
				{
					gps_indicated_altitude_ft = value;
					NotifyPropertyChanged("Gps_indicated_altitude_ft");
				}

			}
		}


		private double attitude_indicator_internal_roll_deg;

		public double Attitude_indicator_internal_roll_deg
		{
			get { return attitude_indicator_internal_roll_deg; }
			set
			{
				// Checking if we need to update Attitude_indicator_internal_roll_deg.
				if (attitude_indicator_internal_roll_deg != value)
				{
					attitude_indicator_internal_roll_deg = value;
					NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
				}

			}
		}


		private double attitude_indicator_internal_pitch_deg;

		public double Attitude_indicator_internal_pitch_deg
		{
			get { return attitude_indicator_internal_pitch_deg; }
			set
			{
				// Checking if we need to update Attitude_indicator_internal_pitch_deg.
				if (attitude_indicator_internal_pitch_deg != value)
				{
					attitude_indicator_internal_pitch_deg = value;
					NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
				}
			}
		}


		private double altimeter_indicated_altitude_ft;
		public double Altimeter_indicated_altitude_ft
		{
			get { return altimeter_indicated_altitude_ft; }
			set
			{
				// Checking if we need to update Altimeter_indicated_altitude_ft.
				if (altimeter_indicated_altitude_ft != value)
				{
					altimeter_indicated_altitude_ft = value;
					NotifyPropertyChanged("Altimeter_indicated_altitude_ft");
				}

			}
		}

		private double longitude = Natbag_Longitude;
		public double Longitude
		{
			get { return this.longitude; }
			set
			{
				// Checking if we need to update Longitude.
				if (this.longitude != value)
				{
					if (value > 180)
					{
						value = 180;
					}
					else if (value < -180)
					{
						value = -180;
					}
					this.longitude = value;
					location.Longitude = value;
					this.NotifyPropertyChanged("LongitudeT");
					this.NotifyPropertyChanged("Location");

				}
			}
		}
		private double latitude = Natbag_Latitude;
		public double Latitude
		{
			get { return this.latitude; }
			set
			{
				// Checking if we need to update Latitude.
				if (this.latitude != value)
				{
					if (value > 90)
					{
						value = 90;
					}
					else if (value < -90)
					{
						value = -90;
					}
					this.latitude = value;
					location.Latitude = value;
					this.NotifyPropertyChanged("LatitudeT");
					this.NotifyPropertyChanged("Location");
				}
			}
		}
		private readonly Location location;
		public Location Location
		{
			get { return new Location(latitude, longitude); }
		}

	}
}
