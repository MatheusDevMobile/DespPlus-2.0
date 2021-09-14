using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface INavigatorService
    {
        void BeginNavigation(string url, IReadOnlyDictionary<string, object> parameters = null, bool navigationBar = true);
        Task NavigateToAsync(string url, IReadOnlyDictionary<string, object> parameters = null);
        Task BackToAsync(IReadOnlyDictionary<string, object> parameters = null);
    }
}
