using FFY.Models;

namespace FFY.Data.Factories
{
    public interface IProductFactory
    {
        Product CreateProduct(string name,
            int quantity,
            decimal price,
            decimal discountedPrice,
            int discountPercentage,
            bool hasDiscount,
            string description,
            int categoryId,
            Category category,
            int roomId,
            Room room,
            string imagePath,
            bool isDeleted = false);
    }
}
