using System;
using System.IO;
namespace Lab_6
{
    internal class Program
    {
        private const ushort Polynomial = 0x1021;
        private const ushort InitialValue = 0xFFFF;

        public static ushort ComputeChecksum(byte[] data)
        {
            ushort crc = InitialValue;

            foreach (byte b in data)
            {
                crc ^= (ushort)(b << 8); 

                for (int i = 0; i < 8; i++) 
                {
                    if ((crc & 0x8000) != 0) 
                    {
                        crc = (ushort)((crc << 1) ^ Polynomial);
                    }
                    else
                    {
                        crc <<= 1; 
                    }
                }
            }

            return crc;
        }

        public static ushort ComputeChecksumFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не знайдено.");
            }

            byte[] fileData = File.ReadAllBytes(filePath); 
            return ComputeChecksum(fileData); 
        }

        public static void Main()
        {
            Console.Write("Enter file path: ");
            string filePath = Console.ReadLine();

            try
            {
                ushort checksum = ComputeChecksumFromFile(filePath);
                Console.WriteLine($"CRC-16 CCITT for file: {checksum:X4}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eror: {ex.Message}");
            }
        }
    }
}