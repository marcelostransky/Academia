    using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.AlunoComando.Entrada
{
    public class AddEnderecoComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public int IdAluno { get; set; }

        public bool Valido()
        {
            AddNotifications(
                new ValidationContract()
                .HasMinLen(Logradouro, 3, "Logradouro", "O Logradouro deve conter pelo menos 3 caracteres")
                .HasMaxLen(Logradouro, 150, "Logradouro", "O Logradouro deve conter no máximo 150 caracteres")
                .IsNotNull(Logradouro, "Logradouro", "O Logradouro não pode ser nulo")

                .HasMaxLen(Numero, 9, "Numero", "O Numero deve conter no máximo 9 caracteres")

                .HasMinLen(Bairro, 3, "Bairro", "O Bairro deve conter pelo menos 3 caracteres")
                .HasMaxLen(Bairro, 45, "Bairro", "O Bairro deve conter no máximo 45 caracteres")
                .IsNotNull(Bairro, "Bairro", "O bairro não pode ser nulo")
                                                                                                    
                .HasMinLen(Cidade, 3, "Cidade", "A Cidade deve conter pelo menos 3 caracteres")
                .HasMaxLen(Cidade, 150, "Cidade", "A Cidade deve conter no máximo 150 caracteres")
                .IsNotNull(Cidade, "Cidade", "A Cidade não pode ser nulo")

                .HasMinLen(Uf , 2, "Uf", "Estado deve conter apenas 2 letras.")

                .HasMaxLen(Cep, 10, "Cep", "O Cep deve conter no máximo 150 caracteres")
                .IsNotNull(Cep, "Cep", "O Cep não pode ser nulo")

                );
            return Valid;
        }
    }
}
