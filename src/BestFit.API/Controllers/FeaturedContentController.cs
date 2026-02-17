using AutoMapper;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturedContentController : ControllerBase
    {
        private readonly FeaturedContentService featuredContentService;
        private readonly IMapper mapper;

        public FeaturedContentController(FeaturedContentService featuredContentService,IMapper mapper)
        {
            this.featuredContentService = featuredContentService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var featuredContents = featuredContentService.GetAllFeaturedContents();

            var featuredContentsDTO = mapper.Map<List<FeaturedContent>>(featuredContents);

            return Ok(featuredContentsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var featuredContent = featuredContentService.GetFeaturedContentById(id);


            return Ok(mapper.Map<FeaturedContentResponse>(featuredContent));
        }

        

        [HttpPost]
        public IActionResult Post([FromForm] AddFeaturedContentRequestDTO  addFeaturedContentRequestDTO)
        {
            ValidateFileUpload(addFeaturedContentRequestDTO);
            if(ModelState.IsValid)
            {
                var contentDomainModel = mapper.Map<FeaturedContent>(addFeaturedContentRequestDTO);

                contentDomainModel = featuredContentService.CreateFeaturedContent(contentDomainModel, contentDomainModel.File);

                var featuredContentDTO = mapper.Map<FeaturedContentResponse>(contentDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = contentDomainModel.Id }, featuredContentDTO);

            }
            return BadRequest(ModelState);
            
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateFeaturedContentRequestDTO updateFeaturedContentRequestDTO)
        {
            var contentDomainModel = mapper.Map<FeaturedContent>(updateFeaturedContentRequestDTO);

            contentDomainModel = featuredContentService.UpdateFeaturedContent(contentDomainModel);

            if (contentDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<FeaturedContentResponse>(contentDomainModel));
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var content = featuredContentService.DeleteFeaturedContent(id);

            if (content == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<FeaturedContentResponse>(content));
            }
        }

        private void ValidateFileUpload(AddFeaturedContentRequestDTO requestDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".svg" };
            if (!allowedExtensions.Contains(Path.GetExtension(requestDTO.File.FileName)))
            {
                ModelState.AddModelError("file","Unsupported file Extension");
            }
            if(requestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size More than 10MB,please upload a smaller size file");
            }


                
        }
    }


}
