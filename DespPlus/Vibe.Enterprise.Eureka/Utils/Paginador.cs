using System;
using PowerDev.Enterprise.Eureka.Interfaces;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public class Paginador: IPaginador
    {
        public Paginador(int qtdRegistrosPorPagina)
        {
            RegistrosPorPagina = qtdRegistrosPorPagina;
        }

        private int _pagina;
        public int Pagina
        {
            get
            {
                return _pagina;
            }
            set
            {
                _pagina = value;
                if (_pagina > TotalPaginas && TotalPaginas > 0)
                    _pagina = TotalPaginas;
            }
        }
        public int TotalPaginas { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int TotalRegistros { get; set; }

        public void ProximaPagina()
        {
            Pagina++;
        }
        public override string ToString()
        {
            return String.Format("Pág.: {0} de {1} [Tam.Pág.:{2} Total: {3} registros]", Pagina, TotalPaginas, RegistrosPorPagina, TotalRegistros);
        }
    }
}