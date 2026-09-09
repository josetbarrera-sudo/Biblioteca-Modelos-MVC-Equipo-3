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
    public class Editorial
    {
        private int _idEditorial;
        private string _nombre = string.Empty;
        private string _pais = string.Empty;
        private int _anioFundacion;
        private string _correoContacto = string.Empty;
        private string _rutaImagen = string.Empty;
        private bool _estado;

        public int IdEditorial
        {
            get => _idEditorial;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ID de la editorial debe ser mayor a 0.");
                _idEditorial = value;
            }
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de la editorial no puede estar vacío.");
                _nombre = value.Trim();
            }
        }

        public string Pais
        {
            get => _pais;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El país no puede estar vacío.");
                _pais = value.Trim();
            }
        }

        public int AnioFundacion
        {
            get => _anioFundacion;
            set
            {
                if (value <= 0 || value > DateTime.Now.Year)
                    throw new ArgumentException("El año de fundación no es válido.");
                _anioFundacion = value;
            }
        }

        public string CorreoContacto
        {
            get => _correoContacto;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("El correo de contacto no tiene un formato válido.");
                _correoContacto = value.Trim();
            }
        }

        public string RutaImagen
        {
            get => _rutaImagen;
            set => _rutaImagen = string.IsNullOrWhiteSpace(value) ? "editorial_default.png" : value.Trim();
        }

        public bool Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public Editorial()
        {
            this.IdEditorial = 1;
            this.Nombre = "Editorial Sin Nombre";
            this.Pais = "Desconocido";
            this.AnioFundacion = DateTime.Now.Year;
            this.CorreoContacto = "contacto@editorial.com";
            this.RutaImagen = "editorial_default.png";
            this.Estado = true;
        }

        public Editorial(int idEditorial, string nombre, string pais, int anioFundacion, string correoContacto, string rutaImagen, bool estado)
        {
            this.IdEditorial = idEditorial;
            this.Nombre = nombre;
            this.Pais = pais;
            this.AnioFundacion = anioFundacion;
            this.CorreoContacto = correoContacto;
            this.RutaImagen = rutaImagen;
            this.Estado = estado;
        }

        public int CalcularAntiguedad()
        {
            return DateTime.Now.Year - this.AnioFundacion;
        }

        public int CalcularAntiguedad(int anioReferencia)
        {
            if (anioReferencia < this.AnioFundacion)
                throw new ArgumentException("El año de referencia no puede ser anterior a la fundación.");
            return anioReferencia - this.AnioFundacion;
        }

        public override string ToString()
        {
            string estadoStr = Estado ? "Activa" : "Inactiva";
            return $"[Editorial #{IdEditorial}] {Nombre} | País: {Pais} | Fundada: {AnioFundacion} | Estado: {estadoStr}";
        }
    }
}
