using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoTelefoneComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoTelefoneComando.Saida;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores
{
    public class TipoTelefoneManipulador : Notifiable, IComandoManipulador<CreateTipoTelefoneComando>
    {
        private readonly ITipoTelefoneRepositorio _repositorio;
        public TipoTelefoneManipulador(ITipoTelefoneRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IComandoResultado> ManipuladorAsync(CreateTipoTelefoneComando comando)
        {
            // Verificar se o nome já existe na base
            if (await _repositorio.CheckNomeTipoTelefone(comando.DesTipoTelefone))
                AddNotification("DesTipoTelefone", "Este Tipo de telefone já está em uso");
            // Criar a entidade
            var tipoTelefone = new TipoTelefone(comando.DesTipoTelefone);
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
            var id = await _repositorio.SalvarAsync(tipoTelefone);


            // Retornar o resultado para tela
            return new ComandoResultado(true, "Tipo telefone cadastrado com sucesso", new
            {
                Id = id,
                Nome = tipoTelefone.DesTipoTelefone,
                Status = true
            });
        }
    }
}
