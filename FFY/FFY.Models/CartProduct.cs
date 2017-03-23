using System.ComponentModel.DataAnnotations;

namespace FFY.Models
{
    public class CartProduct
    {
        public CartProduct()
        {

        }

        public CartProduct(int quantity, 
            Product product,
            bool isInCart = true,
            bool isOutOfStock = false) : this()
        {
            this.Quantity = quantity;
            this.Product = product;
            this.IsInCart = isInCart;
            this.IsOutOfStock = isOutOfStock;
        }

        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public bool IsInCart { get; set; }

        public bool IsOutOfStock { get; set; }
    }
}