namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class TipoTelefone
    {
        public TipoTelefone(string desTipoTelefone)
        {
            DesTipoTelefone = desTipoTelefone;
        }
        public int Id { get; private set; }
        public string DesTipoTelefone { get; private set; }
        public bool Status { get; private set; }
    }
}