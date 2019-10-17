using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProyectoModular
{
    class Usuarios
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }

        public Usuarios(int id, string nombre, string apellidos, DateTime fechaNacimiento, string email, string contrasena)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Contrasena = contrasena;
        }

        public Usuarios(string nombre, string apellidos, DateTime fechaNacimiento, string email, string contrasena)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Contrasena = contrasena;
        }
        public Usuarios()
        {
        }
    }
}
