using System;
using System.IO;
using System.Linq;
using EcommercePlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EcommercePlatform.Controllers
{
    
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment environment;

        public ProductosController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            

            this.context = context;
            this.environment = environment;
        }

        
        public IActionResult Index()
        {
            return View(context.Productos.ToList());
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            Producto producto = new Producto();
            return EditarAgregar("Agregar", producto);
        }

        [HttpPost]
        public IActionResult Agregar(int id, string nombre, string descripcion, string proveedor,
            IFormFile imagen, float precio = 0)
        {
            Producto producto = new Producto()
            {
                Id = id,
                Nombre = nombre,
                Descripcion = descripcion,
                Proveedor = proveedor,
                Precio = precio,
            };

            if (!Empty(producto))
            {
                producto.UniqueFileImage = AlmacenarImagen(imagen, producto);

                context.Productos.Add(producto);
                context.SaveChanges();
            }
            

            // funcion redireccionar  
            // return RedirectToAction("Index", "Home");

            return View("Index", context.Productos.ToList());
        }

        

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var producto = context.Productos.FirstOrDefault(x => x.Id == id);

            return EditarAgregar("Editar", producto);
        }

        [HttpPost]
        public IActionResult Editar(Producto producto, IFormFile imagen)
        {
            if (imagen != null)
            {
                producto.UniqueFileImage = AlmacenarImagen(imagen, producto);
            }
            
            context.Entry(producto).State = EntityState.Modified;
            context.SaveChanges();

            return View("Index", context.Productos.ToList());
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var producto = context.Productos.FirstOrDefault(x => x.Id == id);
            if (producto != null)
            {
                context.Productos.Remove(producto);
                context.SaveChanges();
            }

            return View("Index", context.Productos.ToList());
        }
        
        // Otros metodos
        public string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        public IActionResult EditarAgregar(string Action, Producto producto)
        {
            ViewData["Action"] = Action;
            return View("EditarAgregar", producto);
        }

        public IActionResult ListarTodo()
        {
            return View(context.Productos.ToList());
        }

        // validacion de campos
        public bool Empty(Producto producto)
        {
            if (producto.Nombre.Equals("") || producto.Descripcion.Equals("") ||
                producto.Proveedor.Equals("") || producto.Precio == 0)
                return true;
            else
                return false;
        }

        public string AlmacenarImagen(IFormFile imagen, Producto producto)
        {
            var uniqueFileName = GetUniqueFileName(imagen.FileName);
            string uploads = Path.Combine(environment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, uniqueFileName);
            imagen.CopyTo(new FileStream(filePath, FileMode.Create));

            return uniqueFileName;
        }
        
    }
}