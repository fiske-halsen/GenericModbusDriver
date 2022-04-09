// See https://aka.ms/new-console-template for more information
using EasyModbus;
using ModBusClientTester;
using ParticleCommunicator.Communicator;
using ParticleCommunicator.Helpers;
using ParticleCommunicator.Models;
using System.Collections;
using System.Diagnostics;
using System.Net.Sockets;

ModbusClient e = new ModbusClient();

//// connection
//e.Connect("192.168.0.14", 502); //home Ip

//var holdings = e.ReadHoldingRegisters(57, 2);
//var converted = ModbusClient.ConvertRegistersToInt(holdings, ModbusClient.RegisterOrder.HighLow);
//e.Connect("10.8.4.61", 502);
//e.WriteSingleRegister(1,12);

//var test1 = new RegisterClass()
//{
//    RegisterNumber = 1,
//    RegisterValue = 3

//};

//var test2 = new RegisterClass()
//{
//    RegisterNumber = 1,
//    RegisterValue = 2

//};

//var isEqual = test1.Equals(test2);

// Small script to find changed Registers :D

//List<RegisterClass> listBefore = new List<RegisterClass>();
//List<RegisterClass> listAfter = new List<RegisterClass>();
//List<RegisterClass> finalList = new List<RegisterClass>();

//for (int i = 0; i < 9999; i++)
//{
//    var holdingsTest = e.ReadHoldingRegisters(i, 1);
//    listBefore.Add(new RegisterClass()
//    {
//        RegisterNumber = i,
//        RegisterValue = holdingsTest[0]
//    });
//}

//e.Disconnect();
//Thread.Sleep(10000);
//e.Connect("192.168.0.18", 502);
//for (int i = 0; i < 9999; i++)
//{
//    var holdingsTest = e.ReadHoldingRegisters(i, 1);
//    listAfter.Add(new RegisterClass()
//    {
//        RegisterNumber = i,
//        RegisterValue = holdingsTest[0]
//    });
//}
//var hello = "";

//Console.WriteLine("yeeeee");
//var differences = listBefore.Except(listAfter).Concat(listAfter.Except(listBefore));

//Console.WriteLine("yeeeeeeeeeeeeEVENMORE");
//foreach (var item in differences)
//{
//    Console.WriteLine($"Register number: {item.RegisterNumber}");
//    Console.WriteLine($"Register number: {item.RegisterValue}");
//}

////var inputs = e.ReadHoldingRegisters(3008,4);
//return;

// -------------------------------- Phillips Libary ------------------------------
ParticleCommunicator.Communicator.ApexR5pCommunicator apex = new ParticleCommunicator.Communicator.ApexR5pCommunicator();

// Works
apex.ConnectToModbusDevice("192.168.0.14");

apex.StopInstrument();
return;
//apex.StopDataValidation();
//apex.StopLocationValidation();

//Thread.Sleep(2000);
//apex.StartLocationValidation();

apex.StartInstrument();

apex.ParticleDataRecordEvent += new ApexR5pSubscriberExample().OnMyEventRaised;

 Task.Run(() =>
{
    Thread.Sleep(60000);
    apex.isSampling = false;
});

await apex.GetParticleData(3);

Debug.WriteLine("IsSampling: " + apex.isSampling);

//return;

// -------------------------------- PHILLIPS LIBARY TEST METHODS ------------------------------

// Works
// Works
//apex.StartInstrument();


// Works
//Debug.WriteLine(apex.GetModbusRegisterMapVersion());

// Works
//Debug.WriteLine(apex.GetFirmwareVersion());


// works
//var deviceStatus  = apex.GetDeviceStatus();

// Works
//Debug.WriteLine(apex.GetProductName());

// Works
//Debug.WriteLine(apex.GetModelName());

//Works
//Debug.WriteLine(apex.GetInstrumentSerialNumber());

// Works
//apex.DisconnectFromModbusDevice();


// Works
//apex.SaveAllHoldingRegistersToEEPROM();


// Works
//apex.SaveTheInstrumentParameters();

// Works
//apex.ClearAllDataRecords();    

// Works
//apex.StartLocationValidation();

