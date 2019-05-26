namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Telefone
    {
        public Telefone(int ddd, int codigoPais, int numero, TipoTelefone tipoTelefone)
        {
            CodigoArea = ddd;
            CodigoPais = codigoPais;
            Numero = numero;
            TipoTelefone = tipoTelefone;

        }
        public int CodigoArea { get; private set; }
        public int CodigoPais { get; private set; }
        public int Numero { get; private set; }
        public TipoTelefone TipoTelefone { get; private set; }
    }
}