using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebGam.EF
{
    public class User
    {      
        public User(){}
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash {get; set;}
        public string UniqueKey { get; set; } 
        public string Status { get; set; }
        public bool Confirmed { get; set; }      
        public DateTime Date { get; set; }
        public virtual UserForConfirmedEmail UserForConfirmedEmail { get; set; }
   }

}
