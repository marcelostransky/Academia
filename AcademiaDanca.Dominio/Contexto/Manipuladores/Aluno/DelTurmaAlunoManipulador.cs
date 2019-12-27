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
    public class DelTurmaAlunoManipulador : Notifiable, IComandoManipulador<DelTurmaAlunoComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        public DelTurmaAlunoManipulador(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(DelTurmaAlunoComando comando)
        {
            //Criar Entidade
            var turmaAluno = new TurmaAluno(comando.IdTurma, comando.IdAluno);
            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var total = await _repositorio.DeletarTurmaAluno(turmaAluno);
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
