namespace Juegoteca.Biblioteca.Interfaces
{
    public interface IVideojuego
    {
        string Titulo { get; set; }
        string Estudio { get; set; }
        int HorasJuego { get; set; }
        string ToString();
    }
}