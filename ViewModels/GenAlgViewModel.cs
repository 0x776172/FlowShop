using FlowShop.Model;
using FlowShop.Models;
using FlowShop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlowShop.ViewModels
{
    internal class GenAlgViewModel : BaseViewModel
    {
        #region Field
        private List<ChromosomeModel> _populasi;
        private List<ProsesModel> _data;
        private List<string> _dataView;

        private double _mutation = 0;
        private double _crossover = 0;
        private List<double> _probability;
        private int _chromosome = 0;
        private int _generasi = 0;
        private int _currGen = 0;

        private List<double> _fitness;

        private BindingList<string> _text = new BindingList<string>();

        private NavigationView _view;
        #endregion

        #region Properties
        private NavigationView View { get => _view; }
        public double MutationRate
        {
            get => _mutation;
            set => SetProperty(ref _mutation, value);
        }
        public double CrossoverRate
        {
            get => _crossover;
            set => SetProperty(ref _crossover, value);
        }
        public List<double> Probability
        {
            get => _probability;
            set => SetProperty(ref _probability, value);
        }
        public int JumlahChromosome
        {
            get => _chromosome;
            set => SetProperty(ref _chromosome, value);
        }
        public int JumlahGenerasi
        {
            get => _generasi;
            set => SetProperty(ref _generasi, value);
        }
        public int GenerasiSekarang
        {
            get => _currGen;
            set => SetProperty(ref _currGen, value);
        }
        public List<ChromosomeModel> Populasi
        {
            get => _populasi;
            set => SetProperty(ref _populasi, value);
        }
        public List<ProsesModel> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }
        public List<string> DataView
        {
            get => _dataView;
            set => SetProperty(ref _dataView, value);
        }
        public BindingList<string> ProsesText
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public List<double> Fitness
        {
            get { return _fitness; }
            set { _fitness = value; }
        }

        #endregion

        public GenAlgViewModel(NavigationView view, List<ProsesModel> data)
        {
            _view = view;
            Data = CopyAll(data);
            GetDataJob();
            //DataView = dataView;
            _fitness = new List<double>();
            _probability = new List<double>();
            ProsesText.ListChanged += ProsesText_ListChanged;
        }

        private void ProsesText_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ProsesText));
        }

        public async Task<List<ChromosomeModel>> GenerateChromosome()
        {
            ProsesText.Clear();
            string tempText = "Initializing Populasi Awal\n";
            var temp = CopyAll(Data);
            var res = new List<ChromosomeModel>();
            for (var i = 0; i < JumlahChromosome; i++)
            {
                var temp1 = CopyAll(temp.OrderBy(idx => Guid.NewGuid()).ToList());
                var chr = new ChromosomeModel(temp1);
                await chr.CountMakespan();
                tempText += $"Chromosome - {i}: {chr}\n";
                res.Add(chr);
            }
            ProsesText.Add($"{tempText}");
            return res;
        }
        public async void doneBtnClicked()
        {
            Populasi = await GenerateChromosome();
            await DoGenetic();
        }
        public void resetBtnClicked()
        {
            View.CurrentView = new GenAlgViewModel(View, Data);
        }
        private void GetDataJob()
        {
            List<string> dataView = new List<string>();

            foreach (var i in Data)
            {
                dataView.Add(i.ToString());
            }
            DataView = dataView;
        }

        public async Task<bool> DoGenetic()
        {
            await Task.Run(async () =>
            {
                for (var gen = 1; gen <= JumlahGenerasi; gen++)
                {
                    GenerasiSekarang = gen;
                    string temp = $"Proses generasi ke - {gen}:\n";
                    temp += await DoFitness();
                    temp += Selection();
                    temp += Crossover();
                    temp += Mutation();
                    temp += await ShowBestChromosome();

                    Application.Current.Dispatcher.Invoke(new Action(() => { ProsesText.Add(temp); }));
                }
            });
            return true;
        }
        private string Selection()
        {
            RouletteOfWheel();

            return "Proses selection\n";
        }

        private async Task<string> DoFitness()
        {
            _fitness = new List<double>();
            foreach (var c in Populasi)
            {
                await c.CountMakespan();
                _fitness.Add(1 / c.Makespan);
            }
            return "Proses menghitung Fitness..\n";
        }

        private void RouletteOfWheel()
        {
            Probability = new List<double>();
            var sumFitness = Fitness.Sum();

            foreach (var f in Fitness)
            {
                Probability.Add(f / sumFitness);
            }

            var sumProb = 0.0;
            var SumProb = new List<double>();
            foreach (var i in Probability)
            {
                sumProb += i;
                SumProb.Add(sumProb);
            }

            var randomGenerated = GenerateRandomProb();


            for (var i = 0; i < randomGenerated.Count - 1; i++)
            {
                for (var j = 0; j < Probability.Count - 1; j++)
                {
                    if (randomGenerated[i] > SumProb[j])
                    {
                        continue;
                    }
                    else
                    {
                        Populasi[i] = Populasi[j].Clone() as ChromosomeModel;
                        break;
                    }
                }
            }
        }
        private string Crossover()
        {
            (List<int> index, List<ChromosomeModel> parent) = GetParent();

            for (var i = 0; i < parent.Count - 1; i++)
            {
                Populasi[index[i]] = i == parent.Count - 1 ?
                    CrossoverProc(parent[i], parent[0]) :
                    CrossoverProc(parent[i], parent[i + 1]);
            }

            return "Proses Crosover..\n";
        }
        private (List<int> index, List<ChromosomeModel> parent) GetParent()
        {

            double pc = CrossoverRate / 100;
            var index = new List<int>();
            var parent = new List<ChromosomeModel>();
            var cvProb = GenerateRandomProb();
            for (var i = 0; i < cvProb.Count - 1; i++)
            {
                if (cvProb[i] < pc)
                {
                    index.Add(i);
                    parent.Add(Populasi[i].Clone() as ChromosomeModel);
                }
            }
            return (index, parent);
        }
        private ChromosomeModel CrossoverProc(ChromosomeModel c1, ChromosomeModel c2)
        {
            Random r = new Random();
            var num1 = r.Next(c1.Jobs.Count);
            var num2 = r.Next(c1.Jobs.Count);

            var start = num1 < num2 ? num1 : num2;
            var end = num1 > num2 ? num1 : num2;
            for (var s = start; s <= end; s++)
            {
                var idx = c1.Jobs.FindIndex(e => e.Name == c2.Jobs[s].Name);
                if (idx >= start && idx <= end)
                {
                    c1.Jobs[s] = c2.Jobs[s].Clone() as ProsesModel;
                }
            }
            CorrectCrossover(c1, c2, start, end);
            return c1;
        }
        private ChromosomeModel CorrectCrossover(ChromosomeModel c, ChromosomeModel c2, int start, int stop)
        {

            //for (var i = start; i <= stop; i++)
            //{
            //    var idx = c.Jobs.FindIndex(e => e.Name == c2.Jobs[i].Name);
            //}
            var res = new List<int>();
            foreach (var i in c2.Jobs)
            {
                var cRes = Enumerable.Range(0, c.Jobs.Count).Where(item => c.Jobs[item].Name == i.Name).ToList();
                if (cRes.Count > 1)
                {
                    cRes.ForEach(e =>
                    {
                        if (e >= start && e <= stop) res.Add(e);
                    });
                }
            }
            for (var i = 0; i < res.Count - 1; i++)
            {
                foreach(var p in c2.Jobs)
                {
                    var idx = c.Jobs.FindIndex(e => e.Name == p.Name);
                    if (idx < 0)
                    {
                        c.Jobs[res[i]] = p.Clone() as ProsesModel;
                    }
                }
            }
            return c;
        }
        public string Mutation()
        {
            DoMutation();

            return "Proses Mutasi..\n";
        }
        private void DoMutation()
        {
            //var result = new List<int>();
            double pm = MutationRate / 100;
            int count = int.Parse(Math.Round(JumlahChromosome * pm).ToString());
            Random r = new Random();
            for (var i = 0; i < count; i++)
            {
                GenMutation(r.Next(JumlahChromosome));
            }
        }

        private void GenMutation(int index)
        {
            Random r = new Random();
            int g1 = r.Next(Populasi[index].Jobs.Count);
            int g2 = r.Next(Populasi[index].Jobs.Count);
            var temp1 = Populasi[index].Jobs[g2].Clone() as ProsesModel;
            var temp2 = Populasi[index].Jobs[g1].Clone() as ProsesModel;
            Populasi[index].Jobs[g1] = temp1;
            Populasi[index].Jobs[g2] = temp2;
        }

        public async Task<string> ShowBestChromosome()
        {
            double min = 999999;
            int index = -1;
            int minIndex = 0;
            foreach (var p in Populasi)
            {
                index++;
                await p.CountMakespan();
                if (p.Makespan < min)
                {
                    min = p.Makespan;
                    minIndex = index;
                }
            }
            index = minIndex;
            string result = Populasi[index].ToString() + $"with makespan: {Populasi[index].Makespan}";
            return result;
        }
        private List<double> GenerateRandomProb()
        {
            Random random = new Random();
            var result = new List<double>();
            for (var i = 0; i < Populasi.Count - 1; i++)
            {
                result.Add(random.NextDouble());
            }
            return result;
        }
        private List<T> CopyAll<T>(List<T> data) where T : ICloneable
        {
            var res = new List<T>();
            foreach (var i in data)
            {
                var temp = (T)i.Clone();
                res.Add(temp);
            }
            return res;
        }
    }
}
