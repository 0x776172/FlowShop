using FlowShop.Model;
using FlowShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowShop.ViewModels
{
    internal class GenAlgViewModel : BaseViewModel
    {
        public double MutationRate { get; set; }
        public double CrossoverRate { get; set; }
        public int JumlahChromosome { get; set; }
        public int JumlahGenerasi { get; set; }
        public int GenerasiSekarang { get; set; }
        public int Probability { get; set; }
        public List<ChromosomeModel> Populasi { get; set; }
        public List<ProsesModel> Data { get; set; }
        public GenAlgViewModel(List<ProsesModel> data, int jumlahChromosome, int jumlahGenerasi, int mutationRate, int crossoverRate, int probability)
        {
            MutationRate = mutationRate / 100;
            CrossoverRate = crossoverRate / 100;
            JumlahChromosome = jumlahChromosome;
            JumlahGenerasi = jumlahGenerasi;
            Probability = probability;
            Data = data.GetRange(0, data.Count);
        }

        public async Task<List<ChromosomeModel>> GenerateChromosome()
        {
            var ch = new List<ChromosomeModel>();
            for (int i = 0; i < JumlahChromosome; i++)
            {
                var rand = new Random();
                var randCh = Data.OrderBy(_ => rand.Next()).ToList();
                var result = new ChromosomeModel(randCh);
                await result.CountDelay();
                ch.Add(result);
            }
            ch[0].Jobs.ForEach(job => { Debug.WriteLine(job.Name); });
            return ch;
        }
        public void doneBtnClicked()
        {
            Debug.WriteLine("clicked");
        }
    }
}
