using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Net.Http;
using DriverApp.Models.DTOs.Requests;
using Newtonsoft.Json;


namespace DriverApp.Services
{
    public class GetLocationService
    {
        readonly bool stopping = false;

        private HttpClient httpClient;

        private DriverLocationRequest driverLocation;

        int driverId;

        public GetLocationService() 
        {
            httpClient = new HttpClient();
            driverId = (int)Application.Current.Properties["DriverId"];
        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(async () => {
                while (!stopping)
                {
                    token.ThrowIfCancellationRequested();
                    try
                    {
                        await Task.Delay(5000);

                        var request = new GeolocationRequest(GeolocationAccuracy.High);
                        var location = await Geolocation.GetLocationAsync(request);
                        if (location != null)
                        {
                            var message = new LocationMessage
                            {
                                Latitude = location.Latitude,
                                Longitude = location.Longitude
                            };

                            await UpdateDriverLocation(driverId, message.Latitude, message.Longitude);

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                MessagingCenter.Send(message, "Location");
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var errormessage = new LocationErrorMessage();
                            MessagingCenter.Send(errormessage, "LocationError");
                        });
                    }
                }
                return;
            }, token);
        }

        private async Task UpdateDriverLocation(int driverId, double latitude, double longitude)
        {
            httpClient.BaseAddress = new Uri("http://192.168.1.51:45455");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                driverLocation = new DriverLocationRequest()
                {
                    DriverId = driverId,
                    Latitude = latitude,
                    Longitude = longitude
                };

                var content = new StringContent(JsonConvert.SerializeObject(driverLocation),
                    Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Driver/UpdateDriverLocation", content);
            }
            catch (Exception ex) { }
        }
    }
}
