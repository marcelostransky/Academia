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

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto
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

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.SalvarTurmaAsync(turmaAluno);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Aluno cadastrado com sucesso", new
            {
                Id = id,
                //Nome = aluno.Nome,
                Status = true
            });
        }
    }
}
