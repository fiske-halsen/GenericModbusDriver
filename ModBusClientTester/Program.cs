// See https://aka.ms/new-console-template for more information
using EasyModbus;
using ModBusClientTester;
using System.Net;
using System.Net.Sockets;
using System.Text;


ModbusClient e = new ModbusClient();

e.Connect("192.168.0.17", 502);


// ------------------------- Write to registers --------------------------
// Register col is col -1 
//e.WriteSingleRegister(1, 12);
//e.WriteSingleRegister(24, -1);

// 3 for enable alarms, 1 for disable
//e.WriteSingleRegister(3009, 1);
//e.WriteSingleRegister(3011, 1);

//e.WriteSingleRegister(30, 20638);
//e.WriteSingleRegister(31, 20638);

//bool[] readCoils = e.ReadCoils(30, 1);

// -------------------------- Holding registers -------------------- 
int[] readHoldingRegisters = e.ReadInputRegisters(8, 2);  //Read 10 Holding Registers from Server, starting with Address 1

int[] newest = { readHoldingRegisters[1], readHoldingRegisters[0]};

//dt = dt.AddSeconds(-dt.Second);

var tester = Helpers.ConvertRegistersToInt(newest);

Console.WriteLine(tester);

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









































