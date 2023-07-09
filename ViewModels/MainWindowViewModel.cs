using FlowShop.Model;
using FlowShop.Models;
using FlowShop.Utils;
using FlowShop.Views;
using System.Collections.Generic;
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
        #endregion

        public DataProduksiViewModel DataProduksi
        {
            get => _dataProd;
        }

        public BaseViewModel View { get => _view.CurrentView; }

        public MainWindowViewModel()
        {
            //View.CurrentView = 
            _dataProd = new DataProduksiViewModel(_view);
            _view = new NavigationView();
            _view.ViewChanged += View_ViewChanged;
            _view.CurrentView = new DataProduksiViewModel(_view);
        }

        private void View_ViewChanged()
        {
            OnPropertyChanged(nameof(View));
        }

        
    }
}
