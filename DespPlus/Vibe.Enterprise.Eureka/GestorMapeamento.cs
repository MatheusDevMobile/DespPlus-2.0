using AutoMapper;
using PowerDev.Enterprise.Eureka.Validadores;
using System.Collections.Generic;
using PowerDev.Enterprise.Eureka.Interfaces;

namespace PowerDev.Enterprise.Eureka
{
    public abstract class GestorMapeamento<T> : IGestorMapeamento where T : IGestorMapeamento, new()
    {
        public static IGestorMapeamento Instancia { get; protected set; }
        public static void Inicializar()
        {
            if (Instancia != null)
                return;

            Instancia = new T();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ValidadorPerfilMapeamento>();

                foreach (var p in Instancia.PerfisConfigurados)
                {
                    cfg.AddProfile(p);
                }
            });
            Instancia.ConfiguracaoAdicional();
            ((GestorMapeamento<T>)Instancia).Mapper = config.CreateMapper();
        }
        protected GestorMapeamento()
        {
        }

        protected List<Profile> listaPerfils = new List<Profile>();
        public IMapper Mapper { get; protected set; }
        public IReadOnlyList<Profile> PerfisConfigurados => listaPerfils.AsReadOnly();

        public void AdicionarPerfil<TPerfil>() where TPerfil : Profile, new()
        {
            listaPerfils.Add(new TPerfil());
        }
        public virtual void ConfiguracaoAdicional()
        {
        }
    }
}