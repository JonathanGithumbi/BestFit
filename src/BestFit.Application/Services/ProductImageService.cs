using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class ProductImageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;
        private string wwwRootPath;

        public ProductImageService(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor,IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
            this.wwwRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        public IEnumerable<ProductImage> GetAllProductImage()
        {
            var productImageList = unitOfWork.ProductImageRepository.GetAll();
            return productImageList;
        }
        public bool CreateProductImage(ProductImage productImageRequest)
        {
            
            
            var files = productImageRequest.Files;
            foreach (var file in files)
            {
                var productImage = new ProductImage
                {
                    ProductId = productImageRequest.ProductId
                };
                if (file != null)
                {
                    //New ProductImage entity 
                    

                    string fileName = file.FileName;
                    var uploadRoot = Path.Combine(wwwRootPath, "assets", "images");
                    var extension = Path.GetExtension(file.FileName);


                    if (!string.IsNullOrEmpty(productImage.ImageURL))
                    {
                        var oldPicPath = Path.Combine(wwwRootPath, productImage.ImageURL);
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
                    productImage.ImageURL = urlFilePath;
                }
                if (productImage.Id == null || productImage.Id <= Guid.Empty)
                {
                    unitOfWork.ProductImageRepository.Add(productImage);

                }
                else
                {
                    unitOfWork.ProductImageRepository.Update(productImage);

                }

                unitOfWork.Save();

            }
            return true;
        }
        public ProductImage GetProductImageById(Guid id)
        {
            var productImage = unitOfWork.ProductImageRepository.GetFirstOrDefault(x => x.Id == id);
            return productImage;
        }
        public ProductImage UpdateProductImage(ProductImage productImage)
        {
            var existingProductImage = GetProductImageById(productImage.Id);

            unitOfWork.ProductImageRepository.Update(productImage);
            unitOfWork.Save();
            return productImage;
        }

        public bool DeleteProductImage(Guid id)
        {
            var productImage = unitOfWork.ProductImageRepository.GetFirstOrDefault(x => x.Id == id);

            if (productImage != null)
            {
                unitOfWork.ProductImageRepository.Remove(productImage);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
