using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommercePlatform.Models
{
    public class Producto
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Proveedor { get; set; }
        public float Precio { get; set; }
        [HiddenInput]   
        public string UniqueFileImage { get; set; }
        
        public bool Deseo { get; set; }
    }
}

