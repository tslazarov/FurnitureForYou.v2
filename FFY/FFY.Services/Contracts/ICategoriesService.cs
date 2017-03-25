using FFY.Models;
using System.Collections.Generic;

namespace FFY.Services.Contracts
{
    public interface ICategoriesService
    {
        void AddCategory(Category category);

        Category GetCategoryById(int id);

        IEnumerable<Category> GetCategories();

    }
}
