using GettingOffBoxData.Model;
using GettingOffBoxData.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GettingOffBoxData.Client
{
    public class ReadsListener
    {
        private readonly TcpClient _tcpClient;
        private readonly IReadRepository _readRepository;

        private readonly string _host;
        private readonly int _port;

        public ReadsListener(string host, int port, IReadRepository readRepository)
        {
            _host = host;
            _port = port;
            _tcpClient = new TcpClient(_host, _port);
            _readRepository = readRepository;
            StartReading();

        }

        private Read MappRead(string stringToMap)
        {
            var array = stringToMap.Split('#');
            if (array.Length == 7)
            {
                return new Read()
                {
                    ID = int.Parse(array[0]),
                    UniqueID = Guid.Parse(array[1]),
                    TagID = array[2],
                    TimeStamp = array[3], //DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(array[3])).UtcDateTime,
                    ReaderNo = array[4],
                    AntennaID = array[5],
                    IPAdress = array[6],
                };
            }

            return null;
        }

        private void SaveRead(Read read)
        {
            _readRepository.SaveRead(read);
        }

        private void StartReading()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string readFromServer = null;

            while (true)
            {
                try
                {
                    requestCount++;

                    NetworkStream stream = _tcpClient.GetStream();
                    stream.ReadTimeout = 4000;

                    int bufferSize = (int)_tcpClient.ReceiveBufferSize;
                    if (bufferSize > bytesFrom.Length)
                    {
                        bufferSize = bytesFrom.Length;
                    }

                    try
                    {
                        int bytesRead = stream.Read(bytesFrom, 0, bufferSize);
                        stream.Flush();

                        if (bytesRead == 0)
                        {
                            throw new System.IO.IOException("Connection seems to be refused or closed.");
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        byte[] ping = System.Text.Encoding.UTF8.GetBytes("%");
                        stream.WriteTimeout = 1;

                        stream.Write(ping, 0, ping.Length);
                        continue;
                    }
                    readFromServer = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    readFromServer = readFromServer.Replace("\0", "");
                    Console.WriteLine(readFromServer);

                    var read = MappRead(readFromServer);
                    if(read != null)
                        SaveRead(read);
                }
                catch (Exception ex) when (ex is ObjectDisposedException || ex is InvalidOperationException || ex is System.IO.IOException)
                {
                    Debug.WriteLine(ex.ToString());
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    break;
                }
            }
        }
    }
}
