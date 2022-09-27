using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manga.Models;
using MangaExpressController.Data;

namespace MangaExpressController.Areas.Identity.Pages.AdminP.RolesP
{
    public class DetailsModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public DetailsModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Rol Rol { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rol = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (Rol == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
