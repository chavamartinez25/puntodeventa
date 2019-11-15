using System;
using System.Data;
using System.Windows.Forms;
using SistemaVentas.Negocio;

namespace SistemaVentas.Presentacion
{
    public partial class FrmLogIn : Form
    {
        public FrmLogIn()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit(); // La aplicación finaliza
        }

        private void NtnAcceder_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable Tabla = new DataTable();
                Tabla = NUsuario.LogIn(TxtEmail.Text,TxtClave.Text);

                if (Tabla.Rows.Count <= 0)
                {
                    MessageBox.Show("El email o la clave es incorrecta!", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Revisamos si el estado del usuario es activo o inactivo
                    if (Convert.ToBoolean(Tabla.Rows[0][4]) == false)
                    {
                        MessageBox.Show("El usuario está desactivado, favor de contactar a sistemas!", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        FrmPrincipal frm = new FrmPrincipal();
                        // Enviamos los valores a las variables de la pantalla principal
                        frm.IdUsuario = Convert.ToInt32(Tabla.Rows[0][0]);
                        frm.IdRol = Convert.ToInt32(Tabla.Rows[0][1]);
                        frm.Rol = Convert.ToString(Tabla.Rows[0][2]);
                        frm.Nombre = Convert.ToString(Tabla.Rows[0][3]);
                        frm.Estado = Convert.ToBoolean(Tabla.Rows[0][4]);

                        frm.Show(); // Mostramos pantalla principal
                        this.Hide(); // Ocultamos el formulario de Login
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
