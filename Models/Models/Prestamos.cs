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
    public class Prestamos
    {
        private int _id;
        private Usuarios _usuario = new Usuarios();
        private Libros[] _librosPrestados = new Libros[3];
        private DateTime _fechaEntrega;
        private bool _status;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0) throw new ArgumentException("ID debe ser mayor a 0.");
                _id = value;
            }
        }

        public Usuarios Usuario { get => _usuario; set => _usuario = value ?? new Usuarios(); }
        public Libros[] LibrosPrestados { get => _librosPrestados; set => _librosPrestados = value ?? new Libros[3]; }
        public DateTime FechaEntrega { get => _fechaEntrega; set => _fechaEntrega = value; }
        public bool Status { get => _status; set => _status = value; }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "prestamo_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Prestamos()
        {
            this.Id = 1;
            this._usuario = new Usuarios();
            this.FechaEntrega = DateTime.Now.AddDays(7);
            this.Status = true;
            this.RutaImagen = "prestamo_default.png";
            this.Estado = true;
        }

        public Prestamos(int id, Usuarios usuario, Libros libroInicial, string rutaImagen, bool estado)
        {
            this.Id = id;
            this._usuario = usuario ?? new Usuarios();
            this.LibrosPrestados[0] = libroInicial;
            this.FechaEntrega = DateTime.Now.AddDays(7);
            this.Status = true;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public string ImprimirP()
        {
            string estado = Status ? "PENDIENTE" : "ENTREGADO";
            return $"Ticket: {Id} | Usuario: {Usuario.Nombre} | Estado: {estado}";
        }

        public string ImprimirP(object parametroExtra)
        {
            return ImprimirP();
        }

        public override string ToString() => ImprimirP();
    }
}
