/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */
using System;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class Reserva : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Reserva> _listaReservas = new List<Reserva>();

        private Usuarios _usuario = new Usuarios();
        private Libros _libroReservado = new Libros();
        private DateTime _fechaReserva;
        private DateTime _fechaLimite;
        private string _rutaImagen = string.Empty;
        private bool _estado;

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

        // Antes: constructor sin parámetros de base y campo propio "_idReserva = 1".
        // Ahora: Id se maneja a través de EntidadBase (base(...)).
        public Reserva()
            : base(1, DateTime.Now, true)
        {
            this._usuario = new Usuarios();
            this._libroReservado = new Libros();
            this.FechaReserva = DateTime.Now;
            this.FechaLimite = DateTime.Now.AddDays(3);
            this.RutaImagen = "reserva_default.png";
            this.Estado = false;
        }

        // Antes: "int idReserva" se guardaba en un campo propio (_idReserva).
        // Ahora: se pasa a EntidadBase y se usa la propiedad Id heredada.
        public Reserva(int id, Usuarios usuario, Libros libroReservado, DateTime fechaReserva, DateTime fechaLimite, string rutaImagen, bool estado)
            : base(id, DateTime.Now, true)
        {
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

        // ---- IAlmacenamientoCRUD (antes Reserva no implementaba nada) ----
        public void InsertarRegistro(object objeto)
        {
            if (objeto is Reserva reserva)
                _listaReservas.Add(reserva);
            else
                throw new ArgumentException("El objeto no es del tipo Reserva.");
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            return _listaReservas.Find(r => r.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Reserva reservaActualizada)
            {
                Reserva existente = _listaReservas.Find(r => r.Id == reservaActualizada.Id);
                if (existente != null)
                {
                    int indice = _listaReservas.IndexOf(existente);
                    _listaReservas[indice] = reservaActualizada;
                }
                else
                {
                    throw new ArgumentException("No se encontró la reserva a actualizar.");
                }
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Reserva.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            Reserva existente = _listaReservas.Find(r => r.Id == idBuscado);
            if (existente != null)
                _listaReservas.Remove(existente);
            else
                throw new ArgumentException("No se encontró la reserva a eliminar.");
        }

        public static List<Reserva> ObtenerTodos() => _listaReservas;

        public override string ToString()
        {
            string estadoStr = Estado ? "Vigente" : "Cancelada/Vencida";
            return $"[Reserva #{Id}] Usuario: {Usuario.NombreCompleto} | Libro: {LibroReservado.Titulo} | Límite: {FechaLimite:d} | Estado: {estadoStr}";
        }
    }
}