using BlazorPeliculas.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPeliculas.Shared.DTOs
{
    public class PersonaVisualizarDTO
    {
        public Persona Persona { get; set; }
       
        public List<Pelicula> Peliculas{ get; set; }
       
    }
}
