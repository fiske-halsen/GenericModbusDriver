using ParticleCommunicator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ParticleCommunicator.Communicator.ParticleCommunicatorApexR5p;

namespace ParticleCommunicator.Communicator
{
    public class Subscriber
    {
        public void OnMyEventRaised(object sender, EventArgs e)
        {
            var args = ((ParticleDataRecordArgs) e).ParticleRecords;

            foreach (var item in args)
            {
                Debug.WriteLine("ParticleDataChannel 1: " + item.ParticalChannel1Count);
                Debug.WriteLine("ParticleDataChannel 2: " + item.ParticalChannel2Count);
            }
        }
    }
}
