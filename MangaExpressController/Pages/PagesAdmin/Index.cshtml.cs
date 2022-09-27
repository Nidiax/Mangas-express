using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Models;
using MangaExpressController.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaExpressController.Pages.PagesAdmin
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext mangaContext;

        public MangaM Manga { get; set; }
        public IList<MangaM> Mangas { get; set; }
        public IndexModel(/*ILogger<IndexModel> logger,*/ ApplicationDbContext _context)
        {
            // _logger = logger;
            mangaContext = _context;
        }

        //[Authorize(Roles = "UsuarioCompra")]
        public async Task<IActionResult> OnGetAsync()

        {

            //Para iniciar el programa es necesario tener creados estos objetos y establecerlos de manera estatica
            // PanelPrincipal = await _context.PanelPrincipals.FirstOrDefaultAsync(m => m.PanelId == 1);
            //CierreConvocatoria = await _context.CierreConvocatorias.FirstOrDefaultAsync(n => n.CierreId == 2);
            //RegistroIns = await _context.RegistroInss.FirstOrDefaultAsync(n => n.RegistroInsId == 2);

            Mangas = await mangaContext.Mangas.Where(x => x.Estatus).ToListAsync();

            /*CardAvisoss = await _context.CardAvisos.ToListAsync();
            if (PanelPrincipal == null)
            {
                return NotFound();
            }*/
            return Page();
        }

        /* public IActionResult Index()//Aqui se llena la lista
         {
             // var user = (Usuarios)Session["Usuario"];
             var mangas = mangaContext.mangas.Where(x => x.Status).ToList();
             return Page ();
         }*/

        /*
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/

        [HttpGet]
        public IActionResult Details(int id)
        {


            var manga = mangaContext.Mangas.FirstOrDefault(x => x.Id == id);

            return RedirectToPage("./_DetailsModelPartial", manga);
        }

    }
}
