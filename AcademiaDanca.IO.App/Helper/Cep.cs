using AcademiaDanca.IO.App.Models;
using Correios.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Helper
{
    public class Cep
    {
        public async Task<CepRetornoModel> ObterLogradouroAsync(string cep)
        {
            CepRetornoModel cepRetornoModel = new CepRetornoModel();
            using (var ws = new WsCorreios.AtendeClienteClient())
            {
                var resultado = await ws.consultaCEPAsync(cep);
                cepRetornoModel.Estado = resultado.@return.uf;
                cepRetornoModel.Cidade = resultado.@return.cidade;
                cepRetornoModel.Bairro = resultado.@return.bairro;
                cepRetornoModel.Rua = resultado.@return.end;
                cepRetornoModel.Complemento = resultado.@return.complemento2;

            }
            

            return cepRetornoModel;
        }

    }
}
