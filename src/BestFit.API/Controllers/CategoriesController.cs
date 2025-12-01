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
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(CategoryService categoryService,IUnitOfWork unitOfWork)
        {
            this.categoryService = categoryService;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Category> categoryList = categoryService.GetAllCategories();
            return Ok(categoryList);
        }
    }
}
