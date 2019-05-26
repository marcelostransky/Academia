using AcademiaDanca.IO.Dominio.Contexto.Vo;

namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Filiacao
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public TipoFiliacao TipoFiliacao { get; private set; }
    }
}