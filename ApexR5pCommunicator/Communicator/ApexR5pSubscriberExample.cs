using System;
using System.Diagnostics;
using System.Globalization;
using static ParticleCommunicator.Communicator.ApexR5pCommunicator;

namespace ParticleCommunicator.Communicator
{
    public class ApexR5pSubscriberExample
    {
        public void OnMyEventRaised(object sender, EventArgs e)
        {
            var args = ((ParticleDataRecordArgs) e).ParticleRecords;

            Debug.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss",
                                            CultureInfo.InvariantCulture));

            foreach (var item in args)
            {
                Debug.WriteLine("ParticleDataChannel 1: " + item.ParticalChannel1Count);
                Debug.WriteLine("ParticleDataChannel 2: " + item.ParticalChannel2Count);
            }
        }
    }
}
