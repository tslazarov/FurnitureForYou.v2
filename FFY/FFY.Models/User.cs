using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FFY.Models
{
    public class User : IdentityUser
    {
        private ICollection<Order> orders;
        private ICollection<Product> ratedProducts;
        private ICollection<Product> favoritedProducts;

        public User()
        {
            this.Orders = new HashSet<Order>();
            this.ratedProducts = new HashSet<Product>();
            this.favoritedProducts = new HashSet<Product>();

        }

        public User(string username,
            string firstName,
            string lastName,
            string email) : this()
        {
            this.UserName = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public override string UserName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string LastName { get; set; }

        public virtual ICollection<Order> Orders
        {
            get
            {
                return this.orders;
            }
            set
            {
                this.orders = value;
            }
        }

        public virtual ICollection<Product> RatedProducts
        {
            get
            {
                return this.ratedProducts;
            }
            set
            {
                this.ratedProducts = value;
            }
        }

        public virtual ICollection<Product> FavoritedProducts
        {
            get
            {
                return this.favoritedProducts;
            }
            set
            {
                this.favoritedProducts = value;
            }
        }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
