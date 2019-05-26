using System.Threading.Tasks;
using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoTelefoneComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TipotelefoneController : Controller
    {
        private readonly ITipoTelefoneRepositorio _repositorio;
        private readonly TipoTelefoneManipulador _manipulador;

        public TipotelefoneController(ITipoTelefoneRepositorio repositorio, TipoTelefoneManipulador manipulador)
        {
            _repositorio = repositorio;
            _manipulador = manipulador;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IComandoResultado> Criar(string nome)
        {
            var resultado = await _manipulador.ManipuladorAsync(new CreateTipoTelefoneComando { DesTipoTelefone = nome });
            return resultado;
        }
    }
}