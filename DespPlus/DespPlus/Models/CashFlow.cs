using DespPlus.Data.Repository.Interface;
using System;
using Xamarin.Forms;

namespace DespPlus.Models
{
    public class CashFlow : IEntity
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Value { get; set; }
        public string ValueLabel { get; set; }
        public bool IsIncome { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
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
            //string icon;
            //switch (Category.Name)
            //{
            //    case "Alimentação":
            //        icon = "restaurant.png";
            //        break;
            //    case "Combutível":
            //        icon = "car.png";
            //        break;
            //    default:
            //        icon = "house.png";
            //        break;
            //}
            //CategoryItem.Name == "Salário" ? "money.png" :
            return "money.png";
        }
    }
}
