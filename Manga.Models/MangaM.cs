using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manga.Models
{
    public class MangaM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "La descripcion es requerida")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El autor es requerido")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "La marca es requerida")]
        public string Marca { get; set; }
        public string Imagen { get; set; }

        public bool Estatus { get; set; } = true;

        public virtual ICollection<MangaUsuario> MangaUsuarios { get; set; }
    }
}
