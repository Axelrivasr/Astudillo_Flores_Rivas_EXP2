using System;
using System.IO;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Servicios
{
    public class ExportadorTxt : IExportador
    {
        public void ExportarATxt(Usuario usuario, string rutaArchivo)
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo, false)) // false = sobrescribe
            {
                sw.WriteLine($"Jugador: {usuario.Nombre}");
                sw.WriteLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm}");
                sw.WriteLine("====================================");

                foreach (var coleccion in usuario.Colecciones)
                {
                    sw.WriteLine($"\nColección: {coleccion.Key}");
                    int totalHoras = 0;

                    foreach (var juego in coleccion.Value)
                    {
                        sw.WriteLine($"- {juego.ToString()}");
                        totalHoras += juego.HorasJuego;
                    }

                    sw.WriteLine($"Total de horas en la colección: {totalHoras} h");
                }
            }
        }
    }
}