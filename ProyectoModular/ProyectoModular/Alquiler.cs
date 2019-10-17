using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProyectoModular
{
    class Alquiler
    {

        public int Id { get; set; }
        public int IdUsuarios { get; set; }
        public int IdPeliculas { get; set; }
        public DateTime FechaAlquiler { get; set; }
        public DateTime FechaDevolucion { get; set; }

        public Alquiler(int id, int idUsuarios, int idPeliculas, DateTime fechaAlquiler, DateTime fechaDevolucion)
        {
            Id = id;
            IdUsuarios = idUsuarios;
            IdPeliculas = idPeliculas;
            FechaAlquiler = fechaAlquiler;
            FechaDevolucion = fechaDevolucion;
        }

        public Alquiler(int idUsuarios, int idPeliculas, DateTime fechaAlquiler, DateTime fechaDevolucion)
        {
            IdUsuarios = idUsuarios;
            IdPeliculas = idPeliculas;
            FechaAlquiler = fechaAlquiler;
            FechaDevolucion = fechaDevolucion;
        }

        public Alquiler(int id, int idUsuarios, int idPeliculas, DateTime fechaAlquiler)
        {
            Id = id;
            IdUsuarios = idUsuarios;
            IdPeliculas = idPeliculas;
            FechaAlquiler = fechaAlquiler;
        }
    }
}
