using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Manga.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaExpressController.Areas.Identity.Pages.CarritoUsuarioP
{
    [Authorize(Roles = "UsuarioCompra")]
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

            List<MangaUsuario> lista = new List<MangaUsuario>();
            lista = await _context.MangaUsuarios.ToListAsync();


            var myItem = (from c in _context.MangaUsuarios where c.MID == MangaUsuario.MID && c.UID == MangaUsuario.UID select c).FirstOrDefault();

            if (myItem == null)
            {
                _context.MangaUsuarios.Add(MangaUsuario);
                await _context.SaveChangesAsync();
                return RedirectToPage("/CarritoUsuarioP/CarritoM");
            }
          

            if (MangaM == null)
            {
                return NotFound();
            }

            return RedirectToPage("/CarritoUsuarioP/CarritoM");

        }


    }
}
