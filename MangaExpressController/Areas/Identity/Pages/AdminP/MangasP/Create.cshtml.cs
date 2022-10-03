using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MangaExpressController.Areas.Identity.Pages.AdminP.MangasP
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;
        [BindProperty]
        public IFormFile Imagen { get; set; }
        public IWebHostEnvironment HostEnvoriment { get;}
        [BindProperty]
        public MangaM MangaM { get; set; }

        public CreateModel(MangaExpressController.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            HostEnvoriment = webHostEnvironment;
        }

        public CreateModel()
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
           //var modelrespuesta = ValidateModel(MangaM);
           var respuesta = ValidateExistImage(MangaM.Imagen);

            if (!ModelState.IsValid)
                return Page();
            if (respuesta == true)
            {

                if (!string.IsNullOrEmpty(MangaM.Imagen))
                {
                    var filePath = Path.Combine(HostEnvoriment.WebRootPath, "images", MangaM.Imagen);
                    System.IO.File.Delete(filePath);
                }

                MangaM.Imagen = ProcessUploadFile();
            }

             _context.Mangas.Add(MangaM);
                await _context.SaveChangesAsync();
            
            
          

            return RedirectToPage("./Index");
        }



        public bool ValidateExistImage(string Image)
        {

           if(Image != null)
            {
                return true;
            }
            
           return false;
           
        }


        public bool ValidateInputs(int precio, string Nombre, string Descripcion)
        {
            if (precio != 0 && Nombre != "" && Descripcion != "")
            {
                return true;
            }

            return false;
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
