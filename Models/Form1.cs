/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Models;

namespace Biblioteca
{
    public partial class Form1 : Form
    {
        private static int _contadorIdLibros = 1;
        private static readonly List<Editorial> _editorialesUI = new List<Editorial>();
        private static readonly List<Multa> _multasUI = new List<Multa>();
        private static readonly List<Genero> _generosUI = new List<Genero>();
        private static readonly List<Prestamos> _prestamosUI = new List<Prestamos>();
        private ComboBox _cbxNivelAccesoUI;
        private Button _btnBuscarAutorUI;
        private Button _btnBuscarGeneroUI;
        private Button _btnBuscarMultaUI;
        private readonly Dictionary<int, string> _estadoEditorialUI = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _estadoAdministradorUI = new Dictionary<int, string>();

        public Form1()
        {
            InitializeComponent();

            // Los eventos de Género, Agregar, ya están conectados desde el Designer.
            ConfigurarEventos();
            ConfigurarNivelAcceso();
            ConfigurarBotonesBuscar();
            ConfigurarEstadosEspeciales();
            InicializarDatosInterfaz();
        }

        private void ConfigurarEventos()
        {
            // Usuarios
            btnAgregarUsuario.Click += btnAgregarUsuario_Click;
            btnModificarUsuario.Click += btnModificarUsuario_Click;
            btnEliminarUsuario.Click += btnEliminarUsuario_Click;
            btnLimpiarUsuario.Click += btnLimpiarUsuario_Click;
            button5.Click += button5_Click;

            // Libros
            btnCrearLi.Click += btnCrearLi_Click;
            btnBuscarLi.Click += btnBuscarLi_Click;
            btnMostrarLi.Click += btnMostrarLi_Click;
            btnActualizarLi.Click += btnActualizarLi_Click;
            btnBorrarLi.Click += btnBorrarLi_Click;

            // Autores
            btn_Autor_AgregarPic.Click += btn_Autor_AgregarPic_Click;
            btn_Autor_ModificarPic.Click += btn_Autor_ModificarPic_Click;
            btn_Autor_EliminarPic.Click += btn_Autor_EliminarPic_Click;
            btn_Autor_LimpiarPic.Click += btn_Autor_LimpiarPic_Click;
            btn_Autor_Imagen.Click += btn_Autor_Imagen_Click;

            // Género
            btn_GenModificar.Click += btn_GenModificar_Click;
            btn_GenEliminar.Click += btn_GenEliminar_Click;
            btn_GenLimpiar.Click += btn_GenLimpiar_Click;
            button6.Click += button6_Click;

            // Préstamos
            btnCrearPres.Click += btnCrearPres_Click;
            btnBuscarPres.Click += btnBuscarPres_Click;
            btnMostrarPres.Click += btnMostrarPres_Click;
            btnActualizarPres.Click += btnActualizarPres_Click;
            btnBorrarrPres.Click += btnBorrarrPres_Click;
            button14.Click += button14_Click;

            // Reservas
            btnCrearReserva.Click += btnCrearReserva_Click;
            btnModificarReserva.Click += btnModificarReserva_Click;
            btnEliminarReserva.Click += btnEliminarReserva_Click;
            btnLimpiarReserva.Click += btnLimpiarReserva_Click;
            button10.Click += button10_Click;

            // Multas
            btn_Multa_Agregar.Click += btn_Multa_Agregar_Click;
            btn_Multa_Modificar.Click += btn_Multa_Modificar_Click;
            btn_Multa_Eliminar.Click += btn_Multa_Eliminar_Click;
            btn_Multa_Limpiar.Click += btn_Multa_Limpiar_Click;
            button7.Click += button7_Click;

            // Editoriales
            btnCrearEdi.Click += btnCrearEdi_Click;
            btnBuscarEdi.Click += btnBuscarEdi_Click;
            btnMostrarEdi.Click += btnMostrarEdi_Click;
            btnActualizarEdi.Click += btnActualizarEdi_Click;
            btnBorarEdi.Click += btnBorarEdi_Click;
            button15.Click += button15_Click;

            // Administradores
            btnCrearAdm.Click += btnCrearAdm_Click;
            btnBuscarAdm.Click += btnBuscarAdm_Click;
            btnMostrarAdm.Click += btnMostrarAdm_Click;
            btnActualizarAdm.Click += btnActualizarAdm_Click;
            btnBorarAdm.Click += btnBorarAdm_Click;
            button16.Click += button16_Click;

            // Personas
            btnCrearPer.Click += btnCrearPer_Click;
            btnBuscarPer.Click += btnBuscarPer_Click;
            btnMostrarPer.Click += btnMostrarPer_Click;
            btnActualizarPer.Click += btnActualizarPer_Click;
            btnBorrarPer.Click += btnBorrarPer_Click;
            button9.Click += button9_Click;
        }

        private void ConfigurarBotonesBuscar()
        {
            _btnBuscarAutorUI = CrearBotonBuscar(
                btn_Autor_LimpiarPic,
                "btnBuscarAutorUI",
                btnBuscarAutorUI_Click
            );

            _btnBuscarGeneroUI = CrearBotonBuscar(
                btn_GenLimpiar,
                "btnBuscarGeneroUI",
                btnBuscarGeneroUI_Click
            );

            _btnBuscarMultaUI = CrearBotonBuscar(
                btn_Multa_Limpiar,
                "btnBuscarMultaUI",
                btnBuscarMultaUI_Click
            );
        }

        private Button CrearBotonBuscar(Button referencia, string nombre, EventHandler evento)
        {
            Button boton = new Button
            {
                Name = nombre,
                Text = "Buscar",
                Size = referencia.Size,
                Font = referencia.Font,
                Anchor = referencia.Anchor,
                TabStop = true
            };

            Control contenedor = referencia.Parent;
            int x = referencia.Right + 8;
            int y = referencia.Top;

            if (x + boton.Width > contenedor.ClientSize.Width)
            {
                x = referencia.Left;
                y = referencia.Bottom + 6;
            }

            boton.Location = new Point(x, y);
            boton.Click += evento;
            contenedor.Controls.Add(boton);
            boton.BringToFront();
            return boton;
        }

        private void ConfigurarEstadosEspeciales()
        {
            cbxEstadoEdi.Items.Clear();
            cbxEstadoEdi.Items.AddRange(new object[]
            {
                "Activo",
                "Inactivo",
                "Bloqueada",
                "Suspendida"
            });
            cbxEstadoEdi.SelectedIndex = 0;

            cbxEstadoAdm.Items.Clear();
            cbxEstadoAdm.Items.AddRange(new object[]
            {
                "Activo",
                "Inactivo",
                "De vacaciones",
                "Suspendido"
            });
            cbxEstadoAdm.SelectedIndex = 0;
        }

        private string ObtenerEstadoEditorial(int id, bool esActivo)
        {
            if (_estadoEditorialUI.TryGetValue(id, out string estado))
                return estado;

            return esActivo ? "Activo" : "Inactivo";
        }

        private string ObtenerEstadoAdministrador(int id, bool esActivo)
        {
            if (_estadoAdministradorUI.TryGetValue(id, out string estado))
                return estado;

            return esActivo ? "Activo" : "Inactivo";
        }

        private bool EstadoEsActivoEditorial(string estado)
        {
            return estado.Equals("Activo", StringComparison.OrdinalIgnoreCase);
        }

        private bool EstadoEsActivoAdministrador(string estado)
        {
            return estado.Equals("Activo", StringComparison.OrdinalIgnoreCase);
        }

