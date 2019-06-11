
using AcademiaDanca.IO.Dominio.Contexto.Vo;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Aluno : Notifiable
    {
        public Aluno(
            int id,
            string nome,
            DateTime dataNascimento,
            Endereco endereco,
            Email email)
        {
            _turmas = new List<Turma>();
            _filiacoes = new List<Filiacao>();
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Endereco = endereco;
            Id = id;
            Email = email;
        }
        public Aluno(
            int id,
            string nome,
            DateTime dataNascimento,
            Endereco endereco,
            Email email, Guid uifId
            , string telefone, string celular, string foto)
        {
            _turmas = new List<Turma>();
            _filiacoes = new List<Filiacao>();
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Endereco = endereco;
            Id = id;
            Email = email;
            UifId = uifId;
            Telefone = telefone;
            Celular = celular;
            Foto = foto;
        }

        public Aluno(int id, string foto)
        {
            Id = id;
            Foto = foto;
        }

        private readonly IList<Turma> _turmas;
        private readonly IList<Filiacao> _filiacoes;
        public int Id { get; private set; }
        public Guid UifId { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public string Foto { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Endereco Endereco { get; private set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public IReadOnlyCollection<Filiacao> Filiacoes => _filiacoes.ToArray();
        public IReadOnlyCollection<Turma> Turmas => _turmas.ToArray();


        public void AddFiliacao(Filiacao filiacao)
        {
            _filiacoes.Add(filiacao);
        }

        public void AddTurma(Turma turma)
        {
            _turmas.Add(turma);
        }
    }
}
