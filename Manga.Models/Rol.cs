using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Models
{
   public class Rol : IdentityRole
    {
        public string Descripcion { get; set; }
    }
}

