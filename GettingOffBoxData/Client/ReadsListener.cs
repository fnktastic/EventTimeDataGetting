using GettingOffBoxData.Model;
using GettingOffBoxData.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GettingOffBoxData.Client
{
    public class ReadsListener
    {
        private readonly TcpClient _tcpClient;
        private readonly IReadRepository _readRepository;

        private readonly string _host;
        private readonly int _port;
        private readonly List<string> _readLines;

        public ReadsListener(string host, int port, IReadRepository readRepository)
        {
            _readRepository = readRepository;
            _readLines = new List<string>();
            _host = host;
            _port = port;
            try
            {
                _tcpClient = new TcpClient(_host, _port);
            }
            catch (SocketException)
            {
                Console.WriteLine("Unable to connect to server");
                return;
            }

            StartReading();

        }

        private void CheckStringForReadings(string stringToCheck)
        {
            var readings = stringToCheck.Split('@');
            foreach (var reading in readings)
                SaveRead(MappRead(reading));

        }

        private Read MappRead(string stringToMap)
        {


            var array = stringToMap.Split('#');
            if (array.Length == 7)
            {
                Console.WriteLine(stringToMap);
                return new Read()
                {
                    ID = 0,
                    UniqueID = Guid.Parse(array[1]),
                    TagID = array[2],
                    TimeStamp = array[3],
                    ReaderNo = array[4],
                    AntennaID = array[5],
                    IPAddress = array[6],
                };
            }

            return null;
        }

        private void SaveRead(Read read)
        {
            if (read != null)
                _readRepository.SaveRead(read);
        }

        private void StartReading()
        {
            byte[] data = new byte[10000];
            string stringData;
            int recvieveLength;

            NetworkStream ns = _tcpClient.GetStream();

            while (true)
            {
                ns.Flush();
                data = new byte[10000];
                recvieveLength = ns.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recvieveLength);
                _readLines.Add(stringData);
                CheckStringForReadings(stringData);
            }
        }
    }
}
