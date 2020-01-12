using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada.Com_Matricula;
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
        Task<int> RegistrarItemMatricula(MatriculaItem item);
        Task<int> RegistrarItemMatriculaTemp(MatriculaItemTemp item);
        Task<IEnumerable<ItemMatriculaQueryResultado>> ObterItensMatriculaPor(int id);
        Task<bool> CheckMatriculaItemTempExisteAsync(MatriculaItemComando comando);
        Task<IEnumerable<ItemMatriculaQueryResultado>> ObterMatriculaItensTempPor(Guid idMatriculaGuid);
        Task<int> DeletarItemMatriculaTemp(string idMatriculaGuid, int idTurma);
        Task AtualizaItemMatriculaTemp(MatriculaItemTemp matriculaItemTemp);
    }
}
