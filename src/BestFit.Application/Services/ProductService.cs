using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;
        private string wwwRootPath;

        public ProductService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
            this.wwwRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        public IEnumerable<Product> GetAllProduct(string? filterOn=null,string?filterQuery=null,string?sortBy = null,
            bool isAscending=true,int pageNumber=1,int pageSize=10,double fromPrice =0,double toPrice =10000,string? includeProperties=null)
        {
            
            var productList = unitOfWork.ProductRepository.GetAll(filterOn,filterQuery, sortBy ,  isAscending ,pageNumber,pageSize,fromPrice,toPrice,includeProperties=includeProperties);
            return productList;
        }
        public Product CreateProduct(Product product)
        {
            var file = product.File;
            if (file != null)
            {
                string fileName = file.FileName;
                var uploadRoot = Path.Combine(wwwRootPath, "assets", "images");
                var extension = Path.GetExtension(file.FileName);


                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldPicPath = Path.Combine(wwwRootPath, product.ImageUrl);
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
                product.ImageUrl = urlFilePath;
            }
            if (product.Id == null || product.Id <= Guid.Empty)
            {
                unitOfWork.ProductRepository.Add(product);

            }
            else
            {
                unitOfWork.ProductRepository.Update(product);

            }
            unitOfWork.ProductRepository.Add(product);
            unitOfWork.Save();
            return product;
        }
        public Product GetProductById(Guid id,string? includeProperties=null)
        {
            var product = unitOfWork.ProductRepository.GetFirstOrDefault(x => x.Id == id,includeProperties);
            return product;
        }
        public Product UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);

            unitOfWork.ProductRepository.Update(product);
            unitOfWork.Save();
            return product;
        }

        public bool DeleteProduct(Guid id)
        {
            var product = unitOfWork.ProductRepository.GetFirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                unitOfWork.ProductRepository.Remove(product);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public Product GetSingleProduct(Expression<Func<Product, bool>> filter)
        {
            var product = unitOfWork.ProductRepository.GetFirstOrDefault(filter);
            return product; 
        }
    }
}
