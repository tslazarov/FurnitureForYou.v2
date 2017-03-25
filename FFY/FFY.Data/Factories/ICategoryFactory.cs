using FFY.Models;

namespace FFY.Data.Factories
{
    public interface ICategoryFactory
    {
        Category CreateCategory(string name, string imagePath);
    }
}
