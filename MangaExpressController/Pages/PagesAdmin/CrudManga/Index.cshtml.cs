using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manga.Models;
using MangaExpressController.Data;

namespace MangaExpressController.Pages.PagesAdmin.CrudManga
{
    public class IndexModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public IndexModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MangaM> MangaM { get;set; }

        public async Task OnGetAsync()
        {
            MangaM = await _context.Mangas.ToListAsync();
        }
    }
}
