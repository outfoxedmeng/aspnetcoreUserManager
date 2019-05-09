﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.Data;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Heavy.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var users = _userManager.Users.Select(x => new UserIndexViewModel
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName
            }).ToList();


            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            // 这里可以使用异步方法来替代_userManager.Users.Select
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Delete User Failed!!!");
            }
            ModelState.AddModelError(string.Empty, "Can't find the specified user!!!");
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

    }
}