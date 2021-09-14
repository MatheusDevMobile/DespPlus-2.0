using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespPlus.Models
{
    public class CashFlow
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Value { get; set; }
        public string ValueLabel { get; set; }
        public string RegisterType { get; set; }
        public string CategoryDescription { get; set; }
        public string OtherCategoryDescription { get; set; }
        public string PaymentDescription { get; set; }
        public string OtherPaymentDescription { get; set; }
        public string Comment { get; set; }
        public string ImageString { get; set; }
        [NotMapped]
        public string Icon { get; set; }
        [NotMapped]
        public string LabelColor { get; set; }

    }
}
