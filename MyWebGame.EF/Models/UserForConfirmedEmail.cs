using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebGam.EF
{
    public class UserForConfirmedEmail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Key { get; set; }
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
