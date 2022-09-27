using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaExpressController.Pages.PagesAdmin.CrudUsuarios
{
    public class IndexModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public IndexModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Usuario> Usuario { get; set; }

        public async Task OnGetAsync()
        {
            Usuario = await _context.Users.ToListAsync();
        }
    }
}
