using DespPlus.Data;
using DespPlus.Data.Mock;
using DespPlus.Data.Repository;
using DespPlus.Data.Repository.Interface;
using DespPlus.Interface;
using DespPlus.Services;
using DespPlus.Services.Interface;
using DespPlus.ViewModels;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace DespPlus.Aplication
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
        }

        private void ConfigureViewModels()
        {
            Container.Register<MainPageVM>();
            Container.Register<RegisterVM>();
            Container.Register<RegisterInfoModalVM>();
            Container.Register<FilePopupPageVM>();
        }

        private void ConfigureServices()
        {
            Container.Register<ICashFlowService, CashFlowService>();
            Container.Register<INavigatorService, NavigatorService>(Lifestyle.Singleton);
            Container.Register<IHandleExeptionService, HandleExeptionService>(Lifestyle.Singleton);
            Container.Register<IPickPhotoService, PickPhotoService>();
            Container.Register <ICategoryService, CategoryMock>();
            Container.Register <IPaymentMethodService, PaymentMethodMock>();

            Container.RegisterInstance<IShowAlertService>(new ShowAlertService());
        }

        private void ConfigureRepositories()
        {
            Container.Register<ICashFlowRepository, CashFlowRepository>();
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
