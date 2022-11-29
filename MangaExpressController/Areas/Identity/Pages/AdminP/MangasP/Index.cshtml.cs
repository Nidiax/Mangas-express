using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manga.Models;
using MangaExpressController.Data;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MangaExpressController.Areas.Identity.Pages.AdminP.MangasP
{
    public class IndexModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public IndexModel(MangaExpressController.Data.ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment= hostingEnvironment;
            _context = context;
        }

        public IList<MangaM> MangaM { get;set; }
        public MangaM Manga { get;set; }

        public async Task OnGetAsync()
        {
            MangaM = await _context.Mangas.ToListAsync();
        }
        

        public async Task<PartialViewResult> OnGetManga(int id)
        {
            return Partial("./Edit", await _context.Mangas.FindAsync(id));
        }

        public async Task OnGetStatus(int id, bool status)
        {
            var manga = _context.Mangas.Find(id);
            manga.Estatus = status;
            _context.Mangas.Update(manga);
            _context.SaveChanges();
            await OnGetAsync();
        }

        //public async Task<IActionResult> OnGetPDFAsync() {
        //    MangaM = await _context.Mangas.ToListAsync();
        //    HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
        //    WebKitConverterSettings settings = new WebKitConverterSettings();
        //    //Set WebKit path
        //    settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
        //    //Assign WebKit settings to HTML converter
        //    htmlConverter.ConverterSettings = settings;
        //    PdfDocument document = htmlConverter.Convert("https://localhost:44324/Identity/AdminP/MangasP");
        //    MemoryStream stream = new MemoryStream();
        //    document.Save(stream);
        //    return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Sample.pdf");
        //}
    }

}
