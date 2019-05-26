using AcademiaDanca.IO.Dominio.Contexto.Vo;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Funcionario : Notifiable
    {
        public int Id { get; private set; }
        public Nome Nome { get; private set; }
        public string Foto { get; set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Perfil Perfil { get; private set; }
        public int IdPerfil { get; set; }
        public Funcionario(int id, Nome nome, Email email, Cpf cpf, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
        public Funcionario(int id, Nome nome, Email email, Cpf cpf, DateTime dataNascimento, int idPerfil, string foto)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            IdPerfil = idPerfil;
            Foto = foto;
        }

        public Funcionario(int id, string foto)
        {
            Id = id;
            Foto = foto;
        }
        public Funcionario(int id)
        {
            Id = id;
        }
    }
}
