using System.IO;
using Xunit;
using Juegoteca.Biblioteca.Servicios;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Tests.Datos;

namespace Juegoteca.Tests
{
    public class ExportadorTxtTests
    {
        private string _rutaTemp;
        private ExportadorTxt _exportador;

        public ExportadorTxtTests()
        {
            _rutaTemp = Path.GetTempFileName();
            _exportador = new ExportadorTxt();
        }

        [Fact]
        public void ExportarATxt_CreaArchivo_ConContenidoCorrecto()
        {
            try
            {
                // Arrange
                var usuario = DatosPrueba.UsuarioConColecciones();

                // Act
                _exportador.ExportarATxt(usuario, _rutaTemp);
                string contenido = File.ReadAllText(_rutaTemp);

                // Assert
                Assert.True(File.Exists(_rutaTemp));
                Assert.Contains("Jugador: Ana", contenido);
                Assert.Contains("Colección: Pendientes", contenido);
                Assert.Contains("Celeste - Matt Makes Games (8 h)", contenido);
                Assert.Contains("Total de horas en la colección: 8 h", contenido);
            }
            finally { File.Delete(_rutaTemp); }
        }

        [Fact]
        public void ExportarATxt_UsuarioSinColecciones_SoloEscribeEncabezado()
        {
            try
            {
                // Arrange
                var usuario = new Usuario("SinJuegos");

                // Act
                _exportador.ExportarATxt(usuario, _rutaTemp);
                string contenido = File.ReadAllText(_rutaTemp);

                // Assert
                Assert.Contains("Jugador: SinJuegos", contenido);
                Assert.DoesNotContain("Colección:", contenido);
            }
            finally { File.Delete(_rutaTemp); }
        }

        [Fact]
        public void ExportarATxt_Sobrescritura_MantieneSoloUltimaExportacion()
        {
            try
            {
                // Arrange
                var usr1 = new Usuario("Primero");
                var usr2 = new Usuario("Segundo");

                // Act
                _exportador.ExportarATxt(usr1, _rutaTemp);
                _exportador.ExportarATxt(usr2, _rutaTemp); // Sobrescribe
                string contenido = File.ReadAllText(_rutaTemp);

                // Assert
                Assert.Contains("Jugador: Segundo", contenido);
                Assert.DoesNotContain("Jugador: Primero", contenido);
            }
            finally { File.Delete(_rutaTemp); }
        }
    }
}