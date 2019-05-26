namespace AcademiaDanca.IO.Dominio.Contexto.Entidade
{
    public class Perfil
    {
        public Perfil(int id, string desPerfil)
        {
            Id = id;
            DesPerfil = desPerfil;
        }
        public int Id { get;private set; }
        public string DesPerfil { get;private set; }
    }
}
