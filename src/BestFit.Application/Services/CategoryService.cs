using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CategoryService> logger;

        public CategoryService(IUnitOfWork unitOfWork,ILogger<CategoryService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            logger.LogInformation(@"...
                                    Processing Get all Categories Request
                                    ");
            var categoryList = unitOfWork.CategoryRepository.GetAll();
            logger.LogInformation(@"...
                                    Succsessfully processed get all categories request
                                    ");
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
