using System;
using System.Collections.Generic;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Gestores
{
    public partial class GestorVideojuegos : IGestorVideojuegos
    {
        public List<Videojuego> JuegosDisponibles { get; set; }

        public GestorVideojuegos()
        {
            JuegosDisponibles = new List<Videojuego>();
        }

        public void AgregarJuego(Videojuego juego)
        {
            if (!JuegosDisponibles.Contains(juego))
                JuegosDisponibles.Add(juego);
        }

        public bool EliminarJuego(Videojuego juego)
        {
            return JuegosDisponibles.Remove(juego);
        }

        public bool EliminarPorIndice(int indice)
        {
            if (indice >= 0 && indice < JuegosDisponibles.Count)
            {
                JuegosDisponibles.RemoveAt(indice);
                return true;
            }
            return false;
        }

        public List<Videojuego> BuscarPorTitulo(string texto)
        {
            List<Videojuego> coincidencias = new List<Videojuego>();
            foreach (var juego in JuegosDisponibles)
            {
                if (juego.Titulo.Contains(texto, StringComparison.OrdinalIgnoreCase))
                    coincidencias.Add(juego);
            }
            return coincidencias;
        }

        public void LimpiarCatalogo()
        {
            JuegosDisponibles.Clear();
        }
    }
}