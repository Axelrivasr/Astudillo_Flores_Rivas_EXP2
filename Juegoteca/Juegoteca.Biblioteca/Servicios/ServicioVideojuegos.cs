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

        public ServicioVideojuegos(IGestorVideojuegos gestor, IExportador exportador)
        {
            Gestor = gestor ?? throw new ArgumentNullException(nameof(gestor));
            Exportador = exportador ?? throw new ArgumentNullException(nameof(exportador));
            Usuarios = new List<Usuario>();
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

        public void OrdenarCatalogo(string algoritmo)
        {
            if (algoritmo == "quick")
            {
                Gestor.QuickSort(Gestor.JuegosDisponibles);
                Console.WriteLine("Catálogo ordenado exitosamente con QuickSort.");
            }
            else if (algoritmo == "merge")
            {
                Gestor.JuegosDisponibles = Gestor.MergeSort(Gestor.JuegosDisponibles);
                Console.WriteLine("Catálogo ordenado exitosamente con MergeSort.");
            }
            else
            {
                throw new ArgumentException("Algoritmo no válido.");
            }
        }

        public int OrdenarColeccionYCalcularHoras(Usuario usuario, string coleccion)
        {
            var juegos = usuario.ObtenerColeccion(coleccion);

            // Ordenar la colección usando el QuickSort del Gestor
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