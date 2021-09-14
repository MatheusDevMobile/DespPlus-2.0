using System;
using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface IHandleExeptionService
    {
        Task ExeptionNotExpected(Exception ex, string title = "Falha", string message = "Ocorreu um erro não esperado.");
    }
}
