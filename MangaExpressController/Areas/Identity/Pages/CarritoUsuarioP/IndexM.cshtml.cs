using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaExpressController.Areas.Identity.Pages.CarritoUsuarioP
{
    [Authorize(Roles = "UsuarioCompra, Administrador")]
    public class IndexMModel : PageModel
    {
        private ApplicationDbContext mangaContext;


        public MangaM Manga { get; set; }
        public IList<MangaM> Mangas { get; set; }
        public bool search { get; set; }
        public IndexMModel(/*ILogger<IndexModel> logger,*/ ApplicationDbContext _context)
        {
            // _logger = logger;
            mangaContext = _context;
        }
        public async Task<IActionResult> OnGetAsync(string searchBy)
        {
            search = !string.IsNullOrEmpty(searchBy);
            Mangas = await (!string.IsNullOrEmpty(searchBy) ? mangaContext.Mangas.Where(x => x.Estatus && x.Nombre.Contains(searchBy)) : mangaContext.Mangas.Where(x => x.Estatus)).ToListAsync();
            return Page();
        }
    }
}
