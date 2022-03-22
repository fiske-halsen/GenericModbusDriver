using EasyModbus;
using EasyModbus.Exceptions;
using ParticleCommunicator.Helpers;
using ParticleCommunicator.Models;

namespace ParticleCommunicator.Communicator
{
    /// <summary>
    /// Class for communicating with ApexR5P particle counter
    /// </summary>
    public class ParticleCommunicatorApexR5p
    {
        #region Class Variables
        private ModbusClient modbusClient;
        private DateTime defaultDate = new DateTime(1970, 1, 1);
        #endregion

        #region Enums
        // Only support first two particle channels for now
        public enum ParticleChannel
        {
            ParticleChannel1,
            ParticleChannel2,
            ParticleChannel3,
            ParticleChannel4
        }

        public enum CommandRegister40001
        {
            SaveToEEPROM = 1,
            ClearDataBuffer = 3,
            SaveInstrumentParameters = 4,
            StartInstrument = 11,
            StopInstrument = 12,
            SaveClockSettings = 13
        }

        public enum SingleRegisters
        {
            ModbusVersion = 0,
            CommandRegister = 1,
            DeviceStatus = 2,
            FirmwareVersion = 3,
            LocationNumber = 25,
            ProductNameChars1 = 6,
            ProductNameChars2 = 7,
            ProductNameChars3 = 8,
            ProductNameChars4 = 9,
            ProductNameChars5 = 10,
            ProductNameChars6 = 11,
            ProductNameChars7 = 12,
            ProductNameChars8 = 13,
            ModelNameChars1 = 14,
            ModelNameChars2 = 15,
            ModelNameChars3 = 16,
            ModelNameChars4 = 17,
            ModelNameChars5 = 18,
            ModelNameChars6 = 19,
            ModelNameChars7 = 20,
            ModelNameChars8 = 21,
            FlowRate = 22,
            FlowRateUnitChars1 = 40,
            FlowRateUnitChars2 = 41,
            RecordCount = 23,
            RecordCountIndex = 24
        }

        public enum LowHoldingRegisters
        {
            SensorSerialLow = 4,
            InstrumentTimeLow = 26,
            SetInstrumentTimeLow = 34,
            HoldTime = 31,
            InitialDelay = 29,
            SampleTime = 33,
            AlarmParticelChannel1 = 3009,
            AlarmParticleChannel2 = 3011,
            AlarmThresHoldParticleChannel1 = 5009,
            AlarmThresHoldParticleChannel2 = 5011
        }

        public enum HighHoldingRegisters
        {
            SensorSerialHigh = 5,
            InstrumentTimeHigh = 27,
            SetInstrumentTimeHigh = 35,
            HoldTime = 30,
            InitialDelay = 28,
            SampleTime = 32,
            AlarmParticelChannel1 = 3008,
            AlarmParticleChannel2 = 3010,
            AlarmThresHoldParticleChannel1 = 5008,
            AlarmThresHoldParticleChannel2 = 5010
        }

        public enum AlarmStatusValue
        {
            EnableAlarm = 3,
            DisableAlarm = 1
        }

        public enum LowInputRegisters
        {

        }

        public enum HighInputRegisters
        {

        }
        #endregion

        /// <summary>
        /// Gets the modbus register map version
        /// </summary>
        /// <returns> Returns the modbus register map version</returns>
        public string GetModbusRegisterMapVersion()
        {
            var mapVersionHoldingRegister = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModbusVersion, 1)[0];
            var convertToVNumber = ((decimal)mapVersionHoldingRegister / 100).ToString();
            return $"v.{convertToVNumber}.";
        }

        /// <summary>
        /// Gets the firmware version
        /// </summary>
        /// <returns>Returns the firmware version</returns>
        public string GetFirmwareVersion()
        {
            var firmwareVersionHoldingRegister = modbusClient.ReadHoldingRegisters((int)SingleRegisters.FirmwareVersion, 1)[0];
            var convertToVNumber = ((decimal)firmwareVersionHoldingRegister / 100).ToString();
            return $"v.{convertToVNumber}.";
        }

