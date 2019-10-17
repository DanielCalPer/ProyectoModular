using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProyectoModular
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-575QA94\\SQLEXPRESS;Initial Catalog=VIDEOCLUB;Integrated Security=True");
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {


            Console.WriteLine();
            Console.WriteLine("Bienvenido a tu VideoClub Online");
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            Console.WriteLine("Elige una de estas 3 opciones: ");
            Console.WriteLine("1. Registrarse:");
            Console.WriteLine("2. LogIn: ");
            Console.WriteLine("3. Salir: ");

            int eleccion;
            if (Int32.TryParse(Console.ReadLine(), out eleccion))
            {
                switch (eleccion)
                {
                    case 1:
                        Registrarse();
                        break;
                    case 2:
                        LogIn();
                        break;
                    case 3:
                        Console.WriteLine("Gracias por su visita, le esperamos pronto");

                        break;
                    default:
                        Console.WriteLine("No has escogido una opcion válida");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No has escogido una opcion válida");
            }




        }
        public static void SubMenu(Usuarios usuario)
        {

            bool closeprogram = false;
            do
            {

                Console.WriteLine();
                Console.WriteLine("++++++++++++++++++++++++++++++++");
                Console.WriteLine("1. Ver Peliculas Disponibles");
                Console.WriteLine("2. Alquilar Películas");
                Console.WriteLine("3. Mis Alquileres:");
                Console.WriteLine("4. LogOut: ");


                int eleccion;
                if (Int32.TryParse(Console.ReadLine(), out eleccion))
                {
                    switch (eleccion)
                    {
                        case 1:
                            VerPeliculasDisponibles(usuario);
                            break;
                        case 2:
                            AlquilarPeliculas(usuario);
                            break;
                        case 3:
                            MisAlquileres(usuario);
                            break;
                        case 4:
                            Console.WriteLine("Gracias por su visita, le esperamos pronto");
                            Menu();
                            closeprogram = true;
                            break;
                        default:
                            Console.WriteLine("No has escogido una opcion válida");
                            break;
                    }
                }
            } while (!closeprogram);
        }
        public static void ModificarBase(string query)
        {
            if (query != null) // modifica la base si la consulta no esta vacia. 
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                Console.WriteLine(command.ExecuteNonQuery());
                connection.Close();
            }
        }
        public static bool ConsultarBase(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read()) //Si el reader se ejecuta correctamente, la query coincide con la BBDD
            {
                connection.Close();
                return true;
            }

            connection.Close(); // Si no devuelve falso y no cumple. 
            return false;

        }
        public static void Registrarse()
        {
            bool choose = false;
            int cont = 0;
            do
            {
                if (cont == 3)
                {

                    Console.WriteLine("Parece que tienes algún problema con el registro, por favor ponte en contacto con nuestro Servicio de Atención al Cliente");
                    choose = true;
                }
                else
                {
                    Console.WriteLine("Introduzce tu nombre, por favor:");
                    string nombre = Console.ReadLine().ToLower();

                    if (nombre.Length <= 30 && nombre != "")
                    {
                        Console.WriteLine("Ahora, introduzce tu apellido:");
                        string apellido = Console.ReadLine().ToLower();

                        if (apellido.Length <= 30 && apellido != "")
                        {
                            Console.WriteLine("Cual es tu fecha de nacimiento (dd/MM/yyyy)?:");
                            string fechaNacimiento = (Console.ReadLine().ToLower());

                            DateTime fecha;
                            if (DateTime.TryParse(fechaNacimiento, out fecha)) // comprobar fecha sea valida
                            {
                                Console.WriteLine("Introduzca su email, por favor:");
                                string email = Console.ReadLine().ToLower();

                                if (email.Length <= 30 && email.Contains("@") && email.Substring(email.Length - 4, 3) == ".com")
                                {
                                    Console.WriteLine("Elige una contraseña:");
                                    string contrasena = Console.ReadLine().ToLower();

                                    if (contrasena.Length <= 30 && contrasena != "")
                                    {
                                        // Si se cumple todo, insertamos los datos en la BBDD. 
                                        string query = $"INSERT INTO USUARIOS (nombre, apellido, fechaNacimiento, email, contrasena) values('{nombre}','{apellido}','{fechaNacimiento}','{email}','{contrasena}')";
                                        ModificarBase(query);
                                        choose = true;


                                    }
                                    else
                                    {
                                        Console.WriteLine("No has introducido una contraseña válida:");
                                        cont++;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No has introducido un email válido:");
                                    cont++;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No has introducido un fecha válido, ¡CUIDADO CON EL FORMATO!:");
                                cont++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No has introducido un apellido válido:");
                            cont++;
                        }

                    }
                    else
                    {
                        Console.WriteLine("No has introducido un nombre válido:");
                        cont++;
                    }
                }

            } while (!choose);

        }
        public static Usuarios LogIn()
        {
            bool choose = false;
            int cont = 0;
            do
            {

                if (cont == 3)
                {
                    Console.WriteLine("Parece que tienes algún problema con su LogIn, por favor ponte en contacto con nuestro Servicio de Atención al Cliente");
                    choose = true;
                }
                else
                {
                    Console.WriteLine("Introduzca su email, por favor:");
                    string email = Console.ReadLine().ToLower();
                    Console.WriteLine("Introduzca su contrasena, por favor:");
                    string contrasena = Console.ReadLine().ToLower();

                    string query = $"SELECT EMAIL, CONTRASENA from USUARIOS WHERE EMAIL LIKE '{email}' AND CONTRASENA LIKE '{contrasena}' ";

                    if (ConsultarBase(query)) // si la query se cumple, significa que los valores existen en la BBDD y puede logearse.
                    {
                        query = $"SELECT * from USUARIOS WHERE email LIKE '{email}'"; // consultamos lo que necesitamos en la BBDD
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection); // RELACION QUERY Y CONEXION
                        SqlDataReader reader = command.ExecuteReader();

                        Usuarios usuario = new Usuarios();

                        while (reader.Read()) // Construimos el objeto completo
                        {
                            usuario = new Usuarios(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), Convert.ToDateTime(reader[3].ToString()), reader[4].ToString(), reader[5].ToString()); //IMPORTANTE, VARCHAR NO ES STRING, HAY QUE CONVERTIRLO
                        }
                        connection.Close();
                        choose = true; // salimos bucle
                        SubMenu(usuario); // Nos vamos al Submenú.
                        return usuario; // Lo devolvemos completo para usarlo en el Submenú. 
                    }
                    else
                    {
                        Console.WriteLine("El email o usuario no son correctos");
                        cont++;
                    }
                }


            } while (!choose);
            return null;

        }
        public static void VerPeliculasDisponibles(Usuarios usuario)
        {
            TimeSpan edad = DateTime.Now - usuario.FechaNacimiento;
            string query = $"SELECT * FROM PELICULAS where EdadRecomendada <= {edad.Days / 365}"; // CUIDADO CON EL FORMATO TIMESPAN, HAY QUE ELEGIRLO
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection); // RELACION QUERY Y CONEXION
            SqlDataReader reader = command.ExecuteReader(); // Mientras lee en la base de datos, que genere objetos (peliculas) con esos datos y los meta en una lista 

            List<Peliculas> peliculas = new List<Peliculas>();

            while (reader.Read())
            {
                Peliculas peli1 = new Peliculas(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3].ToString()), reader[4].ToString()); //IMPORTANTE, VARCHAR NO ES STRING, HAY QUE CONVERTIRLO
                peliculas.Add(peli1);
            }

            foreach (Peliculas item in peliculas)
            {
                Console.WriteLine($"{item.Id} Nombre: {item.Titulo} Estado: {item.Estado}"); // MOSTRAMOS LOS ATRIBUTOS DE LA LISTA
            }

            connection.Close();

            int eleccion;
            do
            {
                Console.WriteLine(); //LEGIBILIDAD
                Console.WriteLine("Quieres ver la información completa de alguna de las películas? Para volver al Submenú pulsa: 0");


                if (Int32.TryParse(Console.ReadLine(), out eleccion))
                {

                    foreach (Peliculas item in peliculas)
                    {
                        if (item.Id == eleccion)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Título: {item.Titulo}\n Sinopsis: {item.Sinopsis}\n Edad Recomendad: {item.EdadRecomendada}\n Estado: {item.Estado}");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No has introducido un Id válido");
                }
            } while (eleccion != 0);


        }
        public static void AlquilarPeliculas(Usuarios usuarios)
        {
            Console.WriteLine("¿Que películas deseas alquilar?");

            TimeSpan edad = DateTime.Now - usuarios.FechaNacimiento;
            string query = $"SELECT * FROM PELICULAS where EdadRecomendada <= {edad.Days / 365} AND ESTADO LIKE 'LIBRE'"; //lista capada por EDAD y ESTADO
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection); // 
            SqlDataReader reader = command.ExecuteReader(); // Mientras lee en la base de datos, que meta esos valores en una lista. 

            List<Peliculas> peliculas = new List<Peliculas>();

            while (reader.Read()) // CREAMOS LISTA
            {
                Peliculas peli1 = new Peliculas(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3].ToString()), reader[4].ToString()); //IMPORTANTE, VARCHAR NO ES STRING, HAY QUE CONVERTIRLO
                peliculas.Add(peli1);
            }
            foreach (Peliculas item in peliculas) // MOSTRAMOS LISTA CON SU ID PARA QUE EL USER ELIJA
            {
                Console.WriteLine($"{item.Id} {item.Titulo}");
            }

            connection.Close();

            int eleccion;
            if (Int32.TryParse(Console.ReadLine(), out eleccion))
            {
                foreach (Peliculas item in peliculas)
                {
                    if (item.Id == eleccion)
                    {
                        query = $"UPDATE PELICULAS SET ESTADO = 'OCUPADO' WHERE ID LIKE '{item.Id}'";
                        ModificarBase(query);
                        Console.WriteLine("Has alquilado la película satisfactoriamente");


                        query = $"INSERT INTO ALQUILER (IDUSUARIOS, IDPELICULAS, FECHAALQUILER) VALUES ('{usuarios.Id}', '{item.Id}', '{DateTime.Today}')";
                        ModificarBase(query);
                        Console.WriteLine("La película se ha registrado en nuestra BB.DD.");

                    }
                }
            }
            else
            {
                Console.WriteLine("No has introducido un Id válido");
            }
        }
        public static void MisAlquileres(Usuarios usuario)
        {
            string query = $"SELECT * FROM ALQUILER where IDUSUARIOS LIKE '{usuario.Id}' AND FECHADEVOLUCION IS NULL";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection); // RELACION QUERY Y CONEXION
            SqlDataReader reader = command.ExecuteReader(); // Mientras lee en la base de datos, que meta esos valores en una lista. 

            List<Alquiler> alquiler = new List<Alquiler>();

            while (reader.Read())
            {
                Alquiler Alq1 = new Alquiler(Convert.ToInt32(reader[0].ToString()), Convert.ToInt32(reader[1].ToString()), Convert.ToInt32(reader[2].ToString()), Convert.ToDateTime(reader[3].ToString())); //IMPORTANTE, VARCHAR NO ES STRING, HAY QUE CONVERTIRLO
                alquiler.Add(Alq1);
            }

            connection.Close();

            if (alquiler.Count != 0)
            {
   
                foreach (Alquiler item in alquiler) // LISTA PELICULAS ALQUILADAS EN FUNCION DE SI SE CUMPLE LA FECHADEVO O NO
                {

                    if (DateTime.Today >= item.FechaAlquiler.AddDays(2))// COMPROBAMOS QUE EL TIEMPO DE ALQUILER A EXPIRADO (2 DIAS)
                    {
                        bool choose = false;
                        int eleccion;
                        do
                        {
                            Console.WriteLine("La fecha de devolución de alguna de tus películas ya ha expirado. Si deseas devolver alguna de ellas, selecciona el Id de la película.");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Id Alquiler: {item.Id}\n Id Usuario: {item.IdUsuarios}\n Id Película:  {item.IdPeliculas}\n Fecha Alquiler: {item.FechaAlquiler}");
                            Console.ForegroundColor = ConsoleColor.White;

                            if (Int32.TryParse(Console.ReadLine(), out eleccion)) // Aquí elige y actualizamos el estado en PELICULAS Y RESGISTRO ALQUILERES
                            {
                                foreach (Alquiler alquilada in alquiler)
                                {
                                    if (alquilada.IdPeliculas == eleccion) //SELECCIONA LA ID DE LA PELICULA
                                    {

                                        query = $"UPDATE ALQUILER SET FECHADEVOLUCION = '{DateTime.Today}'"; // ACTUALIZAMOS FECHADEVO
                                        ModificarBase(query);
                                        Console.WriteLine("La película ha sido devuelta correctamente");

                                        query = $"UPDATE PELICULAS SET ESTADO = 'LIBRE' WHERE ID LIKE {alquilada.IdPeliculas}"; //ACTUALIZAMOS ESTADO A LIBRE OTRA VEZ
                                        ModificarBase(query);
                                        Console.WriteLine("La película vuelve a estar disponible");
                                        choose = true;

                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("No has introducido un Id válido");
                            }

                        } while (!choose);
                    }
                    else
                    {
                        Console.WriteLine($"Id Alquiler: {item.Id}\n Id Usuario: {item.IdUsuarios}\n Id Película:  {item.IdPeliculas}\n Fecha Alquiler: {item.FechaAlquiler}");
                    }

                }
                
            }
            else
            {
                Console.WriteLine("No tienes películas alquiladas");
            }

        }

    }

}


