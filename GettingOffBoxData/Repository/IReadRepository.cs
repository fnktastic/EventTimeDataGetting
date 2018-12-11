using GettingOffBoxData.Model;
using System.Collections.Generic;

namespace GettingOffBoxData.Repository
{
    public interface IReadRepository
    {
        IEnumerable<Read> Reads { get; }
        void SaveRead(Read read);
    }
}
