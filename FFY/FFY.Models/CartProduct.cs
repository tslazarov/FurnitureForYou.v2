using System.ComponentModel.DataAnnotations;

namespace FFY.Models
{
    public class CartProduct
    {
        public CartProduct()
        {

        }

        public CartProduct(int quantity, Product product) : this()
        {
            this.Quantity = quantity;
            this.Product = product;
        }

        public CartProduct(int quantity,
            int? productId,
            Product product,
            decimal total) : this()
        {
            this.Quantity = quantity;
            this.ProductId = productId;
            this.Product = product;
            this.Total = total;
        }

        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}