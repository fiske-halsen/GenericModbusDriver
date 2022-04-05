using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCommunicator.Models
{
    public class SampleStatusWord
    {
        public bool IsBadLaser { get; set; }
        public bool IsBadFlow { get; set; }
        public bool IsParticleOverFlow { get; set; }
        public bool IsMalfunctionDetected { get; set; }
        public bool IsThresholdHighStatusExceeded { get; set; }
        public bool IsThresholdLowStatusExceeded { get; set; }
        public bool IsSamplerError { get; set; }
    }
}