// Works
//apex.StopLocationValidation();

//Works
//apex.StartDataValidation();

// Stop data validation
//apex.StopDataValidation();

// Works
//Debug.WriteLine(apex.GetInstrumentTime());

// Works
//apex.SetInstrumentTime(DateTime.Now);

// Works
//Debug.WriteLine(apex.GetLocationNumber());

//Works
//apex.SetLocationNumber(3);

// Works
//Debug.WriteLine(apex.GetHoldTime());

//Works
//apex.SetHoldTime(50000);

// Works
//Debug.WriteLine(apex.GetSampleTime());

// Works
//apex.SetSampleTime(50000);

// Works
//var channelAlarmStatus = apex.GetChannelAndAlarmStatus(ApexR5pCommunicator.ParticleChannel.ParticleChannel1);

// Works
//apex.SetParticleChannelEnableStatus(ApexR5pCommunicator.ParticleChannel.ParticleChannel1, ApexR5pCommunicator.ChannelStatusValue.DisableChannel);

// Works 
//apex.SetAlarmForChannel(ApexR5pCommunicator.ParticleChannel.ParticleChannel1, ApexR5pCommunicator.AlarmStatusValue.DisableAlarm);

// Works
//apex.SetAlarmThresHoldForParticleChannel(ApexR5pCommunicator.ParticleChannel.ParticleChannel1, 40000);

// Works
//apex.SetAlarmThresHoldForParticleChannel(ApexR5pCommunicator.ParticleChannel.ParticleChannel1, 999999);
//Debug.WriteLine(apex.GetAlarmThresHoldForParticleChannel(ApexR5pCommunicator.ParticleChannel.ParticleChannel1));

// Works
//Debug.WriteLine(apex.GetInstrumentFlowRate());

// Works
//Debug.WriteLine(apex.GetInstrumentFlowRatUnit());

// Works
//Debug.WriteLine(apex.GetTotalDataRecordCount());

// Works
//Debug.WriteLine(apex.GetCurrentInitialDelay());

// Works
//Debug.WriteLine(apex.GetCalibrationDueDate());

// Works
//Debug.WriteLine(apex.GetLastCalibrationDate());

// Works
//Debug.WriteLine(apex.GetLastSampleTimeStamp());

// Works
//Debug.WriteLine(apex.GetLastSettingsChangeTimeStamp());

// Works
//var deviceOptions = apex.GetDeviceOptionStatus();

// Works
//var sampleStatusWord = apex.GetSampleStatusWord();

// Works


// -------------------------------- PHILLIPS LIBARY TEST METHODS END ------------------------------

// --------------------------- Phillips libary for sample data -------------------------
//apex.StopInstrument();

//apex.ClearAllDataRecords();

//Thread.Sleep(2000);

//apex.StartInstrument();
//apex.ParticleDataRecordEvent += new ApexR5pSubscriberExample().OnMyEventRaised;

//await apex.GetParticleData(3, 35);

// --------------------------- Phillips libary for sample data -------------------------

//apex.SetParticleChannelEnableStatus(ParticleCommunicatorApexR5p.ParticleChannel.ParticleChannel1, ParticleCommunicatorApexR5p.ChannelStatusValue.DisableChannel);

//apex.SetAlarmForChannel(ParticleCommunicatorApexR5p.ParticleChannel.ParticleChannel1, ParticleCommunicatorApexR5p.AlarmStatusValue.DisableAlarm);

//var holdTime = apex.GetHoldTime();
//var sampleTime = apex.GetSampleTime();

// ---------------------------- ALL METHODS TESTING --------------------------------------

// ------------------------------------- LIVE SAMPLING TEST -------------------------------

//apex.stopinstrument();

//apex.clearalldatarecords();

//thread.sleep(2000);

//apex.startinstrument();

//list<particledatarecord> list = new list<particledatarecord>();

//debug.writeline("started.....");

//apex.particledatarecordevent += new subscriber().onmyeventraised;

//await apex.getparticledata(2);

//debug.write(list.count);

//foreach (var item in list)
//{
//    debug.writeline("particle channel 1 " + item.particalchannel1count);
//    debug.writeline("particle channel 2 " + item.particalchannel2count);
//}

