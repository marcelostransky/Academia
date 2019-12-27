using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Aluno
{
    public class AddTurmaAlunoManipulador : Notifiable, IComandoManipulador<AddTurmaComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        public AddTurmaAlunoManipulador(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(AddTurmaComando comando)
        {

            //Criar Entidade
            var turmaAluno = new TurmaAluno(comando.IdTurma, comando.IdAluno);

            //Validar Turma/Aluno Unico
            if (await _repositorio.CheckTurmaAlunoAsync(turmaAluno))
                AddNotification("Turma", "Aluno já está cadastrado para turma informada");

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var total = await _repositorio.SalvarTurmaAsync(turmaAluno);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Turma/Aluno cadastrado com sucesso", new
            {
                Id = total,
                Nome = "OK",
                Status = true
            });
        }
    }
}
