using AcademiaDanca.IO.Compartilhado.Comando;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademiaDanca.IO.Dominio.Contexto.Comandos.DadosExternos.Entrada
{
    public class DadosExternosComando : Notifiable, IComando
    {
        public string CodAluno { get; set; }
        public string Nome { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Endereco { get; set; }
        public string Filiacao1 { get; set; }
        public string Filiacao2 { get; set; }
        public string DataNascimento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public List<DadosExternosComando> lista { get; set; }
        public DadosExternosComando()
        {
            lista = new List<DadosExternosComando>();
        }

        public bool Valido()
        {
            AddNotifications(new ValidationContract()
               .IsTrue(lista.Count > 0, "Lista", "Lista vazia"));
            return Valid;
        }
    }
}
