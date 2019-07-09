using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IFinanceiroRepositorio
    {
        Task<int> MatricularAssyncAsync(Matricula matricula);
        Task<bool> CheckMatriculaExisteAsync(Matricula matricula);
    }
}
