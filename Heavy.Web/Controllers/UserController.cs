using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.Data;
using Heavy.Web.Models;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Heavy.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
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
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            var res = await _userManager.CreateAsync(new ApplicationUser
            {
                BirthDate = newUser.BirthDate,
                Country = newUser.Country,
                Email = newUser.Email,
                UserName = newUser.UserName
            }, newUser.Password);

            if (res.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(newUser);
        }
    }
}