using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CategoryService> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;
        private string wwwRootPath;

        public CategoryService(IUnitOfWork unitOfWork,ILogger<CategoryService> logger,IHttpContextAccessor httpContextAccessor,IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
            this.wwwRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        public IEnumerable<Category> GetAllCategories(Expression<Func<Category, bool>>? filter=null)
        {
            logger.LogInformation(@"...
                                    Processing Get all Categories Request
                                    ");
            var categoryList = unitOfWork.CategoryRepository.GetAll(filter);
            logger.LogInformation(@"...
                                    Succsessfully processed get all categories request
                                    ");
            return categoryList;

        }
        public Category GetSingleCategory(Expression<Func<Category, bool>>? filter)
        {
            
            var category = unitOfWork.CategoryRepository.GetFirstOrDefault(filter);

            return category;

        }


        public Category CreateCategory(Category category)
        {
            var file = category.File;
            if (file != null)
            {
                string fileName = file.FileName;
                var uploadRoot = Path.Combine(wwwRootPath, "assets", "images");
                var extension = Path.GetExtension(file.FileName);


                if (!string.IsNullOrEmpty(category.ImageUrl))
                {
                    var oldPicPath = Path.Combine(wwwRootPath, category.ImageUrl);
                    if (File.Exists(oldPicPath))
                    {
                        File.Delete(oldPicPath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(uploadRoot, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/wwwroot/images/{fileName}";
                category.ImageUrl = urlFilePath;
            }

            if (category.Id == null || category.Id <= Guid.Empty)
            {
                unitOfWork.CategoryRepository.Add(category);

            }
            else
            {
                unitOfWork.CategoryRepository.Update(category);

            }

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