// ---------------------------------- LIVE SAMPLING END --------------------------------------

////var sampleStatusHoldings1 = e.ReadInputRegisters(6,1)[0];
////var sampleStatusHoldings2 = e.ReadInputRegisters(7,2)[0];

////var bitArrayHigh = new BitArray(new int[] { sampleStatusHoldings1 });
////var bitArrayLow = new BitArray(new int[] { sampleStatusHoldings2 });

////var sampleTimeInSeconds = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(0, 2), ModbusClient.RegisterOrder.HighLow);
////var sampleTimeConverted = new DateTime(1970, 1, 1).AddSeconds(sampleTimeInSeconds);
////var sampleStatus = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(6, 2), ModbusClient.RegisterOrder.HighLow);
////var location = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(4, 2), ModbusClient.RegisterOrder.HighLow);
////var particleChannel1Count = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(8, 2), ModbusClient.RegisterOrder.HighLow);
////var particleChannel2Count = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(10, 2), ModbusClient.RegisterOrder.HighLow);

////ModBusClientTester.DataRecord record = new ModBusClientTester.DataRecord()
////{
////    SampleTime = sampleTimeConverted.ToString(),
////    Location = location,
////    SampleStatus = sampleStatus,
////    ParticalChannel1Count = particleChannel1Count,
////    ParticalChannel2Count = particleChannel2Count
////};

////Console.WriteLine($"Sample time:" +
////    $"{record.SampleTime} \n" +
////    $"Location: {record.Location} \n" +
////    $"Sample Status: " +
////    $"{record.SampleStatus} \n" +
////    $"Particle channel 1 Count: {record.ParticalChannel1Count} \n" +
////    $"Particle channel 2 Count {record.ParticalChannel2Count}");

// ---------------------- Last Sample Time Stamp ----------------------------

//var SampleTimeStampRegisters = e.ReadHoldingRegisters(59, 2);

//var testers = EasyModbus.ModbusClient.ConvertRegistersToInt(SampleTimeStampRegisters, ModbusClient.RegisterOrder.HighLow);
//var dateTime2 = new DateTime(1970, 1, 1);
//var datetimeReal = dateTime2.AddSeconds(testers);

// -------------------- Last settings change ------------------------------
//var SettingsChangeRegisters = e.ReadHoldingRegisters(61, 2);

//var testers1 = EasyModbus.ModbusClient.ConvertRegistersToInt(SettingsChangeRegisters, ModbusClient.RegisterOrder.HighLow);
//var dateTime3 = new DateTime(1970, 1, 1);
//var datetimeReal1 = dateTime3.AddSeconds(testers1);

// -------------------------- Last calibration date -----------------------------

/*var calibrationDuedateHoldingsRegisters = e.ReadHoldingRegisters(73 , 2);
var testers = EasyModbus.ModbusClient.ConvertRegistersToInt(calibrationDuedateHoldingsRegisters, ModbusClient.RegisterOrder.HighLow);
var dateTime2 = new DateTime(1970, 1, 1);
var datetimeReal = dateTime2.AddSeconds(testers);Ø*/


// ----------------------------- Serial number ---------------------------
//var serialRegisters = e.ReadHoldingRegisters(57,2);

//var inter = EasyModbus.ModbusClient.ConvertRegistersToInt(serialRegisters, ModbusClient.RegisterOrder.HighLow);


//var serialRegisters1 = e.ReadHoldingRegisters(4, 2);

//var inter1 = EasyModbus.ModbusClient.ConvertRegistersToInt(serialRegisters1, ModbusClient.RegisterOrder.HighLow);

// ------------------------ Device options -----------------------------

//var deviceOptionsRegisters = e.ReadHoldingRegisters(49, 1);

// ------------------------- Calibration due date --------------------------------

//var calibrationDuedateHoldingsRegisters = e.ReadHoldingRegisters(46 , 2);
//var testers = EasyModbus.ModbusClient.ConvertRegistersToInt(calibrationDuedateHoldingsRegisters, ModbusClient.RegisterOrder.HighLow);
//var dateTime2 = new DateTime(1970, 1, 1);
//var datetimeReal = dateTime2.AddSeconds(testers);


