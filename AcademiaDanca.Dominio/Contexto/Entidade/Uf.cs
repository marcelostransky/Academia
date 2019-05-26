namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Uf
    {
       

        public int Id { get; set; }
        public string DesUf { get; set; }
        public string Sigla { get; set; }
        public Uf(int id, string descricao, string sigla)
        {
            Id = id;
            DesUf = descricao;
            Sigla = sigla;
        }

       
    }
}