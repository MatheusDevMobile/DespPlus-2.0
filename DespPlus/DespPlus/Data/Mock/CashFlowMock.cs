using Bogus;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DespPlus.Data.Mock
{
    public class CashFlowMock : ICashFlowService
    {
        private readonly List<CashFlow> _listExpenses;
        public CashFlowMock()
        {
            _listExpenses = new Faker<CashFlow>().RuleFor(a => a.Value, f => f.Random.Int(100, 800))
                                                 .RuleFor(a => a.Date, f => f.Date.Between(DateTime.Today, DateTime.Today))
                                                 .RuleFor(a => a.Time, f => f.Date.Timespan())
                                                 .RuleFor(a => a.Comment, f => f.Hacker.Phrase())
                                                 .RuleFor(a => a.CategoryDescription, f => f.Hacker.Verb())
                                                 .Generate(10);
        }

        //public async Task<List<Expense>> GetAllExpenses()
        //{
        //    return await Task.Run(() =>
        //    {
        //        return _listExpenses;
        //    });
        //}
        //public async Task<double> GetSumExpensesValue()
        //{
        //    return await Task.Run(() =>
        //    {
        //        var total = _listExpenses.Sum(l => l.Value);
        //        return total;
        //    });
        //}

        //public async Task<List<Expense>> GetExpensesToday(DateTime dateToday)
        //{
        //    return await Task.Run(() =>
        //    {
        //        var list = _listExpenses.Where(d => d.Date == dateToday).ToList();
        //        return list;
        //    });
        //}

        //public Task<List<Expense>> GetExpensesOfMonth()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Expense>> GetExpensesOfWeek()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Expense>> GetExpensesOfYear()
        //{
        //    throw new NotImplementedException();
        //}
        public Task<bool> CreateRegister(CashFlow cashFlow)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRegister(CashFlow cashFlow)
        {
            throw new NotImplementedException();
        }

        public Task<List<CashFlow>> GetAllCashFlow()
        {
            throw new NotImplementedException();
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
