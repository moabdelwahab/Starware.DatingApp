using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.DTOs
{
    public class LoginDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(15)]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(4)]
        [MaxLength(12)]
        public string Password { get; set; }

    }
}
