using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository
{
    public class CashFlowRepository : ICashFlowRepository
    {
        private DespPlusContext Context { get; }

        public CashFlowRepository(DespPlusContext context)
        {
            Context = context;
        }
        public async Task<bool> Delete(CashFlow cashFlow)
        {
            try
            {
                Context.CashFlows.Remove(cashFlow);
                var row = await Context.SaveChangesAsync();

                return row > 0;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<CashFlow>> GetAll()
        {
            try
            {
                var list = await Context.CashFlows.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Save(CashFlow cashFlow)
        {
            try
            {
                Context.CashFlows.Add(cashFlow);
                var row = await Context.SaveChangesAsync();

                return row > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
