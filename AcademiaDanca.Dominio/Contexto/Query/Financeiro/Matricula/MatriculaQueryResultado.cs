using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Query.Financeiro.Matricula
{
    public class MatriculaQueryResultado
    {
        public AlunoQueryResultado MatriculaAluno { get; set; }
        public AlunoMatriculaQuery MatriculaBase { get; set; }
         public List<ItemMatriculaQueryResultado> MatriculaItens { get; set; }
    }
}
