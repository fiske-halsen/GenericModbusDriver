using EasyModbus;
using EasyModbus.Exceptions;
using ParticleCommunicator.Helpers;

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
        public enum ParticleChannel
        {
            ParticleChannel1,
            ParticleChannel2
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
            FirmwareVersion = 3,
            LocationId = 25
        }

        public enum LowHoldingRegisters
        {
            SensorSerialLow = 4,
            InstrumentTimeLow = 26,
            SetInstrumentTimeLow = 34,
            HoldTime = 31,
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
            SampleTime = 32,
            AlarmParticelChannel1 = 3008,
            AlarmParticleChannel2 = 3010,
            AlarmThresHoldParticleChannel1 = 5008,
            AlarmThresHoldParticleChannel2 = 5010
        }

        public enum LowInputRegisters
        {

        }

        public enum HighInputRegisters
        {

        }

        public enum AlarmStatus
        {
            EnableAlarm = 3,
            DisableAlarm = 1,
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
        public void GetDeviceStatus()
        {

        }
        /// <summary>
        /// Gets the particle product name
        /// </summary>
        /// <returns>Returns the productName</returns>
        public string getProductName()
        {
            return "";
        }
        /// <summary>
        /// Gets the particle serial number
        /// </summary>
        /// <returns></returns>
        public int GetParticleSerialNumber()
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
            return modbusClient.ReadHoldingRegisters((int)SingleRegisters.LocationId, 1)[0];
        }

        /// <summary>
        /// Method that sets the location id
        /// </summary>
        /// <param name="locationId">Location id</param>
        public void SetLocationNumber(int locationId)
        {
            modbusClient.WriteSingleRegister((int)SingleRegisters.LocationId, locationId);
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
            // Check if hold time is in min-max range
            if (holdTimeInSeconds < 0 || holdTimeInSeconds > 359999)
            {
                throw new ModbusException("Hold time min = 0, max = 359999");
            }

            var holdTimeLowRegisterSeconds = HelperService.GetMinValueSampleHoldRegister();

            // check if hold time in seconds only has to be written to the low register
            if (holdTimeInSeconds <= holdTimeLowRegisterSeconds)
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
            if (sampleTimeInSeconds < 0 || sampleTimeInSeconds > 86399)
            {
                throw new ModbusException("Sample time min = 0, max = 86399");
            }
            var sampleTimeLowRegisterSeconds = HelperService.GetMinValueSampleHoldRegister();

            if (sampleTimeInSeconds <= sampleTimeLowRegisterSeconds)
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
        public bool CheckIfAlarmIsEnabledForChannel(ParticleChannel particleChannel)
        {
            int alarmChannelRegister;

            if (particleChannel.Equals(ParticleChannel.ParticleChannel1))
            {
                alarmChannelRegister = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.AlarmParticelChannel1, 1)[0];
            }
            else
            {
                alarmChannelRegister = modbusClient.ReadHoldingRegisters((int)LowHoldingRegisters.AlarmParticleChannel2, 1)[0];
            }

            var isAlarmEnabled = HelperService.IsAlarmEnabled(alarmChannelRegister);

            return isAlarmEnabled;
        }

        /// <summary>
        /// Sets the alarmstatus for a given particle channel
        /// </summary>
        /// <param name="particleChannel">The particle channel</param>
        /// <param name="alarmStatus">The alarm status</param>
        public void SetAlarmForChannel(ParticleChannel particleChannel, AlarmStatus alarmStatus)
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

    }
}
