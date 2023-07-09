using FlowShop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowShop.Models
{
    internal class GenAlg
    {
        public double MutationRate { get; set; }
        public double CrossoverRate { get; set; }
        public int JumlahChromosome { get; set; }
        public int JumlahGenerasi { get; set; }
        public int GenerasiSekarang { get; set; }
        public List<ChromosomeModel> Populasi { get; set; }
        public List<ProsesModel> Data { get; set; }
        public GenAlg(List<ProsesModel> data, int jumlahChromosome, int jumlahGenerasi, double mutationRate, double crossoverRate)
        {
            MutationRate = mutationRate;
            CrossoverRate = crossoverRate;
            JumlahChromosome = jumlahChromosome;
            JumlahGenerasi = jumlahGenerasi;
            Data = data.GetRange(0, data.Count);
        }

        public List<ChromosomeModel> GenerateChromosome()
        {
            var ch = new List<ChromosomeModel>();
            for (int i = 0; i < JumlahChromosome; i++)
            {
                var rand = new Random();
                var randCh = Data.OrderBy(_ => rand.Next()).ToList();
                ch.Add(new ChromosomeModel(randCh));
            }
            ch[0].Jobs.ForEach(job => { Debug.WriteLine(job.Name); });
            return ch;
        }
    }
}
