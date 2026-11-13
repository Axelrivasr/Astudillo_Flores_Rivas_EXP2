using System.Collections.Generic;
using Xunit;
using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Tests.Datos;

namespace Juegoteca.Tests
{
    public class AlgoritmosTests
    {
        private GestorVideojuegos _gestor;

        public AlgoritmosTests()
        {
            _gestor = DatosPrueba.GestorPoblado();
        }

        [Fact]
        public void QuickSort_ListaCompleta_OrdenaAscendenteEnElLugar()
        {
            // Arrange
            var lista = _gestor.JuegosDisponibles;

            // Act
            _gestor.QuickSort(lista);

            // Assert
            Assert.Equal("Tetris", lista[0].Titulo); // 5h
            Assert.Equal("Zelda: BOTW", lista[^1].Titulo); // 120h
        }

        [Fact]
        public void MergeSort_ListaCompleta_RetornaListaNuevaOrdenada()
        {
            // Arrange
            var listaOriginal = _gestor.JuegosDisponibles;
            var originalTercerElemento = listaOriginal[2].Titulo;

            // Act
            var nuevaLista = _gestor.MergeSort(listaOriginal);

            // Assert
            Assert.Equal("Tetris", nuevaLista[0].Titulo);
            Assert.NotSame(listaOriginal, nuevaLista);
            Assert.Equal(originalTercerElemento, listaOriginal[2].Titulo); // Original intacta
        }

        [Fact]
        public void Ordenamiento_CasosLimite_NoFallaYSigueOrdenado()
        {
            // Arrange
            var vacia = new List<Videojuego>();
            var unElemento = new List<Videojuego> { new Videojuego("Juego", "Est", 10) };

            // Act
            _gestor.QuickSort(vacia);
            var mergeUn = _gestor.MergeSort(unElemento);

            // Assert
            Assert.Empty(vacia);
            Assert.Single(mergeUn);
        }

        [Fact]
        public void BusquedaBinaria_Encontrado_RetornaIndiceValido()
        {
            // Act
            int idxHades = _gestor.BusquedaBinaria("Hades");
            int idxPortal = _gestor.BusquedaBinaria("PORTAL 2");

            // Assert
            Assert.True(idxHades >= 0);
            Assert.True(idxPortal >= 0);
        }

        [Fact]
        public void BusquedaBinaria_NoEncontradoYVacio_RetornaMenosUno()
        {
            // Act
            int idx = _gestor.BusquedaBinaria("FakeGame");
            _gestor.LimpiarCatalogo();
            int idxVacio = _gestor.BusquedaBinaria("Hades");

            // Assert
            Assert.Equal(-1, idx);
            Assert.Equal(-1, idxVacio);
        }

        [Fact]
        public void BusquedaBinaria_NoAlteraOrdenOriginal()
        {
            // Arrange
            var original = new List<Videojuego>(_gestor.JuegosDisponibles);

            // Act
            _gestor.BusquedaBinaria("Hades");

            // Assert
            for (int i = 0; i < original.Count; i++)
            {
                Assert.Equal(original[i].Titulo, _gestor.JuegosDisponibles[i].Titulo);
            }
        }
    }
}