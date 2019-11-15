using System;
using System.Windows.Forms;
using SistemaVentas.Negocio;

namespace SistemaVentas.Presentacion
{
    public partial class FrmRol : Form
    {
        public FrmRol()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                // Mandamos al DataGridView el listado de categorias resultante de la capa de negocio 
                DgvListado.DataSource = NRol.Listar();
                this.Formato(); // Formato de columndas de la tabla
                lblTotal.Text = "Total de registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                // El StackTrace sirve para mostrar los mensajes de la pila de excepciones
                // Ya que tambien capturamos excepciones en la capa de negocios.
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Formato()
        {
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[0].HeaderText = "ID";
            DgvListado.Columns[1].Width = 200;
            DgvListado.Columns[1].HeaderText = "Nombre";
        }

        private void FrmRol_Load(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}
