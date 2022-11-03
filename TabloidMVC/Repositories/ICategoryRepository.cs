using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void DeleteCategory(int id);
        Category GetCategoryById(int id);

        void UpdateCategory(Category category);
        //Inserting AddCategory into Irepository
        void AddCategory(Category category);
    }
}