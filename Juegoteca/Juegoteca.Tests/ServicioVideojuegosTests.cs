using System;
using Xunit;
using Juegoteca.Biblioteca.Servicios;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Tests.Datos;

namespace Juegoteca.Tests
{
    public class ServicioVideojuegosTests
    {
        private ServicioVideojuegos _servicio;
        private ExportadorPrueba _exportadorPrueba;

        public ServicioVideojuegosTests()
        {
            _exportadorPrueba = new ExportadorPrueba();
            _servicio = new ServicioVideojuegos(DatosPrueba.GestorPoblado(), _exportadorPrueba);
        }

        [Fact]
        public void Constructor_DependenciasNulas_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ServicioVideojuegos(null, _exportadorPrueba));
            Assert.Throws<ArgumentNullException>(() => new ServicioVideojuegos(DatosPrueba.GestorPoblado(), null));
        }

        [Fact]
        public void RegistrarUsuario_Y_ContarUsuarios_FuncionanCorrectamente()
        {
            // Act
            var usuario = _servicio.RegistrarUsuario("Zaa");

            // Assert
            Assert.Equal("Zaa", usuario.Nombre);
            Assert.Equal(1, _servicio.ContarUsuarios());
        }

        [Fact]
        public void BuscarUsuario_IgnoreCase_EncuentraONulo()
        {
            // Arrange
            _servicio.RegistrarUsuario("Ana");

            // Act & Assert
            Assert.NotNull(_servicio.BuscarUsuario("ANA"));
            Assert.Null(_servicio.BuscarUsuario("Inexistente"));
        }

        [Fact]
        public void EliminarUsuario_ExistenteEInexistente_RetornaBooleano()
        {
            // Arrange
            _servicio.RegistrarUsuario("Eliminar");

            // Act & Assert
            Assert.True(_servicio.EliminarUsuario("Eliminar"));
            Assert.Equal(0, _servicio.ContarUsuarios());
            Assert.False(_servicio.EliminarUsuario("Falso"));
        }

        [Fact]
        public void OrdenarCatalogo_Burbuja_LanzaArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _servicio.OrdenarCatalogo("burbuja"));
        }

        [Fact]
        public void OrdenarColeccionYCalcularHoras_RetornaSumaYOrdena()
        {
            // Arrange
            var user = new Usuario("Test");
            user.CrearColeccion("MiColeccion");
            user.AgregarJuegoAColeccion("MiColeccion", new Videojuego("Hades", "Supergiant", 25));
            user.AgregarJuegoAColeccion("MiColeccion", new Videojuego("Celeste", "Matt", 8));

            // Act
            int suma = _servicio.OrdenarColeccionYCalcularHoras(user, "MiColeccion");
            var col = user.ObtenerColeccion("MiColeccion");

            // Assert
            Assert.Equal(33, suma);
            Assert.Equal("Celeste", col[0].Titulo); // 8h queda primero
        }

        [Fact]
        public void ExportarColecciones_LlamaAlExportadorUnaVez()
        {
            // Arrange
            var user = new Usuario("Test");
            string ruta = "test.txt";

            // Act
            _servicio.ExportarColecciones(user, ruta);

            // Assert
            Assert.Equal(1, _exportadorPrueba.LlamadasExportar);
            Assert.Equal(user, _exportadorPrueba.UsuarioGuardado);
            Assert.Equal(ruta, _exportadorPrueba.RutaGuardada);
        }
    }
}