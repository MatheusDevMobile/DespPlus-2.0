namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface IPaginador
    {
        int Pagina { get; set; }
        int TotalPaginas { get; set; }
        int RegistrosPorPagina { get; set; }
        int TotalRegistros { get; set; }
    }
}