using Impinj.OctaneSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendingOffBoxData
{
    public sealed class TagRead
    {
        public readonly string EPC;
        public ImpinjReader Reader;
        public Tag Tag;
        public readonly DateTime UTC = DateTime.UtcNow;

        public TagRead(ImpinjReader reader, string epc, Impinj.OctaneSdk.Tag tag)
        {
            this.Reader = reader;
            this.EPC = epc;
            this.Tag = tag;
        }
    }
}
