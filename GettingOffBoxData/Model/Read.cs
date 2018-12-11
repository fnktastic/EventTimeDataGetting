using System;

namespace GettingOffBoxData.Model
{
    public class Read
    {
        public int ID { get; set; }
        public Guid UniqueID { get; set; }
        public string TagID { get; set; }
        public string TimeStamp { get; set; }
        public string ReaderNo { get; set; }
        public string AntennaID { get; set; }
        public string IPAdress { get; set; }
    }
}
