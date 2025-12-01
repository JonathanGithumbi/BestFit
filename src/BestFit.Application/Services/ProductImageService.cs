using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
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

        public ProductImageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductImage> GetAllProductImage()
        {
            var productImageList = unitOfWork.ProductImageRepository.GetAll();
            return productImageList;
        }
        public ProductImage CreateProductImage(ProductImage productImage)
        {
            unitOfWork.ProductImageRepository.Add(productImage);
            unitOfWork.Save();
            return productImage;
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
