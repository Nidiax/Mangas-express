using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authorization;

namespace MangaExpressController.Pages.PagesAdmin.CrudRoles
{

    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public CreateModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Rol Rol { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            

            _context.Roles.Add(Rol);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }




    }
}
