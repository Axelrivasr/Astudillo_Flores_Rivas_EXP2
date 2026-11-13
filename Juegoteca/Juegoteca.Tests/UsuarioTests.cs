using System;
using System.Collections.Generic;
using Xunit;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Tests
{
    public class UsuarioTests
    {
        [Fact]
        public void Constructor_AsignaNombre_InicializaColecciones()
        {
            // Arrange & Act
            var usuario = new Usuario("Zaa");

            // Assert
            Assert.Equal("Zaa", usuario.Nombre);
            Assert.NotNull(usuario.Colecciones);
            Assert.Empty(usuario.Colecciones);
        }

        [Fact]
        public void CrearColeccion_NuevaColeccion_SeCreaVacia()
        {
            // Arrange
            var usuario = new Usuario("Ana");

            // Act
            usuario.CrearColeccion("Favoritos");

            // Assert
            Assert.True(usuario.Colecciones.ContainsKey("Favoritos"));
            Assert.Empty(usuario.Colecciones["Favoritos"]);
        }

        [Fact]
        public void CrearColeccion_ColeccionExistente_LanzaInvalidOperationException()
        {
            // Arrange
            var usuario = new Usuario("Ana");
            usuario.CrearColeccion("Favoritos");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => usuario.CrearColeccion("Favoritos"));
        }

        [Fact]
        public void AgregarJuegoAColeccion_JuegoValido_NoAgregaDuplicados()
        {
            // Arrange
            var usuario = new Usuario("Jugador");
            usuario.CrearColeccion("Pendientes");
            var juego = new Videojuego("Celeste", "Matt", 8);

            // Act
            usuario.AgregarJuegoAColeccion("Pendientes", juego);
            usuario.AgregarJuegoAColeccion("Pendientes", juego); // Intento de duplicado

            // Assert
            Assert.Single(usuario.Colecciones["Pendientes"]);
        }

        [Fact]
        public void AgregarJuegoAColeccion_ColeccionInexistente_LanzaKeyNotFoundException()
        {
            // Arrange
            var usuario = new Usuario("Jugador");
            var juego = new Videojuego("Celeste", "Matt", 8);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => usuario.AgregarJuegoAColeccion("Inexistente", juego));
        }

        [Fact]
        public void ContarJuegos_MultiplesColecciones_RetornaSumaTotal()
        {
            // Arrange
            var usuario = new Usuario("Admin");
            usuario.CrearColeccion("Col1");
            usuario.CrearColeccion("Col2");
            usuario.AgregarJuegoAColeccion("Col1", new Videojuego("Juego1", "Est", 10));
            usuario.AgregarJuegoAColeccion("Col2", new Videojuego("Juego2", "Est", 20));

            // Act
            int total = usuario.ContarJuegos();

            // Assert
            Assert.Equal(2, total);
        }

        [Fact]
        public void ObtenerColeccion_Y_EliminarColeccion_FuncionaCorrectamente()
        {
            // Arrange
            var usuario = new Usuario("Test");
            usuario.CrearColeccion("EliminarMe");

            // Act & Assert (Obtener)
            var col = usuario.ObtenerColeccion("EliminarMe");
            Assert.NotNull(col);

            // Act & Assert (Eliminar)
            Assert.True(usuario.EliminarColeccion("EliminarMe"));
            Assert.False(usuario.EliminarColeccion("EliminarMe"));
        }

        [Fact]
        public void Colecciones_SonIndependientesPorUsuario()
        {
            // Arrange
            var user1 = new Usuario("User1");
            var user2 = new Usuario("User2");

            // Act
            user1.CrearColeccion("Unica");

            // Assert
            Assert.True(user1.Colecciones.ContainsKey("Unica"));
            Assert.False(user2.Colecciones.ContainsKey("Unica"));
        }
    }
}