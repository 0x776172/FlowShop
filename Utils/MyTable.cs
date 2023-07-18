using FlowShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlowShop.Utils
{
    internal class MyTable : Grid
    {
        private int Row { get; set; }
        private int Column { get; set; }
        public MyTable(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public (UIElement element, string type) GetChild(int row, int column)
        {
            var childs = Children.Cast<UIElement>();
            var element = childs.Where(x => GetRow(x) == row && GetColumn(x) == column).FirstOrDefault();
            return (element, GetType().Name);
        }
        public async Task<List<ProsesModel>> GetTableValue()
        {
            var value = new List<ProsesModel>();
            await Task.Run(() =>
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    for (int row = 1; row < RowDefinitions.Count; row++)
                    {
                        var (element, type) = GetChild(row, 0);
                        var jobName = ((Label)((Border)element).Child).Content.ToString();
                        var proses = new ProsesModel(jobName, new List<double>());
                        for (int col = 1; col < ColumnDefinitions.Count; col++)
                        {
                            var child = GetChild(row, col);
                            var tbTemp = (TextBox)child.element;
                            proses.Durations.Add(double.Parse(tbTemp.Text == "" ? 0.ToString() : tbTemp.Text));
                        }
                        value.Add(proses);
                    }
                }));
            });
            return value;
        }
        public async Task<bool> CreateTable()
        {
            await Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    for (int i = 0; i <= Row; i++)
                    {
                        var r = new RowDefinition() { Height = GridLength.Auto };
                        RowDefinitions.Add(r);
                    }

                    for (int i = 0; i <= Column; i++)
                    {
                        ColumnDefinitions.Add(new ColumnDefinition() { });
                    }

                    CreateHeader();
                    CreateJobName();
                    CreateEntry();
                }));
            });
            return true;
        }
        private void CreateHeader()
        {
            var jobLabel = new Label() { Content = "Job", HorizontalAlignment = HorizontalAlignment.Center };
            //Grid.SetRow(jobLabel, 0);
            //Grid.SetColumn(jobLabel, 0);
            Children.Add(CreateBorder(jobLabel, 0, 0));

            for (int i = 1; i < ColumnDefinitions.Count; i++)
            {
                var label = new Label() { Content = $"Mesin {i}", HorizontalAlignment = HorizontalAlignment.Center };
                //Grid.SetRow(label, 0);
                //Grid.SetColumn(label, i);
                Children.Add(CreateBorder(label, 0, i));
            }
        }
        private void CreateJobName()
        {
            for (int i = 1; i < RowDefinitions.Count; i++)
            {
                var label = new Label() { Content = $"Job {i}", HorizontalAlignment = HorizontalAlignment.Center };
                //Grid.SetRow(label, i);
                //Grid.SetColumn(label, 0);
                Children.Add(CreateBorder(label, i, 0));
            }
        }
        private void CreateEntry()
        {
            for (int row = 1; row < RowDefinitions.Count; row++)
            {
                for (int col = 1; col < ColumnDefinitions.Count; col++)
                {
                    var tb = new TextBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Width = double.NaN
                    };
                    SetRow(tb, row);
                    SetColumn(tb, col);
                    Children.Add(tb);
                }
            }
        }
        private Border CreateBorder(UIElement child, int x, int y, double thickness = 0.5)
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(thickness);
            border.BorderBrush = Brushes.Black;
            border.Child = child;
            SetRow(border, x);
            SetColumn(border, y);
            return border;
        }
    }
}
