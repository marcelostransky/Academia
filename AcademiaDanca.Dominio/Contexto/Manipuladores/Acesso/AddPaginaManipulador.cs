using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Acesso.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Acesso
{
    public class AddPaginaManipulador : Notifiable, IComandoManipulador<AddPaginaComando>
    {
        private readonly IAcessoRepositorio _repositorio;
        public AddPaginaManipulador(IAcessoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(AddPaginaComando comando)
        {
            //Criar Entidade
            var pagina = new Pagina(comando.Id, comando.DesPagina);

            //Validar Turma/Aluno Unico
            if (await _repositorio.CheckPaginaAsync(pagina.DesPagina))
                AddNotification("Descricao", "Pagina já cadastrada no sistema");

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var total = await _repositorio.SalvarAsync(pagina);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Pagina cadastrado com sucesso", new
            {
                Id = total,
                Nome = "OK",
                Status = true
            });
        }
    }
}
