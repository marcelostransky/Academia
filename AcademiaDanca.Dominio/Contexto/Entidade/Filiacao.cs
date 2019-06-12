using AcademiaDanca.IO.Dominio.Contexto.Vo;

namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Filiacao
    {
        public Filiacao(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public TipoFiliacao TipoFiliacao { get; private set; }
    }
}