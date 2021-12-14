using DespPlus.Data.Repository.Interface;

namespace DespPlus.Models
{
    public class PaymentMethod : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