// Turn off/on
//e.WriteSingleRegister(1,12);

//Thread.Sleep(2000);

//e.WriteSingleRegister(1,11);

// -------------------- Initial Delay --------------------------

//var holdingInitialDelay = e.ReadHoldingRegisters(28,2);

// --------------------------- Flow rate -----------------------

//var holdings = e.ReadHoldingRegisters(22,1)[0];

// --------------------- Record count ----------------------

//var recordHolding = e.ReadHoldingRegisters(200,100);

// ---------------------- Flow rate unit ----------------------

/*var holdings1 = e.ReadHoldingRegisters(40, 1);
var holdings2 = e.ReadHoldingRegisters(41, 1);


var text1 = EasyModbus.ModbusClient.ConvertRegistersToString(holdings1,0,2);
var text2 = EasyModbus.ModbusClient.ConvertRegistersToString(holdings2,0,2);*/


//var holdings = e.ReadHoldingRegisters(3009,1)[0];

//var bitArray = new BitArray(new int[] { holdings });

/*AlarmStatus alarmStatus = new AlarmStatus()
{
    IsChannelEnabled = bitArray[0],
    IsAlarmEnabled = bitArray[1]
};*/

/*BitArray b = new BitArray(new int[] { holdings});

DeviceStatus ds = new DeviceStatus()
{
    IsRunning = b[0],
    IsSampling = b[1],
    IsNewData = b [2]
};

Console.WriteLine(ds);

bool[] bits = new bool[b.Count];
b.CopyTo(bits, 0);*/

//var holdingsTest = e.ReadHoldingRegisters(5008,2);

//var converted = EasyModbus.ModbusClient.ConvertRegistersToInt(holdingsTest, ModbusClient.RegisterOrder.HighLow);

// Wait with this one
//ParticleCommunicator.Communicator.ParticleCommunicator pc = new ParticleCommunicator.Communicator.ParticleCommunicator();

//HelperService.GetMinValueSampleHoldRegister();

//pc.ConnectToModbusDevice("192.168.0.14", 502);

//pc.SetInstrumentTime(DateTime.Now);

//var convertHoldTime = EasyModbus.ModbusClient.ConvertIntToRegisters(40504, ModbusClient.RegisterOrder.HighLow);

//e.WriteSingleRegister(32, convertHoldTime[0]);
//e.WriteSingleRegister(33, convertHoldTime[1]);


//var locRegister = e.ReadHoldingRegisters(32,2);

//var converted = EasyModbus.ModbusClient.ConvertRegistersToInt(locRegister, ModbusClient.RegisterOrder.HighLow);

//var firmwareVersionHoldingRegister = e.ReadHoldingRegisters(3, 1);

// ----------------------- for product name -------------
/*var modelHoldingRegister1 = e.ReadHoldingRegisters(6, 1);
var modelHoldingRegister2 = e.ReadHoldingRegisters(7, 1);
var modelHoldingRegister3 = e.ReadHoldingRegisters(8, 1);
var modelHoldingRegister4 = e.ReadHoldingRegisters(9, 1);
var modelHoldingRegister5 = e.ReadHoldingRegisters(10, 1);
var modelHoldingRegister6 = e.ReadHoldingRegisters(11, 1);
var modelHoldingRegister7 = e.ReadHoldingRegisters(12, 1);
var modelHoldingRegister8 = e.ReadHoldingRegisters(13, 1);

// Todo make sure that you can use the highlow option for the ConvertRegistersToString method

var tester1 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister1, 0, 2);
var tester2 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister2, 0, 2);
var tester3 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister3, 0, 2);
var tester4 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister4, 0, 2);
var tester5 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister5, 0, 2);
var tester6 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister6, 0, 2);
var tester7 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister7, 0, 2);
var tester8 = EasyModbus.ModbusClient.ConvertRegistersToString(modelHoldingRegister8, 0, 2);

<<<<<<< HEAD
var registers = BitConverter.GetBytes(productHoldingRegisters[0]);
byte[] registerResult = new byte[2];
registerResult[0] = registers[1];
registerResult[1] = registers[0];
=======
var concat = $"{tester1}{tester2}" +
                   $"{tester3}{tester4}" +
                   $"{tester5}{tester6}" +
                   $"{tester7}{tester8}";

var index = concat.Replace("\0", string.Empty);*/

