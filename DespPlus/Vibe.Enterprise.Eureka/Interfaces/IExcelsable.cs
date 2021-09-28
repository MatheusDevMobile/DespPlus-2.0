using System;
using System.Collections;

namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface IExcelsable//<T> where T :IRegistroExcel
    {
        IList Registros { get; set; }
        Type TipoRegsitros { get; }
    }

}
