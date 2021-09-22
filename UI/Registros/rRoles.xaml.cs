using RegistroCompleto.BLL;
using RegistroCompleto.Entidades;
using RegistroUsuariosWPF.BLL;
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
    /// Interaction logic for rRoles.xaml
    /// </summary>
    public partial class rRoles : Window
    {
        private Roles Db = new Roles();
        public rRoles()
        {
            InitializeComponent();
            this.DataContext = Db;
        }
        private void Clean()
        {
            this.Db = new Roles();
            this.DataContext = Db;
        }
        private bool Validar()
        {
            bool posibilidadEscritura = true;

            if (FechaDatePicker.Text.Length == 0)
            {
                posibilidadEscritura = false;
                MessageBox.Show("Ha ocurrido un error, Debe Seleccionar la fecha", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else if (DescripcionTextBox.Text.Length == 0)
            {
                posibilidadEscritura = false;
                MessageBox.Show("Algo ha salido mal, Ingresa la descripcion", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return posibilidadEscritura;
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var roless = RolesBLL.Buscar(Utilidades.ToInt(RolIdTextBox.Text));
            if (roless != null)
                this.Db = roless;
            else
            {
                this.Db = new Roles();
                MessageBox.Show("No se ha Encontrado", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.DataContext = this.Db;
        }

        private void BotonNuevo_Click(object sender, RoutedEventArgs e)
        {
            Clean();
        }

        private void BotonGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (!Validar())
                return;

            if (RolesBLL.ExisteDescripcion(DescripcionTextBox.Text))
            {
                MessageBox.Show("Este rol no esta disponible, Elige otro", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var paso = RolesBLL.Guardar(this.Db);

            if (paso)
            {
                Clean();
                MessageBox.Show("Transacion Exitosa!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Proceso Erroneo", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Eliminar(Utilidades.ToInt(RolIdTextBox.Text)))
            {
                Clean();
                MessageBox.Show("Registro Eliminado!", "Exito",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se puede eliminar", "Fallo",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
