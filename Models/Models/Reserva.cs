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
    public class Reserva
    {
        private int _idReserva;
        private Usuarios _usuario = new Usuarios();
        private Libros _libroReservado = new Libros();
        private DateTime _fechaReserva;
        private DateTime _fechaLimite;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public int IdReserva
        {
            get => _idReserva;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ID de la reserva debe ser mayor a 0.");
                _idReserva = value;
            }
        }

        public Usuarios Usuario
        {
            get => _usuario;
            set => _usuario = value ?? new Usuarios();
        }

        public Libros LibroReservado
        {
            get => _libroReservado;
            set => _libroReservado = value ?? new Libros();
        }

        public DateTime FechaReserva
        {
            get => _fechaReserva;
            set => _fechaReserva = value;
        }

        public DateTime FechaLimite
        {
            get => _fechaLimite;
            set
            {
                if (value < _fechaReserva)
                    throw new ArgumentException("La fecha límite no puede ser anterior a la fecha de reserva.");
                _fechaLimite = value;
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "reserva_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Reserva()
        {
            this.IdReserva = 1;
            this._usuario = new Usuarios();
            this._libroReservado = new Libros();
            this.FechaReserva = DateTime.Now;
            this.FechaLimite = DateTime.Now.AddDays(3);
            this.RutaImagen = "reserva_default.png";
            this.Estado = false;
        }

        public Reserva(int idReserva, Usuarios usuario, Libros libroReservado, DateTime fechaReserva, DateTime fechaLimite, string rutaImagen, bool estado)
        {
            this.IdReserva = idReserva;
            this.Usuario = usuario;
            this.LibroReservado = libroReservado;
            this.FechaReserva = fechaReserva;
            this.FechaLimite = fechaLimite;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public int CalcularDiasRestantes()
        {
            TimeSpan diferencia = this.FechaLimite - DateTime.Now;
            return diferencia.Days;
        }

        public int CalcularDiasRestantes(DateTime fechaReferencia)
        {
            TimeSpan diferencia = this.FechaLimite - fechaReferencia;
            return diferencia.Days;
        }

        public override string ToString()
        {
            string estadoStr = Estado ? "Vigente" : "Cancelada/Vencida";
            return $"[Reserva #{IdReserva}] Usuario: {Usuario.NombreCompleto} | Libro: {LibroReservado.Titulo} | Límite: {FechaLimite:d} | Estado: {estadoStr}";
        }
    }
}
