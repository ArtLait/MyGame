using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebGam.EF
{
     public class ResetPasswordRepository
     {
         DatabaseContext db;
        public ResetPasswordRepository()
        {
            db = new DatabaseContext();
        }
        public ResetPassword Get(int id)
        {
            return new ResetPassword();
        }
        public void Save(ResetPassword model)
        {
            db.ResetPasswords.Add(model);
            db.SaveChanges();
        }
        public ResetPassword CheckKey(string key)
        {          
            return db.ResetPasswords.FirstOrDefault(t => t.Key == key);
        }
         public void UpdatePassword(string Email, string newPassword)
         {
            User model =  db.Users.FirstOrDefault(t => t.Email == Email);
            if (model != null)
            {
                model.PasswordHash = newPassword;
                db.SaveChanges();
            }
         }
         public void DeleteResetKey(string email)
         {
             ResetPassword user = db.ResetPasswords.FirstOrDefault(u => u.Email == email);
             if (user != null)
             {
                 db.ResetPasswords.Remove(user);
                 db.SaveChanges();
             }         
         }
    }
}
