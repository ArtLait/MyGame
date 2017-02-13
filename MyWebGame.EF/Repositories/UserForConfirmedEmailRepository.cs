using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyWebGam.EF
{
    public class UserForConfirmedEmailRepository : IDisposable
    {
        private UserContext db;
        public UserForConfirmedEmailRepository(){

            db = new UserContext();
        }
        public void Save(UserForConfirmedEmail u)
        {
            db.UserForConfirmedEmails.Add(u);
            db.SaveChanges();
        }
        public UserForConfirmedEmail Get(int id)
        {
            return db.UserForConfirmedEmails.Find(id);
        }
        public UserForConfirmedEmail CheckKey(string key)
        {
            return db.UserForConfirmedEmails.FirstOrDefault(u => u.Key == key);
        }
        public void SetUserConfirmed(int id)
        {
            db.Users.Find(id).Confirmed = true;
            db.SaveChanges();
        }
        public void DeleteKey(int id)
        {
            UserForConfirmedEmail user = db.UserForConfirmedEmails.FirstOrDefault(u => u.Id == id);
           // if (user != null)
            //{                
                db.UserForConfirmedEmails.Remove(user);
            //}
            db.SaveChanges();
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
