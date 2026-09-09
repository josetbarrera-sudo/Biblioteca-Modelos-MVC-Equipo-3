/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */

using System;

namespace Biblioteca.Models
{
    public class Libros
    {
        private string _isbn = string.Empty;
        private string _titulo = string.Empty;
        private decimal _precioRentaDiaria;
        private int _copiasDisponibles;
        private bool _esNovedad;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public string ISBN
        {
            get => _isbn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El ISBN no puede estar vacío.");
                _isbn = value.Trim();
            }
        }

        public string Titulo
        {
            get => _titulo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El título no puede estar vacío.");
                _titulo = value.Trim();
            }
        }

        public decimal PrecioRentaDiaria
        {
            get => _precioRentaDiaria;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El precio de renta debe ser mayor a cero.");
                _precioRentaDiaria = value;
            }
        }

        public int CopiasDisponibles
        {
            get => _copiasDisponibles;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Las copias no pueden ser negativas.");
                _copiasDisponibles = value;
            }
        }

        public bool EsNovedad
        {
            get => _esNovedad;
            set => _esNovedad = value;
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "libro_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Libros()
        {
            this.ISBN = "000-0000000000";
            this.Titulo = "Sin Título";
            this.PrecioRentaDiaria = 20.0m;
            this.CopiasDisponibles = 1;
            this.EsNovedad = false;
            this.RutaImagen = "libro_default.png";
            this.Estado = true;
        }

        public Libros(string isbn, string titulo, decimal precioRentaDiaria, int copiasDisponibles, bool esNovedad, string rutaImagen, bool estado)
        {
            this.ISBN = isbn;
            this.Titulo = titulo;
            this.PrecioRentaDiaria = precioRentaDiaria;
            this.CopiasDisponibles = copiasDisponibles;
            this.EsNovedad = esNovedad;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public decimal CalcularCostoRenta(int dias)
        {
            if (dias <= 0)
                throw new ArgumentException("Los días deben ser mayores a cero.");

            return dias * this.PrecioRentaDiaria;
        }

        public decimal CalcularCostoRenta(int dias, decimal porcentajeDescuento)
        {
            if (porcentajeDescuento < 0 || porcentajeDescuento > 100)
                throw new ArgumentOutOfRangeException(nameof(porcentajeDescuento), "El descuento debe ser entre 0 y 100.");

            decimal costoBase = CalcularCostoRenta(dias);
            decimal descuento = costoBase * (porcentajeDescuento / 100m);
            return costoBase - descuento;
        }

        public override string ToString()
        {
            string novedadStr = EsNovedad ? " (Novedad)" : "";
            string estadoStr = Estado ? "Disponible" : "No disponible";
            return $"[ISBN: {ISBN}] {Titulo}{novedadStr} | Stock: {CopiasDisponibles} | Precio/Día: ${PrecioRentaDiaria:F2} | Estado: {estadoStr}";
        }
    }
}
