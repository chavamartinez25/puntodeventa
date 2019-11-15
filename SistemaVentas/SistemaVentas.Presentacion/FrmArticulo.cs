using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SistemaVentas.Negocio;

namespace SistemaVentas.Presentacion
{
    public partial class FrmArticulo : Form
    {
        // Variables
        private string NombreAnt;
        private string RutaImagen;
        private string RutaDestino;
        private string Directorio = "C:\\SistemaVentas\\";

        public FrmArticulo()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                // Mandamos al DataGridView el listado de categorias resultante de la capa de negocio 
                DgvListado.DataSource = NArticulo.Listar();
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
                DgvListado.DataSource = NArticulo.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[2].Visible = false; // Id Categoría
            DgvListado.Columns[0].Width = 100; // Select ancho
            DgvListado.Columns[1].Width = 100; // Id Categoria ancho
            DgvListado.Columns[3].Width = 100; // Categoría ancho
            DgvListado.Columns[3].HeaderText = "Categoría"; // Categoria header
            DgvListado.Columns[4].Width = 100; // Codigo ancho
            DgvListado.Columns[4].HeaderText = "Código"; // Código Header 
            DgvListado.Columns[5].Width = 150; // Nombre ancho
            DgvListado.Columns[6].Width = 100; // Precio de venta ancho
            DgvListado.Columns[6].HeaderText = "Precio Venta"; // Precio de venta Header
            DgvListado.Columns[7].Width = 100; // Stock ancho
            DgvListado.Columns[8].Width = 200; // Descripción Ancho
            DgvListado.Columns[8].HeaderText = "Descripción"; // Descripción Header
            DgvListado.Columns[9].Width = 100; // Imagen ancho
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtCodigo.Clear();
            PanelCodigo.BackgroundImage = null;
            BtnGuardarCodigo.Enabled = true;
            TxtPrecioVenta.Clear();
            TxtStock.Clear();
            TxtImagen.Clear();
            PicImagen.Image = null;     
            TxtDescripcion.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
            ErrorIcono.Clear();
            this.RutaDestino = "";
            this.RutaImagen = "";

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