        private void btnBuscarAutorUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txB_Autor_Codigo.Text.Trim(), out int codigo))
            {
                MostrarError("El código del autor debe ser un número válido.");
                return;
            }

            Autores encontrado = Autores.ObtenerTodos().Find(a => a.Id == codigo);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el autor.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txB_Autor_Codigo.Text = encontrado.Id.ToString();
            txB_Autor_Nombre.Text = encontrado.NombreCompleto;
            txB_Autor_Edad.Text = encontrado.Edad.ToString();
            txB_Autor_Correo.Text = encontrado.Correo;
            txB_Autor_Nacionalidad.Text = encontrado.Nacionalidad;
            txB_Autor_RutaImagen.Text = encontrado.RutaImagen;
            ckB_Estado.Checked = encontrado.Estado;
            CargarImagen(encontrado.RutaImagen, lblFotoAutor);

            MessageBox.Show("Autor encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscarGeneroUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txB_IDGenero.Text.Trim(), out int id))
            {
                MostrarError("El ID del género debe ser un número válido.");
                return;
            }

            Genero encontrado = _generosUI.Find(g => g.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el género.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txB_IDGenero.Text = encontrado.Id.ToString();
            txB_GenNombre.Text = encontrado.Nombre;
            txB_GenDescripcion.Text = encontrado.Descripcion;
            textBox7.Text = encontrado.RutaImagen;
            ckB_Genero_Activo.Checked = encontrado.EsActivo;
            CargarImagen(encontrado.RutaImagen, lblFotoGenero);

            MessageBox.Show("Género encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscarMultaUI_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txB_IDMulta.Text.Trim(), out int id))
            {
                MostrarError("El ID de la multa debe ser un número válido.");
                return;
            }

            Multa encontrada = _multasUI.Find(m => m.Id == id);
            if (encontrada == null)
            {
                MessageBox.Show("No se encontró la multa.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txB_IDMulta.Text = encontrada.Id.ToString();
            txB_Multa_Motivo.Text = encontrada.Motivo;
            nUD_Multa_MontoBase.Value = encontrada.MontoBase;
            dTP_Multa_FechaEmision.Value = encontrada.FechaEmision;
            ckB_Multa_Pagada.Checked = encontrada.Pagada;
            textBox8.Text = encontrada.RutaImagen;
            ckB_MultaEstado.Checked = encontrada.EsActivo;

            if (encontrada.Usuario != null)
                cmB_Usuario.SelectedItem = encontrada.Usuario;

            CargarImagen(encontrada.RutaImagen, lblFotoMulta);
            MessageBox.Show("Multa encontrada.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =====================================================
        // POLIMORFISMO POR HERENCIA
        // =====================================================

        private string ObtenerInformacionPersonaPolimorfica(Persona persona)
        {
            // La referencia es Persona, pero el objeto real puede ser Usuarios o Autores.
            Persona referencia = persona;

            string informacion = "";
            informacion += $"Nombre: {referencia.NombreCompleto}" + Environment.NewLine;
            informacion += $"Edad: {referencia.Edad}" + Environment.NewLine;
            informacion += $"Correo: {referencia.Correo}" + Environment.NewLine;
            informacion += $"Estado: {(referencia.EsActivo ? "Activo" : "Inactivo")}" + Environment.NewLine;

            if (referencia is Usuarios usuario)
            {
                informacion += "Tipo: Usuario" + Environment.NewLine;
                informacion += $"Libros prestados: {usuario.LibrosPrestados}" + Environment.NewLine;
                informacion += $"Multa acumulada: ${usuario.MultaAcumulada:F2}" + Environment.NewLine;
                informacion += $"Rol: {(usuario.EsProfesor ? "Profesor" : "Estudiante")}" + Environment.NewLine;
            }
            else if (referencia is Autores autor)
            {
                informacion += "Tipo: Autor" + Environment.NewLine;
                informacion += $"Nacionalidad: {autor.Nacionalidad}" + Environment.NewLine;
            }
            else
            {
                informacion += "Tipo: Persona" + Environment.NewLine;
            }

            return informacion;
        }

        private void InicializarDatosInterfaz()
        {
            ActualizarContadorLibros();
            RefrescarListaUsuarios();
            MostrarTodosLosLibros();
            MostrarAutores();
            MostrarGeneros();
            MostrarPrestamos();
            MostrarReservas();
            MostrarMultas();
            MostrarEditoriales();
            MostrarAdministradores();
            MostrarPersonas();
            CargarUsuariosEnCombos();
        }

        // =====================================================
        // MÉTODOS AUXILIARES GENERALES
        // =====================================================

        private void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool TryParseInt(TextBox caja, string nombre, out int valor)
        {
            if (!int.TryParse(caja.Text.Trim(), out valor))
            {
                MostrarError($"El campo {nombre} debe contener un número entero válido.");
                caja.Focus();
                return false;
            }
            return true;
        }

        private bool TryParseDecimal(TextBox caja, string nombre, out decimal valor)
        {
            if (!decimal.TryParse(caja.Text.Trim(), out valor))
            {
                MostrarError($"El campo {nombre} debe contener un número válido.");
                caja.Focus();
                return false;
            }
            return true;
        }

        private bool TryGetUsuario(string textoId, out Usuarios usuario)
        {
            usuario = null;
            if (!int.TryParse(textoId.Trim(), out int id))
                return false;

            usuario = Usuarios.ObtenerTodos().Find(u => u.Id == id);
            return usuario != null;
        }

        private bool TryGetLibro(string isbn, out Libros libro)
        {
            libro = Libros.ObtenerTodos().Find(l => l.ISBN.Equals(isbn.Trim(), StringComparison.OrdinalIgnoreCase));
            return libro != null;
        }

        private void CargarImagen(string ruta, Label destino)
        {
            if (destino == null)
                return;

            if (destino.Image != null)
            {
                Image anterior = destino.Image;
                destino.Image = null;
                anterior.Dispose();
            }

            if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                return;

            try
            {
                using FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
                using Image imagenTemporal = Image.FromStream(stream);
                destino.Image = new Bitmap(imagenTemporal);
            }
            catch
            {
                // La ruta se conserva aunque la imagen no pueda previsualizarse.
            }
        }

        private string SeleccionarImagen(OpenFileDialog dialogo)
        {
            dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Todos los archivos|*.*";
            dialogo.Title = "Seleccionar imagen";

            if (dialogo.ShowDialog() == DialogResult.OK)
                return dialogo.FileName;

            return string.Empty;
        }

        private void ActualizarContadorLibros()
        {
            if (Libros.ObtenerTodos().Count == 0)
                _contadorIdLibros = 1;
            else
                _contadorIdLibros = Libros.ObtenerTodos().Max(l => l.Id) + 1;
        }

        private void CargarUsuariosEnCombos()
        {
            int? idMulta = null;
            int? idReserva = null;

            if (cmB_Usuario.SelectedItem is Usuarios usuarioSeleccionado)
                idMulta = usuarioSeleccionado.Id;

            if (int.TryParse(textBox5.Text.Trim(), out int idReservaTemp))
                idReserva = idReservaTemp;

            cmB_Usuario.DataSource = null;
            cmB_Usuario.DataSource = Usuarios.ObtenerTodos().ToList();
            cmB_Usuario.DisplayMember = "NombreCompleto";
            cmB_Usuario.ValueMember = "Id";

            if (idMulta.HasValue)
            {
                Usuarios encontrado = Usuarios.ObtenerTodos().Find(u => u.Id == idMulta.Value);
                if (encontrado != null)
                    cmB_Usuario.SelectedItem = encontrado;
            }

            // El campo de reserva se maneja por ID escrito manualmente.
            if (idReserva.HasValue)
                textBox5.Text = idReserva.Value.ToString();
        }

        private bool EsDisponible(string texto)
        {
            return texto.Equals("Disponible", StringComparison.OrdinalIgnoreCase) ||
                   texto.Equals("Disposnible", StringComparison.OrdinalIgnoreCase);
        }

        private void EscribirSeparador(TextBox caja, string titulo)
        {
            caja.AppendText("==============================================" + Environment.NewLine);
            caja.AppendText(titulo + Environment.NewLine);
            caja.AppendText("----------------------------------------------" + Environment.NewLine);
        }

        private void EscribirDato(TextBox caja, string etiqueta, object valor)
        {
            caja.AppendText($"{etiqueta}: {valor}" + Environment.NewLine);
        }

        private string NombreLibro(Libros libro)
        {
            if (libro == null)
                return "Sin libro relacionado";
            return $"{libro.Titulo} (ISBN: {libro.ISBN})";
        }

        private string ObtenerLibrosRelacionadosConUsuario(Usuarios usuario)
        {
            if (usuario == null)
                return "Sin usuario";

            List<string> libros = new List<string>();

            foreach (Prestamos prestamo in _prestamosUI.Where(p => p.Usuario != null && p.Usuario.Id == usuario.Id))
            {
                foreach (Libros libro in prestamo.LibrosPrestados)
                {
                    if (libro != null)
                    {
                        string dato = NombreLibro(libro);
                        if (!libros.Contains(dato))
                            libros.Add(dato);
                    }
                }
            }

            return libros.Count == 0 ? "Sin préstamo de libro relacionado" : string.Join(", ", libros);
        }

        private void ConfigurarNivelAcceso()
        {
            // Se conserva txtNivelAcc en el Designer, pero visualmente se sustituye
            // por un ComboBox sin agregar botones ni modificar el formulario manualmente.
            if (txtNivelAcc == null || txtNivelAcc.Parent == null)
                return;

            Control contenedor = txtNivelAcc.Parent;
            _cbxNivelAccesoUI = new ComboBox();
            _cbxNivelAccesoUI.Name = "cbxNivelAccesoUI";
            _cbxNivelAccesoUI.Font = txtNivelAcc.Font;
            _cbxNivelAccesoUI.Location = txtNivelAcc.Location;
            _cbxNivelAccesoUI.Size = txtNivelAcc.Size;
            _cbxNivelAccesoUI.Anchor = txtNivelAcc.Anchor;
            _cbxNivelAccesoUI.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbxNivelAccesoUI.Items.AddRange(new object[]
            {
                "1 - Básico",
                "2 - Operador",
                "3 - Administrador",
                "4 - Supervisor",
                "5 - Máximo"
            });

            contenedor.Controls.Add(_cbxNivelAccesoUI);
            txtNivelAcc.Visible = false;
            _cbxNivelAccesoUI.SelectedIndex = 2;
        }

        private bool TryGetNivelAcceso(out int nivel)
        {
            nivel = 0;

            if (_cbxNivelAccesoUI != null && _cbxNivelAccesoUI.SelectedIndex >= 0)
            {
                nivel = _cbxNivelAccesoUI.SelectedIndex + 1;
                return true;
            }

            return TryParseInt(txtNivelAcc, "Nivel de acceso", out nivel);
        }

        private void MostrarNivelAcceso(int nivel)
        {
            if (_cbxNivelAccesoUI != null && nivel >= 1 && nivel <= 5)
                _cbxNivelAccesoUI.SelectedIndex = nivel - 1;
            else
                txtNivelAcc.Text = nivel.ToString();
        }

        // =====================================================
        // USUARIOS
        // =====================================================

        private void btnAgregarUsuario_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtIDUsuario, "ID del usuario", out int id) ||
                    !TryParseInt(txtEdadUsuario, "Edad", out int edad) ||
                    !TryParseInt(txtLibrosPrestaodosUsuario, "Libros prestados", out int librosPrestados) ||
                    !TryParseDecimal(txtMultaAcumulada, "Multa acumulada", out decimal multa))
                    return;

                if (Usuarios.ObtenerTodos().Any(u => u.Id == id))
                {
                    MostrarError("Ya existe un usuario con ese ID.");
                    return;
                }

                Usuarios nuevo = new Usuarios(
                    id,
                    txtNombreUsuario.Text,
                    edad,
                    txtCorreoUsuario.Text,
                    librosPrestados,
                    multa,
                    chbRol.Checked,
                    txtImagenUsuario.Text,
                    chbEstadoUsuario.Checked
                );

                nuevo.InsertarRegistro(nuevo);
                RefrescarListaUsuarios();
                CargarUsuariosEnCombos();
                CargarImagen(nuevo.RutaImagen, lblFotoUsuario);

                MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar usuario");
            }
        }

        private void btnModificarUsuario_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtIDUsuario, "ID del usuario", out int id) ||
                    !TryParseInt(txtEdadUsuario, "Edad", out int edad) ||
                    !TryParseInt(txtLibrosPrestaodosUsuario, "Libros prestados", out int librosPrestados) ||
                    !TryParseDecimal(txtMultaAcumulada, "Multa acumulada", out decimal multa))
                    return;

                Usuarios existente = Usuarios.ObtenerTodos().Find(u => u.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el usuario a modificar.");
                    return;
                }

                Usuarios actualizado = new Usuarios(
                    id,
                    txtNombreUsuario.Text,
                    edad,
                    txtCorreoUsuario.Text,
                    librosPrestados,
                    multa,
                    chbRol.Checked,
                    txtImagenUsuario.Text,
                    chbEstadoUsuario.Checked
                );

                existente.ActualizarRegistro(actualizado);
                RefrescarListaUsuarios();
                CargarUsuariosEnCombos();
                CargarImagen(actualizado.RutaImagen, lblFotoUsuario);

                MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar usuario");
            }
        }

        private void btnEliminarUsuario_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIDUsuario.Text.Trim(), out int id))
                {
                    MostrarError("El ID del usuario debe ser un número válido.");
                    return;
                }

                Usuarios auxiliar = new Usuarios();
                auxiliar.EliminarRegistro(id.ToString());

                RefrescarListaUsuarios();
                CargarUsuariosEnCombos();
                LimpiarCamposUsuario();

                MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar usuario");
            }
        }

        private void btnLimpiarUsuario_Click(object? sender, EventArgs e)
        {
            LimpiarCamposUsuario();
        }

        private void LimpiarCamposUsuario()
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
            chbEstadoUsuario.Checked = true;
            CargarImagen(string.Empty, lblFotoUsuario);
        }

        private void RefrescarListaUsuarios()
        {
            txtInfoUsuario.Clear();
            foreach (Usuarios u in Usuarios.ObtenerTodos())
            {
                EscribirSeparador(txtInfoUsuario, $"USUARIO #{u.Id}");
                EscribirDato(txtInfoUsuario, "Nombre", u.NombreCompleto);
                EscribirDato(txtInfoUsuario, "Tipo", u.EsProfesor ? "Profesor" : "Estudiante");
                EscribirDato(txtInfoUsuario, "Edad", u.Edad);
                EscribirDato(txtInfoUsuario, "Correo", u.Correo);
                EscribirDato(txtInfoUsuario, "Libros prestados", u.LibrosPrestados);
                EscribirDato(txtInfoUsuario, "Multa acumulada", $"${u.MultaAcumulada:F2}");
                EscribirDato(txtInfoUsuario, "Estado", u.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(txtInfoUsuario, "Libros relacionados", ObtenerLibrosRelacionadosConUsuario(u));
                EscribirDato(txtInfoUsuario, "Imagen", u.RutaImagen);
                txtInfoUsuario.AppendText(Environment.NewLine);
            }
        }

        private void button5_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenUsuario.Text = ruta;
                CargarImagen(ruta, lblFotoUsuario);
            }
        }

        // =====================================================
        // LIBROS
        // =====================================================

        private void btnCrearLi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseDecimal(txtCosto, "Costo", out decimal costo))
                    return;

                if (Libros.ObtenerTodos().Any(l => l.ISBN.Equals(txtIsbn.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    MostrarError("Ya existe un libro con ese ISBN.");
                    return;
                }

                ActualizarContadorLibros();

                Libros nuevo = new Libros(
                    _contadorIdLibros++,
                    txtIsbn.Text,
                    txtTitulo.Text,
                    costo,
                    1,
                    cbxNovedad.Text.Equals("Si", StringComparison.OrdinalIgnoreCase),
                    txtImagen.Text,
                    EsDisponible(cbxEstado.Text)
                );

                nuevo.InsertarRegistro(nuevo);
                MostrarTodosLosLibros();
                MessageBox.Show("Libro creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear libro");
            }
        }

        private void btnBuscarLi_Click(object? sender, EventArgs e)
        {
            if (!TryGetLibro(txtIsbn.Text, out Libros encontrado))
            {
                MessageBox.Show("No se encontró un libro con ese ISBN.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarLibroEnFormulario(encontrado);
            MessageBox.Show("Libro encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarLi_Click(object? sender, EventArgs e)
        {
            MostrarTodosLosLibros();
        }

        private void btnActualizarLi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseDecimal(txtCosto, "Costo", out decimal costo))
                    return;

                if (!TryGetLibro(txtIsbn.Text, out Libros existente))
                {
                    MostrarError("No se encontró el libro a actualizar.");
                    return;
                }

                Libros actualizado = new Libros(
                    existente.Id,
                    txtIsbn.Text,
                    txtTitulo.Text,
                    costo,
                    existente.CopiasDisponibles,
                    cbxNovedad.Text.Equals("Si", StringComparison.OrdinalIgnoreCase),
                    txtImagen.Text,
                    EsDisponible(cbxEstado.Text)
                );

                existente.ActualizarRegistro(actualizado);
                MostrarTodosLosLibros();
                MessageBox.Show("Libro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar libro");
            }
        }

        private void btnBorrarLi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryGetLibro(txtIsbn.Text, out Libros existente))
                {
                    MostrarError("No se encontró el libro a eliminar.");
                    return;
                }

                existente.EliminarRegistro(existente.Id.ToString());
                ActualizarContadorLibros();
                MostrarTodosLosLibros();
                MessageBox.Show("Libro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al borrar libro");
            }
        }

        private Libros BuscarLibroPorIsbn(string isbn)
        {
            return Libros.ObtenerTodos().Find(l => l.ISBN.Equals(isbn.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private void CargarLibroEnFormulario(Libros libro)
        {
            txtIsbn.Text = libro.ISBN;
            txtTitulo.Text = libro.Titulo;
            txtCosto.Text = libro.PrecioRentaDiaria.ToString("F2");
            cbxNovedad.Text = libro.EsNovedad ? "Si" : "No";
            cbxEstado.Text = libro.EsActivo ? "Disponible" : "Prestado";
            txtImagen.Text = libro.RutaImagen;
            CargarImagen(libro.RutaImagen, lblFotoLIbro);
        }

        private void MostrarTodosLosLibros()
        {
            txbInfo.Clear();
            foreach (Libros l in Libros.ObtenerTodos())
            {
                EscribirSeparador(txbInfo, $"LIBRO #{l.Id}");
                EscribirDato(txbInfo, "ISBN", l.ISBN);
                EscribirDato(txbInfo, "Título", l.Titulo);
                EscribirDato(txbInfo, "Precio por día", $"${l.PrecioRentaDiaria:F2}");
                EscribirDato(txbInfo, "Copias disponibles", l.CopiasDisponibles);
                EscribirDato(txbInfo, "Novedad", l.EsNovedad ? "Sí" : "No");
                EscribirDato(txbInfo, "Estado", l.EsActivo ? "Disponible" : "Prestado");
                EscribirDato(txbInfo, "Imagen", l.RutaImagen);
                txbInfo.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // AUTORES
        // =====================================================

        private void btn_Autor_AgregarPic_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_Autor_Codigo, "Código del autor", out int codigo) ||
                    !TryParseInt(txB_Autor_Edad, "Edad del autor", out int edad))
                    return;

                if (Autores.ObtenerTodos().Any(a => a.Id == codigo))
                {
                    MostrarError("Ya existe un autor con ese código.");
                    return;
                }

                Autores nuevo = new Autores(
                    codigo,
                    txB_Autor_Nombre.Text,
                    edad,
                    txB_Autor_Correo.Text,
                    txB_Autor_Nacionalidad.Text,
                    txB_Autor_RutaImagen.Text,
                    ckB_Estado.Checked
                );

                nuevo.InsertarRegistro(nuevo);
                MostrarAutores();
                CargarImagen(nuevo.RutaImagen, lblFotoAutor);
                MessageBox.Show("Autor agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar autor");
            }
        }

        private void btn_Autor_ModificarPic_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_Autor_Codigo, "Código del autor", out int codigo) ||
                    !TryParseInt(txB_Autor_Edad, "Edad del autor", out int edad))
                    return;

                Autores existente = Autores.ObtenerTodos().Find(a => a.Id == codigo);
                if (existente == null)
                {
                    MostrarError("No se encontró el autor a modificar.");
                    return;
                }

                Autores actualizado = new Autores(
                    codigo,
                    txB_Autor_Nombre.Text,
                    edad,
                    txB_Autor_Correo.Text,
                    txB_Autor_Nacionalidad.Text,
                    txB_Autor_RutaImagen.Text,
                    ckB_Estado.Checked
                );

                existente.ActualizarRegistro(actualizado);
                MostrarAutores();
                CargarImagen(actualizado.RutaImagen, lblFotoAutor);
                MessageBox.Show("Autor actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar autor");
            }
        }

        private void btn_Autor_EliminarPic_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txB_Autor_Codigo.Text.Trim(), out int codigo))
                {
                    MostrarError("El código del autor debe ser un número válido.");
                    return;
                }

                new Autores().EliminarRegistro(codigo.ToString());
                MostrarAutores();
                LimpiarAutor();
                MessageBox.Show("Autor eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar autor");
            }
        }

        private void btn_Autor_LimpiarPic_Click(object? sender, EventArgs e)
        {
            LimpiarAutor();
        }

        private void LimpiarAutor()
        {
            txB_Autor_Codigo.Clear();
            txB_Autor_Nombre.Clear();
            txB_Autor_Edad.Clear();
            txB_Autor_Correo.Clear();
            txB_Autor_Nacionalidad.Clear();
            txB_Autor_RutaImagen.Clear();
            ckB_Estado.Checked = true;
            CargarImagen(string.Empty, lblFotoAutor);
        }

        private void btn_Autor_Imagen_Click(object? sender, EventArgs e)
        {
            string ruta = SeleccionarImagen(oFD_Autor_RutaImagen);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txB_Autor_RutaImagen.Text = ruta;
                CargarImagen(ruta, lblFotoAutor);
            }
        }

        private void MostrarAutores()
        {
            textBox9.Clear();
            foreach (Autores autor in Autores.ObtenerTodos())
            {
                EscribirSeparador(textBox9, $"AUTOR #{autor.Id}");
                EscribirDato(textBox9, "Nombre", autor.NombreCompleto);
                EscribirDato(textBox9, "Edad", autor.Edad);
                EscribirDato(textBox9, "Correo", autor.Correo);
                EscribirDato(textBox9, "Nacionalidad", autor.Nacionalidad);
                EscribirDato(textBox9, "Estado", autor.Estado ? "Activo" : "Inactivo");
                EscribirDato(textBox9, "Imagen", autor.RutaImagen);
                textBox9.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // GÉNERO
        // =====================================================

        private void btn_GenAgregar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDGenero, "ID del género", out int id))
                    return;

                if (_generosUI.Any(g => g.Id == id))
                {
                    MostrarError("Ya existe un género con ese ID.");
                    return;
                }

                Genero nuevo = new Genero(
                    id,
                    txB_GenNombre.Text,
                    txB_GenDescripcion.Text,
                    textBox7.Text,
                    ckB_Genero_Activo.Checked
                );

                nuevo.InsertarRegistro(nuevo);
                _generosUI.Add(nuevo);
                MostrarGeneros();
                CargarImagen(nuevo.RutaImagen, lblFotoGenero);
                MessageBox.Show("Género agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar género");
            }
        }

        private void btn_GenModificar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDGenero, "ID del género", out int id))
                    return;

                Genero existente = _generosUI.Find(g => g.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el género a modificar.");
                    return;
                }

                Genero actualizado = new Genero(
                    id,
                    txB_GenNombre.Text,
                    txB_GenDescripcion.Text,
                    textBox7.Text,
                    ckB_Genero_Activo.Checked
                );

                existente.ActualizarRegistro(actualizado);
                int indiceGenero = _generosUI.FindIndex(g => g.Id == id);
                if (indiceGenero >= 0)
                    _generosUI[indiceGenero] = actualizado;
                MostrarGeneros();
                CargarImagen(actualizado.RutaImagen, lblFotoGenero);
                MessageBox.Show("Género actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar género");
            }
        }

        private void btn_GenEliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txB_IDGenero.Text.Trim(), out int id))
                {
                    MostrarError("El ID del género debe ser un número válido.");
                    return;
                }

                new Genero().EliminarRegistro(id.ToString());
                _generosUI.RemoveAll(g => g.Id == id);
                MostrarGeneros();
                btn_GenLimpiar_Click(sender, e);
                MessageBox.Show("Género eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar género");
            }
        }

        private void btn_GenLimpiar_Click(object? sender, EventArgs e)
        {
            txB_IDGenero.Clear();
            txB_GenNombre.Clear();
            txB_GenDescripcion.Clear();
            textBox7.Clear();
            ckB_Genero_Activo.Checked = true;
            CargarImagen(string.Empty, lblFotoGenero);
        }

        private void button6_Click(object? sender, EventArgs e)
        {
            string ruta = SeleccionarImagen(oFD_Genero_RutaImagen);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                textBox7.Text = ruta;
                CargarImagen(ruta, lblFotoGenero);
            }
        }

        private void MostrarGeneros()
        {
            textBox10.Clear();
            foreach (Genero genero in _generosUI)
            {
                EscribirSeparador(textBox10, $"GÉNERO #{genero.Id}");
                EscribirDato(textBox10, "Nombre", genero.Nombre);
                EscribirDato(textBox10, "Descripción", genero.Descripcion);
                EscribirDato(textBox10, "Estado", genero.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(textBox10, "Imagen", genero.RutaImagen);
                textBox10.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // PRÉSTAMOS
        // =====================================================

        private void btnCrearPres_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtID, "ID del préstamo", out int id))
                    return;

                if (!TryGetUsuario(txtUsuario.Text, out Usuarios usuario))
                {
                    MostrarError("No se encontró el usuario indicado.");
                    return;
                }

                if (!TryGetLibro(txtLibro.Text, out Libros libro))
                {
                    MostrarError("No se encontró el libro indicado por su ISBN.");
                    return;
                }

                if (_prestamosUI.Any(p => p.Id == id))
                {
                    MostrarError("Ya existe un préstamo con ese ID.");
                    return;
                }

                Prestamos nuevo = new Prestamos(id, usuario, libro, txtImagenLi.Text, EsDisponible(cbxEstadoLi.Text));
                nuevo.FechaEntrega = dtpFechaEntrega.Value;
                nuevo.Status = cbxStatus.Text.Equals("Si", StringComparison.OrdinalIgnoreCase);
                nuevo.InsertarRegistro(nuevo);
                _prestamosUI.Add(nuevo);
                CargarImagen(nuevo.RutaImagen, lblFotoPrestamo);

                MostrarPrestamos();
                MessageBox.Show("Préstamo creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear préstamo");
            }
        }

        private void btnBuscarPres_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text.Trim(), out int id))
            {
                MostrarError("El ID del préstamo debe ser un número válido.");
                return;
            }

            Prestamos encontrado = _prestamosUI.Find(p => p.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el préstamo.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarPrestamoEnFormulario(encontrado);
            MessageBox.Show("Préstamo encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarPres_Click(object? sender, EventArgs e)
        {
            MostrarPrestamos();
        }

        private void btnActualizarPres_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtID, "ID del préstamo", out int id))
                    return;

                Prestamos existente = _prestamosUI.Find(p => p.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el préstamo a actualizar.");
                    return;
                }

                if (!TryGetUsuario(txtUsuario.Text, out Usuarios usuario) || !TryGetLibro(txtLibro.Text, out Libros libro))
                {
                    MostrarError("Verifica el ID del usuario y el ISBN del libro.");
                    return;
                }

                Prestamos actualizado = new Prestamos(id, usuario, libro, txtImagenLi.Text, EsDisponible(cbxEstadoLi.Text));
                actualizado.FechaEntrega = dtpFechaEntrega.Value;
                actualizado.Status = cbxStatus.Text.Equals("Si", StringComparison.OrdinalIgnoreCase);

                existente.ActualizarRegistro(actualizado);
                int indicePrestamo = _prestamosUI.FindIndex(p => p.Id == id);
                if (indicePrestamo >= 0)
                    _prestamosUI[indicePrestamo] = actualizado;
                CargarImagen(actualizado.RutaImagen, lblFotoPrestamo);
                MostrarPrestamos();
                MessageBox.Show("Préstamo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar préstamo");
            }
        }

        private void btnBorrarrPres_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text.Trim(), out int id))
                {
                    MostrarError("El ID del préstamo debe ser un número válido.");
                    return;
                }

                new Prestamos().EliminarRegistro(id.ToString());
                _prestamosUI.RemoveAll(p => p.Id == id);
                MostrarPrestamos();
                MessageBox.Show("Préstamo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar préstamo");
            }
        }

        private void button14_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenLi.Text = ruta;
                CargarImagen(ruta, lblFotoPrestamo);
            }
        }

        private void CargarPrestamoEnFormulario(Prestamos prestamo)
        {
            txtID.Text = prestamo.Id.ToString();
            txtUsuario.Text = prestamo.Usuario.Id.ToString();
            txtLibro.Text = prestamo.LibrosPrestados[0]?.ISBN ?? string.Empty;
            dtpFechaEntrega.Value = prestamo.FechaEntrega;
            cbxStatus.Text = prestamo.Status ? "Si" : "No";
            cbxEstadoLi.Text = prestamo.EsActivo ? "Disponible" : "Prestado";
            txtImagenLi.Text = prestamo.RutaImagen;
            CargarImagen(prestamo.RutaImagen, lblFotoPrestamo);
        }

        private void MostrarPrestamos()
        {
            txbInfoPres.Clear();
            foreach (Prestamos prestamo in _prestamosUI)
            {
                Libros libro = prestamo.LibrosPrestados != null && prestamo.LibrosPrestados.Length > 0
                    ? prestamo.LibrosPrestados[0]
                    : null;

                EscribirSeparador(txbInfoPres, $"PRÉSTAMO #{prestamo.Id}");
                EscribirDato(txbInfoPres, "Usuario", $"#{prestamo.Usuario.Id} - {prestamo.Usuario.NombreCompleto}");
                EscribirDato(txbInfoPres, "Estado del usuario", prestamo.Usuario.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(txbInfoPres, "Libro", NombreLibro(libro));
                EscribirDato(txbInfoPres, "Fecha de entrega", prestamo.FechaEntrega.ToString("dd/MM/yyyy"));
                EscribirDato(txbInfoPres, "Préstamo activo", prestamo.Status ? "Sí" : "No");
                EscribirDato(txbInfoPres, "Estado del registro", prestamo.EsActivo ? "Disponible" : "Prestado");
                EscribirDato(txbInfoPres, "Imagen", prestamo.RutaImagen);
                txbInfoPres.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // RESERVAS
        // =====================================================

        private void btnCrearReserva_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(textBox6, "ID de la reserva", out int id))
                    return;

                if (!TryGetUsuario(textBox5.Text, out Usuarios usuario))
                {
                    MostrarError("No se encontró el usuario de la reserva.");
                    return;
                }

                if (!TryGetLibro(textBox12.Text, out Libros libro))
                {
                    MostrarError("No se encontró el libro reservado por su ISBN.");
                    return;
                }

                if (Reserva.ObtenerTodos().Any(r => r.Id == id))
                {
                    MostrarError("Ya existe una reserva con ese ID.");
                    return;
                }

                Reserva nueva = new Reserva(
                    id,
                    usuario,
                    libro,
                    dtpFechaReserva.Value,
                    dtpFechaEntregaReserva.Value,
                    txtImagenREserva.Text,
                    chbEstado.Checked
                );

                nueva.InsertarRegistro(nueva);
                CargarImagen(nueva.RutaImagen, lblFotoREserva);
                MostrarReservas();
                MessageBox.Show("Reserva agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear reserva");
            }
        }

        private void btnModificarReserva_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(textBox6, "ID de la reserva", out int id))
                    return;

                Reserva existente = Reserva.ObtenerTodos().Find(r => r.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la reserva a modificar.");
                    return;
                }

                if (!TryGetUsuario(textBox5.Text, out Usuarios usuario) || !TryGetLibro(textBox12.Text, out Libros libro))
                {
                    MostrarError("Verifica el ID del usuario y el ISBN del libro.");
                    return;
                }

                Reserva actualizada = new Reserva(
                    id,
                    usuario,
                    libro,
                    dtpFechaReserva.Value,
                    dtpFechaEntregaReserva.Value,
                    txtImagenREserva.Text,
                    chbEstado.Checked
                );

                existente.ActualizarRegistro(actualizada);
                CargarImagen(actualizada.RutaImagen, lblFotoREserva);
                MostrarReservas();
                MessageBox.Show("Reserva modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar reserva");
            }
        }

        private void btnEliminarReserva_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBox6.Text.Trim(), out int id))
                {
                    MostrarError("El ID de la reserva debe ser un número válido.");
                    return;
                }

                new Reserva().EliminarRegistro(id.ToString());
                MostrarReservas();
                btnLimpiarReserva_Click(sender, e);
                MessageBox.Show("Reserva eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar reserva");
            }
        }

        private void btnLimpiarReserva_Click(object? sender, EventArgs e)
        {
            textBox6.Clear();
            textBox5.Clear();
            textBox12.Clear();
            txtImagenREserva.Clear();
            chbEstado.Checked = true;
            dtpFechaReserva.Value = DateTime.Now;
            dtpFechaEntregaReserva.Value = DateTime.Now.AddDays(3);
            CargarImagen(string.Empty, lblFotoREserva);
        }

        private void button10_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenREserva.Text = ruta;
                CargarImagen(ruta, lblFotoREserva);
            }
        }

        private void MostrarReservas()
        {
            txtInfoReserva.Clear();
            foreach (Reserva reserva in Reserva.ObtenerTodos())
            {
                EscribirSeparador(txtInfoReserva, $"RESERVA #{reserva.Id}");
                EscribirDato(txtInfoReserva, "Usuario", $"#{reserva.Usuario.Id} - {reserva.Usuario.NombreCompleto}");
                EscribirDato(txtInfoReserva, "Estado del usuario", reserva.Usuario.EsActivo ? "Activo" : "Inactivo");
                EscribirDato(txtInfoReserva, "Libro", NombreLibro(reserva.LibroReservado));
                EscribirDato(txtInfoReserva, "Fecha de reserva", reserva.FechaReserva.ToString("dd/MM/yyyy"));
                EscribirDato(txtInfoReserva, "Fecha límite", reserva.FechaLimite.ToString("dd/MM/yyyy"));
                EscribirDato(txtInfoReserva, "Estado", reserva.Estado ? "Vigente" : "Cancelada/Vencida");
                EscribirDato(txtInfoReserva, "Imagen", reserva.RutaImagen);
                txtInfoReserva.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // MULTAS
        // =====================================================

        private void btn_Multa_Agregar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDMulta, "ID de la multa", out int id))
                    return;

                if (cmB_Usuario.SelectedItem is not Usuarios usuario)
                {
                    MostrarError("Selecciona un usuario para la multa.");
                    return;
                }

                if (_multasUI.Any(m => m.Id == id))
                {
                    MostrarError("Ya existe una multa con ese ID.");
                    return;
                }

                Multa nueva = new Multa(
                    id,
                    usuario,
                    txB_Multa_Motivo.Text,
                    nUD_Multa_MontoBase.Value,
                    dTP_Multa_FechaEmision.Value,
                    ckB_Multa_Pagada.Checked,
                    textBox8.Text,
                    ckB_MultaEstado.Checked
                );

                nueva.InsertarRegistro(nueva);
                _multasUI.Add(nueva);
                CargarImagen(nueva.RutaImagen, lblFotoMulta);
                MostrarMultas();
                MessageBox.Show("Multa agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al agregar multa");
            }
        }

        private void btn_Multa_Modificar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txB_IDMulta, "ID de la multa", out int id))
                    return;

                Multa existente = _multasUI.Find(m => m.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la multa a modificar.");
                    return;
                }

                if (cmB_Usuario.SelectedItem is not Usuarios usuario)
                {
                    MostrarError("Selecciona un usuario para la multa.");
                    return;
                }

                Multa actualizada = new Multa(
                    id,
                    usuario,
                    txB_Multa_Motivo.Text,
                    nUD_Multa_MontoBase.Value,
                    dTP_Multa_FechaEmision.Value,
                    ckB_Multa_Pagada.Checked,
                    textBox8.Text,
                    ckB_MultaEstado.Checked
                );

                existente.ActualizarRegistro(actualizada);
                int indiceMulta = _multasUI.IndexOf(existente);
                if (indiceMulta >= 0)
                    _multasUI[indiceMulta] = actualizada;
                CargarImagen(actualizada.RutaImagen, lblFotoMulta);
                MostrarMultas();
                MessageBox.Show("Multa actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al modificar multa");
            }
        }

        private void btn_Multa_Eliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txB_IDMulta.Text.Trim(), out int id))
                {
                    MostrarError("El ID de la multa debe ser un número válido.");
                    return;
                }

                new Multa().EliminarRegistro(id.ToString());
                Multa multaLocal = _multasUI.Find(m => m.Id == id);
                if (multaLocal != null)
                    _multasUI.Remove(multaLocal);
                MostrarMultas();
                btn_Multa_Limpiar_Click(sender, e);
                MessageBox.Show("Multa eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar multa");
            }
        }

        private void btn_Multa_Limpiar_Click(object? sender, EventArgs e)
        {
            txB_IDMulta.Clear();
            txB_Multa_Motivo.Clear();
            nUD_Multa_MontoBase.Value = 0;
            dTP_Multa_FechaEmision.Value = DateTime.Now;
            ckB_Multa_Pagada.Checked = false;
            ckB_MultaEstado.Checked = true;
            textBox8.Clear();
            CargarImagen(string.Empty, lblFotoMulta);
        }

        private void button7_Click(object? sender, EventArgs e)
        {
            string ruta = SeleccionarImagen(oFD_Multa_RutaImagen);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                textBox8.Text = ruta;
                CargarImagen(ruta, lblFotoMulta);
            }
        }

        private void MostrarMultas()
        {
            textBox11.Clear();
            foreach (Multa multa in _multasUI)
                textBox11.AppendText(multa + Environment.NewLine);
        }

        // =====================================================
        // EDITORIALES
        // =====================================================

        private void btnCrearEdi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtIDEditorial, "ID de la editorial", out int id))
                    return;

                if (EditorialesContieneId(id))
                {
                    MostrarError("Ya existe una editorial con ese ID.");
                    return;
                }

                Editorial nueva = CrearEditorialDesdeFormulario(id);
                nueva.InsertarRegistro(nueva);
                _editorialesUI.Add(nueva);
                _estadoEditorialUI[id] = cbxEstadoEdi.Text;
                MostrarEditoriales();
                CargarImagen(nueva.RutaImagen, lblFotoEditorial);
                MessageBox.Show("Editorial creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear editorial");
            }
        }

        private void btnBuscarEdi_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtIDEditorial.Text.Trim(), out int id))
            {
                MostrarError("El ID de la editorial debe ser un número válido.");
                return;
            }

            Editorial encontrado = _editorialesUI.Find(e => e.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró la editorial.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarEditorialEnFormulario(encontrado);
            MessageBox.Show("Editorial encontrada.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarEdi_Click(object? sender, EventArgs e)
        {
            MostrarEditoriales();
        }

        private void btnActualizarEdi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtIDEditorial, "ID de la editorial", out int id))
                    return;

                Editorial existente = _editorialesUI.Find(e => e.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la editorial a actualizar.");
                    return;
                }

                Editorial actualizada = CrearEditorialDesdeFormulario(id);
                existente.ActualizarRegistro(actualizada);
                _estadoEditorialUI[id] = cbxEstadoEdi.Text;
                int indiceEditorial = _editorialesUI.IndexOf(existente);
                if (indiceEditorial >= 0)
                    _editorialesUI[indiceEditorial] = actualizada;
                MostrarEditoriales();
                CargarImagen(actualizada.RutaImagen, lblFotoEditorial);
                MessageBox.Show("Editorial actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar editorial");
            }
        }

        private void btnBorarEdi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIDEditorial.Text.Trim(), out int id))
                {
                    MostrarError("El ID de la editorial debe ser un número válido.");
                    return;
                }

                new Editorial().EliminarRegistro(id.ToString());
                Editorial editorialLocal = _editorialesUI.Find(e => e.Id == id);
                if (editorialLocal != null)
                    _editorialesUI.Remove(editorialLocal);
                _estadoEditorialUI.Remove(id);
                MostrarEditoriales();
                MessageBox.Show("Editorial eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar editorial");
            }
        }

        private Editorial CrearEditorialDesdeFormulario(int id)
        {
            return new Editorial(
                id,
                txtNombreEdi.Text,
                txtPais.Text,
                dtpAnioFundacion.Value.Year,
                txtCorreoEdi.Text,
                txtImagenEdi.Text,
                EstadoEsActivoEditorial(cbxEstadoEdi.Text)
            );
        }

        private bool EditorialesContieneId(int id)
        {
            return _editorialesUI.Any(e => e.Id == id);
        }

        private void CargarEditorialEnFormulario(Editorial editorial)
        {
            txtIDEditorial.Text = editorial.Id.ToString();
            txtNombreEdi.Text = editorial.Nombre;
            txtPais.Text = editorial.Pais;
            dtpAnioFundacion.Value = new DateTime(editorial.AnioFundacion, 1, 1);
            txtCorreoEdi.Text = editorial.CorreoContacto;
            txtImagenEdi.Text = editorial.RutaImagen;
            cbxEstadoEdi.Text = ObtenerEstadoEditorial(editorial.Id, editorial.EsActivo);
            CargarImagen(editorial.RutaImagen, lblFotoEditorial);
        }

        private void button15_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenEdi.Text = ruta;
                CargarImagen(ruta, lblFotoEditorial);
            }
        }

        private void MostrarEditoriales()
        {
            txbInfoEdi.Clear();
            foreach (Editorial editorial in _editorialesUI)
            {
                EscribirSeparador(txbInfoEdi, $"EDITORIAL #{editorial.Id}");
                EscribirDato(txbInfoEdi, "Nombre", editorial.Nombre);
                EscribirDato(txbInfoEdi, "País", editorial.Pais);
                EscribirDato(txbInfoEdi, "Año de fundación", editorial.AnioFundacion);
                EscribirDato(txbInfoEdi, "Correo", editorial.CorreoContacto);
                EscribirDato(txbInfoEdi, "Antigüedad", $"{editorial.CalcularAntiguedad()} años");
                EscribirDato(txbInfoEdi, "Estado", ObtenerEstadoEditorial(editorial.Id, editorial.EsActivo));
                EscribirDato(txbInfoEdi, "Imagen", editorial.RutaImagen);
                txbInfoEdi.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // ADMINISTRADORES
        // =====================================================

        private void btnCrearAdm_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtCodigoAdm, "Código del administrador", out int id) ||
                    !TryParseInt(txtEdadAdmin, "Edad", out int edad) ||
                    !TryGetNivelAcceso(out int nivel))
                    return;

                if (Administrador.ObtenerTodos().Any(a => a.Id == id))
                {
                    MostrarError("Ya existe un administrador con ese código.");
                    return;
                }

                Administrador nuevo = new Administrador(
                    id,
                    txtNombreAdmin.Text,
                    edad,
                    txtCorreoAdmin.Text,
                    nivel,
                    txtDepartamento.Text,
                    dtpFechaIngreso.Value,
                    txtImagenAdm.Text,
                    EstadoEsActivoAdministrador(cbxEstadoAdm.Text)
                );

                nuevo.InsertarRegistro(nuevo);
                _estadoAdministradorUI[id] = cbxEstadoAdm.Text;
                MostrarAdministradores();
                CargarImagen(nuevo.RutaImagen, lblFotoAdministrador);
                MessageBox.Show("Administrador creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear administrador");
            }
        }

        private void btnBuscarAdm_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtCodigoAdm.Text.Trim(), out int id))
            {
                MostrarError("El código del administrador debe ser un número válido.");
                return;
            }

            Administrador encontrado = Administrador.ObtenerTodos().Find(a => a.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró el administrador.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarAdministradorEnFormulario(encontrado);
            MessageBox.Show("Administrador encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarAdm_Click(object? sender, EventArgs e)
        {
            MostrarAdministradores();
        }

        private void btnActualizarAdm_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(txtCodigoAdm, "Código del administrador", out int id) ||
                    !TryParseInt(txtEdadAdmin, "Edad", out int edad) ||
                    !TryGetNivelAcceso(out int nivel))
                    return;

                Administrador existente = Administrador.ObtenerTodos().Find(a => a.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró el administrador a actualizar.");
                    return;
                }

                Administrador actualizado = new Administrador(
                    id,
                    txtNombreAdmin.Text,
                    edad,
                    txtCorreoAdmin.Text,
                    nivel,
                    txtDepartamento.Text,
                    dtpFechaIngreso.Value,
                    txtImagenAdm.Text,
                    EstadoEsActivoAdministrador(cbxEstadoAdm.Text)
                );

                existente.ActualizarRegistro(actualizado);
                _estadoAdministradorUI[id] = cbxEstadoAdm.Text;
                MostrarAdministradores();
                CargarImagen(actualizado.RutaImagen, lblFotoAdministrador);
                MessageBox.Show("Administrador actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar administrador");
            }
        }

        private void btnBorarAdm_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtCodigoAdm.Text.Trim(), out int id))
                {
                    MostrarError("El código del administrador debe ser un número válido.");
                    return;
                }

                new Administrador().EliminarRegistro(id.ToString());
                _estadoAdministradorUI.Remove(id);
                MostrarAdministradores();
                MessageBox.Show("Administrador eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar administrador");
            }
        }

        private void button16_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenAdm.Text = ruta;
                CargarImagen(ruta, lblFotoAdministrador);
            }
        }

        private void CargarAdministradorEnFormulario(Administrador admin)
        {
            txtCodigoAdm.Text = admin.Id.ToString();
            txtNombreAdmin.Text = admin.NombreCompleto;
            txtEdadAdmin.Text = admin.Edad.ToString();
            txtCorreoAdmin.Text = admin.Correo;
            MostrarNivelAcceso(admin.NivelAcceso);
            txtDepartamento.Text = admin.Departamento;
            dtpFechaIngreso.Value = admin.FechaIngreso;
            txtImagenAdm.Text = admin.RutaImagen;
            cbxEstadoAdm.Text = ObtenerEstadoAdministrador(admin.Id, admin.Estado);
            CargarImagen(admin.RutaImagen, lblFotoAdministrador);
        }

        private void MostrarAdministradores()
        {
            txbInfoAdm.Clear();
            foreach (Administrador admin in Administrador.ObtenerTodos())
            {
                EscribirSeparador(txbInfoAdm, $"ADMINISTRADOR #{admin.Id}");
                EscribirDato(txbInfoAdm, "Nombre", admin.NombreCompleto);
                EscribirDato(txbInfoAdm, "Edad", admin.Edad);
                EscribirDato(txbInfoAdm, "Correo", admin.Correo);
                EscribirDato(txbInfoAdm, "Nivel de acceso", $"{admin.NivelAcceso} de 5");
                EscribirDato(txbInfoAdm, "Departamento", admin.Departamento);
                EscribirDato(txbInfoAdm, "Fecha de ingreso", admin.FechaIngreso.ToString("dd/MM/yyyy"));
                EscribirDato(txbInfoAdm, "Estado", ObtenerEstadoAdministrador(admin.Id, admin.Estado));
                EscribirDato(txbInfoAdm, "Imagen", admin.RutaImagen);
                txbInfoAdm.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // PERSONAS
        // =====================================================

        private void btnCrearPer_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(textBox4, "Código de la persona", out int id) ||
                    !TryParseInt(txtEdad, "Edad", out int edad))
                    return;

                if (Persona.ObtenerTodos().Any(p => p.Id == id))
                {
                    MostrarError("Ya existe una persona con ese código.");
                    return;
                }

                Persona nueva = new Persona(id, txtNombreCPer.Text, edad, txtCorreoPer.Text)
                {
                    EsActivo = EsDisponible(cbxEstadoPer.Text)
                };

                nueva.InsertarRegistro(nueva);
                MostrarPersonas();
                MessageBox.Show("Persona creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al crear persona");
            }
        }

        private void btnBuscarPer_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text.Trim(), out int id))
            {
                MostrarError("El código de la persona debe ser un número válido.");
                return;
            }

            Persona encontrado = Persona.ObtenerTodos().Find(p => p.Id == id);
            if (encontrado == null)
            {
                MessageBox.Show("No se encontró la persona.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarPersonaEnFormulario(encontrado);
            MessageBox.Show("Persona encontrada.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrarPer_Click(object? sender, EventArgs e)
        {
            MostrarPersonas();
        }

        private void btnActualizarPer_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryParseInt(textBox4, "Código de la persona", out int id) ||
                    !TryParseInt(txtEdad, "Edad", out int edad))
                    return;

                Persona existente = Persona.ObtenerTodos().Find(p => p.Id == id);
                if (existente == null)
                {
                    MostrarError("No se encontró la persona a actualizar.");
                    return;
                }

                Persona actualizada = new Persona(id, txtNombreCPer.Text, edad, txtCorreoPer.Text)
                {
                    EsActivo = EsDisponible(cbxEstadoPer.Text)
                };

                existente.ActualizarRegistro(actualizada);
                MostrarPersonas();
                MessageBox.Show("Persona actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al actualizar persona");
            }
        }

        private void btnBorrarPer_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBox4.Text.Trim(), out int id))
                {
                    MostrarError("El código de la persona debe ser un número válido.");
                    return;
                }

                new Persona().EliminarRegistro(id.ToString());
                MostrarPersonas();
                MessageBox.Show("Persona eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message, "Error al eliminar persona");
            }
        }

        private void button9_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            string ruta = SeleccionarImagen(dialogo);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                txtImagenPer.Text = ruta;
                CargarImagen(ruta, label12);
            }
        }

        private void CargarPersonaEnFormulario(Persona persona)
        {
            textBox4.Text = persona.Id.ToString();
            txtNombreCPer.Text = persona.NombreCompleto;
            txtEdad.Text = persona.Edad.ToString();
            txtCorreoPer.Text = persona.Correo;
            cbxEstadoPer.Text = persona.EsActivo ? "Disponible" : "Prestado";
            txtImagenPer.Text = string.Empty;
        }

        private void MostrarPersonas()
        {
            txtInfoPersona.Clear();

            // Lista polimórfica: una misma colección de Persona contiene
            // objetos concretos de Usuarios y Autores.
            List<Persona> personasPolimorficas = new List<Persona>();

            foreach (Persona persona in Persona.ObtenerTodos())
                personasPolimorficas.Add(persona);

            foreach (Usuarios usuario in Usuarios.ObtenerTodos())
            {
                if (!personasPolimorficas.Any(p => p.Id == usuario.Id))
                    personasPolimorficas.Add(usuario);
            }

            foreach (Autores autor in Autores.ObtenerTodos())
            {
                if (!personasPolimorficas.Any(p => p.Id == autor.Id))
                    personasPolimorficas.Add(autor);
            }

            foreach (Persona persona in personasPolimorficas)
            {
                EscribirSeparador(txtInfoPersona, $"PERSONA #{persona.Id}");
                txtInfoPersona.AppendText(ObtenerInformacionPersonaPolimorfica(persona));
                txtInfoPersona.AppendText(Environment.NewLine);
            }
        }

        // =====================================================
        // EVENTO QUE YA EXISTÍA EN EL DESIGNER
        // =====================================================

        private void label51_Click(object? sender, EventArgs e)
        {
        }
    }
}
