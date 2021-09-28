namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface IEntidade : IElementoRegistro
    {
        bool EstaAtiva { get; }
        void AtivarDesativar();
        bool Equals(IEntidade obj);
    }
}