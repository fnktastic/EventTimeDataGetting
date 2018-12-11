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
                AntennaID = "2",
                IPAdress = "127.0.0.1",
                ReaderNo = "1",
                TagID = "TAG_12",
                UniqueID = Guid.NewGuid(),
            };


            while (true)
            {
                Thread.Sleep(200);
                read.TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                read.ID++;
                read.UniqueID = Guid.NewGuid();
                InitServer.OnTagRead(read);
            }
        }
    }

}
