using System;
using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;

namespace Juegoteca.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Inicialización
            var gestor = new GestorVideojuegos();
            var exportador = new ExportadorTxt();
            var servicio = new ServicioVideojuegos(gestor, exportador);

            // Añadir 8 juegos de ejemplo
            gestor.AgregarJuego(new Videojuego("Celeste", "Matt Makes Games", 8));
            gestor.AgregarJuego(new Videojuego("Hollow Knight", "Team Cherry", 40));
            gestor.AgregarJuego(new Videojuego("Minecraft", "Mojang", 100));
            gestor.AgregarJuego(new Videojuego("Tetris", "Alexey Pajitnov", 5));
            gestor.AgregarJuego(new Videojuego("Portal 2", "Valve", 10));
            gestor.AgregarJuego(new Videojuego("Stardew Valley", "ConcernedApe", 60));
            gestor.AgregarJuego(new Videojuego("Hades", "Supergiant", 25));
            gestor.AgregarJuego(new Videojuego("Zelda: BOTW", "Nintendo", 120));

            Console.WriteLine("Bienvenido al Sistema de Gestión de Videojuegos\n");

            // 2. Registro de usuario
            Console.WriteLine("REGISTRO DE USUARIO");
            Console.Write("Ingrese su nombre de usuario: ");
            string nombreUsuario = Console.ReadLine();
            var usuario = servicio.RegistrarUsuario(nombreUsuario);
            Console.WriteLine($"Usuario '{usuario.Nombre}' registrado. ¡Bienvenido/a, {usuario.Nombre}!\n");

            // 3. Creación de colección inicial
            Console.Write("Nombre de su primera colección: ");
            string coleccionActual = Console.ReadLine();
            usuario.CrearColeccion(coleccionActual);
            Console.WriteLine($"Colección '{coleccionActual}' creada exitosamente.\n");

            // 4. Menú principal
            bool salir = false;
            while (!salir)
            {
                int cantidadJuegos = usuario.ObtenerColeccion(coleccionActual).Count;
                Console.WriteLine("\nMENÚ PRINCIPAL");
                Console.WriteLine($"Colección actual: '{coleccionActual}' ({cantidadJuegos} juegos)");
                Console.WriteLine("1. Mostrar juegos disponibles (y ordenar catálogo)");
                Console.WriteLine("2. Ordenar colección actual y calcular horas");
                Console.WriteLine("3. Búsqueda binaria en catálogo");
                Console.WriteLine("4. Crear nueva colección");
                Console.WriteLine("5. Cambiar de colección actual");
                Console.WriteLine("6. Ver mis colecciones");
                Console.WriteLine("7. Exportar mis colecciones a .txt");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out int opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Write("¿Desea ordenar el catálogo? (quick / merge / enter para omitir): ");
                            string algoritmo = Console.ReadLine().ToLower();
                            if (algoritmo == "quick" || algoritmo == "merge")
                            {
                                try
                                {
                                    Console.WriteLine(servicio.OrdenarCatalogo(algoritmo));
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }

                            Console.WriteLine("\n--- Catálogo Disponible ---");
                            for (int i = 0; i < gestor.JuegosDisponibles.Count; i++)
                            {
                                Console.WriteLine($"[{i}] {gestor.JuegosDisponibles[i].ToString()}");
                            }
                            break;

                        case 2:
                            Console.WriteLine("\nOrdenando colección actual...");
                            int totalHoras = servicio.OrdenarColeccionYCalcularHoras(usuario, coleccionActual);
                            Console.WriteLine($"La colección ha sido ordenada.");
                            Console.WriteLine($"Total de horas de juego en '{coleccionActual}': {totalHoras} h");
                            break;

                        case 3:
                            Console.Write("\nIngrese el título exacto a buscar: ");
                            string tituloBuscar = Console.ReadLine();
                            int indice = gestor.BusquedaBinaria(tituloBuscar);

                            if (indice != -1)
                            {
                                Console.WriteLine($"¡Juego encontrado! Índice de coincidencia: {indice}");
                                Console.Write("Ingrese el índice para agregarlo a su colección actual (o 'cancelar'): ");
                                string entrada = Console.ReadLine();

                                if (int.TryParse(entrada, out int indiceElegido))
                                {
                                    if (indiceElegido >= 0 && indiceElegido < gestor.JuegosDisponibles.Count)
                                    {
                                        var juegoAsignar = gestor.JuegosDisponibles[indiceElegido];
                                        usuario.AgregarJuegoAColeccion(coleccionActual, juegoAsignar);
                                        Console.WriteLine("Juego agregado a la colección.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("El juego no se encuentra en el catálogo.");
                            }
                            break;

                        case 4:
                            Console.Write("\nIngrese el nombre de la nueva colección: ");
                            string nuevaColeccion = Console.ReadLine();
                            try
                            {
                                usuario.CrearColeccion(nuevaColeccion);
                                Console.WriteLine("Colección creada con éxito.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 5:
                            Console.WriteLine("\nSus colecciones actuales:");
                            Console.WriteLine(usuario.MostrarColecciones());
                            Console.Write("Ingrese el nombre de la colección a la que desea cambiar: ");
                            string cambiarA = Console.ReadLine();
                            if (usuario.Colecciones.ContainsKey(cambiarA))
                            {
                                coleccionActual = cambiarA;
                                Console.WriteLine($"Cambio exitoso. Ahora gestiona: '{coleccionActual}'");
                            }
                            else
                            {
                                Console.WriteLine("La colección indicada no existe.");
                            }
                            break;

                        case 6:
                            Console.WriteLine("\n--- Mis Colecciones ---");
                            Console.WriteLine(usuario.MostrarColecciones());
                            break;

                        case 7:
                            string ruta = $"{usuario.Nombre}_Colecciones.txt";
                            servicio.ExportarColecciones(usuario, ruta);
                            Console.WriteLine($"\nColecciones exportadas exitosamente al archivo: {ruta}");
                            break;

                        case 8:
                            salir = true;
                            Console.WriteLine("Saliendo del sistema...");
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Intente de nuevo.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                }
            }
        }
    }
}