using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoFiliacaoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoFiliacaoComando.Saida;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores
{
    public class TipoFiliacaoManipulador : Notifiable, IComandoManipulador<CriarTipoFiliacaoComando>
    {
        private readonly ITipoFiliacaoRepositorio _repositorio;
        public TipoFiliacaoManipulador(ITipoFiliacaoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(CriarTipoFiliacaoComando comando)
        {
           
            // Verificar se o nome já existe na base
            if (await _repositorio.CheckNomeAsync(comando.DesTipoFiliacao))
                AddNotification("Nome", "Este Tipo filiação já está em uso");
            // Criar a entidade
            var tipoFiliacao = new TipoFiliacao(comando.DesTipoFiliacao);
            // Validar Comando
            comando.Valido();

            AddNotifications(comando.Notifications);

            if (Invalid)
            {
                return new ComandoResultado(
                   false,
                   "Por favor, corrija os campos abaixo",
                   Notifications);
            }

            // Persistir o cliente
            var id = await _repositorio.SalvarAsync(tipoFiliacao);


            // Retornar o resultado para tela
            return new ComandoResultado(true, "Tipo telefone cadastrado com sucesso", new
            {
                Id = id,
                Nome = tipoFiliacao.DesTipoFiliacao,
                Status = true
            });
        }
    }
}
