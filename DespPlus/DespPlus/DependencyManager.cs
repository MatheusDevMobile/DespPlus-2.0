using DespPlus.Interface;
using System;
using System.Collections.Generic;

namespace DespPlus
{
    public abstract class DependencyManager : IDependencyManager
    {
        public static IDependencyManager Instance { get; protected set; }

        public abstract object GetInstance(Type type);
        public abstract TInstance GetInstance<TInstance>() where TInstance : class;
        public abstract IEnumerable<object> GetInstanceIEnumerable(Type type);
        public abstract IEnumerable<TInstance> GetInstancesIEnumerable<TInstance>() where TInstance : class;
    }

    public abstract class DependencyManager<T> : DependencyManager where T : IDependencyManager, new()
    {
        public static void Initialize()
        {
            if (Instance != null)
                return;

            Instance = new T();
        }
        protected DependencyManager()
        {
        }
    }
}
