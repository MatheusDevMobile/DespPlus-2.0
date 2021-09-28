using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using PowerDev.Enterprise.Eureka.Validadores;

namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface IValidador<TEntidade> : IValidador, IValidator<TEntidade> //where TEntidade : class
    {
        ValidacaoResultado Validar(TEntidade entidade);
        ValidacaoResultado Validar(TEntidade entidade, string rulerSet);
        ValidacaoResultado ValidarPropriedade(TEntidade entidade, Expression<Func<TEntidade, object>> propriedade);
        Task<ValidacaoResultado> ValidarAsync(TEntidade entidade, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidacaoResultado> ValidarAsync(TEntidade entidade, string rulerSet, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidacaoResultado> ValidarPropriedadeAsync(TEntidade entidade, Expression<Func<TEntidade, object>> propriedade, CancellationToken cancellationToken = default(CancellationToken));
    }
    public interface IValidador
    {
        List<PropertyRule> PegarRegras();
    }
}