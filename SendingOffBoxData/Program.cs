using GettingOffBoxData.Model;
using System;
using System.Globalization;
using System.Threading;

namespace SendingOffBoxData
{
    class Program
    {
        static void Main(string[] args)
        {
            InitServer.Init();
            InitServer.StartReading();

            Read read = new Read()
            {
                ID = 1,
                AntennaNumber = "2",
                IpAddress = "127.0.0.1",
                ReaderNumber = "1",
                EPC = "TAG_12",
                UniqueReadingID = Guid.NewGuid().ToString(),
                PeakRssiInDbm = "-11dBm",
            };

            Console.ReadKey();
            Console.WriteLine("LET'S GO!");

            while (true)
            {
                Thread.Sleep(1000);
                read.Time = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                read.ID++;
                read.UniqueReadingID = Guid.NewGuid().ToString();
                InitServer.OnTagRead(read);
            }
        }
    }

}
