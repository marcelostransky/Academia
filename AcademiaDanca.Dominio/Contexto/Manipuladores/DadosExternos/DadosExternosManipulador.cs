﻿using AcademiaDanca.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.DadosExternos.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.DadosExternos
{
    public class DadosExternosManipulador : Notifiable, IComandoManipulador<DadosExternosComando>
    {
        private readonly IAlunoRepositorio _repositorio;
        private readonly IEnderecoRepositorio _repositorioEnd;
        public DadosExternosManipulador(IAlunoRepositorio repositorio, IEnderecoRepositorio repositorioEnd)
        {
            _repositorio = repositorio;
            _repositorioEnd = repositorioEnd;
        }
        public async Task<IComandoResultado> ManipuladorAsync(DadosExternosComando comando)
        {
            foreach (var item in comando.lista)
            {
                DateTime data = ObterData(string.IsNullOrEmpty(item.DataNascimento) ? "01/01/1901" : item.DataNascimento);
                var aluno = new AcademiaDanca.Dominio.Contexto.Entidade.Aluno(0, item.Nome, data, null, null, Guid.NewGuid(), item.Tel1, item.Tel2, "profile.jpg", null, item.CodAluno);
                var id = await _repositorio.SalvarAsync(aluno);

                //Cadastrar Endereco
                var logradouro = new Endereco(item.Endereco, 0, null, null,
                    item.Bairro, item.Cidade, item.Cep, new Uf(0, string.Empty, "SP"));
                var idEnd = await _repositorioEnd.SalvarAsync(logradouro, id);
                //Cadastrar responsavel
                var idResponsavel = 0;
                if (!string.IsNullOrEmpty(item.Filiacao1))
                {
                    var filiacao = new Filiacao(0, item.Filiacao1, 1, item.Tel1);
                    filiacao.AddEmail(new Vo.Email($"{id}_1@academia.com"));
                    idResponsavel = await _repositorio.CheckFiliacaoAsync(filiacao);
                    if (idResponsavel <= 0)
                        idResponsavel = await _repositorio.SalvarFiliacaoAsync(filiacao);
                    _repositorio.SalvarFiliacaoAlunoAsync(id, idResponsavel);
                }
                if (!string.IsNullOrEmpty(item.Filiacao2))
                {
                    var filiacao = new Filiacao(0, item.Filiacao2, 2, item.Tel1);
                    filiacao.AddEmail(new Vo.Email($"{id}_2@academia.com"));
                    idResponsavel = await _repositorio.CheckFiliacaoAsync(filiacao);
                    if (idResponsavel <= 0)
                        idResponsavel = await _repositorio.SalvarFiliacaoAsync(filiacao);
                    _repositorio.SalvarFiliacaoAlunoAsync(id, idResponsavel);
                }
               

                AddNotifications(comando.Notifications);
            }

            //Criar Entidade

            //Validar Comando

            if (Invalid)
            {
                return new ComandoResultado(false, "Por favor, corrija os campos abaixo", Notifications);
            }
            //Persistir Dados

            // Retornar o resultado para tela
            return new ComandoResultado(true, "Aluno cadastrado com sucesso", new
            {
                Id = 1,
                Nome = "sd.Nome",
                Status = true
            });
        }

        private DateTime ObterData(string v)
        {
            var data = Convert.ToDateTime(v);
            if (!v.Equals("01/01/1901"))
            {

                if (data.Year < 1920)
                {
                    data = data.AddYears(100);
                }
            }
            return data;
        }
    }
}
