﻿using AnorocMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace AnorocMobileApp.Services
{
    //This class interacts with the API server on behalf of the User class
    class SignUpService
    {
        HttpClient http;
        
        public SignUpService()
        {

        }
        // TODO:
        // Link to our API
        public async Task<bool> registerUserAsync(string password)
        {
            http = new HttpClient();
            http.BaseAddress = new Uri("localhost:8080");

            // Have to make seporate call to Facebook API to get the email address for the 

            string jsonData = "{" + $"'userEmail':'{User.Email}', 'username':'{User.FirstName}', 'surname':'{User.UserSurname}', 'password':'{password}'" + "}";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PostAsync("/foo/login", content);

            // this result string should be something like: "{"Token":"rgh2ghgdsfds"}"
            Response result = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());
            if (result.Token.Equals("error"))
                return false;
            else
                return true;
        }
    }
    class Response
    {
        public string Token { get; set; }
    }
}

