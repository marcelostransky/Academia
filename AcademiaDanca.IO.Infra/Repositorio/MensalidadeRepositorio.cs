using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Infra.Repositorio
{
    public class MensalidadeRepositorio : IMensalidadeRepositorio
    {
        public Task<int> DeletarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditarAsync(Mensalidade mensalidade)
        {
            throw new NotImplementedException();
        }

        public Task<MensalidadesQueryResultado> ObterMensalidadePorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MensalidadesQueryResultado>> ObterMensalidadesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SalvarAsync(Mensalidade mensalidade)
        {
            throw new NotImplementedException();
        }
    }
}
