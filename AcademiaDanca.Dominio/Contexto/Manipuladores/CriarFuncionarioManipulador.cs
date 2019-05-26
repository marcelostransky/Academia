using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.FuncionarioComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using AcademiaDanca.IO.Dominio.Contexto.Vo;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores
{
    public class CriarFuncionarioManipulador : Notifiable, IComandoManipulador<CriarFuncionarioComando>
    {
        private readonly IFuncionarioRepositorio _repositorio;
        private readonly ILoginRepositorio _repositorioLogin;
        public CriarFuncionarioManipulador(IFuncionarioRepositorio repositorio, ILoginRepositorio repositorioLogin)
        {
            _repositorio = repositorio;
            _repositorioLogin = repositorioLogin;
        }
        public async Task<IComandoResultado> ManipuladorAsync(CriarFuncionarioComando comando)
        {
            //Validar Login Unico
            if (await _repositorioLogin.CheckLoginAsync(comando.Login))
                AddNotification("Login", "Login informado já está em uso no sistema");

            //Validar Cpf Unico
            if (await _repositorio.CheckCpfAsync(comando.Cpf))
                AddNotification("Cpf", "Cpf informado já está em uso no sistema");

            //Validar Email Unico
            if (await _repositorio.CheckEmailAsync(comando.Email))
                AddNotification("Email", "Email informado já está em uso no sistema");

            // Validar Comando
            comando.Valido();
            AddNotifications(comando.Notifications);
            // Validara Vo
            var email = new Email(comando.Email);
            var nome = new Nome(comando.Nome);
            var cpf = new Cpf(comando.Cpf);

            AddNotifications(email.Notifications);
            AddNotifications(nome.Notifications);
            AddNotifications(cpf.Notifications);



            //Criar a entidade
            var funcionario = new Funcionario(0, nome, email, cpf, comando.DataNascimento, comando.IdPerfil, comando.Foto);
            var credencial = new Credencial(0, comando.Login, comando.Senha);

            AddNotifications(funcionario.Notifications);
            AddNotifications(credencial.Notifications);
            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir os dados

            await _repositorio.SalvarAsync(funcionario, credencial);
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
