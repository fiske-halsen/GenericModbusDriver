using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusClientTester
{
    public class DataRecord
    {
        public string SampleTime { get; set; }
        public int Location { get; set; }
        public int SampleStatus { get; set; }
        public int ParticalChannel1Count { get; set; }
        public int ParticalChannel2Count { get; set; }

    }
}
