using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebGam.EF
{
    public class ResetPassword
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Key { get; set; }
    }
}
