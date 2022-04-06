using EasyModbus.Exceptions;
using ParticleCommunicator.Models;
using System;
using System.Collections;
using static ParticleCommunicator.Communicator.ApexR5pCommunicator;

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
        private const int MAX_HOLDING_VALUE = 32767;
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
        public static ChannelAlarmStatus GetDeviceAlarmStatus(int alarmStatusNumber)
        {
            var bitArray = ConvertLowRegisterIntToBits(alarmStatusNumber);

            ChannelAlarmStatus alarmStatus = new ChannelAlarmStatus()
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
        public static DeviceStatus GetDeviceStatusFromInt(int deviceStatusNumber, int additionalDeviceStatusNumber)
        {
            var bitArrayDeviceStatus = ConvertLowRegisterIntToBits(deviceStatusNumber);
            var bitArrayAdditionalDeviceStatus = ConvertLowRegisterIntToBits(additionalDeviceStatusNumber);

            DeviceStatus deviceStatus = new DeviceStatus()
            {
                IsRunning = bitArrayDeviceStatus[0],
                IsSampling = bitArrayDeviceStatus[1],
                IsNewData = bitArrayDeviceStatus[2],
                IsDeviceError = bitArrayDeviceStatus[3],
                IsInDataValidation = bitArrayDeviceStatus[9],
                IsInLocationValidation = bitArrayDeviceStatus[10],
                IsLaserOutOfSpec = bitArrayDeviceStatus[11],
                IsFlowOutOfSpec = bitArrayDeviceStatus[12],
                IsInstrumentServiceNeeded = bitArrayDeviceStatus[13],
                IsHighAlarmThresHoldExceeded = bitArrayDeviceStatus[14],
                IsLowAlarmThresHoldExceeded = bitArrayDeviceStatus[15],
                IsLaserPowerOutOfSpec = bitArrayAdditionalDeviceStatus[0],
                IsLaserCurrentOutOfSpec = bitArrayAdditionalDeviceStatus[1],
                IsLaserSupplyOutOfSpec = bitArrayAdditionalDeviceStatus[2],
                IsLaserLifeStatusOutOfSpec = bitArrayAdditionalDeviceStatus[3],
                IsUnitsFlowBelowThreshold = bitArrayAdditionalDeviceStatus[4],
                IsPhotoAmpOutOfSpec = bitArrayAdditionalDeviceStatus[5],
                IsPhotoAmpBackGroundOutOfSpec = bitArrayAdditionalDeviceStatus[6],
                IsPhotoDiodeFailed = bitArrayAdditionalDeviceStatus[7],
                IsDevicePastCalibrationDue = bitArrayAdditionalDeviceStatus[8],
                IsUnitInLocationBracketMode = bitArrayAdditionalDeviceStatus[9],
            };

            return deviceStatus;
        }

        /// <summary>
        /// Gets the device status options
        /// </summary>
        /// <param name="deviceOptionNumber"></param>
        /// <returns></returns>

        public static DeviceOptionStatus GetDeviceOptionStatus(int deviceOptionNumber)
        {
            var bitArray = ConvertLowRegisterIntToBits(deviceOptionNumber);

            DeviceOptionStatus deviceOptionStatus = new DeviceOptionStatus()
            {
                IsFastDownloadEnabled = bitArray[5],
                IsLocationBracketEnabled = bitArray[6],
                IsSoftwareControlledRGBEnabled = bitArray[0],
            };

            return deviceOptionStatus;
        }

        /// <summary>
        ///  Checks if a hold time or sample time only needs to return low register
        /// </summary>
        /// <param name="highRegisterValue"></param>
        /// <returns></returns>
        public static bool CheckIfOnlyReturnLowRegister(int highRegisterValue)
        {
            if (highRegisterValue == 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Gets the instument default date
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDefaultInstrumentDate()
        {
            return new DateTime(1970, 1, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatusNumber"></param>
        /// <returns></returns>
        public static SampleStatusWord GetSampleStatusWord(int sampleStatusNumber)
        {
            var bitArray = ConvertLowRegisterIntToBits(sampleStatusNumber);

            SampleStatusWord sampleStatusWord = new SampleStatusWord()
            {
                IsBadLaser = bitArray[0],
                IsBadFlow = bitArray[1],
                IsParticleOverFlow = bitArray[2],
                IsMalfunctionDetected = bitArray[3],
                IsThresholdHighStatusExceeded = bitArray[4],
                IsThresholdLowStatusExceeded = bitArray[5],
                IsSamplerError = bitArray[6]
            };

            return sampleStatusWord;
        }


        /// <summary>
        /// Converts and BitArray to int
        /// </summary>
        /// <param name="bitArray"></param>
        /// <returns></returns>
        /// <exception cref="ModbusException"></exception>
        public static int GetIntFromBitArray(BitArray bitArray)
        {
            if (bitArray.Length > 32)
            {
                throw new ModbusException("Argument length shall be at most 32 bits.");
            }

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

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
            if (seconds < DEFAULT_MIN_HOLD_DELAY_TIME || seconds > DEFAULT_MAX_HOLD_DELAY_TIME)
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
        /// Checks if a int only needs to be written to low register
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckIfOnlyToLowerRegister(int value)
        {
            if (value <= MAX_HOLDING_VALUE)
            {
                return true;
            }

            return false;
        }
    }
}
