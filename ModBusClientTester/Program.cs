// See https://aka.ms/new-console-template for more information
using EasyModbus;
using ModBusClientTester;
using ParticleCommunicator.Communicator;
using ParticleCommunicator.Helpers;

ModbusClient e = new ModbusClient();

// connection
e.Connect("192.168.0.14", 502);

var holdingsTest = e.ReadHoldingRegisters(5008,2);

var converted = EasyModbus.ModbusClient.ConvertRegistersToInt(holdingsTest, ModbusClient.RegisterOrder.HighLow);


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
//var productHoldingRegisters = e.ReadHoldingRegisters(14, 1);

//int[] convert = { productHoldingRegisters[1], productHoldingRegisters[0]};

// Todo make sure that you can use the highlow option for the ConvertRegistersToString method

/*var tester = EasyModbus.ModbusClient.ConvertRegistersToString(productHoldingRegisters, 0, 2);

var registers = BitConverter.GetBytes(productHoldingRegisters[0]);
byte[] registerResult = new byte[2];
registerResult[0] = registers[1];
registerResult[1] = registers[0];

var no2 = System.Text.Encoding.Default.GetString(registerResult);

Console.WriteLine(tester);*/

//------------------------- product name ending --------------------------

// Turn off/on
//e.WriteSingleRegister(1, 11);
//e.WriteSingleRegister(1, 12);

// just the stop the execution
return;

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
var sampleTimeInSeconds = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(0, 2), ModbusClient.RegisterOrder.HighLow);
var sampleTimeConverted = new DateTime(1970, 1, 1).AddSeconds(sampleTimeInSeconds);
var sampleStatus = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(6, 2), ModbusClient.RegisterOrder.HighLow);
var location = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(4, 2), ModbusClient.RegisterOrder.HighLow);
var particleChannel1Count = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(8, 2), ModbusClient.RegisterOrder.HighLow);
var particleChannel2Count = EasyModbus.ModbusClient.ConvertRegistersToInt(e.ReadInputRegisters(10, 2), ModbusClient.RegisterOrder.HighLow);

ModBusClientTester.DataRecord record = new ModBusClientTester.DataRecord()
{
    SampleTime = sampleTimeConverted.ToString(),
    Location = location,
    SampleStatus = sampleStatus,
    ParticalChannel1Count = particleChannel1Count,
    ParticalChannel2Count = particleChannel2Count
};

Console.WriteLine($"Sample time:" +
    $"{record.SampleTime} \n" +
    $"Location: {record.Location} \n" +
    $"Sample Status: " +
    $"{record.SampleStatus} \n" +
    $"Particle channel 1 Count: {record.ParticalChannel1Count} \n" +
    $"Particle channel 2 Count {record.ParticalChannel2Count}");

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
int[] readHoldingRegisters = e.ReadInputRegisters(44, 2);  //Read 10 Holding Registers from Server, starting with Address 1

int[] newest = { readHoldingRegisters[1], readHoldingRegisters[0] };

//dt = dt.AddSeconds(-dt.Second);

var tester2 = EasyModbus.ModbusClient.ConvertRegistersToInt(readHoldingRegisters, ModbusClient.RegisterOrder.HighLow);

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


e.Disconnect();                                                //Disconnect from Server
Console.Write("Press any key to continue . . . ");
Console.ReadKey(true);