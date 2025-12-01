using AutoMapper;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService categoryService;
        
        private readonly IMapper mapper;

        public CategoriesController(CategoryService categoryService,IMapper mapper)
        {
            this.categoryService = categoryService;
           
            this.mapper = mapper;
        }
        
        //GetAll Categories
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Category> categoryList = categoryService.GetAllCategories();
            
            var categoryListResponseDTO = mapper.Map<List<CategoryResponseDTO>>(categoryList);
            return Ok(categoryList);
        }

        //Get CategoryByID
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            var category = categoryService.GetCategoryById(id);

            
            return Ok(mapper.Map<CategoryResponseDTO>(category));
        }

        //post to create a new category
        [HttpPost]
        public IActionResult Post([FromBody] AddCategoryRequestDTO addCategoryRequestDTO)
        {
            var categoryDomainModel = mapper.Map<Category>(addCategoryRequestDTO);

            categoryDomainModel = categoryService.CreateCategory(categoryDomainModel);
            var categoryDTO = mapper.Map<CategoryResponseDTO>(categoryDomainModel);

            return CreatedAtAction(nameof(GetById), new {id = categoryDomainModel.Id},categoryDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDTO updateCategoryRequestDTO )
        {
            var categoryDomainModel = mapper.Map<Category>(updateCategoryRequestDTO);

            categoryDomainModel = categoryService.UpdateCategory(categoryDomainModel);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CategoryResponseDTO>(categoryDomainModel));
            }
        }
        
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var category = categoryService.DeleteCategory(id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CategoryResponseDTO>(category));
            }
        }
        
    }
}
