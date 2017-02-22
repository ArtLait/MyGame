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
        public ResetPassword getUserOnKey(string key)
        {
           return db.ResetPasswords.FirstOrDefault(t => t.Key == key);
        }
        public void UpdateWithEmail(string  email, string newEmail)
        {
            User user = db.Users.FirstOrDefault(t => t.Email == email);
            if (user != null)
            {
                user.PasswordHash = newEmail;
                db.SaveChanges();
            }
        }
         public void UpdatePasswordWithKey(string key, string newPassword)
         {
            ResetPassword user = db.ResetPasswords.FirstOrDefault(t => t.Key == key);
            if (user != null)
            {
                User model = db.Users.FirstOrDefault(t => t.Id == user.UserId);
                if (model != null)
                {
                    model.PasswordHash = newPassword;
                    db.SaveChanges();
                }
            }
         }
         public void DeleteResetKey(string key)
         {
             ResetPassword user = db.ResetPasswords.FirstOrDefault(u => u.Key == key);
             if (user != null)
             {
                 db.ResetPasswords.Remove(user);
                 db.SaveChanges();
             }         
         }
    }
}
