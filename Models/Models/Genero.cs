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
    public class Genero
    {
        private int _idGenero;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public int IdGenero
        {
            get => _idGenero;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ID del género debe ser mayor a 0.");
                _idGenero = value;
            }
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del género no puede estar vacío.");
                _nombre = value.Trim();
            }
        }

        public string Descripcion
        {
            get => _descripcion;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La descripción no puede estar vacía.");
                _descripcion = value.Trim();
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "genero_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Genero()
        {
            this.IdGenero = 1;
            this.Nombre = "General";
            this.Descripcion = "Sin descripción asignada";
            this.RutaImagen = "genero_default.png";
            this.Estado = true;
        }

        public Genero(int idGenero, string nombre, string descripcion, string rutaImagen, bool estado)
        {
            this.IdGenero = idGenero;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public string GenerarEtiqueta()
        {
            return $"#{Nombre.ToUpper().Replace(" ", "")}";
        }

        public string GenerarEtiqueta(string prefijo)
        {
            if (string.IsNullOrWhiteSpace(prefijo))
                throw new ArgumentException("El prefijo no puede estar vacío.");
            return $"#{prefijo.ToUpper()}_{Nombre.ToUpper().Replace(" ", "")}";
        }

        public override string ToString()
        {
            string estadoStr = Estado ? "Activo" : "Inactivo";
            return $"[Género #{IdGenero}] {Nombre} | {Descripcion} | Estado: {estadoStr}";
        }
    }
}
