using DriverApp.Models.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DriverApp.ViewModels
{
    internal class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DriverResponse signedInDriver;
    }
}
