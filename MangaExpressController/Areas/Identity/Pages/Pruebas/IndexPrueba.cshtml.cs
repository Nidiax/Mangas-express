using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaExpressController.Areas.Identity.Pages.Pruebas
{
    [AllowAnonymous]
    public class IndexPruebaModel : PageModel
    {
        private ApplicationDbContext mangaContext;

        public MangaM Manga { get; set; }
        public IList<MangaM> Mangas { get; set; }
        public IndexPruebaModel(/*ILogger<IndexModel> logger,*/ ApplicationDbContext _context)
        {
            // _logger = logger;
            mangaContext = _context;
        }

        //[Authorize(Roles = "UsuarioCompra")]
        public async Task<IActionResult> OnGetAsync()

        {



            Mangas = await mangaContext.Mangas.Where(x => x.Estatus).ToListAsync();


            return Page();
        }

    }
}
