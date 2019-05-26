using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto
{
    public class AlunoManipulador : Notifiable, IComandoManipulador<CriarAlunoComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        public AlunoManipulador(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(CriarAlunoComando comando)
        {

            //Valiadar vo
            var email = new Vo.Email(comando.Email);
            //Criar Entidade
            var aluno = new Aluno(comando.Id, comando.Nome, comando.DataNascimento, null, email, comando.UifId, comando.Telefone, comando.Celular, comando.Foto);

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.SalvarAsync(aluno);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Aluno cadastrado com sucesso", new
            {
                Id = id,
                Nome = aluno.Nome,
                Status = true
            });
        }
    }
}
