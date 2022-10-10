using DespPlus.Data;
using DespPlus.Data.EFCoreSqlite;
using DespPlus.Data.Repository;
using DespPlus.Data.Repository.Interface;
using DespPlus.Interface;
using DespPlus.Models;
using DespPlus.Services;
using DespPlus.Services.Interface;
using DespPlus.Validators;
using DespPlus.ViewModels;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace DespPlus.ApplicationApp
{
    public class Factory : DependencyManager<Factory>
    {
        protected internal Container Container { get; }
        public Factory()
        {
            Container = new Container();
            Container.Register<DespPlusContext>(Lifestyle.Singleton);

            ConfigureViewModels();
            ConfigureServices();
            ConfigureRepositories();
            ConfigureValidators();
        }

        private void ConfigureValidators()
        {
            Container.Register<RegisterValidator>(Lifestyle.Singleton);
            Container.Register<CategoryPopupValidator>(Lifestyle.Singleton);
            Container.Register<PaymentMethodPopupValidator>(Lifestyle.Singleton);
        }

        private void ConfigureViewModels()
        {
            Container.Register<MainPageVM>();
            Container.Register<RegisterVM>();
            Container.Register<DetailRegisterVM>();
            Container.Register<FilePopupPageVM>();
            Container.Register<SettingsPageVM>();
            Container.Register<PaymentMethodPageVM>();
            Container.Register<CategoryPageVM>();
            Container.Register<PaymentMethodPopupVM>();
            Container.Register<CategoryPopupVM>();
            Container.Register<OnboardingViewModel>();
            Container.Register<AlertPopupVM>();
        }

        private void ConfigureServices()
        {
            Container.Register<ICashFlowService, CashFlowService>(Lifestyle.Scoped);
            Container.Register<INavigatorService, NavigatorService>(Lifestyle.Singleton);
            Container.Register<IHandleExeptionService, HandleExeptionService>(Lifestyle.Singleton);
            Container.Register<IPickPhotoService, PickPhotoService>(Lifestyle.Scoped);
            Container.Register<ICategoryService, CategoryService>(Lifestyle.Scoped);
            Container.Register<IPaymentMethodService, PaymentMethodService>(Lifestyle.Scoped);
            Container.Register<ISeedingService, SeedingService>(Lifestyle.Scoped);

            Container.RegisterInstance<IShowAlertService>(new ShowAlertService());
        }

        private void ConfigureRepositories()
        {
            Container.Register<IRepository<CashFlow>, EFCoreCashFlowRepository>(Lifestyle.Scoped);
            Container.Register<IRepository<Category>, EFCoreCategoryRepository>(Lifestyle.Scoped);
            Container.Register<IRepository<PaymentMethod>, EFCorePaymentMethodRepository>(Lifestyle.Scoped);
        }

        public override object GetInstance(Type type)
        {
            return Container.GetInstance(type);
        }

        public override TInstance GetInstance<TInstance>()
        {
            return Container.GetInstance<TInstance>();
        }

        public override IEnumerable<object> GetInstanceIEnumerable(Type tipo)
        {
            return Container.GetAllInstances(tipo);
        }

        public override IEnumerable<TInstancia> GetInstancesIEnumerable<TInstancia>()
        {
            return Container.GetAllInstances<TInstancia>();
        }

        public void FinishInitialize()
        {
            Container.Verify();
        }

    }
    public static class FactoryExtension
    {
        public static void EndInitialize(this IDependencyManager dependencyManager)
        {
            ((Factory)dependencyManager).FinishInitialize();
        }
        public static Container GetContainer(this IDependencyManager dependencyManager)
        {
            return ((Factory)dependencyManager).Container;
        }
    }
}
