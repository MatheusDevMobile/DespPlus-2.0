using DespPlus.Data.Repository.Interface;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DespPlus.Models
{
    public class Category : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        public bool IsExpense { get; set; }

        [JsonIgnore]
        public Color LabelColor => GetColorLabel();
        private Color GetColorLabel()
        {
            string corHex;
            switch (IsIncome)
            {
                case false:
                    corHex = ((Color)Application.Current.Resources["AlertColor"]).ToHex();
                    break;
                case true:
                    corHex = ((Color)Application.Current.Resources["PrimaryColor"]).ToHex();
                    break;
                default:
            }

            corHex = corHex.Remove(0, 3);
            return Color.FromHex($"#{corHex}");
        }
    }
}
