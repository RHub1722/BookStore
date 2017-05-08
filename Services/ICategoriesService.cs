using System.Collections.Generic;
using Shared.DTOS;

namespace Services
{
    public interface ICategoriesService
    {
        void CreateCategory(CategoryDto item);
        void DeleteCategory(int id);
        CategoryDto GetCategory(int id);
        List<CategoryDto> GetAllCategories();
        void UpdateCategory(int id, CategoryDto item);
    }
}