using GettingOffBoxData.Client;
using System;

namespace GettingOffBoxData
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadCom readCom = new ReadCom("localhost", 10000);
            readCom.StartListening();
            readCom.PrintAllReadings();
            Console.ReadKey();
        }
    }
}
