﻿using AutoMapper;
using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.DTOs;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]

    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos almacenadorDeArchivos;
        private readonly IMapper mapper;

        public PersonasController(ApplicationDbContext context,
            IAlmacenadorArchivos almacenadorDeArchivos,
            IMapper mapper)
        {
            this.context = context;
            this.almacenadorDeArchivos = almacenadorDeArchivos;
            this.mapper = mapper;
        }

        [HttpGet]


        public async Task<ActionResult<List<Persona>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Personas.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.Paginar(paginacion).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaVisualizarDTO>> Get(int id)
        {
            var persona = await context.Personas.Where(x => x.Id == id)

                .Include(x => x.PeliculasActor).ThenInclude(x => x.Pelicula)
                .FirstOrDefaultAsync();

            if (persona == null) { return NotFound(); }

            persona.PeliculasActor = persona.PeliculasActor.OrderByDescending(x => x.Pelicula.Lanzamiento).ToList();

            var model = new PersonaVisualizarDTO();
            model.Persona = persona;
            model.Peliculas = persona.PeliculasActor.Select(x =>
            new Pelicula
            {
                Titulo = x.Pelicula.Titulo,
                Poster = x.Pelicula.Poster,
                Lanzamiento = x.Pelicula.Lanzamiento,
               
                Id = x.PeliculaId
            }).ToList();
            

            return model;
        }

        [HttpGet("buscar/{textoBusqueda}")]
        public async Task<ActionResult<List<Persona>>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda)) { return new List<Persona>(); }
            textoBusqueda = textoBusqueda.ToLower();
            return await context.Personas
                .Where(x => x.Nombre.ToLower().Contains(textoBusqueda)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Persona persona)
        {
            if (!string.IsNullOrWhiteSpace(persona.Foto))
            {
                var fotoPersona = Convert.FromBase64String(persona.Foto);
                persona.Foto = await almacenadorDeArchivos.GuardarArchivo(fotoPersona, "jpg", "personas");
            }

            context.Add(persona);
            await context.SaveChangesAsync();
            return persona.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Persona persona)
        {
            var personaDB = await context.Personas.FirstOrDefaultAsync(x => x.Id == persona.Id);

            if (personaDB == null) { return NotFound(); }

            personaDB = mapper.Map(persona, personaDB);

            if (!string.IsNullOrWhiteSpace(persona.Foto))
            {
                var fotoImagen = Convert.FromBase64String(persona.Foto);
                personaDB.Foto = await almacenadorDeArchivos.EditarArchivo(fotoImagen,
                    "jpg", "personas", personaDB.Foto);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Personas.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Persona { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
