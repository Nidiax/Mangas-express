using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaExpressController.Areas.Identity.Pages.AdminP.UsuariosP
{
    [AllowAnonymous]
    public class DeleteModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public DeleteModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }
        



        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (Usuario == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {

                return NotFound();
            }
            // Mangas = await _context.MangaUsuarios.Where(C => C.UID == id);
            //List<MangaUsuario> list = new List<MangaUsuario>();
            /*list = _context.MangaUsuarios.ToList();

            foreach (var item in list)
            {
                if (item.UID == id )
                {
                    _context.MangaUsuarios.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }*/

            Usuario = await _context.Users.FindAsync(id);

            if (Usuario != null)
            {
                _context.Users.Remove(Usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

/*
  public async Task<IActionResult> OnPostAsync( )
        {
            //var userId = await _userManager.GetUserIdAsync(user); para traer el Id del usuario Logueado
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ID = currentUserID;
            //Manga.UID = currentUserID;

           
           // Manga = await mangaContext.MangaUsuarios.Where(C => C.MID == id && C.UID == ID);

            var myItem = (from c in mangaContext.MangaUsuarios where c.MID == Manga.MID && c.UID == ID select c).FirstOrDefault();
            if (Manga != null)
            {
                    
                    mangaContext.MangaUsuarios.Remove(myItem);
                    await mangaContext.SaveChangesAsync();
                    return RedirectToPage("/CarritoUsuarioP/CarritoM");

            }



           

            return RedirectToPage("/CarritoUsuarioP/CarritoM");

        }
 */