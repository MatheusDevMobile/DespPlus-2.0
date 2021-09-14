using DespPlus.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface ICategoryService
    {
        Task<ObservableCollection<Category>> GetCategories();
    }
}
