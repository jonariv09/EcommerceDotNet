using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommercePlatform.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult loadData()
        {
            ViewBag.ListUsuarios = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            ViewBag.ListRoles = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
            var ListaRoles = _roleManager.Roles.ToList();
            
            return View("Index", ListaRoles);
        }

        [Authorize(Roles="Admin")]
        public IActionResult Index()
        {
            return loadData();
        }

        [HttpPost]
        public IActionResult Index(string Id)
        {
            ViewBag.ListUsuarios = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            ViewBag.ListRoles = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
            var ListaRoles = _roleManager.Roles.ToList();
            ViewBag.Id = Id;
            
            return View(ListaRoles);
        }

        // POST api/RoleModel
        [HttpPost]
        public async Task<IActionResult> AddNewRole(string roleName)
        {
            var _role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(_role);
            return loadData();
        }

        // PUT api/RoleModel/5
        [HttpPost]
        public async Task<IActionResult> AssignUserRole(string userId, string roleId)
        {
            var _user = await _userManager.FindByIdAsync(userId);
            var _role = await _roleManager.FindByIdAsync(roleId);
            var result = await _userManager.AddToRoleAsync(_user, _role.Name);

            return loadData();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(string iduser, string namerole)
        {
            var IdUser = HttpContext.Request.Query["iduser"].ToString();
            var NameRole = HttpContext.Request.Query["namerole"].ToString();
            var user = await _userManager.FindByIdAsync(IdUser);
            await _userManager.RemoveFromRoleAsync(user, NameRole);
            
            return loadData();
        }

    }
}