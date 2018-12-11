using GettingOffBoxData.Client;
using GettingOffBoxData.DataAccess;
using GettingOffBoxData.Repository;
using System;
using System.Data.Entity;

namespace GettingOffBoxData.Client
{
    public class ReadCom
    {
        private readonly Context _context;
        private readonly string _host;
        private readonly int _port;
        private IReadRepository _readRepository;

        public ReadCom(string host, int port)
        {
            Database.SetInitializer(new Initializer());            
            _context = new Context();
            _readRepository = new ReadRepository(_context);
            _host = host;
            _port = port;
        }

        public void StartListening()
        {
            ReadsListener readsListener = new ReadsListener(_host, _port, _readRepository);
        }

        public void PrintAllReadings()
        {
            foreach(var read in _readRepository.Reads)
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", read.ID, read.UniqueID, read.TagID, read.TimeStamp, read.ReaderNo, read.AntennaID, read.IPAdress);            
        }
    }
}
