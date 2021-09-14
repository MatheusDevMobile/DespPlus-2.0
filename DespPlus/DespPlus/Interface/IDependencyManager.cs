using System;
using System.Collections.Generic;

namespace DespPlus.Interface
{
    public interface IDependencyManager
    {
        object GetInstance(Type type);
        IEnumerable<object> GetInstanceIEnumerable(Type type);
        TInstance GetInstance<TInstance>() where TInstance : class;
        IEnumerable<TInstance> GetInstancesIEnumerable<TInstance>() where TInstance : class;
    }
}
