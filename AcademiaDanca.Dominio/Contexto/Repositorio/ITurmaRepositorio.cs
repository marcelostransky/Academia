﻿using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TurmaComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Query.Agenda;
using AcademiaDanca.IO.Dominio.Contexto.Query.Aluno;
using AcademiaDanca.IO.Dominio.Contexto.Query.Turma;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Repositorio
{
    public interface ITurmaRepositorio
    {
        Task<int> SalvarAsync(Turma turma);
        Task<int> EditarAsync(Turma turma);
        Task<IEnumerable<TurmaQueryResultado>> ObterTodosPorAsync(int? idTurma, int? idProfessor, int? idTipoTurma,  bool? status, int? idUsuario);
        Task<IEnumerable<TurmaQueryResultado>> ObterTodosAsync();
        Task<IEnumerable<TurmaQueryResultado>> ObterTodosPorAsync(int ano);
        Task<TurmaQueryResultado> ObterPorAsync(int id);
        Task<bool> CheckTurmaAsync(Turma turma);
        Task<bool> CheckAlunoTurmaAsync(int id);
        Task<bool> CheckAgendamentoTurmaAsync(int id);
        Task<IEnumerable<AlunoPorNomeQuery>> ObterAlunosPorAsync(int idTurma);
        Task<TurmaQuantitativoQueryResultado> CheckQuantitativoTurmaAsync(int id);
        Task<int> DeletarAsync(int id);
        Task<IEnumerable<DiaQueryResultado>> ObterDiaSemana();
        Task<int> DeletarAlunoAsync(int idAluno, int idTurma);
    }
}
