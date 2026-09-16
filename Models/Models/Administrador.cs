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
    public class Autores : Persona
    {
        private string _nacionalidad = string.Empty;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public string Nacionalidad
        {
            get => _nacionalidad;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La nacionalidad no puede estar vacía.");
                _nacionalidad = value.Trim();
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "autor_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public int Codigo
        {
            get => Id;
            set => Id = value;
        }

        public string Nombre
        {
            get => NombreCompleto;
            set => NombreCompleto = value;
        }

        public Autores() : base()
        {
            this.NombreCompleto = "Autor Anónimo";
            this.Nacionalidad = "Desconocida";
            this.RutaImagen = "autor_default.png";
            this.Estado = true;
        }

        public Autores(int idPersona, string nombreCompleto, int edad, string correo, string nacionalidad, string rutaImagen, bool estado)
            : base(idPersona, nombreCompleto, edad, correo)
        {
            this.Nacionalidad = nacionalidad;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public Autores(int codigo, string nombre, string nacionalidad)
            : base(codigo, nombre, 40, "autor@biblioteca.com")
        {
            this.Nacionalidad = nacionalidad;
            this.RutaImagen = "autor_default.png";
            this.Estado = true;
        }

        public Autores(int codigo, string nombre, string nacionalidad, string extra)
            : this(codigo, nombre, nacionalidad) { }

        public Autores(int codigo, string nombre, string nacionalidad, int extra)
            : this(codigo, nombre, nacionalidad) { }

        public decimal CalcularRegalias(decimal ventasTotales)
        {
            if (ventasTotales < 0)
                throw new ArgumentException("Las ventas totales no pueden ser negativas.");
            return ventasTotales * 0.10m;
        }

        public decimal CalcularRegalias(decimal ventasTotales, decimal porcentaje)
        {
            if (ventasTotales < 0)
                throw new ArgumentException("Las ventas totales no pueden ser negativas.");
            if (porcentaje < 0)
                porcentaje = 0;
            return ventasTotales * (porcentaje / 100m);
        }

        public void ImprimirA()
        {
            Console.WriteLine($"   Nombre: {NombreCompleto}\n   Código: {Id}\n   Nacionalidad: {Nacionalidad}");
        }

        public override string ToString()
        {
            return $"Código: {Id} | Autor: {NombreCompleto} | Nacionalidad: {Nacionalidad} | Estado: {(Estado ? "Activo" : "Inactivo")}";
        }
    }
}