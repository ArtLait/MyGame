﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyWebGam.EF
{
    public class UserRepository : IDisposable
    {
        private DatabaseContext db;
        public UserRepository()
        {
            db = new DatabaseContext();
        }
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
        public User GetAuthorizateUser(string name, string PasswordHash)
        {
            return db.Users.FirstOrDefault(u => u.Name == name && u.PasswordHash == PasswordHash);
        }
        public string GetUserWithEmail(string email)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return user.Name;
            }
            return null;
        }
        public bool CheckEmailUniqueness(string email)
        {
            if (db.Users.FirstOrDefault(u => u.Email == email) != null)
                return false;
            else 
                return true;
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
