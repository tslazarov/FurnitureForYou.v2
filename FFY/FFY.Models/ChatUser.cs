using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFY.Models
{
    public class ChatUser
    {
        public ChatUser()
        {

        }

        public ChatUser(string email, string role) : this()
        {
            this.Email = email;
            this.Role = role;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        public string Role { get; set; }
    }
}
