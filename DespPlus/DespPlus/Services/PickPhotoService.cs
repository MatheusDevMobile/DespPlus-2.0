using DespPlus.Models;
using DespPlus.Services.Interface;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DespPlus.Services
{
    public class PickPhotoService : IPickPhotoService
    {
        public string ChangeFileName(string fileName)
        {
            var fileType = fileName.Split(".");
            var newFileName = $"{DateTime.Today:dd-MM-yyyy}.{fileType[1]}";
            
            return newFileName;
        }

        public async Task<ImageReceip> GetImageInformation(FileResult photo)
        {
            if (photo == null)
                return null;

            var ImageReceip = new ImageReceip();
            var memotyStream = new MemoryStream();

            var stream = await photo.OpenReadAsync();
            stream.CopyTo(memotyStream);
            var byteFile = memotyStream.ToArray();

            ImageReceip.Base64 = Convert.ToBase64String(byteFile);

            ImageReceip.ImageSource = ImageSource.FromStream(() =>
            {
                return new MemoryStream(byteFile);
            });

            ImageReceip.ImageName = ChangeFileName(photo.FileName);

            return ImageReceip;
        }

        public async Task<ImageReceip> PickPhotoFromLibrary()
        {
            try
            {
                var ImageReceip = new ImageReceip();

                var photoLibrary = await MediaPicker.PickPhotoAsync();

                return await GetImageInformation(photoLibrary);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
                return null;
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ImageReceip> TakePhoto()
        {
            try
            {
                var photoCamera = await MediaPicker.CapturePhotoAsync();

                return await GetImageInformation(photoCamera);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
                return null;
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
