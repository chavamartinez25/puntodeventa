using System;
using System.Windows.Forms;
using SistemaVentas.Negocio;

namespace SistemaVentas.Presentacion
{
    public partial class FrmUsuario : Form
    {
        // Variables
        string EmailAnt;

        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                // Mandamos al DataGridView el listado de categorias resultante de la capa de negocio 
                DgvListado.DataSource = NUsuario.Listar();
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
                DgvListado.DataSource = NUsuario.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[2].Visible = false; // ID
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[4].Width = 170;
            DgvListado.Columns[5].Width = 100;
            DgvListado.Columns[5].HeaderText = "Documento";
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Número Doc";
            DgvListado.Columns[7].Width = 120;
            DgvListado.Columns[7].HeaderText = "Dirección";
            DgvListado.Columns[8].Width = 120;
            DgvListado.Columns[8].HeaderText = "Teléfono";
            DgvListado.Columns[9].Width = 120;
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtNumeroDocumento.Clear();
            TxtDireccion.Clear();
            TxtEmail.Clear();
            TxtClave.Clear();
            TxtTelefono.Clear();
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

        private void CargarRol()
        {
            try
            {
                CboRol.DataSource = NRol.Listar();
                CboRol.ValueMember = "idrol";
                CboRol.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarRol();
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
                // Forzosamente debe de llenar los datos de Nombre, Rol, Email y Clave para poder registrar un usuario nuevo
                if (TxtNombre.Text == string.Empty || CboRol.Text == string.Empty || TxtEmail.Text == string.Empty || TxtClave.Text == string.Empty)
                {
                    this.MenssajeError("Falta ingresar algunos datos.");
                    // Enviaremos el error marcado a la caja corespondiente.
                    ErrorIcono.SetError(TxtNombre, "Ingrese un Nombre");
                    ErrorIcono.SetError(CboRol, "Seleccione un Rol");
                    ErrorIcono.SetError(TxtEmail, "Ingrese un Email");
                    ErrorIcono.SetError(TxtClave, "Ingrese una Clave");
                }
                else
                {
                    Rpta = NUsuario.Insertar(Convert.ToInt32(CboRol.SelectedValue),TxtNombre.Text.Trim(),CboTipoDocumento.Text,TxtNumeroDocumento.Text.Trim(),TxtDireccion.Text.Trim(),TxtTelefono.Text.Trim(),TxtEmail.Text.Trim(),TxtClave.Text);
                    if (Rpta.Equals("Ok")) // Respuesta ok de la inserción
                    {
                        this.MensajeOk("Se insertó de forma correcta el registro.");
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

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;

                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                CboRol.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idrol"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                CboTipoDocumento.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["tipo_documento"].Value);
                TxtNumeroDocumento.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Num_Documento"].Value);
                TxtDireccion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Direccion"].Value);
                TxtTelefono.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Telefono"].Value);
                this.EmailAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value); // Guardamos el valor del email antes de ser modificado
                TxtEmail.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value);
                TabGeneral.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda Nombre." + "| Error: " + ex.Message);
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                // Forzosamente debe de llenar los datos de Nombre, Rol, Email y Clave para poder registrar un usuario nuevo
                if ( TxtId.Text == string.Empty || TxtNombre.Text == string.Empty || CboRol.Text == string.Empty || TxtEmail.Text == string.Empty)
                {
                    this.MenssajeError("Falta ingresar algunos datos.");
                    // Enviaremos el error marcado a la caja corespondiente.
                    ErrorIcono.SetError(TxtNombre, "Ingrese un Nombre");
                    ErrorIcono.SetError(CboRol, "Seleccione un Rol");
                    ErrorIcono.SetError(TxtEmail, "Ingrese un Email");
                }
                else
                {
                    Rpta = NUsuario.Actualizar(Convert.ToInt32(TxtId.Text),Convert.ToInt32(CboRol.SelectedValue),TxtNombre.Text.Trim(),CboTipoDocumento.Text,TxtNumeroDocumento.Text.Trim(),TxtDireccion.Text.Trim(),TxtTelefono.Text.Trim(),EmailAnt,TxtEmail.Text.Trim(),TxtClave.Text);
                    if (Rpta.Equals("Ok")) // Respuesta ok de la actualizaión
                    {
                        this.MensajeOk("Se actualizó de forma correcta el registro.");
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
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
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
                            Codigo = Convert.ToInt32(row.Cells[1].Value); // Guardamos el ID del usuario
                            Rpta = NUsuario.Activar(Codigo);

                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se activó el usuario: " + Convert.ToString(row.Cells[4].Value));
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
                            Codigo = Convert.ToInt32(row.Cells[1].Value); // Guardamos el ID del usuario
                            Rpta = NUsuario.Desactivar(Codigo);
                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se desactivó el usuario: " + Convert.ToString(row.Cells[4].Value));
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

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Se desea eliminar el(los) registro(s) de forma permanente?",
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
                            Rpta = NUsuario.Eliminar(Codigo);

                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se eliminó el usuario: " + Convert.ToString(row.Cells[4].Value));
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
