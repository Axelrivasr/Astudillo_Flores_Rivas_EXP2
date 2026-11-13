using System;
using Xunit;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Interfaces;

namespace Juegoteca.Tests
{
    public class VideojuegoTests
    {
        [Fact]
        public void Constructor_ValoresValidos_AsignaPropiedades()
        {
            string titulo = "Celeste";
            string estudio = "Matt Makes Games";
            int horas = 8;

            var juego = new Videojuego(titulo, estudio, horas);

            Assert.Equal(titulo, juego.Titulo);
            Assert.Equal(estudio, juego.Estudio);
            Assert.Equal(horas, juego.HorasJuego);
        }

        [Fact]
        public void ToString_FormatoCorrecto_DevuelveCadenaEsperada()
        {
            var juego = new Videojuego("Hades", "Supergiant", 25);

            string resultado = juego.ToString();

            Assert.Equal("Hades - Supergiant (25 h)", resultado);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Constructor_HorasInvalidas_LanzaArgumentException(int horasInvalidas)
        {
            Assert.Throws<ArgumentException>(() => new Videojuego("Test", "Test Estudio", horasInvalidas));
        }

        [Fact]
        public void ImplementaInterfaz_InstanciaComoIVideojuego_ExponePropiedades()
        {
            IVideojuego juego = new Videojuego("Minecraft", "Mojang", 100);

            Assert.Equal("Minecraft", juego.Titulo);
            Assert.IsAssignableFrom<IVideojuego>(juego);
        }
    }
}