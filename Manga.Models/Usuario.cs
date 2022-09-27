using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Models
{
    public class Usuario : IdentityUser
    {

        public virtual ICollection<MangaUsuario> MangaUsuarios { get; set; }
       

    }
}
