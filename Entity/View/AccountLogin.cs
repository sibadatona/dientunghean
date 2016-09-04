using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entity.View
{
    public class AccountLogin
    {
        [Required]
        [Key]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
