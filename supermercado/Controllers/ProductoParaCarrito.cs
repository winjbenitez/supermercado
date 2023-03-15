using Microsoft.AspNetCore.Mvc;
using supermercado.Models;

namespace supermercado.Controllers
{
    [Route("productos")]
    public class ProductoParaCarrito : Controller
    {
        [Route("index")]
        [Route("/")]
        [Route("")]
        public IActionResult Index()
        {
            var proModel = new ProductoModel();
            ViewBag.productos=proModel.getTodo();
            return View();
        }
    }
}
