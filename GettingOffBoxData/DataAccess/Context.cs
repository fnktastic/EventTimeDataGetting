using GettingOffBoxData.Model;
using System.Data.Entity;

namespace GettingOffBoxData.DataAccess
{
    public class Context : DbContext
    {
        public Context() : base("reads") { }

        public DbSet<Read> Reads { get; set; }
    }

    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        public Initializer()
        {
            using (var context = new Context())
            {
                InitializeDatabase(context);
            }
        }
    }
}
