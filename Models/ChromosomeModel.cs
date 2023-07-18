using FlowShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FlowShop.Models
{
    internal class ChromosomeModel : ICloneable
    {
        public ChromosomeModel(List<ProsesModel> jobs)
        {
            Jobs = new List<ProsesModel>();
            foreach (var job in jobs)
            {

                Jobs.Add(job.Clone() as ProsesModel);
            }
        }
        public List<ProsesModel> Jobs { get; set; }
        public double Makespan { get; set; }
        private async Task<bool> CountDelay()
        {
            await Task.Run(() =>
            {
                for (int i = 1; i < Jobs[0].Durations.Count; i++)
                {
                    var tm = 0.0;
                    var tr = 0.0;
                    var dt = 0.0;
                    var delay = 0.0;
                    for (int j = 0; j < Jobs.Count; j++)
                    {
                        //tm += Jobs[j].Durations[i - 1];
                        tm += Jobs[j].Durations[i - 1] + (i > 1 ? Jobs[j].Delay[i - 2] : 0);
                        if (j == 0)
                        {
                            tr += tr;
                            dt += delay;
                            delay = tm;
                        }
                        else
                        {
                            tr += Jobs[j - 1].Durations[i];
                            dt += delay;
                            delay = Math.Max(0, tm - tr - dt);
                        }
                        Jobs[j].Delay.Add(delay);
                    }
                }
            });
            return true;
        }
        public async Task CountMakespan()
        {
            await CountDelay();
            await Task.Run(() =>
            {
                var makespan = 0.0;
                var sumOfJob = 0.0;
                var sumOfDelay = 0.0;
                foreach (var j in Jobs)
                {
                    sumOfJob += j.Durations[j.Durations.Count - 1];
                    sumOfDelay += j.Delay[j.Delay.Count - 1];
                    j.Delay.Clear();
                }
                makespan = sumOfJob + sumOfDelay;
                Makespan = makespan;

                Debug.WriteLine($"{sumOfJob} + {sumOfDelay} = {makespan}");
            });
        }
        public override string ToString()
        {
            var res = string.Empty;
            foreach(var i in Jobs)
            {
                res += i.Name + ", ";
            }
            return res;
        }

        public object Clone()
        {
            var copiedJobs = new List<ProsesModel>();
            foreach(var j in Jobs)
            {
                copiedJobs.Add((ProsesModel)j.Clone());
            }
            return new ChromosomeModel(copiedJobs);
        }
    }
}
