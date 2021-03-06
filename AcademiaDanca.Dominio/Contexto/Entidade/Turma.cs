﻿using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using FluentValidator;
using System.Collections.Generic;
using System.Linq;

namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Turma : Notifiable
    {
        private readonly IList<Aluno> _alunos;
        private readonly IList<Agenda> _agendas;


        public int Id { get; private set; }
        public string CodTurma { get; private set; }
        public string DesTurma { get; private set; }
        public Funcionario Professor { get; private set; }
        public IReadOnlyCollection<Aluno> Alunos => _alunos.ToArray();
        public IReadOnlyCollection<Agenda> Agendamentos => _agendas.ToArray();
        public TurmaTipo TurmaTipo { get; private set; }

        public bool Status { get; private set; }
        public Turma(int id, string codTurma, string desTurma, Funcionario professor,
            TurmaTipo turmaTipo, bool status)
        {
            Id = id;
            DesTurma = desTurma;
            Professor = professor;
            TurmaTipo = turmaTipo;
            CodTurma = codTurma;

            Status = status;
            _alunos = new List<Aluno>();
            _agendas = new List<Agenda>();
        }

        public Turma(int idTurma)
        {
            Id = idTurma;
            _alunos = new List<Aluno>();
            _agendas = new List<Agenda>();
        }

        public void AddAluno(Aluno aluno)
        {
            _alunos.Add(aluno);
        }

    }
}