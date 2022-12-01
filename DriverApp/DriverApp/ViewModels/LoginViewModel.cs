using DriverApp.Models.DTOs.Responses;
using DriverApp.Services;
using DriverApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DriverApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command LoginCommand { get; }

        private string phoneNumber;
        private string password;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs(Password));
            }
        }

        private DriverResponse driverResponse;
        public DriverResponse DriverResponse
        {
            get => driverResponse;
            set
            {
                driverResponse = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriverResponse"));
            }
        }

        private bool runActivityIndicator = false;
        public bool RunActivityIndicator
        {
            get => runActivityIndicator;
            set
            {
                if (runActivityIndicator != value)
                {
                    runActivityIndicator = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        public LoginViewModel()
        {
            RunActivityIndicator = false;
            phoneNumber = "996556892343";
            password = "test123";
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {
            RunActivityIndicator = true;
            if (string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password))
            {
                RunActivityIndicator = false;
                await Shell.Current.DisplayAlert("Пустые значения", "Пожалуйста введите ИНН и пароль для входа в систему!", "Ок");
            }
            else
            {
                RunActivityIndicator = true;

                DriverResponse = await DriverSignIn.DriverSignInInstance.AuthenticateDriverAsync(PhoneNumber, Password);

                if (DriverResponse != null)
                {
                    RunActivityIndicator = false;

                    //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}?{nameof(HomeViewModel.DriverResponse)}={DriverResponse}");
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                else
                {
                    RunActivityIndicator = false;
                    await Shell.Current.DisplayAlert("Не удалось войти в систему", "Пожалуйста введите правильные данные", "Ок");

                }
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
