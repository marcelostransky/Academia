using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class TurmaAluno
    {
        public int IdTurma { get; private set; }
        public int IdAluno { get; private set; }
        public TurmaAluno(int idTurma, int idAluno)
        {
            IdAluno = idAluno;
            IdTurma = IdTurma;
        }
    }
}
