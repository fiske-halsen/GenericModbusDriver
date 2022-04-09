using System;
using System.Diagnostics;
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

                Debug.WriteLine("InstrumentSerial: " + item.InstrumentSerial);
                Debug.WriteLine("ParticleDataChannel 0,5 μm: " + item.ParticalChannel1Count);
                Debug.WriteLine("ParticleDataChannel 5 μm: " + item.ParticalChannel2Count);
                Debug.WriteLine("SampleTime Stamp: " + item.SampleTimeStamp);
                Debug.WriteLine("Location: " + item.Location);
                Debug.WriteLine("SampleTime: " + item.SampleTime);

                Debug.WriteLine("Bad flow: " + item.SampleStatus.IsBadFlow);
                Debug.WriteLine("Bad laser: " + item.SampleStatus.IsBadLaser);
                Debug.WriteLine("Malfuntion detected: " + item.SampleStatus.IsMalfunctionDetected);
                Debug.WriteLine("Particle overflow: " + item.SampleStatus.IsParticleOverFlow);
                Debug.WriteLine("Sample Error: " + item.SampleStatus.IsSamplerError);
                Debug.WriteLine("Threshold exceeded: " + item.SampleStatus.IsThresholdHighStatusExceeded);

                Debug.WriteLine("----------------------------------------- END -----------------------------------------");
            }
        }
    }
}