        private void CargarCategoria()
        {
            try
            {
                // Obtiene todas las categorias de la capa negocio en la clase NCategorias
                CboCategoria.DataSource = NCategoria.Seleccionar();
                // Los registros llegan en diccionario y accedemos a ellos a través del nombre de las cabeceras 
                CboCategoria.ValueMember = "idcategoria";
                CboCategoria.DisplayMember = "Nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            // Cuando carge esta vista, hará referencia a la funcion de listar que mostrará el listado de
            // Categorias.
            this.Listar();
            this.CargarCategoria();
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
                    Rpta = NCategoria.Insertar(TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
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

        private void BtnBuscar_Click_1(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnCargarImagen_Click(object sender, EventArgs e)
        {
            // Para especificar que el buscador abrirá archivos solo de imagenes
            OpenFileDialog File = new OpenFileDialog();
            File.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (File.ShowDialog() == DialogResult.OK) // Si ha sido seleccionada una imagen
            {
                PicImagen.Image = Image.FromFile(File.FileName); // Obtenemos el archivo en si
                // Obtenemos el nombre de la imagen Solo considera el texto despues del \
                TxtImagen.Text = File.FileName.Substring(File.FileName.LastIndexOf("\\") + 1); // + 1 Para no tomar el \
                this.RutaImagen = File.FileName; // Guardamos toda la ruta
            }
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode(); // Creamos el objeto de BarCode
            Codigo.IncludeLabel = true; // Se agrega la etiqueta del codigo (Números)
            // Codificamos todos los atributos del código de barras y se lo asignamos al objeto de Panel
            PanelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128,TxtCodigo.Text,Color.Black,Color.White,400,100);
            BtnGuardarCodigo.Enabled = true;
        }

        private void BtnGuardarCodigo_Click(object sender, EventArgs e)
        {
            Image ImgFinal = (Image)PanelCodigo.BackgroundImage.Clone();// Clonamos el código de barras

            SaveFileDialog DialogoGuardar = new SaveFileDialog();
            DialogoGuardar.AddExtension = true; // Agrega extensión
            DialogoGuardar.Filter = "Image PNG (*.png)|*.png"; // Formato de imagen PNG
            DialogoGuardar.ShowDialog(); // Mostramos dialogo de guardado
            if (!string.IsNullOrEmpty(DialogoGuardar.FileName)) // Si se seleccionó algo 
            {
                ImgFinal.Save(DialogoGuardar.FileName, ImageFormat.Png); // Guarda la imagen en la variable
            }
            ImgFinal.Dispose(); // Libera el espacio de la variable
        }

        private void BtnInsertar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (CboCategoria.Text == String.Empty || TxtNombre.Text == string.Empty || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    this.MenssajeError("Falta ingresar algunos datos.");
                    // Enviaremos el error marcado a la caja corespondiente.
                    ErrorIcono.SetError(CboCategoria, "Ingrese una categoria");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingrese un precio de venta");
                    ErrorIcono.SetError(TxtStock, "Ingrese un valor de stock");
                }
                else
                {
                    Rpta = NArticulo.Insertar(Convert.ToInt32(CboCategoria.SelectedValue),TxtCodigo.Text.Trim(),TxtNombre.Text.Trim(),Convert.ToDecimal(TxtPrecioVenta.Text),Convert.ToInt32(TxtStock.Text),TxtDescripcion.Text.Trim(),TxtImagen.Text.Trim());
                    if (Rpta.Equals("Ok")) // Respuesta ok de la inserción
                    {
                        this.MensajeOk("Se insertó de forma correcta el registro.");
                        if (TxtImagen.Text != string.Empty)
                        {
                            // Esta cadena representa la ruta de destino Directorio + Ruta de imagen
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            // Ruta Origen , Ruta Destino
                            File.Copy(this.RutaImagen,this.RutaDestino);
                        }
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

        private void DgvListado_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                CboCategoria.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idcategoria"].Value);
                TxtCodigo.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Codigo"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtPrecioVenta.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Precio_Venta"].Value);
                TxtStock.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Stock"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                string Imagen; // Almacenará la ruta de la imagen del articulo
                Imagen = Convert.ToString(DgvListado.CurrentRow.Cells["Imagen"].Value);
                if (Imagen != string.Empty)
                {
                    // Si hay imagen para el artículo, se muestra en el elemento PIC IMAGE concatenando el directorio y el nombre del archivo imagen
                    PicImagen.Image = Image.FromFile(this.Directorio + Imagen);
                    // Al TXT le damos el nombre de la imagen
                    TxtImagen.Text = Imagen;
                }
                else
                {
                    PicImagen.Image = null;
                    TxtImagen.Text = "";
                }
                TabGeneral.SelectedIndex = 1; // Mostramos la segunda pestaña del formulario
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Seleccione desde la celda nombre. " + "| Error: " + ex.Message );
            }

        }

        private void BtnActualizar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtId.Text == string.Empty || CboCategoria.Text == String.Empty || TxtNombre.Text == string.Empty || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    this.MenssajeError("Falta ingresar algunos datos.");
                    // Enviaremos el error marcado a la caja corespondiente.
                    ErrorIcono.SetError(CboCategoria, "Ingrese una categoria");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingrese un precio de venta");
                    ErrorIcono.SetError(TxtStock, "Ingrese un valor de stock");
                }
                else
                {
                    Rpta = NArticulo.Actualizar(Convert.ToInt32(TxtId.Text),Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(),this.NombreAnt,TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta.Text), Convert.ToInt32(TxtStock.Text), TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
                    if (Rpta.Equals("Ok")) // Respuesta ok de la inserción
                    {
                        this.MensajeOk("Se actualizó de forma correcta el registro.");
                        if (TxtImagen.Text != string.Empty && this.RutaImagen!=string.Empty) // Consideramos la actualización de la imagen por articulo
                        {
                            // Esta cadena representa la ruta de destino Directorio + Ruta de imagen
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            // Ruta Origen , Ruta Destino
                            File.Copy(this.RutaImagen, this.RutaDestino);
                        }
                        this.Listar(); // Listamos de nuevo todos los registros incluyendo el nuevo
                        TabGeneral.SelectedIndex = 0;
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

        private void BtnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
        }

        private void DgvListado_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Si la columna selecionada es igual al index.
            // Esta instruccion nos permitirá marcar y desmarcar el check box de la lista.
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void ChkSeleccionar_CheckedChanged_1(object sender, EventArgs e)
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

        private void BtnEliminar_Click_1(object sender, EventArgs e)
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
                    string Imagen = ""; // Hay que eliminar la imagen asiciada a este articulo

                    // Hay que preparar para eliminar más de un registro, esto hacerlo con un ForEach
                    foreach (DataGridViewRow row in DgvListado.Rows) // Guardamos todas las filas en la variable row del DgvListado
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value)) // Si es true, significa que deseo eliminar la fila
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value); // Guardamos el ID de la categoria
                            Imagen = Convert.ToString(row.Cells[9].Value); // Obtenemos la ruta de la imagen del artículo
                            Rpta = NArticulo.Eliminar(Codigo);

                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se eliminó el registro " + Convert.ToString(row.Cells[2].Value));
                                File.Delete(this.Directorio + Imagen);
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

        private void BtnDesactivar_Click_1(object sender, EventArgs e)
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
                            Rpta = NArticulo.Desactivar(Codigo);
                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se desactivó el registro " + Convert.ToString(row.Cells[5].Value));
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

        private void BtnActivar_Click_1(object sender, EventArgs e)
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
                            Rpta = NArticulo.Activar(Codigo);
                            if (Rpta.Equals("Ok"))
                            {
                                this.MensajeOk("Se activó el registro " + Convert.ToString(row.Cells[5].Value));
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