        /// <summary>
        /// Gets the device status
        /// </summary>
        /// <returns>Device status object</returns>
        public DeviceStatus GetDeviceStatus()
        {
            var deviceStatusHoldingRegister = modbusClient.ReadHoldingRegisters((int)SingleRegisters.DeviceStatus, 1);
            var deviceStatus = HelperService.GetDeviceStatusFromInt(deviceStatusHoldingRegister[0]);
            return deviceStatus;
        }
        /// <summary>
        /// Gets the product name from the particle counter
        /// </summary>
        /// <returns>A string containing the product name</returns>
        public string GetProductName()
        {
            var registerChars1 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars1, 1);
            var registerChars2 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars2, 1);
            var registerChars3 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars3, 1);
            var registerChars4 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars4, 1);
            var registerChars5 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars5, 1);
            var registerChars6 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars6, 1);
            var registerChars7 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars7, 1);
            var registerChars8 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ProductNameChars8, 1);

            var chars1Converted = ModbusClient.ConvertRegistersToString(registerChars1, 0, 2);
            var chars2Converted = ModbusClient.ConvertRegistersToString(registerChars2, 0, 2);
            var chars3Converted = ModbusClient.ConvertRegistersToString(registerChars3, 0, 2);
            var chars4Converted = ModbusClient.ConvertRegistersToString(registerChars4, 0, 2);
            var chars5Converted = ModbusClient.ConvertRegistersToString(registerChars5, 0, 2);
            var chars6Converted = ModbusClient.ConvertRegistersToString(registerChars6, 0, 2);
            var chars7Converted = ModbusClient.ConvertRegistersToString(registerChars7, 0, 2);
            var chars8Converted = ModbusClient.ConvertRegistersToString(registerChars8, 0, 2);

            var concatProductName = $"{chars1Converted}{chars2Converted}" +
                                $"{chars3Converted}{chars4Converted}" +
                                $"{chars5Converted}{chars6Converted}" +
                                $"{chars7Converted}{chars8Converted}";

            return HelperService.RemoveNullsFromString(concatProductName);
        }

        /// <summary>
        /// Gets the model name from the particle counter
        /// </summary>
        /// <returns>A string containing the model name</returns>
        public string GetModelName()
        {
            var registerChars1 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars1, 1);
            var registerChars2 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars2, 1);
            var registerChars3 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars3, 1);
            var registerChars4 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars4, 1);
            var registerChars5 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars5, 1);
            var registerChars6 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars6, 1);
            var registerChars7 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars7, 1);
            var registerChars8 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.ModelNameChars8, 1);

            var chars1Converted = ModbusClient.ConvertRegistersToString(registerChars1, 0, 2);
            var chars2Converted = ModbusClient.ConvertRegistersToString(registerChars2, 0, 2);
            var chars3Converted = ModbusClient.ConvertRegistersToString(registerChars3, 0, 2);
            var chars4Converted = ModbusClient.ConvertRegistersToString(registerChars4, 0, 2);
            var chars5Converted = ModbusClient.ConvertRegistersToString(registerChars5, 0, 2);
            var chars6Converted = ModbusClient.ConvertRegistersToString(registerChars6, 0, 2);
            var chars7Converted = ModbusClient.ConvertRegistersToString(registerChars7, 0, 2);
            var chars8Converted = ModbusClient.ConvertRegistersToString(registerChars8, 0, 2);

            var concatModelName = $"{chars1Converted}{chars2Converted}" +
                                 $"{chars3Converted}{chars4Converted}" +
                                 $"{chars5Converted}{chars6Converted}" +
                                 $"{chars7Converted}{chars8Converted}";

            return HelperService.RemoveNullsFromString(concatModelName);
        }

        /// <summary>
        /// Gets the particle serial number
        /// </summary>
        /// <returns></returns>
        public int GetInstrumentSerialNumber()
        {
            var serialNumberHoldingRegister = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.SensorSerialLow, 2);
            var serialNumber = ModbusClient.ConvertRegistersToInt(serialNumberHoldingRegister, ModbusClient.RegisterOrder.HighLow);
            return serialNumber;
        }

        /// <summary>
        /// Connect to modbus device
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void ConnectToModbusDevice(string ipAddress, int port)
        {
            modbusClient = new ModbusClient();
            modbusClient.Connect(ipAddress, port);
        }

        /// <summary>
        /// Disconnect from modbus device
        /// </summary>
        public void DisconnectFromModbusDevice()
        {
            modbusClient.Disconnect();
        }
        /// <summary>
        /// Save all 40's series register settings to the EEPROM
        /// </summary>
        /// 
        // Need to investigate this
        public void SaveAllHoldingRegistersToEEPROM()
        {
            modbusClient.WriteSingleRegister((int)SingleRegisters.CommandRegister, (int)CommandRegister40001.SaveToEEPROM);
        }

        /// <summary>
        /// Saves only Sample time, hold time and location settings
        /// </summary>
        public void SaveTheInstrumentParameters()
        {
            modbusClient.WriteSingleRegister((int)SingleRegisters.CommandRegister, (int)CommandRegister40001.SaveInstrumentParameters);
        }

        /// <summary>
        /// Clears all data records, sets record count to zero 
        /// </summary>
        public void ClearAllDataRecords()
        {
            modbusClient.WriteSingleRegister((int)SingleRegisters.CommandRegister, (int)CommandRegister40001.ClearDataBuffer);
        }

        /// <summary>
        /// Starts the instrument (Automatic counting), 
        /// Uses the saved Hold time and Sample time
        /// </summary>
        public void StartInstrument()
        {
            modbusClient.WriteSingleRegister((int)SingleRegisters.CommandRegister, (int)CommandRegister40001.StartInstrument);
        }
        /// <summary>
        /// Stops the instrument
        /// Aborts current samples and stops datarecording
        /// </summary>
        public void StopInstrument()
        {
            modbusClient.WriteSingleRegister((int)SingleRegisters.CommandRegister, (int)CommandRegister40001.StopInstrument);
        }

        /// <summary>
        /// Gets the current instrument time from registers
        /// </summary>
        public DateTime GetInstrumentTime()
        {
            var currentTimeRegisters = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.InstrumentTimeLow, 2);
            var highLowToSeconds = ModbusClient.ConvertRegistersToInt(currentTimeRegisters, ModbusClient.RegisterOrder.HighLow);
            return defaultDate.AddSeconds(highLowToSeconds);
        }

        /// <summary>
        /// Sets the instrument real time clock
        /// </summary>
        /// <param name="dateTime">The date time for the instrument configuration</param>
        public void SetInstrumentTime(DateTime dateTime)
        {
            var diffInSeconds = (dateTime - defaultDate).TotalSeconds;

            // Split up the diff in seconds to two 2 byte objects
            var highLowRegisters = ModbusClient.ConvertIntToRegisters((int)diffInSeconds, ModbusClient.RegisterOrder.HighLow);

            //TODO Does not work for now, so write to single at a time instead
            //modbusClient.WriteMultipleRegisters(34, highLowRegisters);

            // Need to fix this code to the above solution
            modbusClient.WriteSingleRegister((int)LowHoldingRegisters.SetInstrumentTimeLow, highLowRegisters[0]);
            modbusClient.WriteSingleRegister((int)HighHoldingRegisters.SetInstrumentTimeHigh, highLowRegisters[1]);

            // Save the real time clock
            modbusClient.WriteSingleRegister((int)SingleRegisters.CommandRegister, (int)CommandRegister40001.SaveClockSettings);
        }

        /// <summary>
        /// Gets the current location id, only one low register
        /// </summary>
        /// <returns> the location ID where data was recorded</returns>
        public int GetLocationNumber()
        {
            return modbusClient.ReadHoldingRegisters((int)SingleRegisters.LocationNumber, 1)[0];
        }

        /// <summary>
        /// Method that sets the location id
        /// </summary>
        /// <param name="LocationNumber">Location number</param>
        public void SetLocationNumber(int LocationNumber)
        {
            modbusClient.WriteSingleRegister((int)SingleRegisters.LocationNumber, LocationNumber);
        }
        /// <summary>
        /// Gets the current hold time
        /// </summary>
        /// <returns>Hold time</returns>
        public int GetHoldTime()
        {
            var locRegister = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.HoldTime, 2);
            return ModbusClient.ConvertRegistersToInt(locRegister, ModbusClient.RegisterOrder.HighLow);
        }

        /// <summary>
        /// Sets the hold time
        /// </summary>
        /// <param name="holdTimeInSeconds"> the hold time</param>
        public void SetHoldTime(int holdTimeInSeconds)
        {

            HelperService.CheckIfValidHoldOrDelayTime(holdTimeInSeconds);

            var holdTimeLowRegisterSeconds = HelperService.GetMinValueSampleHoldRegister();

            var isOnlyLowRegister = HelperService.CheckIfTimeOnlyToLowRegister(holdTimeInSeconds, holdTimeLowRegisterSeconds);

            // check if hold time in seconds only has to be written to the low register
            if (isOnlyLowRegister)
            {
                modbusClient.WriteSingleRegister((int)LowHoldingRegisters.HoldTime, holdTimeLowRegisterSeconds);
                return;
            }

            var convertedHoldTime = ModbusClient.ConvertIntToRegisters(holdTimeInSeconds, ModbusClient.RegisterOrder.HighLow);
            modbusClient.WriteSingleRegister((int)HighHoldingRegisters.HoldTime, convertedHoldTime[0]);
            modbusClient.WriteSingleRegister((int)LowHoldingRegisters.HoldTime, convertedHoldTime[1]);
        }

        /// <summary>
        /// Gets the current sample time
        /// </summary>
        /// <returns> The sample time</returns>
        public int GetSampleTime()
        {
            var sampleRegister = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.SampleTime, 2);
            return ModbusClient.ConvertRegistersToInt(sampleRegister, ModbusClient.RegisterOrder.HighLow);
        }

        /// <summary>
        /// Sets the current sample time
        /// </summary>
        /// <param name="sampleTime">The sample time to be set</param>
        public void SetSampleTime(int sampleTimeInSeconds)
        {
            HelperService.CheckIfValidHoldOrDelayTime(sampleTimeInSeconds);

            var sampleTimeLowRegisterSeconds = HelperService.GetMinValueSampleHoldRegister();

            var isOnlyLowRegister = HelperService.CheckIfTimeOnlyToLowRegister(sampleTimeInSeconds, sampleTimeLowRegisterSeconds);

            if (isOnlyLowRegister)
            {
                modbusClient.WriteSingleRegister((int)LowHoldingRegisters.SampleTime, sampleTimeInSeconds);
                return;
            }

            var convertedSampleTime = ModbusClient.ConvertIntToRegisters(sampleTimeInSeconds, ModbusClient.RegisterOrder.HighLow);
            modbusClient.WriteSingleRegister((int)HighHoldingRegisters.SampleTime, convertedSampleTime[0]);
            modbusClient.WriteSingleRegister((int)LowHoldingRegisters.SampleTime, convertedSampleTime[1]);
        }

        /// <summary>
        /// Checks if a particle channels alarm is enabled
        /// </summary>
        /// <param name="particleChannel">The particle channel</param>
        /// <returns>Boolean if a channels alarm is active or not</returns>
        public AlarmStatus CheckIfAlarmIsEnabledForChannel(ParticleChannel particleChannel)
        {
            int alarmChannelRegisteNumber;

            if (particleChannel.Equals(ParticleChannel.ParticleChannel1))
            {
                alarmChannelRegisteNumber = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.AlarmParticelChannel1, 1)[0];
            }
            else
            {
                alarmChannelRegisteNumber = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.AlarmParticleChannel2, 1)[0];
            }

            var alarmStatus = HelperService.GetDeviceAlarmStatus(alarmChannelRegisteNumber);

            return alarmStatus;
        }

        /// <summary>
        /// Sets the alarmstatus for a given particle channel
        /// </summary>
        /// <param name="particleChannel">The particle channel</param>
        /// <param name="alarmStatus">The alarm status</param>
        public void SetAlarmForChannel(ParticleChannel particleChannel, AlarmStatusValue alarmStatus)
        {
            if (particleChannel.Equals(ParticleChannel.ParticleChannel1))
            {
                modbusClient.WriteSingleRegister((int)LowHoldingRegisters.AlarmParticelChannel1, (int)alarmStatus);

            }
            modbusClient.WriteSingleRegister((int)LowHoldingRegisters.AlarmParticleChannel2, (int)alarmStatus);
        }
        /// <summary>
        /// Gets the current alarm threshold for a given particle channel
        /// </summary>
        /// <param name="particleChannel">The particle channel</param>
        public int GetAlarmThresHoldForParticleChannel(ParticleChannel particleChannel)
        {
            int[] alarmThresholdRegisters;

            if (particleChannel.Equals(ParticleChannel.ParticleChannel1))
            {
                alarmThresholdRegisters = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.AlarmThresHoldParticleChannel1, 2);
            }
            alarmThresholdRegisters = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.AlarmThresHoldParticleChannel2, 2);

            return ModbusClient.ConvertRegistersToInt(alarmThresholdRegisters, ModbusClient.RegisterOrder.HighLow);
        }

        /// <summary>
        /// Sets the alarm threshold for a given channel
        /// </summary>
        /// <param name="particleChannel">The particle channel</param>
        /// <param name="threshold">The threshold value</param>
        public void SetAlarmThresHoldForParticleChannel(ParticleChannel particleChannel, int threshold)
        {
            var convertedThresHoldToRegisters = ModbusClient.ConvertIntToRegisters(threshold, ModbusClient.RegisterOrder.HighLow);

            if (particleChannel.Equals(ParticleChannel.ParticleChannel1))
            {
                modbusClient.WriteSingleRegister((int)HighHoldingRegisters.AlarmThresHoldParticleChannel1, convertedThresHoldToRegisters[0]);
                modbusClient.WriteSingleRegister((int)LowHoldingRegisters.AlarmThresHoldParticleChannel1, convertedThresHoldToRegisters[1]);
            }
            modbusClient.WriteSingleRegister((int)HighHoldingRegisters.AlarmThresHoldParticleChannel2, convertedThresHoldToRegisters[0]);
            modbusClient.WriteSingleRegister((int)LowHoldingRegisters.AlarmThresHoldParticleChannel2, convertedThresHoldToRegisters[1]);
        }

        /// <summary>
        /// Gets the instrument flow rate, use the GetInstrumentFlowRateUnit() for the unit
        /// </summary>
        /// <returns>Returns the flow number</returns>
        public int GetInstrumentFlowRate()
        {
            var flowRateHolding = modbusClient.ReadHoldingRegisters((int)SingleRegisters.FlowRate, 1);
            return flowRateHolding[0] / 100;
        }

        /// <summary>
        /// Gets the instrument flow rate unit
        /// </summary>
        /// <returns>Returns a string containing the flow rate unit</returns>
        public string GetInstrumentFlowRatUnit()
        {
            var holdingsFlowRateRegister1 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.FlowRateUnitChars1, 1);
            var holdingsFlowRateRegister2 = modbusClient.ReadHoldingRegisters((int)SingleRegisters.FlowRateUnitChars2, 1);

            var flowRateChars1 = ModbusClient.ConvertRegistersToString(holdingsFlowRateRegister1, 0, 2);
            var flowRateChars2 = ModbusClient.ConvertRegistersToString(holdingsFlowRateRegister2, 0, 2);

            var concatFlowRateUnit = $"{flowRateChars1}{flowRateChars2}";

            return HelperService.RemoveNullsFromString(concatFlowRateUnit);
        }

        /// <summary>
        /// Gets the total data record count stored in the particle counter
        /// </summary>
        /// <returns>The total data record count</returns>
        public int GetTotalDataRecordCount()
        {
            return modbusClient.ReadHoldingRegisters((int)SingleRegisters.RecordCount, 1)[0];
        }

        /// <summary>
        /// gets the current data record index set in the buffer which you can retrieve data from
        /// </summary>
        /// <returns>The current data record index</returns>
        public int GetCurrentDataRecordIndex()
        {
            return modbusClient.ReadHoldingRegisters((int)SingleRegisters.RecordCountIndex, 1)[0];
        }

        /// <summary>
        /// Sets the current data record index in the buffer to retrieve data from
        /// </summary>
        /// <param name="recordDataIndex"> The record index to be set</param>
        public void SetCurrentDataRecordIndex(int recordDataIndex)
        {
            int totalDataRecord = GetTotalDataRecordCount();

            HelperService.CheckIfValidRecordDataIndex(recordDataIndex, totalDataRecord);
          
            modbusClient.WriteSingleRegister((int)SingleRegisters.RecordCountIndex, recordDataIndex);
        }

        /// <summary>
        /// Gets the current initial delay in seconds
        /// </summary>
        /// <returns> The initial delay </returns>
        public int GetCurrentInitialDelay()
        {
            var holdingInitialDelay = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.InitialDelay,2);
            return ModbusClient.ConvertRegistersToInt(holdingInitialDelay, ModbusClient.RegisterOrder.HighLow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialDelayInSeconds"></param>
        public void SetCurrentInitialDelay(int initialDelayInSeconds)
        {
            HelperService.CheckIfValidHoldOrDelayTime(initialDelayInSeconds);

            var delayTimeLowRegisterSeconds = HelperService.GetMinValueSampleHoldRegister();

            var isOnlyLowRegister = HelperService.CheckIfTimeOnlyToLowRegister(initialDelayInSeconds, delayTimeLowRegisterSeconds);

            if (isOnlyLowRegister)
            {
                modbusClient.WriteSingleRegister((int) LowHoldingRegisters.InitialDelay, initialDelayInSeconds);
                return;
            }
            var convertedDelaTimeRegisters = ModbusClient.ConvertIntToRegisters(initialDelayInSeconds, ModbusClient.RegisterOrder.HighLow);
            modbusClient.WriteSingleRegister((int) HighHoldingRegisters.InitialDelay, convertedDelaTimeRegisters[0]);
            modbusClient.WriteSingleRegister((int) LowHoldingRegisters.InitialDelay, convertedDelaTimeRegisters[1]);
        }
    }
}
