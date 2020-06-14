﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace AnorocMobileApp.Views
{ 
    public partial class SettingsPage : ContentPage
    {

        public SettingsPage()
        {
            InitializeComponent();
        }

        public class Location
        {
            public Location()
            {
                
            }
           
            public string Latitude;
            public string Longitude;
            public string Altitude;
        }

        
        public async Task<Location> getLocationAsync()
        {
            Location loc = new Location();
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Lowest);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    loc.Latitude = location.Latitude.ToString();
                    loc.Longitude = location.Longitude.ToString();
                    loc.Altitude = location.Altitude.ToString();
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                return loc;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                return null;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                return null;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                return null;
            }
            catch (Exception ex)
            {
                // Unable to get location
                return null;
            }

        }

        async void OnToggledAsync(object sender, ToggledEventArgs e)
        {
            if(e.Value == true)
            {
                try
                {

                    //POST
                    postRequestAsync();

                }
                catch (Exception ex)
                {
                    

                    if (ex.InnerException != null)
                    {
                        await DisplayAlert("Attention", ":( " + ex.InnerException.Message, "OK");

                    }
                }
            }
            else
            {
                await DisplayAlert("Attention", "Disabled", "OK");
            }
        }

        public async void postRequestAsync()
        {

            var location = await getLocationAsync();

            var url = "https://10.0.2.2:5001/location/GEOLocation";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            var data  = JsonConvert.SerializeObject(location);
            var c = new StringContent(data, Encoding.UTF8, "application/json");
            c.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(url, c);
            string result = response.Content.ReadAsStringAsync().Result;

            await DisplayAlert("Attention", "Enabled: " + result, "OK");
        }
    }
}
