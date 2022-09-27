using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Models
{
    public class MangaUsuario
    {
        public string UID { get; set; }
        public int MID { get; set; }
        public Usuario Usuario { get; set; }
        public MangaM Manga { get; set; }
    }
}
