using DriverApp.Models.DTOs.Requests;
using DriverApp.Models.DTOs.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DriverApp.Services
{
    internal class DriverSignIn
    {
        private static DriverSignIn _instance;
        private static string BASE_URL = "http://localhost:5278";
        private DriverResponse driverResponse;

        public static DriverSignIn DriverSignInInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new DriverSignIn();
                return _instance;
            }

        }

        private JsonSerializer _jsonSerializer = new JsonSerializer();
        private HttpClient httpClient;

        public DriverSignIn()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5278");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<DriverResponse> AuthenticateDriverAsync(string userName, string password)
        {
            try
            {
                DriverRequest driverRequest = new DriverRequest()
                {
                    UserName = userName,
                    Password = password
                };

                var content = new StringContent(JsonConvert.SerializeObject(driverRequest), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Driver/SignIn", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    driverResponse = JsonConvert.DeserializeObject<DriverResponse>(jsonResult);
                    Application.Current.Properties["DriverId"] = driverResponse.DriverId;
                    Application.Current.Properties["UserName"] = driverResponse.UserName;
                    Application.Current.Properties["BusNumber"] = driverResponse.BusNumber;
                    Application.Current.Properties["InitializedDriver"] = driverResponse;

                    return driverResponse;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
