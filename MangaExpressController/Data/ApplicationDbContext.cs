using Manga.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangaExpressController.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Rol, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(entityTypeBuilder =>
            {
                  entityTypeBuilder.ToTable("Usuarios");
            });

            builder.Entity<Rol>(entityTypeBuilder =>
             {
                 entityTypeBuilder.Property(u => u.Descripcion)
                      .HasMaxLength(100)
                    .IsRequired(true)
                   .IsUnicode(true);
             });

            builder.Entity<MangaUsuario>().HasKey(sc => new { sc.UID, sc.MID });

            builder.Entity<MangaUsuario>()
                .HasOne<Usuario>(sc => sc.Usuario)
                .WithMany(s => s.MangaUsuarios)
                .HasForeignKey(sc => sc.UID);


            builder.Entity<MangaUsuario>()
                .HasOne<MangaM>(sc => sc.Manga)
                .WithMany(s => s.MangaUsuarios)
                .HasForeignKey(sc => sc.MID);


        }

        public DbSet<MangaM> Mangas { get; set; }
        public DbSet<MangaUsuario> MangaUsuarios { get; set; }

    }
}

