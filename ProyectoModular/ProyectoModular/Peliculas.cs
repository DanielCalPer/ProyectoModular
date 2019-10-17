using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProyectoModular
{
    class Peliculas
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public int EdadRecomendada { get; set; }
        public string Estado { get; set; }

        public Peliculas(int id, string titulo, string sinopsis, int edadRecomendada, string estado)
        {
            Id = id;
            Titulo = titulo;
            Sinopsis = sinopsis;
            EdadRecomendada = edadRecomendada;
            Estado = estado;
        }
        public Peliculas(string titulo, string sinopsis, int edadRecomendada, string estado)
        {
            Titulo = titulo;
            Sinopsis = sinopsis;
            EdadRecomendada = edadRecomendada;
            Estado = estado;
        }
        public Peliculas()
        {
        }
    }
}
