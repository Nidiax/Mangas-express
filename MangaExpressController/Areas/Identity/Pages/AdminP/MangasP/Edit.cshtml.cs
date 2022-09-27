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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MangaExpressController.Areas.Identity.Pages.AdminP.MangasP
{
    public class EditModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        [BindProperty]
        public MangaM MangaM { get; set; }
        [BindProperty]
        public IFormFile Imagen { get; set; }
        public IWebHostEnvironment HostEnvoriment { get; }

        public EditModel(MangaExpressController.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            HostEnvoriment = webHostEnvironment;
        }
           

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(Imagen != null)
            {
                if (!string.IsNullOrEmpty(MangaM.Imagen))
                {
                    var filepath = Path.Combine(HostEnvoriment.WebRootPath, "images", MangaM.Imagen);
                    System.IO.File.Delete(filepath);
                }

                MangaM.Imagen = ProcessUploadFile();
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

        private string ProcessUploadFile()
        {
            if (Imagen == null)
                return string.Empty;
            var uploadFolder = Path.Combine(HostEnvoriment.WebRootPath, "Images");
            var fileName = $"{Guid.NewGuid()}_{Imagen.FileName}";
            var filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Imagen.CopyTo(stream);
            }
            return fileName;
        }
    }
}
