using DespPlus.Interface;
using System;

namespace DespPlus.ApplicationApp
{
    public class EnterpriseAplication
    {
        private static bool isInit;
        public static void InitializeAplication<T>() where T : IDependencyManager, new()
        {
            if (isInit)
                return;
            isInit = true;

            try
            {
                Factory.Initialize();
                new T();
                DependencyManager.Instance.EndInitialize();
            }
            catch (Exception ex)
            {
                //TODO implementar analytics do App Center posteriormente Crashes.TrackError(ex)
            }
        }
    }
}
