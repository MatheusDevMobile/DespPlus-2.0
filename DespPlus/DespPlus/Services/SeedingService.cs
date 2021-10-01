using DespPlus.Data;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System;
using System.Linq;

namespace DespPlus.Services
{
    public class SeedingService : ISeedingService
    {
        private DespPlusContext _context;
        public SeedingService()
        {
            _context = DependencyManager.Instance.GetInstance<DespPlusContext>();
        }
        public void Seed()
        {
            if (_context.Categories.Any() || _context.PaymentMethods.Any())
            {
                return;
            }

            Category c1 = new Category { CategoryId = Guid.NewGuid().ToString(), Name = "Alimentação", IsExpense = true, IsIncome = false };
            Category c2 = new Category { CategoryId = Guid.NewGuid().ToString(), Name = "Combustível", IsExpense = true, IsIncome = false };
            Category c3 = new Category { CategoryId = Guid.NewGuid().ToString(), Name = "Salário", IsExpense = false, IsIncome = true };

            PaymentMethod pm1 = new PaymentMethod { PaymentMethodId = Guid.NewGuid().ToString(), Name = "Dinheiro" };
            PaymentMethod pm2 = new PaymentMethod { PaymentMethodId = Guid.NewGuid().ToString(), Name = "Cartão de Crédito" };
            PaymentMethod pm3 = new PaymentMethod { PaymentMethodId = Guid.NewGuid().ToString(), Name = "Cartão de Débito" };
            PaymentMethod pm4 = new PaymentMethod { PaymentMethodId = Guid.NewGuid().ToString(), Name = "PIX" };

            _context.Categories.AddRange(c1, c2, c3);
            _context.PaymentMethods.AddRange(pm1, pm2, pm3, pm4);

            _context.SaveChanges();
        }
    }
}
