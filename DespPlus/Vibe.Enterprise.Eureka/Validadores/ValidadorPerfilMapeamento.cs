﻿using AutoMapper;
using FluentValidation.Results;

namespace PowerDev.Enterprise.Eureka.Validadores
{
    public class ValidadorPerfilMapeamento : Profile
    {
        public ValidadorPerfilMapeamento()
        {
            CreateMap<ValidationResult, ValidacaoResultado>().ForMember(d => d.Erros, op => op.MapFrom(o => o.Errors));
            CreateMap<ValidationFailure, ErroValidacao>().ForMember(d => d.Mensagem, op => op.MapFrom(o => o.ErrorMessage))
                                                         .ForMember(d => d.NomePropriedade, op => op.MapFrom(o => o.PropertyName));
        }
    }
}
