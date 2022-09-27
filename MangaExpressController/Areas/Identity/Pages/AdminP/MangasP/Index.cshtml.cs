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
    public class IndexModel : PageModel
    {
        private readonly MangaExpressController.Data.ApplicationDbContext _context;

        public IndexModel(MangaExpressController.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MangaM> MangaM { get;set; }
        public MangaM Manga { get;set; }

        public async Task OnGetAsync()
        {
            MangaM = await _context.Mangas.Where(m=>m.Estatus).ToListAsync();
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
    }

}
