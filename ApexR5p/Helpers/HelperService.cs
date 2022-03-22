using EasyModbus.Exceptions;
using ParticleCommunicator.Models;
using System.Collections;
using static ParticleCommunicator.Communicator.ParticleCommunicatorApexR5p;

namespace ParticleCommunicator.Helpers
{
    public static class HelperService
    {
        #region Class Variables
        private const int DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_HOURS = 9;
        private const int DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_Minutes = 6;
        private const int DEFAULT_SAMPLE_HOLD_LOW_REGISTER_VALUE_Seconds = 7;
        private const int DEFAULT_MAX_HOLD_DELAY_TIME = 359999;
        private const int DEFAULT_MIN_HOLD_DELAY_TIME = 0;
        private const int DEFAULT_MAX_SAMPLE_TIME = 86399;
        private const int DEFAULT_MIN_SAMPLE_TIME = 0;
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
        /// <summary>
        /// onverts the alarm status number to a bit array and scans each true/false value
        /// </summary>
        /// <param name="alarmStatusNumber">The alarm number</param>
        /// <returns></returns>
        public static AlarmStatus GetDeviceAlarmStatus(int alarmStatusNumber)
        {
            var bitArray = ConvertLowRegisterIntToBits(alarmStatusNumber);

            AlarmStatus alarmStatus = new AlarmStatus()
            {
                IsChannelEnabled = bitArray[0],
                IsAlarmEnabled = bitArray[1]
            };

            return alarmStatus;
        }

        /// <summary>
        /// Converts the device status number to a bit array and scans each true/false value
        /// </summary>
        /// <param name="deviceStatusNumber">The device status number</param>
        /// <returns></returns>
        public static DeviceStatus GetDeviceStatusFromInt(int deviceStatusNumber)
        {
            var bitArray = ConvertLowRegisterIntToBits(deviceStatusNumber);

            DeviceStatus deviceStatus = new DeviceStatus()
            {
                IsRunning = bitArray[0],
                IsSampling = bitArray[1],
                IsNewData = bitArray[2]
            };

            return deviceStatus;
        }

        /// <summary>
        /// Helper method to convert single low register 2-byte int to bit array
        /// </summary>
        /// /// <param name="number">The number that needs to get converted</param>
        /// <returns>Bit array containing 8 bits</returns>
        public static BitArray ConvertLowRegisterIntToBits(int number)
        {
            return new BitArray(new int[] { number });
        }

        /// <summary>
        /// Removes all \0 from string converted from bytes
        /// </summary>
        /// <param name="text">The text that needs to be trimmed</param>
        /// <returns>New trimmed text</returns>
        public static string RemoveNullsFromString(string text)
        {
            return text.Replace("\0", string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordDataRecordIndex"></param>
        /// <param name="totalDataRecordCount"></param>
        /// <returns></returns>
        public static void CheckIfValidRecordDataIndex(int recordDataRecordIndex, int totalDataRecordCount)
        {
            if (recordDataRecordIndex < -1)
            {
                throw new ModbusException("Record data index cant be greater than total data record count");
            }
            if (recordDataRecordIndex > totalDataRecordCount)
            {
                throw new ModbusException("Record data index cant be greater than total data record count");
            }

        }

        /// <summary>
        /// Check if the given time in seconds is within the min-max range
        /// </summary>
        /// <param name="seconds">The numbe of seconds</param>
        public static void CheckIfValidHoldOrDelayTime(int seconds)
        {
            if (seconds < DEFAULT_MIN_HOLD_DELAY_TIME || seconds > DEFAULT_MAX_HOLD_DELAY_TIME )
            {
                throw new ModbusException("Hold/Delay time min = 0, max = 359999");
            }
        }

        /// <summary>
        /// Checks if the given sample time in seconds is within the min-max range
        /// </summary>
        /// <param name="seconds"></param>
        public static void CheckIfValidSampleTime(int seconds)
        {
            if (seconds < DEFAULT_MIN_SAMPLE_TIME || seconds > DEFAULT_MAX_SAMPLE_TIME)
            {
                throw new ModbusException("Hold time min = 0, max = 359999");
            }
        }

        /// <summary>
        /// Checks if the time in seconds only needs to be written to the low register
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="lowRegisterSeconds"></param>
        /// <returns></returns>
        public static bool CheckIfTimeOnlyToLowRegister(int seconds, int lowRegisterSeconds)
        {
            if (seconds <= lowRegisterSeconds)
            {
                return true;
            }

            return false;
        }
    }
}
