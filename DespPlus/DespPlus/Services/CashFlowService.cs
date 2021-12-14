using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services
{
    public class CashFlowService : ICashFlowService
    {
        protected IRepository<CashFlow> CashFlowRepository { get; }
        protected IRepository<Category> CategoryRepository { get; }
        protected IRepository<PaymentMethod> PaymentMethodRepository { get; }
        public CashFlowService(IRepository<CashFlow> repository, IRepository<Category> categoryRepository, IRepository<PaymentMethod> paymentMethodRepository)
        {
            CashFlowRepository = repository;
            CategoryRepository = categoryRepository;
            PaymentMethodRepository = paymentMethodRepository;
        }
        public async Task<bool> CreateRegister(CashFlow cashFlow)
        {
            await CashFlowRepository.Create(cashFlow);
            return true;
        }

        public async Task<bool> UpdateRegister(string id, CashFlow cashFlow)
        {
            if (id != cashFlow.Id)
                return false;

            await CashFlowRepository.Update(cashFlow);
            return true;
        }

        public async Task<bool> DeleteRegister(string id)
        {
            await CashFlowRepository.Delete(id);
            return true;
        }

        public async Task<List<CashFlow>> GetAllCashFlow()
        {
            var cashflows = await CashFlowRepository.GetAll();
            var categories = await CategoryRepository.GetAll();
            var paymentMethods = await PaymentMethodRepository.GetAll();

            foreach (var cashFlow in cashflows)
            {
                foreach (var category in categories)
                {
                    if (category.Id == cashFlow.CategoryId)
                        cashFlow.Category = category;
                }

                foreach (var paymentMethod in paymentMethods)
                {
                    if (paymentMethod.Id == cashFlow.PaymentMethodId)
                        cashFlow.PaymentMethod = paymentMethod;
                }
            }
            return cashflows;
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
