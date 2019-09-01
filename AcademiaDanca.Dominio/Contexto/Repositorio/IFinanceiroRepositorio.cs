using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface IFinanceiroRepositorio
    {
        Task<int> MatricularAsync(Matricula matricula);
        Task<bool> CheckMatriculaExisteAsync(Matricula matricula);
        Task<List<MensalidadesQueryResultado>> ObterMensalidadesPorAlunoAsync(Guid? uifIdAluno, string status, int? ano);
        Task GerarMensalidade(Mensalidade mensalidade);
        Task<bool> RegistrarPagamentoAsync(int idMensalidade, bool pago, double juros);
        
    }
}
