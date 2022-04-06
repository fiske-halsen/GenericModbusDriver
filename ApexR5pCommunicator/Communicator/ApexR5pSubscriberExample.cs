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

            foreach (var item in args)
            {
                Debug.WriteLine("----------------------------------------- ONE SAMPLE ----------------------------------");

                Debug.WriteLine("ParticleDataChannel 1: " + item.ParticalChannel1Count);
                Debug.WriteLine("ParticleDataChannel 2: " + item.ParticalChannel2Count);
                Debug.WriteLine("SampleTime Stamp: " + item.SampleTimeStamp);
                Debug.WriteLine("Location: " + item.Location);
                Debug.WriteLine("SampleTime: " + item.SampleTime);

                Debug.WriteLine("Bad flow: " + item.SampleStatus.IsBadFlow);
                Debug.WriteLine("Bad laser: " + item.SampleStatus.IsBadLaser);
                Debug.WriteLine("Malfuntion detected: " + item.SampleStatus.IsMalfunctionDetected);
                Debug.WriteLine("Particle overflow: " + item.SampleStatus.IsParticleOverFlow);
                Debug.WriteLine("Sample Error: " + item.SampleStatus.IsSamplerError);
                Debug.WriteLine("Threshold high exceeded: " + item.SampleStatus.IsThresholdHighStatusExceeded);
                Debug.WriteLine("Threshold low exceeded: " + item.SampleStatus.IsThresholdLowStatusExceeded);

                Debug.WriteLine("----------------------------------------- END -----------------------------------------");
            }
        }
    }
}
