using System;
using System.Collections.Generic;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Servicios
{
    public class ServicioVideojuegos
    {
        public IGestorVideojuegos Gestor { get; private set; }
        public IExportador Exportador { get; private set; }
        public List<Usuario> Usuarios { get; private set; }

        // Diccionario para el patrón Strategy
        private readonly Dictionary<string, Action> _estrategiasOrdenamiento;

        public ServicioVideojuegos(IGestorVideojuegos gestor, IExportador exportador)
        {
            Gestor = gestor ?? throw new ArgumentNullException(nameof(gestor));
            Exportador = exportador ?? throw new ArgumentNullException(nameof(exportador));
            Usuarios = new List<Usuario>();

            // CORRECCIÓN OCP: Las estrategias se configuran en inicialización sin if/else.
            _estrategiasOrdenamiento = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
            {
                { "quick", () => Gestor.QuickSort(Gestor.JuegosDisponibles) },
                { "merge", () => Gestor.JuegosDisponibles = Gestor.MergeSort(Gestor.JuegosDisponibles) }
            };
        }

        public Usuario RegistrarUsuario(string nombre)
        {
            var usuario = new Usuario(nombre);
            Usuarios.Add(usuario);
            return usuario;
        }

        public Usuario BuscarUsuario(string nombre)
        {
            return Usuarios.Find(u => string.Equals(u.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
        }

        public bool EliminarUsuario(string nombre)
        {
            var usuario = BuscarUsuario(nombre);
            if (usuario != null)
            {
                Usuarios.Remove(usuario);
                return true;
            }
            return false;
        }

        public int ContarUsuarios()
        {
            return Usuarios.Count;
        }

        // CORRECCIÓN SRP y OCP combinadas: No hay Console.WriteLine y no hay If/Else.
        public string OrdenarCatalogo(string algoritmo)
        {
            if (_estrategiasOrdenamiento.TryGetValue(algoritmo, out var ejecutarAlgoritmo))
            {
                ejecutarAlgoritmo();
                return $"Catálogo ordenado exitosamente con {algoritmo}."; // Se retorna el mensaje para que Consola lo imprima
            }
            throw new ArgumentException("Algoritmo no válido.");
        }

        public int OrdenarColeccionYCalcularHoras(Usuario usuario, string coleccion)
        {
            var juegos = usuario.ObtenerColeccion(coleccion);
            Gestor.QuickSort(juegos);

            int suma = 0;
            foreach (var juego in juegos)
                suma += juego.HorasJuego;

            return suma;
        }

        public void ExportarColecciones(Usuario usuario, string ruta)
        {
            Exportador.ExportarATxt(usuario, ruta);
        }
    }
}