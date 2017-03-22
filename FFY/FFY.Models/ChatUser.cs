using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Models
{
    public class ChatUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Role { get; set; }
    }
}
