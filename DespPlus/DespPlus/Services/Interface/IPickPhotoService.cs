using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DespPlus.Services.Interface
{
    public interface IPickPhotoService
    {
        Task<(ImageSource, string, string)> TakePhotoAsync();
        Task<(ImageSource, string, string)> LoadPhotoAsync(FileResult photo);
        Task<(ImageSource, string, string)> PickPhotoFromLibraryAsync();
        string ChangeFileName(string fileName);
    }
}
