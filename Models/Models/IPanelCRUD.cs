using System.Windows.Forms;

namespace Biblioteca
{
    public interface IPanelCRUD
    {
        void EjecutarGuardar();

        void EjecutarBuscar(string id, ErrorProvider alerta);

        void EjecutarActualizar();

        void EjecutarEliminar(StatusStrip barraEstado);
    }
}