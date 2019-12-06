using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    [Serializable]
    public class PredictionEvent
    {
        public PredictionEvent()
        {

        }
        public string ContactName { get; set; }
        public string WeekNumber { get; set; }
        public string Time { get; set; }
    }
}
