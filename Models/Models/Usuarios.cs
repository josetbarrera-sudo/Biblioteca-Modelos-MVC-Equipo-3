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
    public class Usuarios : Persona
    {
        private int _idUsuario;
        private int _librosPrestados;
        private decimal _multaAcumulada;
        private bool _esProfesor;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public string Nombre
        {
            get => NombreCompleto;
            set => NombreCompleto = value;
        }

        public int IdUsuario
        {
            get => _idUsuario;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ID del usuario debe ser mayor a 0.");
                _idUsuario = value;
            }
        }

        public int LibrosPrestados
        {
            get => _librosPrestados;
            set
            {
                if (value < 0)
                    throw new ArgumentException("La cantidad de libros no puede ser negativa.");
                _librosPrestados = value;
            }
        }

        public decimal MultaAcumulada
        {
            get => _multaAcumulada;
            set
            {
                if (value < 0)
                    throw new ArgumentException("La multa acumulada no puede ser negativa.");
                _multaAcumulada = value;
            }
        }

        public bool EsProfesor
        {
            get => _esProfesor;
            set => _esProfesor = value;
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "usuario_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Usuarios() : base()
        {
            this.IdUsuario = 1;
            this.LibrosPrestados = 0;
            this.MultaAcumulada = 0m;
            this.EsProfesor = false;
            this.RutaImagen = "usuario_default.png";
            this.Estado = true;
        }

        public Usuarios(int idUsuario, string nombre, int librosPrestados, decimal multaAcumulada, bool esProfesor)
            : base(1, nombre, 18, "correo@ejemplo.com")
        {
            this.IdUsuario = idUsuario;
            this.LibrosPrestados = librosPrestados;
            this.MultaAcumulada = multaAcumulada;
            this.EsProfesor = esProfesor;
            this.RutaImagen = "usuario_default.png";
            this.Estado = true;
        }

        public Usuarios(int idPersona, string nombreCompleto, int edad, string correo, int idUsuario, int librosPrestados, decimal multaAcumulada, bool esProfesor, string rutaImagen, bool estado)
            : base(idPersona, nombreCompleto, edad, correo)
        {
            this.IdUsuario = idUsuario;
            this.LibrosPrestados = librosPrestados;
            this.MultaAcumulada = multaAcumulada;
            this.EsProfesor = esProfesor;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public decimal CalcularMultaTotal()
        {
            return this.MultaAcumulada;
        }

        public decimal CalcularMultaTotal(int diasRetrasoNuevos)
        {
            if (diasRetrasoNuevos < 0)
                throw new ArgumentException("Los días de retraso no pueden ser negativos.");

            decimal tarifaDiariaPorMora = this.EsProfesor ? 10.0m : 15.0m;
            return this.MultaAcumulada + (diasRetrasoNuevos * tarifaDiariaPorMora);
        }

        public override string ToString()
        {
            string tipoUsuario = EsProfesor ? "Profesor" : "Estudiante";
            string estadoStr = Estado ? "Activo" : "Inactivo";
            return $"[Usuario #{IdUsuario}] {NombreCompleto} ({tipoUsuario}) | Libros: {LibrosPrestados} | Multa: ${MultaAcumulada:F2} | Estado: {estadoStr}";
        }
    }
}
