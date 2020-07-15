﻿using System.Collections.Generic;
using AnorocMobileApp.Models;
using Xamarin.Forms.Maps;
using Location = AnorocMobileApp.Models.Location;

namespace AnorocMobileApp.ViewModels
{
    class MapViewModel
    {
        MapModel mapModel = new MapModel();

        List<Place> placesList;
        public MapViewModel()
        {

            placesList = new List<Place>();
        }
        public List<Place> GetPinsForArea()
        {  
            var resultObject = mapModel.loadJsonFileToList();
            if (resultObject != null)
            {
                foreach (var place in resultObject.PointArray)
                {
                    placesList.Add(new Place
                    {
                        PlaceName = "Test Name",
                        Address = "Test Address",
                        Location = new Location(place.Latitude, place.Longitude),
                        Position = new Position(place.Latitude, place.Longitude),
                        //Icon = place.icon,
                        //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                        //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                    });
                }
                return placesList;
            }
            else
                return null;
        }
    }
}
//PlacesListView.ItemsSource = placesList;
//var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
/* MyMap.Pins.Add(new Pin
                {
                    Label = place.name,
                    Address = place.vicinity,
                    Type = PinType.Place,
                    Position = new Position(place.geometry.location.lat, place.geometry.location.lng)
                });*/
