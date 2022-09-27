using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manga.Models;
using MangaExpressController.Data;

namespace MangaExpressController.Areas.Identity.Pages.AdminP.MangasP
{
    public class DeleteModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public DeleteModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MangaM MangaM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MangaM = await _context.Mangas.FirstOrDefaultAsync(m => m.Id == id);

            if (MangaM == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MangaM = await _context.Mangas.FindAsync(id);

            if (MangaM != null)
            {
                _context.Mangas.Remove(MangaM);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
