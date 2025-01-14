using LiberacionProductoWeb.Models.CertCatalogosViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiberacionProductoWeb.Controllers
{
    public class CertSistemasController : Controller
    {
        // GET: CertSistemas
        public IActionResult Sistemas()
        {
            UnidadMedidaViewModel _unidadMedidaVM = new UnidadMedidaViewModel();
            return View(_unidadMedidaVM);
        }


    }
}
