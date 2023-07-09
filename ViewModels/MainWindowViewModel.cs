using FlowShop.Model;
using FlowShop.Models;
using FlowShop.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlowShop.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Variable
        private string _jmlProd = "0";
        private string _jmlMesin = "0";

        private int _prod = 0;
        private int _mesin = 0;

        private MyTable _dataGrid;
        private Visibility _showDataGrid = Visibility.Collapsed;
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

        public async void Clicked()
        {
            try
            {
                Values = new List<ProsesModel>();
                _prod = int.Parse(JumlahProduksi);
                _mesin = int.Parse(JumlahMesin);
                //Content = await CreateTable(_prod, _mesin);
                Content = new MyTable(_prod, _mesin);
                await Content.CreateTable();
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
            var t = new GenAlg(Values, 10, 10, 0,0);
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
    }
}