//var registers = BitConverter.GetBytes(productHoldingRegisters[0]);
//byte[] registerResult = new byte[2];
//registerResult[0] = registers[1];
//registerResult[1] = registers[0];


//var no2 = System.Text.Encoding.Default.GetString(registerResult);

//Console.WriteLine(tester);

//------------------------- product name ending --------------------------

// Turn off/on
//e.WriteSingleRegister(1, 11);
//e.WriteSingleRegister(1, 12);

// just the stop the execution
//return;

/*var dateTime2 = new DateTime(1970, 1, 1);
var dateTime1 = DateTime.Now;
//Console.WriteLine(date);
var diffInSeconds = (dateTime1 - dateTime2).TotalSeconds;
var collection = e.ReadInputRegisters(40,8);
foreach (var item in collection)
{
    Console.WriteLine(item);
}*/

/*
var ints = EasyModbus.ModbusClient.ConvertIntToRegisters((int) diffInSeconds, ModbusClient.RegisterOrder.HighLow);
e.WriteMultipleRegisters(34,ints);
e.WriteSingleRegister(34, ints[0]);
e.WriteSingleRegister(35, ints[1]);
e.WriteSingleRegister(1, 13);
var d = e.ReadHoldingRegisters(26, 2);
var tests = EasyModbus.ModbusClient.ConvertRegistersToInt(d, ModbusClient.RegisterOrder.HighLow);
Console.WriteLine("Testsss" + tests);
var rose = new DateTime(1970, 1, 1).AddSeconds(tests);*/


//e.WriteSingleRegister(1, 11);

//e.WriteSingleRegister(33, 30);

//e.WriteSingleRegister(1, 1);

//e.WriteSingleRegister(1, 4);

// Get latest sample data as object


// ------------------------- Write to registers --------------------------
// Register col is col -1 
//e.WriteSingleRegister(1, 11);

//e.WriteSingleRegister(24, -1);

// 3 for enable alarms, 1 for disable
//e.WriteSingleRegister(3009, 1);
//e.WriteSingleRegister(3011, 1);

//e.WriteSingleRegister(30, 20638);
//e.WriteSingleRegister(31, 20638);

//bool[] readCoils = e.ReadCoils(30, 1);

// -------------------------- Holding registers -------------------- 
//int[] readHoldingRegisters = e.ReadInputRegisters(44, 2);  //Read 10 Holding Registers from Server, starting with Address 1

//int[] newest = { readHoldingRegisters[1], readHoldingRegisters[0] };

//dt = dt.AddSeconds(-dt.Second);

//var tester2 = EasyModbus.ModbusClient.ConvertRegistersToInt(readHoldingRegisters, ModbusClient.RegisterOrder.HighLow);

//Console.WriteLine($"tester: {tester}");

//int[] readInputRegisters = e.ReadInputRegisters(1, 100);

//for (int i = 0; i < readInputRegisters.Length; i++)
// Console.WriteLine("Value of Input register " + (i + 1) + " " + readInputRegisters[i].ToString());

//DateTime dt = new DateTime(1970, 1, 1).AddSeconds(tester);

//Console.WriteLine(dt + " heeer");

// Console Output
//for (int i = 0; i < readCoils.Length; i++)
//  Console.WriteLine("Value of Coil " + (9 + i + 1) + " " + readCoils[i].ToString());

//for (int i = 0; i < readHoldingRegisters.Length; i++)
// Console.WriteLine("Value of HoldingRegister " + (i + 1) + " " + readHoldingRegisters[i].ToString());

//var tester = ConvertRegistersToString(readHoldingRegisters, 0, 0);

// Console.WriteLine(tester);


//e.Disconnect();                                                //Disconnect from Server
//Console.Write("Press any key to continue . . . ");
//Console.ReadKey(true);