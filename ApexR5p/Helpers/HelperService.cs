using static ParticleCommunicator.Communicator.ParticleCommunicatorApexR5p;

namespace ParticleCommunicator.Helpers
{
    public static class HelperService
    {
        #region Class Variables
        private const int DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_HOURS = 9;
        private const int DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_Minutes = 6;
        private const int DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_Seconds = 7;
        #endregion

        #region Enums
  

        #endregion


        /// <summary>
        /// Converts the default time of 9 hours 6 minuntes and 7 seconds 
        /// to total seconds for holdTime and Sample low register
        /// </summary>
        /// <returns></returns>
        public static int GetMinValueSampleHoldRegister()
        {
            // Convert hours to seconds
            var totalSeconds = 
                (DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_HOURS * 60 * 60) 
                + (DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_Minutes * 60)
                + DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_Seconds;

            return totalSeconds;
        }

        public static bool IsAlarmEnabled(int alarmNum)
        {
            if (alarmNum == (int) AlarmStatus.EnableAlarm)
            {
                return true;
            }
            return false;
        }
    }
}
