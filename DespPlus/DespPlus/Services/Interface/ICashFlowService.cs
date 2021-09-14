using DespPlus.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface ICashFlowService
    {
        Task<List<CashFlow>> GetAllCashFlow();
        Task<List<CashFlow>> GetCashFlowToday(DateTime dateToday);
        Task<List<CashFlow>> GetCashFlowOfWeek();
        Task<List<CashFlow>> GetCashFlowOfMonth();
        Task<List<CashFlow>> GetCashFlowOfYear();
        Task<bool> CreateRegister(CashFlow cashFlow);
        Task<bool> DeleteRegister(CashFlow cashFlow);
    }
}
