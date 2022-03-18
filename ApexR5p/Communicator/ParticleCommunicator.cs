using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;

namespace ParticleCommunicator.Communicator
{
    /// <summary>
    /// Class for communicating with ApexR5P particle counter
    /// </summary>
    public class ParticleCommunicator
    {
        #region class variables
        private ModbusClient modbusClient;
        #endregion
        /// <summary>
        /// Gets the modbus register map version
        /// </summary>
        /// <returns> Returns the modbus register map version</returns>
        public string GetModbusRegisterMapVersion()
        {
            var mapVersionHoldingRegister = modbusClient.ReadHoldingRegisters(0,1)[0];
            var convertToVNumber = ((decimal)mapVersionHoldingRegister / 100).ToString();
            return $"v.{convertToVNumber}.";
        }

        /// <summary>
        /// Gets the firmware version
        /// </summary>
        /// <returns>Returns the firmware version</returns>
        public string GetFirmwareVersion()
        {
            var firmwareVersionHoldingRegister = modbusClient.ReadHoldingRegisters(3, 1)[0];
            var convertToVNumber = ((decimal)firmwareVersionHoldingRegister / 100).ToString();
            return $"v.{convertToVNumber}.";
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
            var serialNumberHoldingRegister = modbusClient.ReadHoldingRegisters(4,2);
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
            modbusClient.WriteSingleRegister(1, 1);
        }

        /// <summary>
        /// Saves only Sample time, hold time and location settings
        /// </summary>
        public void SaveTheInstrumentParameters()
        {
            modbusClient.WriteSingleRegister(1, 4);
        }

        /// <summary>
        /// Clears all data records, sets record count to zero 
        /// </summary>
        public void ClearAllDataRecords()
        {
            modbusClient.WriteSingleRegister(1, 3);
        }
        /// <summary>
        /// Starts the instrument (Automatic counting), 
        /// Uses the saved Hold time and Sample time
        /// </summary>
        public void StartInstrument()
        {
            modbusClient.WriteSingleRegister(1, 11);
        }
        /// <summary>
        /// Stops the instrument
        /// Aborts current samples and stops datarecording
        /// </summary>
        public void StopInstrument()
        {
            modbusClient.WriteSingleRegister(1, 12);
        }

        /// <summary>
        /// Sets the instrument real time clock
        /// </summary>
        /// <param name="dateTime">The date time for the instrument configuration</param>
        public void SetInstrumentTime(DateTime dateTime)
        {
            var defaultDate = new DateTime(1970, 1, 1);
            var diffInSeconds = (dateTime - defaultDate).TotalSeconds;

            // Split up the diff in seconds to two 2 byte objects
            var highLowRegisters = EasyModbus.ModbusClient.ConvertIntToRegisters((int)diffInSeconds, ModbusClient.RegisterOrder.HighLow);

            modbusClient.WriteMultipleRegisters(34, highLowRegisters);

            // Save the real time clock
            modbusClient.WriteSingleRegister(1, 13);
        }






    }
}
