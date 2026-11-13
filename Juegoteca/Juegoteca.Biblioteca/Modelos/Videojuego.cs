using System;
using Juegoteca.Biblioteca.Interfaces;

namespace Juegoteca.Biblioteca.Modelos
{
    public class Videojuego : IVideojuego
    {
        public string Titulo { get; set; }
        public string Estudio { get; set; }
        public int HorasJuego { get; set; }

        public Videojuego(string titulo, string estudio, int horasJuego)
        {
            if (horasJuego <= 0)
                throw new ArgumentException("Las horas de juego deben ser mayores que 0.");

            Titulo = titulo;
            Estudio = estudio;
            HorasJuego = horasJuego;
        }

        public override string ToString()
        {
            return $"{Titulo} - {Estudio} ({HorasJuego} h)";
        }
    }
}