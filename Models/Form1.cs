/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */

using System;
using System.Windows.Forms;
using Biblioteca.Models;

namespace Biblioteca
{
    public partial class Form1 : Form
    {
        private static int _contadorIdLibros = 1;

        public Form1()
        {
            InitializeComponent();

            // Usuarios
            btnAgregarUsuario.Click += btnAgregarUsuario_Click;
            btnModificarUsuario.Click += btnModificarUsuario_Click;
            btnEliminarUsuario.Click += btnEliminarUsuario_Click;
            btnLimpiarUsuario.Click += btnLimpiarUsuario_Click;

            // Libros
            btnCrearLi.Click += btnCrearLi_Click;
            btnBuscarLi.Click += btnBuscarLi_Click;
            btnMostrarLi.Click += btnMostrarLi_Click;
            btnActualizarLi.Click += btnActualizarLi_Click;
            btnBorrarLi.Click += btnBorrarLi_Click;

            // Género
            btn_GenModificar.Click += btn_GenModificar_Click;
            btn_GenEliminar.Click += btn_GenEliminar_Click;
            btn_GenLimpiar.Click += btn_GenLimpiar_Click;
        }

        private void label51_Click(object sender, EventArgs e)
        {
        }

        // =====================================================
        // USUARIOS
        // =====================================================
        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuarios nuevo = new Usuarios(
                    int.Parse(txtIDUsuario.Text),
                    txtNombreUsuario.Text,
                    int.Parse(txtLibrosPrestaodosUsuario.Text),
                    decimal.Parse(txtMultaAcumulada.Text),
                    chbRol.Checked
                );
                nuevo.RutaImagen = txtImagenUsuario.Text;
                nuevo.EsActivo = chbEstadoUsuario.Checked;

                nuevo.InsertarRegistro(nuevo);

                RefrescarListaUsuarios();
                MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuarios actualizado = new Usuarios(
                    int.Parse(txtIDUsuario.Text),
                    txtNombreUsuario.Text,
                    int.Parse(txtLibrosPrestaodosUsuario.Text),
                    decimal.Parse(txtMultaAcumulada.Text),
                    chbRol.Checked
                );
                actualizado.RutaImagen = txtImagenUsuario.Text;
                actualizado.EsActivo = chbEstadoUsuario.Checked;

                actualizado.ActualizarRegistro(actualizado);

                RefrescarListaUsuarios();
                MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuarios auxiliar = new Usuarios();
                auxiliar.EliminarRegistro(txtIDUsuario.Text);

                RefrescarListaUsuarios();
                MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarUsuario_Click(object sender, EventArgs e)
        {
            txtCodUsuario.Clear();
            txtIDUsuario.Clear();
            txtNombreUsuario.Clear();
            txtLibrosPrestaodosUsuario.Clear();
            txtEdadUsuario.Clear();
            txtMultaAcumulada.Clear();
            txtCorreoUsuario.Clear();
            txtNacionalidadUsuario.Clear();
            txtImagenUsuario.Clear();
            chbRol.Checked = false;
            chbEstadoUsuario.Checked = false;
        }

        private void RefrescarListaUsuarios()
        {
            txtInfoUsuario.Clear();
            foreach (Usuarios u in Usuarios.ObtenerTodos())
                txtInfoUsuario.AppendText(u.ToString() + Environment.NewLine);
        }

        // =====================================================
        // LIBROS
        // =====================================================
        private void btnCrearLi_Click(object sender, EventArgs e)
        {
            try
            {
                Libros nuevo = new Libros(
                    _contadorIdLibros++,
                    txtIsbn.Text,
                    txtTitulo.Text,
                    decimal.Parse(txtCosto.Text),
                    1,
                    cbxNovedad.Text == "Si",
                    txtImagen.Text,
                    cbxEstado.Text == "Disposnible"
                );

                nuevo.InsertarRegistro(nuevo);
                MessageBox.Show("Libro creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarTodosLosLibros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarLi_Click(object sender, EventArgs e)
        {
            Libros encontrado = BuscarLibroPorIsbn(txtIsbn.Text);
            if (encontrado != null)
            {
                txtTitulo.Text = encontrado.Titulo;
                txtCosto.Text = encontrado.PrecioRentaDiaria.ToString();
                cbxNovedad.Text = encontrado.EsNovedad ? "Si" : "No";
                cbxEstado.Text = encontrado.EsActivo ? "Disposnible" : "Prestado";
                txtImagen.Text = encontrado.RutaImagen;
                MessageBox.Show("Libro encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se encontró un libro con ese ISBN.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMostrarLi_Click(object sender, EventArgs e)
        {
            MostrarTodosLosLibros();
        }

        private void btnActualizarLi_Click(object sender, EventArgs e)
        {
            try
            {
                Libros existente = BuscarLibroPorIsbn(txtIsbn.Text);
                if (existente == null)
                {
                    MessageBox.Show("No se encontró el libro a actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Libros actualizado = new Libros(
                    existente.Id,
                    txtIsbn.Text,
                    txtTitulo.Text,
                    decimal.Parse(txtCosto.Text),
                    existente.CopiasDisponibles,
                    cbxNovedad.Text == "Si",
                    txtImagen.Text,
                    cbxEstado.Text == "Disposnible"
                );

                actualizado.ActualizarRegistro(actualizado);
                MessageBox.Show("Libro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarTodosLosLibros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrarLi_Click(object sender, EventArgs e)
        {
            try
            {
                Libros existente = BuscarLibroPorIsbn(txtIsbn.Text);
                if (existente == null)
                {
                    MessageBox.Show("No se encontró el libro a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                existente.EliminarRegistro(existente.Id.ToString());
                MessageBox.Show("Libro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarTodosLosLibros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Libros BuscarLibroPorIsbn(string isbn)
        {
            return Libros.ObtenerTodos().Find(l => l.ISBN == isbn);
        }

        private void MostrarTodosLosLibros()
        {
            txbInfo.Clear();
            foreach (Libros l in Libros.ObtenerTodos())
                txbInfo.AppendText(l.ToString() + Environment.NewLine);
        }

        // =====================================================
        // GÉNERO
        // =====================================================
        private void btn_GenAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Genero nuevo = new Genero(
                    int.Parse(txB_IDGenero.Text),
                    txB_GenNombre.Text,
                    txB_GenDescripcion.Text,
                    textBox7.Text,
                    ckB_Genero_Activo.Checked
                );

                nuevo.InsertarRegistro(nuevo);
                MessageBox.Show("Género agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_GenModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Genero actualizado = new Genero(
                    int.Parse(txB_IDGenero.Text),
                    txB_GenNombre.Text,
                    txB_GenDescripcion.Text,
                    textBox7.Text,
                    ckB_Genero_Activo.Checked
                );

                actualizado.ActualizarRegistro(actualizado);
                MessageBox.Show("Género actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_GenEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Genero auxiliar = new Genero();
                auxiliar.EliminarRegistro(txB_IDGenero.Text);
                MessageBox.Show("Género eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_GenLimpiar_Click(object sender, EventArgs e)
        {
            txB_IDGenero.Clear();
            txB_GenNombre.Clear();
            txB_GenDescripcion.Clear();
            textBox7.Clear();
            ckB_Genero_Activo.Checked = false;
        }
    }
}