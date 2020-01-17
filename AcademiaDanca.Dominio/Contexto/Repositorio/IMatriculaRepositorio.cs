using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Matricula;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IMatriculaRepositorio
    {
        Task<MatriculaQueryResultado> ObterMatriculaCompletoAsync(Guid id);
    }
}
