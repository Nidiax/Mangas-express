using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Manga.Models;
using MangaExpressController.Areas.Identity.Pages.AdminP.MangasP;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaExpressController.Areas.Identity.Pages.CarritoUsuarioP
{
    [Authorize(Roles = "UsuarioCompra")]
    public class CarritoMModel : PageModel
    {
        private ApplicationDbContext mangaContext;

        [BindProperty]
        public MangaUsuario Manga { get; set; }
        public IList<MangaM> Mangas { get; set; }

        public IList<MangaUsuario> lista { get; set; }
        public CarritoMModel(/*ILogger<IndexModel> logger,*/ ApplicationDbContext _context)
        {
            // _logger = logger;
            mangaContext = _context;
        }

        //[Authorize(Roles = "UsuarioCompra")]
        public async Task<IActionResult> OnGetAsync()
        {


            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            lista = await mangaContext.MangaUsuarios.Include(l => l.Manga).Where(d => d.UID == currentUserID).ToListAsync();

            //Alumnos = await Alum.Include(l => l.Localidad).ToListAsync();
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
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

    }
}
