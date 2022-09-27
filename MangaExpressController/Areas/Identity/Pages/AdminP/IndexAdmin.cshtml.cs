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

namespace MangaExpressController.Areas.Identity.Pages.AdminP
{
    [Authorize(Roles = "Administrador")]
    public class IndexAdminModel : PageModel
    {
        private ApplicationDbContext mangaContext;

        public MangaM Manga { get; set; }
        public IList<MangaM> Mangas { get; set; }

        public IndexAdminModel(ApplicationDbContext _context)
        {
            mangaContext = _context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Mangas = await mangaContext.Mangas.Where(x => x.Estatus).ToListAsync();
            return Page();
        }
    }
}
