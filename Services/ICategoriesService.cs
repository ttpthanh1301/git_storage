using TourWebsite.ViewModels;

namespace TourWebsite.Services
{
    public interface ICategoriesService
    {
        Task<PaginatedList<CategoriesViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
        Task<IEnumerable<CategoriesViewModel>> GetAll();
        Task<CategoriesViewModel> GetById(int id);
        Task<int> Create(CategoriesRequest request);
        Task<int> Update(CategoriesViewModel request);
        Task<int> Delete(int id);
    }
}