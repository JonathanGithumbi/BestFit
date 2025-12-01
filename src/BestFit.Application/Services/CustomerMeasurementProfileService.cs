using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class CustomerMeasurementProfileService
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerMeasurementProfileService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<CustomerMeasurementProfile> GetAllCustomerMeasurementProfiles()
        {
            var profileList = unitOfWork.CustomerMeasurementProfileRepository.GetAll();
            return profileList;
        }

        public CustomerMeasurementProfile CreateCustomerMeasurementProfile(CustomerMeasurementProfile profile)
        {
            unitOfWork.CustomerMeasurementProfileRepository.Add(profile);
            unitOfWork.Save();
            return profile;
        }

        public CustomerMeasurementProfile GetCustomerMeasurementProfileById(Guid id)
        {
            var profile = unitOfWork.CustomerMeasurementProfileRepository.GetFirstOrDefault(x => x.Id == id);
            return profile;
        }

        public CustomerMeasurementProfile UpdateCustomerMeasurementProfile(CustomerMeasurementProfile profile)
        {
            var existingCustomerMeasurementProfile = GetCustomerMeasurementProfileById(profile.Id);

            unitOfWork.CustomerMeasurementProfileRepository.Update(profile);
            unitOfWork.Save();
            return profile;
        }

        public bool DeleteCustomerMeasurementProfile(Guid id)
        {
            var profile = unitOfWork.CustomerMeasurementProfileRepository.GetFirstOrDefault(x => x.Id == id);

            if (profile != null) 
            {
                unitOfWork.CustomerMeasurementProfileRepository.Remove(profile);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

    }
}
 