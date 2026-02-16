using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using BestFit.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class FeaturedContentService
    {
        private readonly IUnitOfWork unitOfWork;

        public FeaturedContentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<FeaturedContent> GetAllFeaturedContents()
        {
            var featuredContents = unitOfWork.FeaturedContentRepository.GetAll();
            return featuredContents;
        }
        public FeaturedContent CreateFeaturedContent(FeaturedContent featuredContent)
        {
            unitOfWork.FeaturedContentRepository.Add(featuredContent);
            unitOfWork.Save();
            return featuredContent;

        }
        public FeaturedContent GetFeaturedContentById(Guid id)
        {
            var featuredContent = unitOfWork.FeaturedContentRepository.GetFirstOrDefault(x => x.Id == id);
            return featuredContent;
        }
        public FeaturedContent UpdateFeaturedContent(FeaturedContent featuredContent)
        {
            var existingFeaturedContent = unitOfWork.FeaturedContentRepository.GetFirstOrDefault(x=>x.Id == featuredContent.Id);

            unitOfWork.FeaturedContentRepository.Update(existingFeaturedContent);
            unitOfWork.Save();
            return existingFeaturedContent;
        }
        public bool DeleteFeaturedContent(Guid id)
        {
            var featuredContent = unitOfWork.FeaturedContentRepository.GetFirstOrDefault(x => x.Id == id);

            if (featuredContent != null)
            {
                unitOfWork.FeaturedContentRepository.Remove(featuredContent);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public FeaturedContent GetTodayContent(DateTime today)
        {
            var featuredContent = unitOfWork.FeaturedContentRepository.GetFirstOrDefault(x => x.RunFromDate > today && today < x.RunToDate);

            return featuredContent;

        }
    }
}
