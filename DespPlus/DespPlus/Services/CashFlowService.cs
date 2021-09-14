using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DespPlus.Services
{
    public class CashFlowService : ICashFlowService
    {
        protected ICashFlowRepository Repository { get; }
        public CashFlowService(ICashFlowRepository repository)
        {
            Repository = repository;
        }
        public async Task<bool> CreateRegister(CashFlow cashFlow)
        {
            return await Repository.Save(cashFlow);
        }

        public async Task<bool> DeleteRegister(CashFlow cashFlow)
        {
            return await Repository.Delete(cashFlow);
        }

        public async Task<List<CashFlow>> GetAllCashFlow()
        {
            return await Repository.GetAll();
        }

        public Task<List<CashFlow>> GetCashFlowOfMonth()
        {
            throw new NotImplementedException();
        }

        public Task<List<CashFlow>> GetCashFlowOfWeek()
        {
            throw new NotImplementedException();
        }

        public Task<List<CashFlow>> GetCashFlowOfYear()
        {
            throw new NotImplementedException();
        }

        public Task<List<CashFlow>> GetCashFlowToday(DateTime dateToday)
        {
            throw new NotImplementedException();
        }
    }
}
