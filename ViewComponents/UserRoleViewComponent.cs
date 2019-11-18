using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommercePlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePlatform.ViewComponents
{
    public class UserRoleViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleViewComponent(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            IdentityRole Rol = await _roleManager.FindByIdAsync(Id);
            var ListUsers = await _userManager.GetUsersInRoleAsync(Rol.Name);
            ViewBag.NameRole = Rol.Name;
            return View(await _userManager.GetUsersInRoleAsync(Rol.Name));
        }
    }
}