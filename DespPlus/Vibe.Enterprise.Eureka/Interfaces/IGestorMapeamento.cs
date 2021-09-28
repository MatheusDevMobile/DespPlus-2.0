using AutoMapper;
using System.Collections.Generic;

namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface IGestorMapeamento
    {
        IMapper Mapper { get; }
        IReadOnlyList<Profile> PerfisConfigurados { get; }
        void AdicionarPerfil<TPerfil>() where TPerfil : Profile, new();
        void ConfiguracaoAdicional();
    }
}