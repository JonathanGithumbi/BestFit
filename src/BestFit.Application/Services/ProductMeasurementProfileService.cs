using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class ProductMeasurementProfileService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductMeasurementProfileService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductMeasurementProfile> GetAllProductMeasurementProfile()
        {
            var productMeasurementProfileList = unitOfWork.ProductMeasurementProfileRepository.GetAll();
            return productMeasurementProfileList;
        }
        public ProductMeasurementProfile CreateProductMeasurementProfile(ProductMeasurementProfile productMeasurementProfile)
        {
            unitOfWork.ProductMeasurementProfileRepository.Add(productMeasurementProfile);
            unitOfWork.Save(); 
            return productMeasurementProfile;
        }
        public ProductMeasurementProfile GetProductMeasurementProfileById(Guid id)
        {
            var productMeasurementProfile = unitOfWork.ProductMeasurementProfileRepository.GetFirstOrDefault(x => x.Id == id);
            return productMeasurementProfile;
        }
        public ProductMeasurementProfile UpdateProductMeasurementProfile(ProductMeasurementProfile productMeasurementProfile)
        {
            var existingProductMeasurementProfile = GetProductMeasurementProfileById(productMeasurementProfile.Id);

            unitOfWork.ProductMeasurementProfileRepository.Update(productMeasurementProfile);
            unitOfWork.Save();
            return productMeasurementProfile;
        }

        public bool DeleteProductMeasurementProfile(Guid id)
        {
            var productMeasurementProfile = unitOfWork.ProductMeasurementProfileRepository.GetFirstOrDefault(x => x.Id == id);

            if (productMeasurementProfile != null)
            {
                unitOfWork.ProductMeasurementProfileRepository.Remove(productMeasurementProfile);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
