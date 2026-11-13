using System;
using System.Collections.Generic;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Gestores
{
    public partial class GestorVideojuegos
    {
        public void QuickSort(List<Videojuego> lista)
        {
            if (lista == null || lista.Count <= 1) return;
            QuickSortRecursivo(lista, 0, lista.Count - 1);
        }

        private void QuickSortRecursivo(List<Videojuego> lista, int inicio, int fin)
        {
            if (inicio < fin)
            {
                int p = Particionar(lista, inicio, fin);
                QuickSortRecursivo(lista, inicio, p - 1);
                QuickSortRecursivo(lista, p + 1, fin);
            }
        }

        private int Particionar(List<Videojuego> lista, int inicio, int fin)
        {
            int pivote = lista[fin].HorasJuego;
            int i = inicio - 1;
            for (int j = inicio; j < fin; j++)
            {
                if (lista[j].HorasJuego <= pivote)
                {
                    i++;
                    (lista[j], lista[i]) = (lista[i], lista[j]);
                }
            }
            (lista[fin], lista[i + 1]) = (lista[i + 1], lista[fin]);
            return i + 1;
        }

        public List<Videojuego> MergeSort(List<Videojuego> lista)
        {
            if (lista == null || lista.Count <= 1)
                return lista != null ? new List<Videojuego>(lista) : null;

            int medio = lista.Count / 2;
            var izquierda = MergeSort(lista.GetRange(0, medio));
            var derecha = MergeSort(lista.GetRange(medio, lista.Count - medio));

            return Mezclar(izquierda, derecha);
        }

        private List<Videojuego> Mezclar(List<Videojuego> izq, List<Videojuego> der)
        {
            var resultado = new List<Videojuego>();
            int i = 0, j = 0;

            while (i < izq.Count && j < der.Count)
            {
                if (izq[i].HorasJuego <= der[j].HorasJuego)
                    resultado.Add(izq[i++]);
                else
                    resultado.Add(der[j++]);
            }

            while (i < izq.Count) resultado.Add(izq[i++]);
            while (j < der.Count) resultado.Add(der[j++]);

            return resultado;
        }

        public int BusquedaBinaria(string titulo)
        {
            if (JuegosDisponibles == null || JuegosDisponibles.Count == 0) return -1;

            var copia = new List<Videojuego>(JuegosDisponibles);

            // Ordenamiento burbuja manual por título para la búsqueda binaria
            for (int i = 0; i < copia.Count - 1; i++)
            {
                for (int j = 0; j < copia.Count - 1 - i; j++)
                {
                    if (string.Compare(copia[j].Titulo, copia[j + 1].Titulo, StringComparison.OrdinalIgnoreCase) > 0)
                        (copia[j], copia[j + 1]) = (copia[j + 1], copia[j]);
                }
            }

            int inicio = 0, fin = copia.Count - 1;
            while (inicio <= fin)
            {
                int medio = inicio + (fin - inicio) / 2;
                int cmp = string.Compare(copia[medio].Titulo, titulo, StringComparison.OrdinalIgnoreCase);

                if (cmp == 0) return medio; // Retorna el índice en la copia ordenada (como es requerido para la UI posterior)
                if (cmp < 0) inicio = medio + 1;
                else fin = medio - 1;
            }
            return -1;
        }
    }
}