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

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {
            if (string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password))
                await Shell.Current.DisplayAlert("Пустые значения", "Пожалуйста введите ИНН и пароль для входа в систему!", "Ок");
            else
            {
                DriverResponse = await DriverSignIn.DriverSignInInstance.AuthenticateDriverAsync(PhoneNumber, Password);

                if (DriverResponse != null)
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}?{nameof(HomeViewModel.DriverResponse)}={DriverResponse}");
            }

        }
    }
}
