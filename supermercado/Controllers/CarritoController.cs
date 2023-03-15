using Microsoft.AspNetCore.Mvc;
using supermercado.Herramientas;
using supermercado.Models;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace supermercado.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Index()
        {
            var carrito = ConversorParaSesiones.JsonAobjeto<List<Elemento>>(HttpContext.Session, "carrito");
            ViewBag.carrito = carrito;
            ViewBag.total = carrito.Sum(it=>it.Producto.Precio*it.Cantidad);//Usando LINQ
            return View();
        }
        [Route("Agregar/{id}")]
        public IActionResult Agregar(string id) { 
            ProductoModel prodMod=new ProductoModel();
            //la primera vez y la session es vacia
            if (ConversorParaSesiones.JsonAobjeto<List<Elemento>>(HttpContext.Session,"carrito")==null) {
                List<Elemento> carrito = new List<Elemento>();
                carrito.Add(new Elemento { Producto = prodMod.getProducto(id), Cantidad = 1 });
                ConversorParaSesiones.ObjetoAjson(HttpContext.Session, "carrito", carrito);

            } 
            else { 
                List<Elemento> carrito = ConversorParaSesiones.JsonAobjeto<List<Elemento>>(HttpContext.Session,"carrito");
                int indice = existeProducto(id);
                if (indice > -1) {
                    //el valor es 0,1 o mas
                    carrito[indice].Cantidad++;
                }
                else
                {
                    carrito.Add(new Elemento { Producto = prodMod.getProducto(id), Cantidad = 1 });

                }
                ConversorParaSesiones.ObjetoAjson(HttpContext.Session, "carrito", carrito);
            }

            return RedirectToAction("Index"); 
        }
        private int existeProducto(string id)
        {
            List<Elemento> carrito = ConversorParaSesiones.JsonAobjeto<List<Elemento>>(HttpContext.Session, "carrito");
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].Producto.Id.Equals(id))
                    return i;//Sí existe un rpducto con ese id, retornamos su posición en el carrito
            }
            return -1;//Nunca encontró un producto con el id recivido.
        }
        [Route("Quitar/{id}")]
        public IActionResult Quitar(string id)
        {
            List<Elemento> carrito = ConversorParaSesiones.JsonAobjeto<List<Elemento>>(HttpContext.Session, "carrito");
            int indice = existeProducto(id);
            
            if ((carrito[indice].Cantidad) - 1 <= 0)
            {
                carrito.RemoveAt(indice);
            }
            else
            {
                carrito[indice].Cantidad--;
            }

            
            ConversorParaSesiones.ObjetoAjson(HttpContext.Session, "carrito", carrito);
            return RedirectToAction("Index");
        }
       
        public IActionResult RealizarCompra()
        {
            var carrito = ConversorParaSesiones.JsonAobjeto<List<Elemento>>(HttpContext.Session, "carrito");
            //ViewBag.carrito = carrito;
            ViewBag.total = carrito.Sum(it => it.Producto.Precio * it.Cantidad);//Usando LINQ
            List<Elemento> lcarrito = ConversorParaSesiones.JsonAobjeto<List<Elemento>>(HttpContext.Session, "carrito");
            while (lcarrito.Count > 0)
            {
                lcarrito.RemoveAt(0);
            }
            ConversorParaSesiones.ObjetoAjson(HttpContext.Session, "carrito", lcarrito);
            return View("RealizarCompra"); 
         

            
        }
    }
}
