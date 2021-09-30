using DespPlus.Data;
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
            Container.Register<RegisterValidator>();
            Container.Register<CategoryPopupValidator>();
            Container.Register<PaymentMethodPopupValidator>();
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
        }

        private void ConfigureServices()
        {
            Container.Register<ICashFlowService, CashFlowService>();
            Container.Register<INavigatorService, NavigatorService>(Lifestyle.Singleton);
            Container.Register<IHandleExeptionService, HandleExeptionService>(Lifestyle.Singleton);
            Container.Register<IPickPhotoService, PickPhotoService>();
            Container.Register<ICategoryService, CategoryService>();
            Container.Register<IPaymentMethodService, PaymentMethodService>();

            Container.RegisterInstance<IShowAlertService>(new ShowAlertService());
        }

        private void ConfigureRepositories()
        {
            Container.Register<IRegisterRepository<CashFlow>, CashFlowRepository>();
            Container.Register<IRegisterRepository<Category>, CategoryRepository>(Lifestyle.Singleton);
            Container.Register<IRegisterRepository<PaymentMethod>, PaymentMethodRepository>();
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
