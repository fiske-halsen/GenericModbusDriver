using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusClientTester
{
    public class Helpers
    {
        public static Int32 ConvertRegistersToInt(int[] registers)
        {
            if (registers.Length != 2)
                throw new ArgumentException("Input Array length invalid - Array langth must be '2'");
            int highRegister = registers[1];
            int lowRegister = registers[0];
            byte[] highRegisterBytes = BitConverter.GetBytes(highRegister);
            byte[] lowRegisterBytes = BitConverter.GetBytes(lowRegister);
            byte[] doubleBytes = {
                                    lowRegisterBytes[0],
                                    lowRegisterBytes[1],
                                    highRegisterBytes[0],
                                    highRegisterBytes[1]
                                };
            return BitConverter.ToInt32(doubleBytes, 0);
        }
        public static string ConvertRegistersToString(int[] registers, int offset, int stringLength)
        {
            byte[] result = new byte[stringLength];
            byte[] registerResult = new byte[2];

            for (int i = 0; i < stringLength / 2; i++)
            {
                registerResult = BitConverter.GetBytes(registers[offset + i]);
                result[i * 2] = registerResult[0];
                result[i * 2 + 1] = registerResult[1];
            }
            return System.Text.Encoding.Default.GetString(result);
        }
    }
}
