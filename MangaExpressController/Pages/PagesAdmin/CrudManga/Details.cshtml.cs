using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manga.Models;
using MangaExpressController.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MangaExpressController.Pages.PagesAdmin.CrudManga
{
    [Authorize(Roles ="UsuarioCompra")]
    public class DetailsModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public DetailsModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public MangaM MangaM { get; set; }
        [BindProperty]
        public MangaUsuario MangaUsuario { get; set; }
        [BindProperty]
        public int MangaId { get; set; }

        [BindProperty]
        public int UsuarioId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MangaM = await _context.Mangas.FirstOrDefaultAsync(m => m.Id == id);

            if (MangaM == null)
            {
                return NotFound();
            }
            return Page();
        }

        //METODO PARA LLENAR LA TABLA DE MUCHOS A MUCHOS DE MANGAUSUARIO
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            //var userId = await _userManager.GetUserIdAsync(user); para traer el Id del usuario Logueado
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            MangaUsuario.UID = currentUserID;
            _context.MangaUsuarios.Add(MangaUsuario);
            await _context.SaveChangesAsync();
            if (MangaM == null)
            {
                return NotFound();
            }
            return RedirectToPage("./PageUsuario/Carrito");
        }




    }
}
