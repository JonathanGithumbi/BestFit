using BestFit.Application.ViewModels;
using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BestFit.Application.Services.AdminServices
{
    public class ProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private string _wwwRootPath;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ProductViewModel GetProductViewModel(Guid? id)
        {
            var productVM = new ProductViewModel
            {
                Product = new Product(),
                CategoryList = unitOfWork.CategoryRepository.GetAll().Select(l => new SelectListItem { Text = l.Name, Value = l.Id.ToString() })
            };

            if (id != null)
            {
                productVM.Product = unitOfWork.ProductRepository.GetFirstOrDefault(x => x.Id == id);
            }

            return productVM;
        }

        public void UpsertProduct(ProductViewModel productViewModel, IFormFile formFile)
        {
            if (formFile != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploadRoot = Path.Combine(_wwwRootPath, "img", "products");
                var extension = Path.GetExtension(formFile.FileName);

                if (!string.IsNullOrEmpty(productViewModel.Product.Picture))
                {
                    var oldPicPath = Path.Combine(_wwwRootPath, productViewModel.Product.Picture);
                    if (File.Exists(oldPicPath))
                    {
                        File.Delete(oldPicPath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(uploadRoot, fileName + extension), FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

                productViewModel.Product.Picture = Path.Combine("img", "products", fileName + extension);
            }

            if (productViewModel.Product.Id == null)
            {
                unitOfWork.ProductRepository.Add(productViewModel.Product);
            }
            else
            {
                unitOfWork.ProductRepository.Update(productViewModel.Product);
            }
            unitOfWork.Save();
        }


        public IEnumerable<Product> GetAllCategories()
        {
            var productList = unitOfWork.ProductRepository.GetAll();
            return productList;
        }

        public void CreateProduct(Product product)
        {
            unitOfWork.ProductRepository.Add(product);
            unitOfWork.Save();
        }
        public Product GetProductById(Guid id)
        {
            var product = unitOfWork.ProductRepository.GetFirstOrDefault(x => x.Id == id);
            return product;
        }

        public void UpdateProduct(Product product)
        {
            unitOfWork.ProductRepository.Update(product);
            unitOfWork.Save();
        }
        public void DeleteProduct(Guid id)
        {
            var product = unitOfWork.ProductRepository.GetFirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                unitOfWork.ProductRepository.Remove(product);
                unitOfWork.Save();

            }

        }

       
    }
}
