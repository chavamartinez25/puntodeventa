using System;
using System.Windows.Forms;
using SistemaVentas.Negocio;

namespace SistemaVentas.Presentacion
{
    public partial class FrmCategoria : Form
    {

        private string NombreAnt; // Guarda el nombre de la categoria para actualizar solo la descripción

        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                // Mandamos al DataGridView el listado de categorias resultante de la capa de negocio 
                DgvListado.DataSource = NCategoria.Listar();
                this.Formato();
                this.Limpiar(); // Para ocultar el boton de actualizar momentaneamente
                lblTotal.Text = "Total de registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                // El StackTrace sirve para mostrar los mensajes de la pila de excepciones
                // Ya que tambien capturamos excepciones en la capa de negocios.
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Buscar()
        {
            try
            {
                // Mandamos al DataGridView el listado de categorias resultante de la capa de negocio 
                DgvListado.DataSource = NCategoria.Buscar(TxtBuscar.Text);
                this.Formato();
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
            // Formato de Grid View
            DgvListado.Columns[0].Visible = false; // Select
            DgvListado.Columns[1].Visible = false; // ID
            DgvListado.Columns[2].Width = 150; // Nombre
            DgvListado.Columns[3].Width = 400; // Descripcion
            DgvListado.Columns[3].HeaderText = "Descripción"; // Actualizamos el texto del header del listado para descripción
            DgvListado.Columns[4].Width = 100; // Estado
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtDescripcion.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
            ErrorIcono.Clear();

            DgvListado.Columns[0].Visible = false;
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;
        }

        // Esta funcion nos permite mostrar un mensaje de error 
        private void MenssajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            // Cuando carge esta vista, hará referencia a la funcion de listar que mostrará el listado de
            // Categorias.
            this.Listar();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtNombre.Text == string.Empty)
                {
                    this.MenssajeError("Falta ingresar algunos datos.");
                    // Enviaremos el error marcado a la caja corespondiente.
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NCategoria.Insertar(TxtNombre.Text.Trim(),TxtDescripcion.Text.Trim());
                    if (Rpta.Equals("Ok")) // Respuesta ok de la inserción
                    {
                        this.MensajeOk("Se insertó de forma correcta el registro.");
                        this.Limpiar();
                        this.Listar(); // Listamos de nuevo todos los registros incluyendo el nuevo
                    }
                    else
                    {
                        this.MenssajeError(Rpta); // Mensaje de error de la respuesta de insertar  
                        this.Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Listar();
            TabGeneral.SelectedIndex = 0; // Tras cancelar regresamos a la pestaña de listar
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value); // Vamos a asignar el valor del tecto de la celda con el ID
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descipcion"].Value);
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione desde la celda nombre.");
            }
            
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtNombre.Text == string.Empty || TxtId.Text == string.Empty)
                {
                    this.MenssajeError("Falta ingresar algunos datos.");
                    // Enviaremos el error marcado a la caja corespondiente.
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NCategoria.Actualizar(Convert.ToInt32(TxtId.Text), this.NombreAnt, TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    if (Rpta.Equals("Ok")) // Respuesta ok de la inserción
                    {
                        this.MensajeOk("Se actualizó de forma correcta el registro.");
                        this.Limpiar();
                        this.Listar(); // Listamos de nuevo todos los registros incluyendo el nuevo
                    }
                    else
                    {
                        this.MenssajeError(Rpta); // Mensaje de error de la respuesta de insertar  
                        this.Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si la columna selecionada es igual al index.
            // Esta instruccion nos permitirá marcar y desmarcar el check box de la lista.
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Se desea eliminar el(los) registro(s) de forma permanente?",
                                         "Sistema de ventas",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK) // Si el susuario selecciona Ok en el aviso del sistema
                {
                    int Codigo;
                    string Rpta = "";

                    // Hay que preparar para eliminar más de un registro, esto hacerlo con un ForEach
                    foreach ( DataGridViewRow row in DgvListado.Rows ) // Guardamos todas las filas en la variable row del DgvListado
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value)) // Si es true, significa que deseo eliminar la fila
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value); // Guardamos el ID de la categoria
                            Rpta = NCategoria.Eliminar(Codigo);

                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se eliminó el registro " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MenssajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Se desea Activar el(los) registro(s)?",
                                         "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK) // Si el susuario selecciona Ok en el aviso del sistema
                {
                    int Codigo;
                    string Rpta = "";

                    // Hay que preparar para eliminar más de un registro, esto hacerlo con un ForEach
                    foreach (DataGridViewRow row in DgvListado.Rows) // Guardamos todas las filas en la variable row del DgvListado
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value)) // Si es true, significa que deseo eliminar la fila
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value); // Guardamos el ID de la categoria
                            Rpta = NCategoria.Activar(Codigo);

                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se activó el registro " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MenssajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Se desea Desactivar el(los) registro(s)?",
                                         "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK) // Si el susuario selecciona Ok en el aviso del sistema
                {
                    int Codigo;
                    string Rpta = "";

                    // Hay que preparar para eliminar más de un registro, esto hacerlo con un ForEach
                    foreach (DataGridViewRow row in DgvListado.Rows) // Guardamos todas las filas en la variable row del DgvListado
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value)) // Si es true, significa que deseo eliminar la fila
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value); // Guardamos el ID de la categoria
                            Rpta = NCategoria.Desactivar(Codigo);
                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se desactivó el registro " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MenssajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
