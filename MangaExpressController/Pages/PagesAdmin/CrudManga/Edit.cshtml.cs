using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Manga.Models;
using MangaExpressController.Data;

namespace MangaExpressController.Pages.PagesAdmin.CrudManga
{
    public class EditModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public EditModel(MangaExpressController.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MangaM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MangaMExists(MangaM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MangaMExists(int id)
        {
            return _context.Mangas.Any(e => e.Id == id);
        }
    }
}
