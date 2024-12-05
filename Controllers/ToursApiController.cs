using TourWebsite.Services;
using TourWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TourWebsite.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ToursApiController : ControllerBase
    {
        private readonly IToursService _toursService;

        public ToursApiController(IToursService toursService)
        {
            _toursService = toursService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ToursRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _toursService.Create(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _toursService.GetById(id);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAllFilter(string? sortOrder, string? currentFilter, string? searchString, int? pageNumber, int? categoryId, int pageSize = 10)
        {
            var result = await _toursService.GetAllFilter(sortOrder!, currentFilter!, searchString!, pageNumber, pageSize, categoryId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _toursService.Delete(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ToursViewModel request)
        {
            var result = await _toursService.Update(request);
            return Ok(result);
        }
    }
}