using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

//using BestFit.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;
        private string wwwRootPath;

        public FeaturedContentService(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor,IWebHostEnvironment env)
        {

            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
            this.wwwRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        public IEnumerable<FeaturedContent> GetAllFeaturedContents()
        {
            var featuredContents = unitOfWork.FeaturedContentRepository.GetAll();
            return featuredContents;
        }
        public FeaturedContent CreateFeaturedContent(FeaturedContent featuredContent,IFormFile file)
        {

            if(file!=null)
            {
                string fileName = file.FileName;
                var uploadRoot = Path.Combine(wwwRootPath, "assets", "images");
                var extension = Path.GetExtension(file.FileName);


                if (!string.IsNullOrEmpty(featuredContent.ImageUrl))
                {
                    var oldPicPath = Path.Combine(wwwRootPath,featuredContent.ImageUrl);
                    if(File.Exists(oldPicPath))
                    {
                        File.Delete(oldPicPath);
                    }
                }

                using(var fileStream = new FileStream(Path.Combine(uploadRoot,fileName),FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/wwwroot/images/{fileName}";
                featuredContent.ImageUrl = urlFilePath;
            }

            if (featuredContent.Id == null || featuredContent.Id <= Guid.Empty)
            {
                featuredContent.CreatedOn = DateTime.Now;
                unitOfWork.FeaturedContentRepository.Add(featuredContent);

            }
            else
            {
                featuredContent.UpdatedOn = DateTime.Now;
                unitOfWork.FeaturedContentRepository.Update(featuredContent);

            }

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
