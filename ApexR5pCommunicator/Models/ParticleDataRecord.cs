using System;

namespace ParticleCommunicator.Models
{
    public class ParticleDataRecord
    {
        public DateTime SampleTimeStamp { get; set; }
        public int Location { get; set; }
        public int SampleTime { get; set; }
        public SampleStatusWord SampleStatus { get; set; }
        public int ParticalChannel1Count { get; set; }
        public int ParticalChannel2Count { get; set; }
    }
}
