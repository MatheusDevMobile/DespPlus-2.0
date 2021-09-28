using DespPlus.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DespPlus.Services.Interface
{
    public interface IPickPhotoService
    {
        Task<ImageReceip> TakePhoto();
        Task<ImageReceip> GetImageInformation(FileResult photo);
        Task<ImageReceip> PickPhotoFromLibrary();
        string ChangeFileName(string fileName);
    }
}
