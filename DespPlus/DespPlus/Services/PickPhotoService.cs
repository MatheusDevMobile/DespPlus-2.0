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

        public async Task<(ImageSource, string, string)> LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
                return (null, "", "");

            var memotyStream = new MemoryStream();

            var stream = await photo.OpenReadAsync();
            stream.CopyTo(memotyStream);
            var byteFile = memotyStream.ToArray();

            var base64 = Convert.ToBase64String(byteFile);

            var image = ImageSource.FromStream(() =>
            {
                return new MemoryStream(byteFile);
            });

            var imageName = ChangeFileName(photo.FileName);

            return (image, imageName, base64);
        }

        public async Task<(ImageSource, string, string)> PickPhotoFromLibraryAsync()
        {
            try
            {
                var photoLibrary = await MediaPicker.PickPhotoAsync();
                var imageSource = await LoadPhotoAsync(photoLibrary);

                return imageSource;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
                return (null, "", "");
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
                return (null, "", "");
            }
            catch (Exception ex)
            {
                return (null, "", "");
            }
        }

        public async Task<(ImageSource, string, string)> TakePhotoAsync()
        {
            try
            {
                var photoCamera = await MediaPicker.CapturePhotoAsync();
                var imageSource = await LoadPhotoAsync(photoCamera);

                return imageSource;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
                return (null, "", "");
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
                return (null, "", "");
            }
            catch (Exception ex)
            {
                return (null, "", "");
            }
        }
    }
}
