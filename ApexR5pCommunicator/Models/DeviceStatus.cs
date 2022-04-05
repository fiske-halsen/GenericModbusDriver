namespace ParticleCommunicator.Models
{
    public class DeviceStatus
    {
        public bool IsRunning { get; set; }
        public bool IsSampling { get; set; }
        public bool IsNewData { get; set; }
        public bool IsDeviceError { get; set; }
        public bool IsInDataValidation { get; set; }
        public bool IsInLocationValidation { get; set; }
        public bool IsLaserOutOfSpec { get; set; }
        public bool IsFlowOutOfSpec { get; set; }
        public bool IsInstrumentServiceNeeded { get; set; }
        public bool IsHighAlarmThresHoldExceeded { get; set; }
        public bool IsLowAlarmThresHoldExceeded { get; set; }
        public bool IsLaserPowerOutOfSpec { get; set; }
        public bool IsLaserCurrentOutOfSpec { get; set; }
        public bool IsLaserSupplyOutOfSpec { get; set; }
        public bool IsLaserLifeStatusOutOfSpec { get; set; }
        public bool IsUnitsFlowBelowThreshold { get; set; }
        public bool IsPhotoAmpOutOfSpec { get; set; }
        public bool IsPhotoAmpBackGroundOutOfSpec { get; set; }
        public bool IsPhotoDiodeFailed { get; set; }
        public bool IsDevicePastCalibrationDue { get; set; }
        public bool IsUnitInLocationBracketMode { get; set; }
    }
}
