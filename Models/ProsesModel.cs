using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowShop.Model
{
    internal class ProsesModel
    {
        public ProsesModel(string name, List<double> durations)
        {
            Name = name;
            Durations = durations;
            //Delay = new List<double>();
        }
        public string Name { get; set; }
        public List<double> Durations { get; set; }
        public List<double> Delay { get; set; } = new List<double>();
    }
}
