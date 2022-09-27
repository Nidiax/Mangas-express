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
    public class DeleteModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public DeleteModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rol = await _context.Roles.FindAsync(id);

            if (Rol != null)
            {
                _context.Roles.Remove(Rol);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
