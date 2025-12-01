using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categoryList = unitOfWork.CategoryRepository.GetAll();
            return categoryList;
        }

        public Category CreateCategory(Category category)
        {
            unitOfWork.CategoryRepository.Add(category);
            unitOfWork.Save();
            return category;
        }
        public Category GetCategoryById(Guid id)
        {
            var category = unitOfWork.CategoryRepository.GetFirstOrDefault(x => x.Id == id);
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.Id);

            unitOfWork.CategoryRepository.Update(category);
            unitOfWork.Save();
            return category;
        }
        public bool DeleteCategory(Guid id)
        {
            var category = unitOfWork.CategoryRepository.GetFirstOrDefault(x=>x.Id == id);

            if (category != null)
            {
                unitOfWork.CategoryRepository.Remove(category);
                unitOfWork.Save();
                return true;    
            }
            return false;
        }
    }
}
