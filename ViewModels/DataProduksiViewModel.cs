using FlowShop.Model;
using FlowShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using FlowShop.Models;
using FlowShop.Views;

namespace FlowShop.ViewModels
{
    internal class DataProduksiViewModel : BaseViewModel
    {
        #region Variable
        private string _jmlProd = "0";
        private string _jmlMesin = "0";
        private int _jumlahProd = 0;
        private int _jumlahMesin = 0;

        private MyTable _dataGrid;
        private Visibility _showDataGrid = Visibility.Collapsed;

        private NavigationView _view;
        #endregion

        #region Property
        public string JumlahProduksi
        {
            get => _jmlProd;
            set => SetProperty(ref _jmlProd, value);
        }
        public string JumlahMesin
        {
            get => _jmlMesin;
            set => SetProperty(ref _jmlMesin, value);
        }

        public MyTable Content
        {
            get { return _dataGrid; }
            set => SetProperty(ref _dataGrid, value);
        }

        public Visibility ShowDataGrid
        {
            get => _showDataGrid;
            set => SetProperty(ref _showDataGrid, value);
        }
        public List<ProsesModel> Values { get; set; }
        #endregion

        #region Method
        public async void Clicked()
        {
            Debug.WriteLine("test");
            try
            {
                Values = new List<ProsesModel>();
                _jumlahProd = int.Parse(JumlahProduksi);
                _jumlahMesin = int.Parse(JumlahMesin);
                _dataGrid = new MyTable(_jumlahProd, _jumlahMesin);
                Content = new MyTable(_jumlahProd, _jumlahMesin);
                var done = await Content.CreateTable();
                ShowDataGrid = Visibility.Visible;
            }

            catch
            {
                MessageBox.Show("Input harus angka!", "Error!");
            }
        }
        public async void GetJobData()
        {
            Values = CreateDummy();
            _view.CurrentView = new GenAlgViewModel(Values, 10, 10, 0, 0, 0);
        }
        private List<ProsesModel> CreateDummy()
        {
            return new List<ProsesModel>()
            {
                new ProsesModel("Job 1",new List<double>(){52.36,24.56,73.09,83.07,0.0,3.14}),
                new ProsesModel("Job 2",new List<double>(){113.66,51.86,158.07,178.38,0.0,6.86}),
                new ProsesModel("Job 3",new List<double>(){ 0.0, 35.17, 0.0, 175.81, 38.41,3.83}),
                new ProsesModel("Job 4",new List<double>(){55.55,27.49,77.15,144.58,0.0,3.2}),
                new ProsesModel("Job 5",new List<double>(){43.72,22.9,61.86,98.81,0.0,2.76}),
                new ProsesModel("Job 6",new List<double>(){ 59.55, 31.94, 83.56, 133.37, 0.0, 3.69}),
                new ProsesModel("Job 7",new List<double>(){44.33,22.21,61.85,115.54,54.13,2.55}),
                new ProsesModel("Job 8",new List<double>(){0.0,41.88,0.0,187.65,62.65,4.48}),
                new ProsesModel("Job 9",new List<double>(){146.08,76.66,215.29,396.5,288.76,8.73}),
                new ProsesModel("Job 10",new List<double>(){96.52,42.82,139.15,155.73,0.0,5.87}),
                new ProsesModel("Job 11",new List<double>(){75.0,41.63,108.27,169.69,0.0,4.53}),
                new ProsesModel("Job 12",new List<double>(){ 86.91, 45.56, 121.58, 139.84, 0.0, 4.96})
            };
        }
        #endregion

        public DataProduksiViewModel(NavigationView view)
        {
            _view = view;
        }
    }
}
