using RegistroCompleto.BLL;
using RegistroCompleto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroDeUsuario.UI.Registros
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
    {
        private Usuarios usuario = new Usuarios();
        public rUsuarios()
        {
            InitializeComponent();
            this.DataContext = usuario;
        }
        private void Clean()
        {
            this.usuario = new Usuarios();
            this.DataContext = usuario;
        }
        private bool Validar()
        {
            bool esValido = true;

            if (AliasTexBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("No puede dejar campos en blanco. Ingrese el Alias.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (FechaDatePicker.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("No puede dejar campos en blanco. Inserte la fecha", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (RolComboBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("No puede dejar campos en blanco. Seleccione una opcion del ComboBox.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (NombresTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("No puede dejar campos en blanco. Ingrese el nombre completo", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (EmailTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("No puede dejar campos en blanco. Ingrese el Email", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (ClaveTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("No puede dejar campos en blanco. Ingrese la Clave.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var usuarios = UsuariosBLL.Buscar(Utilidades.ToInt(IdTextBox.Text));
            if (usuarios != null)
                this.usuario = usuarios;
            else
            {
                this.usuario = new Usuarios();
                MessageBox.Show("No se ha Encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.DataContext = this.usuario;
        }

        private void BotonNuevo_Click(object sender, RoutedEventArgs e)
        {
            Clean();
        }

        private void BotonGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = UsuariosBLL.Guardar(usuario);

            if (paso)
            {
                Clean();
                MessageBox.Show("Transaccion Completada!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (UsuariosBLL.Eliminar(Utilidades.ToInt(IdTextBox.Text)))
            {
                Clean();
                MessageBox.Show("Registro eliminado!", "Exito",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar", "Fallo",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
