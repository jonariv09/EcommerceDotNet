using System.Linq;
using EcommercePlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EcommercePlatform.ViewModels;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommercePlatform.Controllers
{
    public class UsuariosController : Controller    
    {
        private readonly ApplicationDbContext context;
        // DataBaseContext _context = new DataBaseContext();

        public UsuariosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "index")]
        public async Task<IActionResult> Index()
        {
            return View(await context.Usuarios.ToListAsync());
        }

        // public IActionResult Index(int? id = 0)
        // {
        //     ViewBag.IdSeleccionado = id;
        //     return View(_context.ListUsuarios);
        // }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult Agregar()
        {
            Usuario usuario = new Usuario();
            return EditarAgregar("Agregar", usuario);
        }

        [HttpPost]
        
        public async Task<IActionResult> Agregar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }

            return View("Index", await context.Usuarios.ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult Editar(int id)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Id == id);
            
            return EditarAgregar("Editar", usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                context.Entry(usuario).State = EntityState.Modified;
                context.SaveChanges();
            }

            return View("Index", await context.Usuarios.ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario.Id == id)
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }

            return View("Index", await context.Usuarios.ToListAsync());
        }

        public IActionResult EditarAgregar(string Action, Usuario usuario)
        {
            ViewData["Action"] = Action;
            return View("EditarAgregar", usuario);
        }
        
        public IActionResult FacturasUsuarios()
        {
            // List<FacturaUsuarioViewModel> ListFacturaUsuario = new List<FacturaUsuarioViewModel>();
            // var usuarios = _context.ListUsuarios;

            // foreach(Usuario usuario in usuarios)
            // {
            //     foreach(Factura factura in usuario.Facturas)
            //     {
            //         FacturaUsuarioViewModel facturaUsuarioViewModel = new FacturaUsuarioViewModel();
            //         facturaUsuarioViewModel.NombresUsuario = usuario.Nombres;
            //         facturaUsuarioViewModel.ApellidosUsuario = usuario.Apellidos;
            //         facturaUsuarioViewModel.Correo = usuario.Correo;
            //         facturaUsuarioViewModel.Fecha = factura.Fecha;
            //         facturaUsuarioViewModel.Total = factura.Total;

            //         ListFacturaUsuario.Add(facturaUsuarioViewModel);
            //     }
            // }

            
            // return View(ListFacturaUsuario);
            return View(null);
        }
    }
}