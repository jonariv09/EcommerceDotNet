using System.Collections.Generic;

namespace EcommercePlatform.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public double Total { get; set; }
        public List<Producto> Productos { get; set; }

    }
}