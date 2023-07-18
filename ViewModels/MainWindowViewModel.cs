using FlowShop.Model;
using FlowShop.Models;
using FlowShop.Utils;
using FlowShop.Views;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlowShop.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region variable
        private int _prod = 0;
        private int _mesin = 0;
        private NavigationView _view;
        private DataProduksiViewModel _dataProd;
        private bool _isProgress = false;
        private string _currStatus = "";
        #endregion

        public DataProduksiViewModel DataProduksi
        {
            get => _dataProd;
        }

        public BaseViewModel View { get => _view.CurrentView; }
        public bool IsProgress
        {
            get => _isProgress;
            set => SetProperty(ref _isProgress, value);
        }
        public string CurrentStatus
        {
            get => _currStatus;
            set => SetProperty(ref _currStatus, value);
        }
        public MainWindowViewModel()
        {
            //View.CurrentView = 
            _dataProd = new DataProduksiViewModel(_view);
            _view = new NavigationView();
            _view.ViewChanged += View_ViewChanged;
            _view.CurrentView = new DataProduksiViewModel(_view);
            CurrentStatus = "Data Produksi";
        }

        private void View_ViewChanged()
        {
            OnPropertyChanged(nameof(View));
        }
        public void Update((string status, bool isProgress) param)
        {
            CurrentStatus = param.status;
            IsProgress = param.isProgress;
        }
    }
}
