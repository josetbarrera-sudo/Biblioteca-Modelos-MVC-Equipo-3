using System;
using System.Windows.Forms; // <-- 1. IMPRESCINDIBLE PARA RECONOCER LA VENTANA
using Biblioteca.Models;

namespace Biblioteca // <-- 2. DEBE SER 'Biblioteca', NO 'Biblioteca.Models'
{
    public partial class Form1 : Form // <-- 3. DEBE HEREDAR DE 'Form'
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Aquí continúa el resto de los eventos del formulario...
    }
}