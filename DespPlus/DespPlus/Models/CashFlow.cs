using System;
using Xamarin.Forms;

namespace DespPlus.Models
{
    public class CashFlow
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Value { get; set; }
        public string ValueLabel { get; set; }
        public bool IsIncome { get; set; }
        public string CategoryDescription { get; set; }
        public string OtherCategoryDescription { get; set; }
        public string PaymentMethodDescription { get; set; }
        public string OtherPaymentDescription { get; set; }
        public string Comment { get; set; }
        public string ImageName { get; set; }
        public string ImageString64 { get; set; }
        public string Icon => GetIcon();
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
        private string GetIcon()
        {
            string icon;
            switch (Id)
            {
                case "2":
                    icon = "restaurant.png";
                    break;
                case "3":
                    icon = "car.png";
                    break;
                default:
                    icon = "house.png";
                    break;
            }
            //CategoryItem.Name == "Salário" ? "money.png" :
            return icon;
        }
    }
}
