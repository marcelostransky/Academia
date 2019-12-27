using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Aluno
{
    public class EditarAlunoManipulador : Notifiable, IComandoManipulador<EditarAlunoComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        public EditarAlunoManipulador(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(EditarAlunoComando comando)
        {
          
            //Valiadar vo
            var email = new Vo.Email(comando.Email);
            AddNotifications(email.Notifications);
            if (!string.IsNullOrEmpty(comando.Cpf))
            {
                var cpf = new Vo.Cpf(comando.Cpf);
                AddNotifications(cpf.Notifications);
            }

            //Criar Entidade
            var aluno = new AcademiaDanca.Dominio.Contexto.Entidade.Aluno(comando.Id, comando.Nome, comando.DataNascimento, null, email, comando.UifId, comando.Telefone, comando.Celular, null, comando.Cpf);

            AddNotifications(aluno.Notifications);

            //Validar ocorreu mudanca nos dados
            var alunoQuery = await _repositorio.ObterPorAsync(comando.Id);
            var alunoAtual = new AcademiaDanca.Dominio.Contexto.Entidade.Aluno(alunoQuery.AlunoId, alunoQuery.AlunoNome, alunoQuery.AlunodataNascimento, null,
                new Vo.Email(alunoQuery.AlunoEmail), new Guid(alunoQuery.AlunoGuid), alunoQuery.AlunoTelefone, alunoQuery.AlunoCelular, null, alunoQuery.AlunoCpf);
           
            if (!aluno.Equals(alunoAtual))
            {
                AddNotification("", "Sem dados para atualizar no momento.");
            }

            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }

            //Persistir os dados

            await _repositorio.Editar(aluno);

            // Retornar o resultado para tela
            return new ComandoResultado(true, "Funcionario cadastrado com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });
        }
    }
}
