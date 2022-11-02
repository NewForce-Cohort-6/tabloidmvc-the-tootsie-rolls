using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        //Inserting AddCategory into Irepository
        void AddCategory(Category category);
    }
}