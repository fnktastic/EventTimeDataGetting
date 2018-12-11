using GettingOffBoxData.Model;

namespace SendingOffBoxData
{
    public static class InitServer
    {
        private static Server _server;
        private const int DEFAULT_PORT = 10000;

        public static void Init()
        {
            try
            {
                _server = new Server();
            }
            catch
            {
               _server.Dispose();
            }
       
        }

        public static void StartReading()
        {
            try
            {
                _server.Listen(DEFAULT_PORT);
            }
            catch
            {
                _server.Dispose();
            }
        }

        public static void OnTagRead(Read read)
        {
            _server.OnTagRead(read);
        }
    }

}
