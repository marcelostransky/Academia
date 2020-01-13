﻿using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada
{
  public  class EditarTurmaComando:Notifiable,IComando
    {

        public int Id { get; set; }
        public string CodTurma { get; set; }
        public string DesTurma { get; set; }
        public int IdProfessor { get; set; }
        public int IdProfessorAtual { get; set; }
        public int IdTipoTurma { get; set; }
        public int Ano { get; set; }
        public double Valor { get; set; }
        public int AnoAtual { get; set; }
        public int IdTipoTurmaAtual { get; set; }
        public string DesTurmaAtual { get; set; }
        public string CodTurmaAtual { get; set; }
        public double ValorAtual { get; set; }
        public bool Status { get; set; }
        public bool StatusAtual { get; set; }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .HasMinLen(DesTurma, 3, "Descricao", "Descricao deve conter pelo menos 3 caracteres")
               .HasMaxLen(DesTurma, 300, "login", "Descricao deve conter no máximo 300 caracteres")
               .IsTrue(Id > 0, "Id", "Id informado não é válido")
               .IsTrue(Ano > 0, "Ano", "Ano informado não é válido")
               .IsTrue(Valor > 0, "Valor", "Valor informado não é válido")
               .IsNotNull(Valor, "Valor", "Informe o valor do Curso/Turma")
           );
            return Valid;
        }
    }
}