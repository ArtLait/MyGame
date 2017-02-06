using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebGam.EF.Entity
{
    public class User
    {        
        public int Id { get; set; }        
 
        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string UniqueKey { get; set; }
   
        public string Status { get; set; }       
       
        public DateTime Date { get; set; }
    }
}
