using AutoMapper;
using TourWebsite.Data;
using TourWebsite.Data.Entities;
using TourWebsite.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TourWebsite.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly TourDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesService(TourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CategoriesViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var categories = from m in _context.Categories select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.CategoryName!.Contains(searchString));
            }

            categories = sortOrder switch
            {
                "categoryName_desc" => categories.OrderByDescending(s => s.CategoryName),
                "release_date" => categories.OrderBy(s => s.ReleaseDate),
                "release_date_desc" => categories.OrderByDescending(s => s.ReleaseDate),
                _ => categories.OrderBy(s => s.CategoryName),
            };

            return PaginatedList<CategoriesViewModel>.Create(_mapper.Map<IEnumerable<CategoriesViewModel>>(await categories.ToListAsync()), pageNumber ?? 1, pageSize);
        }
        public async Task<int> Create(CategoriesRequest request)
        {
            var category = _mapper.Map<Category>(request);
            _context.Add(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoriesViewModel>> GetAll()
        {
            var products = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoriesViewModel>>(products);
        }

        public async Task<CategoriesViewModel> GetById(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<CategoriesViewModel>(category);
        }

        public async Task<int> Update(CategoriesViewModel request)
        {
            if (!CategoriesExists(request.Id))
            {
                throw new Exception("Category does not exist");
            }
            _context.Update(_mapper.Map<Category>(request));
            return await _context.SaveChangesAsync();
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}