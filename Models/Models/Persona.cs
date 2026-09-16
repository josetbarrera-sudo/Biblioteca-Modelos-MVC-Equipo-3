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
    public class Persona : EntidadBase
    {
        protected string _nombreCompleto = string.Empty;
        protected int _edad;
        protected string _correo = string.Empty;

        public string NombreCompleto
        {
            get => _nombreCompleto;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre completo no puede estar vacío.");
                _nombreCompleto = value.Trim();
            }
        }

        public int Edad
        {
            get => _edad;
            set
            {
                if (value < 0 || value > 120)
                    throw new ArgumentOutOfRangeException(nameof(value), "La edad debe estar en un rango de 0 a 120 años.");
                _edad = value;
            }
        }

        public string Correo
        {
            get => _correo;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("El correo electrónico proporcionado no tiene un formato válido.");
                _correo = value.Trim();
            }
        }

        public Persona()
            : base(1, DateTime.Now, true)
        {
            this.NombreCompleto = "Sin Nombre";
            this.Edad = 18;
            this.Correo = "correo@ejemplo.com";
        }

        public Persona(int idPersona, string nombreCompleto, int edad, string correo)
            : base(idPersona, DateTime.Now, true)
        {
            this.NombreCompleto = nombreCompleto;
            this.Edad = edad;
            this.Correo = correo;
        }

        public virtual string ObtenerPerfil()
        {
            return $"ID: {this.Id} | Nombre: {this.NombreCompleto}";
        }

        public virtual string ObtenerPerfil(bool incluirContacto)
        {
            string perfilBase = ObtenerPerfil();
            return incluirContacto ? $"{perfilBase} | Correo: {this.Correo} | Edad: {this.Edad}" : perfilBase;
        }

        public override string ToString()
        {
            return $"[Persona] {ObtenerPerfil(true)}";
        }
    }
}