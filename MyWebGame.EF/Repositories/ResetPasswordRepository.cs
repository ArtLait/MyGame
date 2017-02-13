using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebGam.EF
{
    class ResetPasswordRepository
    {
        UserContext db;
        public ResetPasswordRepository()
        {
            db = new UserContext();
        }
        public ResetPassword Get(int id)
        {
            return new ResetPassword();
        }
    }
}
