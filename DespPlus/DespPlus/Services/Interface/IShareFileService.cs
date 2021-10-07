using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface IShareFileService
    {
        Task ShareFileAsync(object file);
    }
}
