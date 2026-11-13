using System.Collections.Generic;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Interfaces
{
    // 1. Segregación de Interfaces
    public interface ICatalogoVideojuegos
    {
        List<Videojuego> JuegosDisponibles { get; set; }
        void AgregarJuego(Videojuego juego);
        bool EliminarJuego(Videojuego juego);
        bool EliminarPorIndice(int indice);
        void LimpiarCatalogo();
    }

    public interface IOrdenadorVideojuegos
    {
        void QuickSort(List<Videojuego> lista);
        List<Videojuego> MergeSort(List<Videojuego> lista);
    }

    public interface IBuscadorVideojuegos
    {
        List<Videojuego> BuscarPorTitulo(string texto);
        int BusquedaBinaria(string titulo);
    }

    // 2. Composición (Mantiene la compatibilidad con el proyecto escolar)
    public interface IGestorVideojuegos : ICatalogoVideojuegos, IOrdenadorVideojuegos, IBuscadorVideojuegos
    {
    }
}