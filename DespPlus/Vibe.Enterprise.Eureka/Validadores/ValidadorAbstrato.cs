using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using PowerDev.Enterprise.Eureka.Extensoes;
using PowerDev.Enterprise.Eureka.Interfaces;

namespace PowerDev.Enterprise.Eureka.Validadores
{
    public abstract class ValidadorAbstrato<TEntidade> : AbstractValidator<TEntidade>, IValidador<TEntidade>
    {
        protected bool configurado;
        protected ValidadorAbstrato(IDisplayNameExtrator displayNameExtrator)
        {
            ValidatorOptions.Global.DisplayNameResolver = (tipo, membro, exp) => displayNameExtrator.ObterDisplay(tipo, membro);
        }
        protected void ValidarConfiguracao()
        {
            if (configurado) return;
            Configurar();
            configurado = true;
        }
        public abstract void Configurar();

        public virtual ValidacaoResultado Validar(TEntidade entidade)
        {
            ValidarConfiguracao();
            ValidationResult r = Validate(entidade);
            var resultado = r.Como<ValidacaoResultado>();
            return resultado;
        }

        public virtual ValidacaoResultado Validar(TEntidade entidade, string rulerSet)
        {
            ValidarConfiguracao();
            //ValidationResult r = this.Validate(entidade, ruleSet: rulerSet);
            ValidationResult r = this.Validate(entidade, opcoes => opcoes.IncludeRuleSets(rulerSet));
            var resultado = r.Como<ValidacaoResultado>();
            return resultado;
        }

        public virtual ValidacaoResultado ValidarPropriedade(TEntidade entidade, Expression<Func<TEntidade, object>> propriedade)
        {
            ValidarConfiguracao();
            //ValidationResult r = this.Validate(entidade, propriedade);
            ValidationResult r = this.Validate(entidade, opcoes => opcoes.IncludeProperties(propriedade));

            var resultado = r.Como<ValidacaoResultado>();
            return resultado;
        }

        public virtual async Task<ValidacaoResultado> ValidarAsync(TEntidade entidade, CancellationToken cancellationToken = default)
        {
            ValidarConfiguracao();
            var r = await ValidateAsync(entidade, cancellationToken);
            return await Task.Run(() => r.Como<ValidacaoResultado>(permitirExcecao: true), cancellationToken);
        }

        public virtual async Task<ValidacaoResultado> ValidarAsync(TEntidade entidade, string rulerSet, CancellationToken cancellationToken = default)
        {
            ValidarConfiguracao();
            //var r = await this.ValidateAsync(entidade, ruleSet: rulerSet, cancellationToken: cancellationToken);
            var r = await this.ValidateAsync(entidade, opcoes => opcoes.IncludeRuleSets(rulerSet), cancellationToken);
            return await Task.Run(() => r.Como<ValidacaoResultado>(), cancellationToken);
        }

        public virtual async Task<ValidacaoResultado> ValidarPropriedadeAsync(TEntidade entidade, Expression<Func<TEntidade, object>> propriedade, CancellationToken cancellationToken = default)
        {
            ValidarConfiguracao();
            //var r = await this.ValidateAsync(entidade, cancellationToken, propriedade);
            var r = await this.ValidateAsync(entidade, opcoes => opcoes.IncludeProperties(propriedade), cancellationToken);
            return await Task.Run(() => r.Como<ValidacaoResultado>(), cancellationToken);
        }

        public virtual List<PropertyRule> PegarRegras()
        {
            ValidarConfiguracao();
            var regras = CreateDescriptor();


            var propertyInfos = typeof(TEntidade).GetRuntimeProperties();
            var validadores = propertyInfos.SelectMany(inf => regras.GetRulesForMember(inf.Name).Select(r => (PropertyRule)r)).ToList();
            return validadores;

        }
    }
}