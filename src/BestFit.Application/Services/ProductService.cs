using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            var productList = unitOfWork.ProductRepository.GetAll();
            return productList;
        }
        public Product CreateProduct(Product product)
        {
            unitOfWork.ProductRepository.Add(product);
            unitOfWork.Save();
            return product;
        }
        public Product GetProductById(Guid id)
        {
            var product = unitOfWork.ProductRepository.GetFirstOrDefault(x => x.Id == id);
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
    }
}
