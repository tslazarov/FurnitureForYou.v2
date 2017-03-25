using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FFY.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IFFYData data;

        public CategoriesService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }


        public void AddCategory(Category category)
        {
            Guard.WhenArgument<Category>(category, "Category cannot be null.")
                .IsNull()
                .Throw();

            this.data.CategoriesRepository.Add(category);
            this.data.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return this.data.CategoriesRepository.GetById(id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.data.CategoriesRepository.All().ToList();
        }
    }
}
