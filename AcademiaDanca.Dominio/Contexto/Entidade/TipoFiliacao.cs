namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class TipoFiliacao
    {
        public TipoFiliacao(string desTipoFiliacao)
        {
            this.DesTipoFiliacao = desTipoFiliacao;
        }
        public int Id { get; private set; }
        public string DesTipoFiliacao { get; private set; }
        public bool Status { get; private set; }
    }
}