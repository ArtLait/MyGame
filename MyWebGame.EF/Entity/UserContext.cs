﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace MyWebGam.EF.Entity
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }
        public DbSet<User> Users { get; set; }

    }
    public class UserRepository : IDisposable
    {
        private UserContext db = new UserContext();
        public void Save(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }
        //public IEnumerable<User> List()
        //{

        //}
        public User Get(int id)
        {
            return db.Users.Find(id);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
