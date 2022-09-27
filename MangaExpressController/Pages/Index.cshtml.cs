using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaExpressController.Pages
{
    [AllowAnonymous]//Con este decorador puede ingresar cualquiera 
    public class IndexModel : PageModel
    {
        private ApplicationDbContext mangaContext;

        public MangaM Manga { get; set; }
        public IList<MangaM> Mangas { get; set; }
        public bool search { get; set; }
        public IndexModel(/*ILogger<IndexModel> logger,*/ ApplicationDbContext _context)
        {
            // _logger = logger;
            mangaContext = _context;
        }

        public async Task<IActionResult> OnGetAsync(string searchBy)
        {
            search = !string.IsNullOrEmpty(searchBy);
            Mangas = await (!string.IsNullOrEmpty(searchBy) ? mangaContext.Mangas.Where(x => x.Estatus && x.Nombre.Contains(searchBy)) : mangaContext.Mangas.Where(x => x.Estatus)).ToListAsync();
            if (User.IsInRole("Administrador"))
                return LocalRedirect("/Identity/AdminP/IndexAdmin");
            return Page();
        }
        public IActionResult Details(int id)
        {
            var manga = mangaContext.Mangas.FirstOrDefault(x => x.Id == id);

            return RedirectToPage("./_DetailsModelPartial", manga);
        }
        
    }
}
