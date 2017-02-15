using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyWebGam.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DbConnection")
        { }
        public DbSet<User> Users { get; set; }   
        public DbSet<UserForConfirmedEmail> UserForConfirmedEmails { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
    }
    
}
