﻿using FluentValidator;

namespace AcademiaDanca.Dominio.Contexto.Entidade
{
    public class Endereco : Notifiable
    {
        public Endereco(
            string logradouro,
            int id,
            string numero,
            string complemento,
            string bairro,
            string cidade,
            string cep,
            Uf uf)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estado = uf;
        }
        public int Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public int Uf { get; private set; }
        public string Cep { get; private set; }
        public Uf Estado { get; set; }
    }
}