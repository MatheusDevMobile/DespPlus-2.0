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
        protected IRegisterRepository<CashFlow> CashFlowRepository { get; }
        protected IRegisterRepository<Category> CategoryRepository { get; }
        protected IRegisterRepository<PaymentMethod> PaymentMethodRepository { get; }
        public CashFlowService(IRegisterRepository<CashFlow> repository, IRegisterRepository<Category> categoryRepository, IRegisterRepository<PaymentMethod> paymentMethodRepository)
        {
            CashFlowRepository = repository;
            CategoryRepository = categoryRepository;
            PaymentMethodRepository = paymentMethodRepository;
        }
        public async Task<bool> CreateRegister(CashFlow cashFlow)
        {
            return await CashFlowRepository.Save(cashFlow);
        }

        public async Task<bool> UpdateRegister(string id, CashFlow cashFlow)
        {
            return await CashFlowRepository.Update(id, cashFlow);
        }

        public async Task<bool> DeleteRegister(string id)
        {
            return await CashFlowRepository.Delete(id);
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
                    if (category.CategoryId == cashFlow.CategoryId)
                        cashFlow.Category = category;
                }

                foreach (var paymentMethod in paymentMethods)
                {
                    if (paymentMethod.PaymentMethodId == cashFlow.PaymentMethodId)
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
