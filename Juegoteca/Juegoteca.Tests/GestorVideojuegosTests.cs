using Xunit;
using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Tests.Datos;

namespace Juegoteca.Tests
{
    public class GestorVideojuegosTests
    {
        private GestorVideojuegos _gestor;

        public GestorVideojuegosTests()
        {
            _gestor = DatosPrueba.GestorPoblado();
        }

        [Fact]
        public void Constructor_CreaListasIndependientesYNoNulas()
        {
            // Arrange & Act
            var gestor1 = new GestorVideojuegos();
            var gestor2 = new GestorVideojuegos();
            gestor1.AgregarJuego(new Videojuego("A", "B", 10));

            // Assert
            Assert.NotNull(gestor1.JuegosDisponibles);
            Assert.NotNull(gestor2.JuegosDisponibles);
            Assert.Single(gestor1.JuegosDisponibles);
            Assert.Empty(gestor2.JuegosDisponibles);
        }

        [Fact]
        public void AgregarJuego_NuevoYDuplicado_FuncionaComoSeEspera()
        {
            // Arrange
            var juegoNuevo = new Videojuego("Nuevo", "Est", 10);

            // Act
            _gestor.AgregarJuego(juegoNuevo); // 8 -> 9
            _gestor.AgregarJuego(juegoNuevo); // intencionalmente duplicado

            // Assert
            Assert.Equal(9, _gestor.JuegosDisponibles.Count);
        }

        [Fact]
        public void EliminarJuego_ExistenteEInexistente_RetornaBooleano()
        {
            // Arrange
            var juegoAEliminar = _gestor.JuegosDisponibles[0];
            var juegoFalso = new Videojuego("Fake", "Fake", 10);

            // Act & Assert
            Assert.True(_gestor.EliminarJuego(juegoAEliminar));
            Assert.Equal(7, _gestor.JuegosDisponibles.Count);

            Assert.False(_gestor.EliminarJuego(juegoFalso));
            Assert.Equal(7, _gestor.JuegosDisponibles.Count);
        }

        [Fact]
        public void EliminarPorIndice_RangoValidoEInvalido_RetornaBooleano()
        {
            // Act & Assert
            Assert.True(_gestor.EliminarPorIndice(0));
            Assert.Equal(7, _gestor.JuegosDisponibles.Count);

            Assert.False(_gestor.EliminarPorIndice(99));
        }

        [Fact]
        public void BuscarPorTitulo_EncuentraParcial_IgnoreCase()
        {
            // Act
            var resultados = _gestor.BuscarPorTitulo("KNIGHT");
            var vacio = _gestor.BuscarPorTitulo("xyz");

            // Assert
            Assert.Single(resultados);
            Assert.Equal("Hollow Knight", resultados[0].Titulo);
            Assert.Empty(vacio);
        }

        [Fact]
        public void LimpiarCatalogo_VaciaLista()
        {
            // Act
            _gestor.LimpiarCatalogo();

            // Assert
            Assert.Empty(_gestor.JuegosDisponibles);
        }

        [Fact]
        public void CatalogoVacio_NoFallaAlBuscarOEliminar()
        {
            // Arrange
            var gestorVacio = new GestorVideojuegos();

            // Act & Assert
            Assert.Empty(gestorVacio.BuscarPorTitulo("Test"));
            Assert.False(gestorVacio.EliminarPorIndice(0));
        }
    }
}