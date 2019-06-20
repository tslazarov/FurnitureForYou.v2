using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FFY.Models
{
    public class Address
    {
        private ICollection<Order> orders;
        public Address()
        {
            this.Orders = new HashSet<Order>();
        }

        public Address(string street, string city, string country) : this()
        {
            this.Street = street;
            this.City = city;
            this.Country = country;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Street { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Country { get; set; }

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
    }
}