using System.Collections.Generic;
using System.Linq;

namespace supermercado.Models
{
    public class ProductoModel
    {
        private List<Producto> productos;
        public ProductoModel()
        {
            productos = new List<Producto>() {
                new Producto{
                    Id = "1",
                    Nombre = "Atun VanCamps",
                    Precio = 12,
                    Imagen = "atun.jpg",
                },
                new Producto{
                    Id = "2",
                    Nombre = "Queso Menonita",
                    Precio = 45,
                    Imagen = "queso.jpg",
                },
                //new Producto{
                //    Id = "prod03",
                //    Nombre = "Galleta",
                //    Precio = 24,
                //    Imagen = "724e1d82c2c537737c11e0219a9fd238.png",
                //},new Producto{
                //    Id = "prod04",
                //    Nombre = "Mapache",
                //    Precio = 200,
                //    Imagen = "uno.jpeg",
                //}
            };
        }
        //Bienvenidos a LINQ
        public List<Producto> getTodo()
        {
            return productos;
        }
        public Producto getProducto(string id)
        {
            return productos.Single(p => p.Id.Equals(id));
        }
    }
}

