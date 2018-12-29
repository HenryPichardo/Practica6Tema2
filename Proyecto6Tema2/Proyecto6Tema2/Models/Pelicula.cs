using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto6Tema2.Models
{
    public class Pelicula
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string ActorPrincipal { get; set; }
        public int No_Actores { get; set; }
        public double Duracion { get; set; }
        public int Estreno { get; set; }
    }
}