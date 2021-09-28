using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository
{
    public class CashFlowRepository : IRegisterRepository<CashFlow>
    {
        private DespPlusContext _context;
        public async Task<bool> Delete(string id)
        {
            _context = new DespPlusContext();
            try
            {
                var register = await _context.CashFlows.FindAsync(id);
                if (register != null)
                { 
                    _context.CashFlows.Remove(register);
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

        public async Task<List<CashFlow>> GetAll()
        {
            _context = new DespPlusContext();
            try
            {
                var list = await _context.CashFlows.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Save(CashFlow cashFlow)
        {
            _context = new DespPlusContext();
            try
            {
                _context.CashFlows.Add(cashFlow);
                var row = await _context.SaveChangesAsync();

                return row > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, CashFlow cashFlow)
        {
            _context = new DespPlusContext();
            try
            {
                var register = await _context.CashFlows.FindAsync(id);
                if (register != null)
                {
                    register.Date = cashFlow.Date;
                    register.Time = cashFlow.Time;
                    register.Value = cashFlow.Value;
                    register.ValueLabel = cashFlow.ValueLabel;
                    register.CategoryDescription = cashFlow.CategoryDescription;
                    register.OtherCategoryDescription = cashFlow.OtherCategoryDescription;
                    register.PaymentMethodDescription = cashFlow.PaymentMethodDescription;
                    register.OtherCategoryDescription = cashFlow.OtherPaymentDescription;
                    register.ImageName = cashFlow.ImageName;
                    register.ImageString64 = cashFlow.ImageString64;
                    register.Comment = cashFlow.Comment;
                    register.IsIncome = cashFlow.IsIncome;

                    _context.CashFlows.Update(register);
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
