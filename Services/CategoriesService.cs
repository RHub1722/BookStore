using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entities;
using Entities.Entities;
using Repository.Interfaces;
using Shared.DTOS;
using System.Linq;

namespace Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _categories;

        public CategoriesService(IMapper mapper, IRepository<Category> categories)
        {
            _mapper = mapper;
            _categories = categories;
        }

        

        public void CreateCategory(CategoryDto item)
        {
            item.Id = 0;
            var map = _mapper.Map<Category>(item);
            _categories.Insert(map);
            _categories.Save();
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categoryDtos = new List<CategoryDto>();
            _categories.Queryable().ToList().ForEach(x => categoryDtos.Add(_mapper.Map<CategoryDto>(x)));
            if (!categoryDtos.Any())
                return null;
            return categoryDtos;
        }

        public CategoryDto GetCategory(int id)
        {
            var resp = _categories.Queryable().FirstOrDefault(x => x.Id == id);
            if (resp == null)
                return null;

            return _mapper.Map<CategoryDto>(resp);
        }

        public void UpdateCategory(int id, CategoryDto item)
        {
            var resp = _categories.Queryable().FirstOrDefault(x => x.Id == id);
            if (resp == null)
                return;
            resp.Name = item.Name;
            resp.ObjectState = ObjectState.Modified;
            _categories.Save();
        }

        public void DeleteCategory(int id)
        {
            var resp = _categories.Queryable().FirstOrDefault(x => x.Id == id);
            if (resp == null)
                return;
            _categories.Delete(resp);
            _categories.Save();
        }
    }
}
