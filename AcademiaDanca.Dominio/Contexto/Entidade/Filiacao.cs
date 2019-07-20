using AcademiaDanca.IO.Dominio.Contexto.Vo;
using FluentValidator;

namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Filiacao : Notifiable
    {
        public Filiacao(int id, string nome, int idTipoFiliacao, string telefone)
        {
            Id = id;
            Nome = nome;
            IdTipoFiliacao = idTipoFiliacao;
            Telefone = telefone;

        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int IdTipoFiliacao { get; private set; }
        public Cpf Documento { get; set; }
        public Email Email { get; set; }
        public string Telefone { get; private set; }

        public void AddEmail(Email email)
        {
            this.Email = email;
        }
        public void AddDocumento(Cpf documento)
        {
            this.Documento = documento;
        }

    }
}