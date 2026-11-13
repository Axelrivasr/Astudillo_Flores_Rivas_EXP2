using System;
using System.Collections.Generic;

namespace Juegoteca.Biblioteca.Modelos
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public Dictionary<string, List<Videojuego>> Colecciones { get; set; }

        public Usuario(string nombre)
        {
            Nombre = nombre;
            Colecciones = new Dictionary<string, List<Videojuego>>();
        }

        public void CrearColeccion(string nombre)
        {
            if (Colecciones.ContainsKey(nombre))
                throw new InvalidOperationException("La colección ya existe.");

            Colecciones[nombre] = new List<Videojuego>();
        }

        public void AgregarJuegoAColeccion(string nombre, Videojuego juego)
        {
            if (!Colecciones.ContainsKey(nombre))
                throw new KeyNotFoundException("La colección no existe.");

            if (!Colecciones[nombre].Contains(juego))
                Colecciones[nombre].Add(juego);
        }

        public bool EliminarColeccion(string nombre)
        {
            return Colecciones.Remove(nombre);
        }

        public List<Videojuego> ObtenerColeccion(string nombre)
        {
            if (Colecciones.TryGetValue(nombre, out var coleccion))
                return coleccion;
            return new List<Videojuego>();
        }

        public int ContarJuegos()
        {
            int total = 0;
            foreach (var coleccion in Colecciones.Values)
                total += coleccion.Count;
            return total;
        }

        public void MostrarColecciones()
        {
            foreach (var kvp in Colecciones)
            {
                Console.WriteLine($"Colección: {kvp.Key}");
                foreach (var juego in kvp.Value)
                {
                    Console.WriteLine($"  {juego.ToString()}");
                }
            }
        }
    }
}