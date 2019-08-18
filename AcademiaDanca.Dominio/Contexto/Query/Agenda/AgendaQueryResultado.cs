namespace AcademiaDanca.IO.Dominio.Contexto.Query.Agenda
{
    public class AgendaQueryResultado
    {
        public int IdAgenda { get; set; }
        public int IdTurma { get; set; }
        public string CodTurma { get; set; }
        public string DesTurma { get; set; }
        public int IdSala { get; set; }
        public string DesSala { get; set; }
        public int IdDia { get; set; }
        public string SiglaDia { get; set; }
        public object Hora { get; set; }
        public int IdProfessor { get; set; }
        public int IdTipoTurma { get; set; }
        public string Professor { get; set; }
        public string TipoTurma { get; set; }
        public int TotalAluno { get; set; }
        
    }
}
