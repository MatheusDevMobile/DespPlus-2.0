using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.ViewModels.Interfaces
{
    public interface IViewModel
    {
        Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters);
        string Title { get; set; }
    }
}
