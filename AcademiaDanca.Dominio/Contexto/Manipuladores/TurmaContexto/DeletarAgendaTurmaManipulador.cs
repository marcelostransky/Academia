using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.TurmaContexto
{
    public class DeletarAgendaTurmaManipulador : Notifiable, IComandoManipulador<DeletarAgendaTurmaComando>
    {
        private readonly IAgendaRepositorio _repositorio;
        public DeletarAgendaTurmaManipulador(IAgendaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(DeletarAgendaTurmaComando comando)
        {
           

            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);


            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

            await _repositorio.DeletarAsync(comando.IdAgenda);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Deletado com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });
        }
    }
}
