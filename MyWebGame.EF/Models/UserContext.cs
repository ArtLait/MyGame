using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyWebGam.EF
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }
        public DbSet<User> Users { get; set; }   
        public DbSet<UserForConfirmedEmail> UserForConfirmedEmails { get; set; }      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserForConfirmedEmail>().HasOptional(u => u.User)
                .WithRequired(u => u.UserForConfirmedEmail);
            //modelBuilder.Entity<User>().HasOptional(u => u.UserForEmail)
            //    .WithRequired(u => u.User);
        }
    }
    
}
