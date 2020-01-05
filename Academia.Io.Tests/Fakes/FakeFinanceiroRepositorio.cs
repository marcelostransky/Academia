using AcademiaDanca.IO.Dominio.Contexto.Comandos.FinanceiroComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Io.Tests.Fakes
{
    public class FakeFinanceiroRepositorio : IFinanceiroRepositorio
    {
        public async Task<bool> CheckMatriculaExisteAsync(Matricula matricula)
        {
            return false;
        }

        public Task<bool> CheckMatriculaItemTempExisteAsync(MatriculaItemComando comando)
        {
            throw new NotImplementedException();
        }

        public async Task GerarMensalidade(Mensalidade mensalidade)
        {
            var lista = mensalidade.Mensalidades();
            foreach (var mes in lista)
            {
            }
        }

        public async Task<int> MatricularAsync(Matricula matricula)
        {
            return await Task.Run(() => 1);
        }

        public Task<IEnumerable<ItemMatriculaQueryResultado>> ObterItensMatriculaPor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemMatriculaQueryResultado>> ObterMatriculaItensTempPor(Guid idMatriculaGuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<MensalidadesQueryResultado>> ObterMensalidadesPorAlunoAsync(Guid? uifIdAluno, string status, int? ano)
        {
            throw new NotImplementedException();
        }

        public Task<int> RegistrarItemMatricula(MatriculaItem item)
        {
            throw new NotImplementedException();
        }

        public Task<int> RegistrarItemMatriculaTemp(MatriculaItemTemp item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegistrarPagamentoAsync(int idMensalidade, bool pago, double juros)
        {
            throw new NotImplementedException();
        }
    }
}
