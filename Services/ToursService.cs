using AutoMapper;
using TourWebsite.Data;
using TourWebsite.Data.Entities;
using TourWebsite.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace TourWebsite.Services
{
    public class ToursService : IToursService
    {
        private readonly TourDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ToursService(TourDbContext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<PaginatedList<ToursViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize, int? categoryId)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var tours = from m in _context.Tours select m;
            if (categoryId != null)
            {
                tours = tours.Where(s => s.CategoryId == categoryId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                tours = tours.Where(s => s.TourName!.Contains(searchString));
            }

            tours = sortOrder switch
            {
                "TourName_desc" => tours.OrderByDescending(s => s.TourName),
                "DateCreated" => tours.OrderBy(s => s.DateCreated),
                "DateCreated_desc" => tours.OrderByDescending(s => s.DateCreated),
                _ => tours.OrderBy(s => s.TourName),
            };

            return PaginatedList<ToursViewModel>.Create(_mapper.Map<IEnumerable<ToursViewModel>>(await tours.ToListAsync()), pageNumber ?? 1, pageSize);
        }
        public async Task<int> Create(ToursRequest request)
        {
            var tour = _mapper.Map<Tours>(request);
            if (request.Image != null)
            {
                tour.ImagePath = await SaveFile(request.Image);
            }
            _context.Add(tour);
            _context.Add(tour);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var tours = await _context.Tours.FindAsync(id);
            if (tours != null)
            {
                if (!string.IsNullOrEmpty(tours.ImagePath))
                    await _storageService.DeleteFileAsync(tours.ImagePath.Replace("/" + USER_CONTENT_FOLDER_NAME + "/", ""));
                _context.Tours.Remove(tours);
            }
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ToursViewModel>> GetAll()
        {
            var products = await _context.Tours.ToListAsync();
            return _mapper.Map<IEnumerable<ToursViewModel>>(products);
        }

        public async Task<ToursViewModel> GetById(int id)
        {
            var tours = await _context.Tours
                .FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<ToursViewModel>(tours);
        }

        public async Task<int> Update(ToursViewModel request)
        {
            if (!ToursExists(request.Id))
            {
                throw new Exception("Tours does not exist");
            }
            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(request.ImagePath))
                    await _storageService.DeleteFileAsync(request.ImagePath.Replace("/" + USER_CONTENT_FOLDER_NAME + "/", ""));
                request.ImagePath = await SaveFile(request.Image);
            }
            _context.Update(_mapper.Map<Tours>(request));
            return await _context.SaveChangesAsync();
        }

        private bool ToursExists(int id)
        {
            return _context.Tours.Any(e => e.Id == id);
        }
    }
}