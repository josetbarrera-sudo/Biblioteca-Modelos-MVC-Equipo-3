using System;

namespace Biblioteca.Models
{
    public interface IAlmacenamientoCRUD
    {
        void InsertarRegistro(object objeto);
        object ConsultarRegistro(string id);
        void ActualizarRegistro(object objeto);
        void EliminarRegistro(string id);
    }
}