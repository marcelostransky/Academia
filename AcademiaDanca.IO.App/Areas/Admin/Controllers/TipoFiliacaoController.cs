using AcademiaDanca.IO.Compartilhado.Comando;
using AcademiaDanca.IO.Dominio.Contexto.Comandos.TipoFiliacaoComando.Entrada;
using AcademiaDanca.IO.Dominio.Contexto.Manipuladores;
using AcademiaDanca.IO.Dominio.Contexto.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TipoFiliacaoController : Controller
    {
        private readonly ITipoFiliacaoRepositorio _repositorio;
        private readonly TipoFiliacaoManipulador _manipulador;

        public TipoFiliacaoController(ITipoFiliacaoRepositorio repositorio, TipoFiliacaoManipulador manipulador)
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
            var comando = new CriarTipoFiliacaoComando();
            comando.DesTipoFiliacao = nome;
            var resultado = await _manipulador.ManipuladorAsync(comando);
            return resultado;
        }
    }
}