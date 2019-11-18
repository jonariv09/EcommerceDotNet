using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePlatform.Models
{
    public class Usuario
    {
        [HiddenInput]
        public int Id { get; set; }
        public string LoginUserName { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Tarjeta { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Factura> Facturas { get; set; }
    }
}