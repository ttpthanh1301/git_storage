using TourWebsite.Services;
using TourWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TourWebsite.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesApiController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoriesRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoriesService.Create(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoriesService.GetById(id);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAllFilter(string? sortOrder, string? currentFilter, string? searchString, int? pageNumber, int pageSize = 10)
        {
            var result = await _categoriesService.GetAllFilter(sortOrder!, currentFilter!, searchString!, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoriesService.Delete(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoriesViewModel request)
        {
            var result = await _categoriesService.Update(request);
            return Ok(result);
        }
    }
}