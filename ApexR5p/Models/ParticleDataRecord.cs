namespace ParticleCommunicator.Models
{
    public class ParticleDataRecord
    {
        public DateTime SampleTime { get; set; }
        public int Location { get; set; }
        public int SampleStatus { get; set; }
        public int ParticalChannel1Count { get; set; }
        public int ParticalChannel2Count { get; set; }
    }
}
