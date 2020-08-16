﻿using System;
using AnorocMobileApp.Models.Itinerary;

namespace AnorocMobileApp.Models
{
    public class Location
    {
        public Location()
        {

        }

        public Location(Position position)
        {
            Latitude = position.Lat;
            Longitude = position.Lon;
        }
        public Location(Xamarin.Essentials.Location loc)
        {
            this.Created = DateTime.Now;
            Carrier_Data_Point = false;

            Latitude = loc.Latitude;
            Longitude = loc.Longitude;
           
        }

        public Location(double lat, double longC, DateTime dateTime, bool carrier)
        {

            Latitude = lat;
            Longitude = longC;
            Created = dateTime;
            Carrier_Data_Point = carrier;
        }
   
        public DateTime Created { get; set; }
        
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Carrier_Data_Point { get; set; }
        public Area Region { get; set; }

        public Location clone()
        {
            return new Location(Latitude, Longitude, Created, Carrier_Data_Point);
        }
    }
}
