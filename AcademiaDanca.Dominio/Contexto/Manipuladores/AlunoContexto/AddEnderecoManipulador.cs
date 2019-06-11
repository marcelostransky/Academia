using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.Aluno.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.AlunoContexto
{
    public class AddEnderecoManipulador : Notifiable, IComandoManipulador<AddEnderecoComando>
    {
        private readonly IEnderecoRepositorio _repositorio;
        public AddEnderecoManipulador(IEnderecoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IComandoResultado> ManipuladorAsync(AddEnderecoComando comando)
        {

            //Criar Entidade
            var logradouro = new Endereco(comando.Logradouro, comando.Id, comando.Numero, comando.Complemento,
                comando.Bairro, comando.Cidade, comando.Cep, new Uf(0, string.Empty, comando.Uf));

            //Validar Comando
            comando.Valido();
            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados
            var id = await _repositorio.SalvarAsync(logradouro, comando.IdAluno);
            // Retornar o resultado para tela
            return new ComandoResultado(true, "Endereço cadastrado com sucesso", new
            {
                Id = id,
                Nome = logradouro.Logradouro,
                Status = true
            });
        }
    }
}
