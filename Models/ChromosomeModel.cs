using FlowShop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlowShop.Models
{
    internal class ChromosomeModel
    {
        public ChromosomeModel(List<ProsesModel> jobs)
        {
            Jobs = jobs.GetRange(0, jobs.Count);
        }
        public List<ProsesModel> Jobs { get; set; }

        public async Task<bool> CountDelay()
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
    }
}
