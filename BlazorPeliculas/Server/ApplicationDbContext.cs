using BlazorPeliculas.Shared.Entidades;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculas.Server
{
    public class ApplicationDbContext : ApiAuthorizationDbContext< IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
           : base(options, operationalStoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneroPelicula>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            modelBuilder.Entity<PeliculaActor>().HasKey(x => new { x.PeliculaId, x.PersonaId });

            var roleAdmin = new IdentityRole()
            { Id = "813b59bc-c451-4b31-8d66-32fbe88edb41", Name = "admin", NormalizedName = "admin" };

            modelBuilder.Entity<IdentityRole>().HasData(roleAdmin);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GeneroPelicula> GenerosPeliculas { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<VotoPelicula> VotosPeliculas { get; set; }
    }
}
