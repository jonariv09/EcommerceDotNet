// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using EcommercePlatform.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace EcommercePlatform.ViewComponents
// {
//     public class FacturaUsuarioViewComponent : ViewComponent
//     {
//         public IViewComponentResult Invoke(int? id)
//         {
//             DataBaseContext context = new DataBaseContext();
            
//             foreach(var usuario in context.ListUsuarios)
//             {
//                 if(usuario.Id == id)
//                 {
//                     ViewBag.NombreCompleto = usuario.Nombres + " " +usuario.Apellidos;
//                     return View(usuario.Facturas);
//                 }
//             }
            
//             return View(null);
//         }
//     }
// }
