using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authorization;

namespace MangaExpressController.Areas.Identity.Pages.AdminP.RolesP
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public IndexModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Rol> Rol { get; set; }

        public async Task OnGetAsync()
        {
            Rol = await _context.Roles.ToListAsync();
        }
    }
}
