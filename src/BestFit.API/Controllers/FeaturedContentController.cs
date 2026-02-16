using AutoMapper;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using BestFit.Web.Models;
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


            return Ok(mapper.Map<FeaturedContentResponseDTO>(featuredContent));
        }

        [HttpGet]
        [Route("{today:datetime}")]
        public IActionResult GetTodayContent(DateTime today)
        {
            var todayFeaturedContent = featuredContentService.GetTodayContent(today);
            return Ok(mapper.Map<FeaturedContentResponseDTO>(todayFeaturedContent));

        }

        [HttpPost]
        public IActionResult Post([FromBody] AddFeaturedContentRequestDTO  addFeaturedContentRequestDTO)
        {
            var contentDomainModel = mapper.Map<FeaturedContent>(addFeaturedContentRequestDTO);

            contentDomainModel = featuredContentService.CreateFeaturedContent(contentDomainModel);
            var featuredContentDTO = mapper.Map<FeaturedContentResponseDTO>(contentDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = contentDomainModel.Id }, featuredContentDTO);

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
                return Ok(mapper.Map<FeaturedContentResponseDTO>(contentDomainModel));
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
                return Ok(mapper.Map<FeaturedContentResponseDTO>(content));
            }
        }
    }


}
