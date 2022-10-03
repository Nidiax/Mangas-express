using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace MangaExpressController.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _aplicationAppDbCOntext;
        // private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,//CUANDO EL BOTON PARA REGISTRARME ME ENVIA AQUI
            ILogger<RegisterModel> logger,
           ApplicationDbContext aplicationAppDbContext)
        //IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _aplicationAppDbCOntext = aplicationAppDbContext;
            //emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        
        public string ReturnUrl { get; set; }
        public IList<Rol> Roles { get; set; }
        public class InputModel
        {
            [Required]
            [StringLength(15, ErrorMessage = "El {0} debe tener al menos {2} y de máximo {1} caracteres.", MinimumLength = 6)]
            [Display(Name = "Nombre de Usuario")]
            public string UserName { get; set; } //LEE EL NOMBRE DEL USUARIO

            [Required]
            [EmailAddress]
            [Display(Name = "Correo electrónico")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y de máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación de la misma no coinciden.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {

                var user = new Usuario { UserName = Input.UserName, Email = Input.Email };
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, Input.Password);//AQUI SE CREA EL USUARIO

              
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    
                    await _userManager.AddToRoleAsync(user, "Administrador"); 
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToPage("./Login");
        }
    }
}
