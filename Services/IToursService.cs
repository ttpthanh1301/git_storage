using TourWebsite.ViewModels;

namespace TourWebsite.Services
{
    public interface IToursService
    {
        Task<PaginatedList<ToursViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize, int? categoryId);
        Task<IEnumerable<ToursViewModel>> GetAll();
        Task<ToursViewModel> GetById(int id);
        Task<int> Create(ToursRequest request);
        Task<int> Update(ToursViewModel request);
        Task<int> Delete(int id);
    }
}