using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Models
{
    public class DatabaseContext: DbContext
    {
        public DbSet<CollectionEmail> Emails { get; set; }
        public DbSet<CustomFolder> Folders { get; set; }
    }
}
