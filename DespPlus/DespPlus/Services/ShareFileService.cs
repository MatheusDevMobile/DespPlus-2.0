using DespPlus.Models;
using DespPlus.Services.Interface;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DespPlus.Services
{
    public class ShareFileService : IShareFileService
    {
        public ShareFileService()
        {

        }

        public async Task ShareFileAsync(object obj)
        {
            var date = DateTime.Today.ToString("dd-MM-yyyy");
            var fileName = $"{date}.json";
            var file = Path.Combine(FileSystem.CacheDirectory, fileName);

            string jsonString = JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, jsonString);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Application.Current.MainPage.Title,
                File = new ShareFile(file)
            });
        }
    }
}
