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
    public class Multa
    {
        private int _idMulta;
        private Usuarios _usuario = new Usuarios();
        private string _motivo = string.Empty;
        private decimal _montoBase;
        private DateTime _fechaEmision;
        private bool _pagada;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public int IdMulta
        {
            get => _idMulta;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ID de la multa debe ser mayor a 0.");
                _idMulta = value;
            }
        }

        public Usuarios Usuario
        {
            get => _usuario;
            set => _usuario = value ?? new Usuarios();
        }

        public string Motivo
        {
            get => _motivo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El motivo de la multa no puede estar vacío.");
                _motivo = value.Trim();
            }
        }

        public decimal MontoBase
        {
            get => _montoBase;
            set
            {
                if (value < 0)
                    throw new ArgumentException("El monto de la multa no puede ser negativo.");
                _montoBase = value;
            }
        }

        public DateTime FechaEmision
        {
            get => _fechaEmision;
            set => _fechaEmision = value;
        }

        public bool Pagada
        {
            get => _pagada;
            set => _pagada = value;
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "multa_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Multa()
        {
            this.IdMulta = 1;
            this._usuario = new Usuarios();
            this.Motivo = "Retraso en devolución";
            this.MontoBase = 0m;
            this.FechaEmision = DateTime.Now;
            this.Pagada = false;
            this.RutaImagen = "multa_default.png";
            this.Estado = true;
        }

        public Multa(int idMulta, Usuarios usuario, string motivo, decimal montoBase, DateTime fechaEmision, bool pagada, string rutaImagen, bool estado)
        {
            this.IdMulta = idMulta;
            this.Usuario = usuario;
            this.Motivo = motivo;
            this.MontoBase = montoBase;
            this.FechaEmision = fechaEmision;
            this.Pagada = pagada;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public decimal CalcularMontoConRecargo()
        {
            decimal tasaRecargoFija = 0.10m;
            return this.MontoBase + (this.MontoBase * tasaRecargoFija);
        }

        public decimal CalcularMontoConRecargo(decimal tasaRecargo)
        {
            if (tasaRecargo < 0)
                throw new ArgumentException("La tasa de recargo no puede ser negativa.");
            return this.MontoBase + (this.MontoBase * (tasaRecargo / 100m));
        }

        public override string ToString()
        {
            string estadoPago = Pagada ? "Pagada" : "Pendiente";
            return $"[Multa #{IdMulta}] Usuario: {Usuario.NombreCompleto} | Motivo: {Motivo} | Monto: ${MontoBase:F2} | {estadoPago}";
        }
    }
}
