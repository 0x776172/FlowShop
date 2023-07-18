using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowShop.Model
{
    internal class ProsesModel:ICloneable
    {
        public string Name { get; set; }
        public List<double> Durations { get; set; }
        public List<double> Delay { get; set; } = new List<double>();
        public ProsesModel(string name, List<double> durations)
        {
            Name = name;
            Durations = durations;
            //Delay = new List<double>();
        }
        public object Clone()
        {
            return new ProsesModel(Name, Durations.GetRange(0, Durations.Count));
        }
        public override string ToString()
        {
            var idx = 0;
            var res = $"{Name}: \n";
            foreach (var j in Durations)
            {
                idx++;
                res += $"Mesin {idx}: {j} Menit \n";
            }
            return res;
        }
    }
}
