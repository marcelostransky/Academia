using AcademiaDanca.IO.Compartilhado;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.ConfiguracaoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Entidade;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.Dominio.Contexto.Manipuladores.Configuracao
{
    public class EditarParametroManipulador : Notifiable, IComandoManipulador<ParametroComando>
    {
        private readonly IParametroRepositorio _repositorio;
        public EditarParametroManipulador(IParametroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IComandoResultado> ManipuladorAsync(ParametroComando comando)
        {
            comando.Valido();

            AddNotifications(comando.Notifications);


            if (Invalid)
            {
                return new ComandoResultado(
                  false,
                  "Por favor, corrija os campos abaixo",
                  Notifications);
            }
            //Persistir Dados
            foreach (var item in comando.Parametros)
            {
                if (item.Valor != item.ValorAtual)
                {
                    try
                    {
                        await _repositorio.Editar(new ParametroItem { Chave = item.Chave, Valor = item.Valor });

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                       }
            }


            // Retornar o resultado para tela
            return new ComandoResultado(true, "Dados atualizado com sucesso", new
            {
                Id = 0,
                Nome = "",
                Status = true
            });

        }
    }
}
