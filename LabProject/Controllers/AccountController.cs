using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LabProject.ViewModels;
using LabProject.Models;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using NuGet.Protocol.Plugins;

namespace LabProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityContext _context;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IdentityContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
        return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Username, AdminName = model.Name};
                if(model.Password != model.PasswordConfirm)
                {
                    ModelState.AddModelError("PasswordConfirm", "Не співпадає з паролем вище");
                    return View(model);
                }
                var existUsername = await _context.Users.FirstOrDefaultAsync(c => c.UserName == user.UserName);
                if(existUsername != null)
                {
                    ModelState.AddModelError("Username", "Ім'я користувача вже зайнято");
                    return View(model);
                }
                var existName = await _context.Users.FirstOrDefaultAsync(c => c.AdminName == user.AdminName);
                if (existName != null)
                {
                    ModelState.AddModelError("Name", "Аккаунт такої людини вже існує");
                    return View(model);
                }
                var existEmail = await _context.Users.FirstOrDefaultAsync(c => c.Email == user.Email);
                if (existEmail != null)
                {
                    ModelState.AddModelError("Email", "Аккаунт з такою поштою вже існує");
                    return View(model);
                }
                // додаємо користувача
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка кукі
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index","Cinemas");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                   user = await _userManager.FindByEmailAsync(model.Username);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // перевіряємо, чи належить URL додатку
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Cinemas");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний логін чи(та) пароль");
                }
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // видаляємо аутентифікаційні куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}