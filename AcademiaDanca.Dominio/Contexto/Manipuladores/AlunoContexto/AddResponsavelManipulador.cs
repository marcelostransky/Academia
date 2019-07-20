using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto
{
    public class AddResponsavelManipulador : Notifiable, IComandoManipulador<AddFiliacaoComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        public AddResponsavelManipulador(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(AddFiliacaoComando comando)
        {


            //Criar Entidade
            var filiacao = new Filiacao(comando.Id, comando.Nome, comando.TipoFiliacao, comando.Telefone);


            //Valiadar vo

            filiacao.Email = new Vo.Email(comando.Email);
            if (!string.IsNullOrEmpty(comando.Documento))
                filiacao.Documento = new Vo.Cpf(comando.Documento);

            //check responsavel Existe
            var id = await _repositorio.CheckFiliacaoAsync(filiacao);
            if (id <= 0)
                id = await _repositorio.SalvarFiliacaoAsync(filiacao);
            //Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var inserido = _repositorio.SalvarFiliacaoAlunoAsync(comando.IdAluno, id);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Responsável cadastrado com sucesso", new
            {
                Id = id,
                Nome = filiacao.Nome,
                Status = true
            });
        }
    }
}
