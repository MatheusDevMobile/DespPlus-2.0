using System;
using System.Collections.Generic;

namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface IGestorDependencia
    {
        Object ObterInstancia(Type tipo);
        IEnumerable<Object> ObterInstancias(Type tipo);
        TInstancia ObterInstancia<TInstancia>() where TInstancia : class;
        IEnumerable<TInstancia> ObterInstancias<TInstancia>() where TInstancia : class;
    }
}