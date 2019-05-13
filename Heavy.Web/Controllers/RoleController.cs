using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.Models;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    //[Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var res = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = model.RoleName
            });
            if (res.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Add Role Failed!!");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                var res = await _roleManager.DeleteAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Delete role ERRORRRRR!!!");
            }
            ModelState.AddModelError(string.Empty, "Can not find the Role!!!!!");
            //注意 ,错误信息返回时, 使用View(model)方法, 这样才能把错误信息显示出来
            return View(nameof(Index), await _roleManager.Roles.ToListAsync());
        }

    }
}