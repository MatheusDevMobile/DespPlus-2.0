using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository
{
    internal class PaymentMethodRepository : IRegisterRepository<PaymentMethod>
    {
        private DespPlusContext _context;
        public async Task<bool> Delete(string id)
        {
            _context = new DespPlusContext();
            try
            {
                var register = await _context.PaymentMethods.FindAsync(id);
                if (register != null)
                {
                    _context.PaymentMethods.Remove(register);
                    var row = _context.SaveChanges();
                    return row > 0;
                }

                return false;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<List<PaymentMethod>> GetAll()
        {
            _context = new DespPlusContext();
            try
            {
                var list = await _context.PaymentMethods.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Save(PaymentMethod paymentMethod)
        {
            _context = new DespPlusContext();
            try
            {
                _context.PaymentMethods.Add(paymentMethod);
                var row = await _context.SaveChangesAsync();

                return row > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, PaymentMethod paymentMethod)
        {
            _context = new DespPlusContext();
            try
            {
                var register = await _context.PaymentMethods.FindAsync(id);
                if (register != null)
                {
                    register.Name = paymentMethod.Name;

                    _context.PaymentMethods.Update(register);
                    var row = _context.SaveChanges();

                    return row > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
