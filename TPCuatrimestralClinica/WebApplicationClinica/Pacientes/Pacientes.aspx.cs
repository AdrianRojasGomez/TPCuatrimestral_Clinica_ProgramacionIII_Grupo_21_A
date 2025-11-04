using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class Pacientes : System.Web.UI.Page
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["ClinicaConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPacientes();
                divMensaje.Visible = false;
            }
        }

        #region MÉTODOS DE DATOS (BD)

        void CargarPacientes(string filtro = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT IdPaciente, Dni, Apellido, Nombre, Email, Telefono FROM Pacientes ";
                    
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        query += "WHERE Dni LIKE @Filtro OR Apellido LIKE @Filtro OR Nombre LIKE @Filtro ";
                    }                    

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                    }

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {                        
                        DataView dv = dt.DefaultView;

                        if (!string.IsNullOrEmpty(this.SortExpression))
                        {
                            
                            dv.Sort = string.Format("{0} {1}", this.SortExpression, this.SortDirection);
                        }
                        else
                        {
                            
                            dv.Sort = "Apellido ASC, Nombre ASC";
                        }
                                                
                        gvPacientes.DataSource = dv;
                    }
                    else
                    {
                        gvPacientes.DataSource = dt;
                    }
                                       
                    gvPacientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar pacientes: {ex.Message}", "danger");
            }
        }

        void CargarDatosPaciente(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Pacientes WHERE IdPaciente = @IdPaciente";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdPaciente", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        hfPacienteId.Value = reader["IdPaciente"].ToString();
                        txtNombre.Text = reader["Nombre"].ToString();
                        txtApellido.Text = reader["Apellido"].ToString();
                        txtDni.Text = reader["Dni"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();
                        txtDireccion.Text = reader["Direccion"].ToString();

                        if (reader["FechaNacimiento"] != DBNull.Value)
                        {
                            txtFechaNacimiento.Text = Convert.ToDateTime(reader["FechaNacimiento"]).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            txtFechaNacimiento.Text = "";
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos del paciente: {ex.Message}", "danger");
            }
        }

        void EliminarPaciente(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Pacientes WHERE IdPaciente = @IdPaciente";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdPaciente", id);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MostrarMensaje("Paciente eliminado correctamente.", "success");
                }
            }
            catch (Exception ex)
            {
                // Verifica si la excepción es por una restricción de clave externa
                if (ex is SqlException sqlEx && sqlEx.Number == 547) // Número de error para FK constraint violation
                {
                    MostrarMensaje("No se puede eliminar el paciente porque tiene turnos asociados.", "danger");
                }
                else
                {
                    MostrarMensaje($"Error al eliminar paciente: {ex.Message}", "danger");
                }
            }
        }

        #endregion

        #region EVENTOS DE BOTONES

        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = true;
            lblFormTitulo.Text = "Nuevo Paciente";
            LimpiarFormulario();
            divMensaje.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = false;
            LimpiarFormulario();
            divMensaje.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                int pacienteIdActual = Convert.ToInt32(hfPacienteId.Value); 
                string dniIngresado = txtDni.Text.Trim();

                int idPacienteExistente = 0;
                string nombreExistente = "";
                string apellidoExistente = "";

                using (SqlConnection conCheck = new SqlConnection(connectionString))
                {
                    string queryCheck = "SELECT IdPaciente, Nombre, Apellido FROM Pacientes WHERE Dni = @Dni";
                    SqlCommand cmdCheck = new SqlCommand(queryCheck, conCheck);
                    cmdCheck.Parameters.AddWithValue("@Dni", dniIngresado);

                    conCheck.Open();
                    using (SqlDataReader reader = cmdCheck.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idPacienteExistente = Convert.ToInt32(reader["IdPaciente"]);
                            nombreExistente = reader["Nombre"].ToString();
                            apellidoExistente = reader["Apellido"].ToString();
                        }
                    }
                }
                // si DNI Duplicado
                if (idPacienteExistente != 0 && idPacienteExistente != pacienteIdActual)
                {                    
                    MostrarMensaje($"Error: El DNI '{dniIngresado}' ya está registrado a nombre de: {nombreExistente} {apellidoExistente}.", "danger");
                    return;
                }
                string query;
                if (pacienteIdActual == 0)
                {
                    query = @"INSERT INTO Pacientes (Dni, Apellido, Nombre, FechaNacimiento, Email, Telefono, Direccion)
                      VALUES (@Dni, @Apellido, @Nombre, @FechaNacimiento, @Email, @Telefono, @Direccion)";
                }
                else
                {
                    query = @"UPDATE Pacientes
                      SET Dni = @Dni, Apellido = @Apellido, Nombre = @Nombre,
                          FechaNacimiento = @FechaNacimiento, Email = @Email,
                          Telefono = @Telefono, Direccion = @Direccion
                      WHERE IdPaciente = @IdPaciente";
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (pacienteIdActual != 0)
                    {
                        cmd.Parameters.AddWithValue("@IdPaciente", pacienteIdActual);
                    }

                    cmd.Parameters.AddWithValue("@Dni", dniIngresado);
                    cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim());
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());

                    if (string.IsNullOrEmpty(txtFechaNacimiento.Text))
                    {
                        cmd.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FechaNacimiento", Convert.ToDateTime(txtFechaNacimiento.Text));
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MostrarMensaje(pacienteIdActual == 0 ? "Paciente creado exitosamente." : "Paciente actualizado exitosamente.", "success");

                    pnlFormulario.Visible = false;
                    CargarPacientes();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar paciente: {ex.Message}", "danger");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarPacientes(txtBuscarPaciente.Text.Trim());
            divMensaje.Visible = false;
        }

        #endregion

        #region EVENTOS DEL GRIDVIEW
        
        public string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? string.Empty; }
            set { ViewState["SortExpression"] = value; }
        }

        public string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }
        protected void gvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            gvPacientes.PageIndex = e.NewPageIndex;
            CargarPacientes();
        }

        protected void gvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarPaciente" || e.CommandName == "CustomDelete")
            {
                int pacienteId = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "EditarPaciente")
                {
                    CargarDatosPaciente(pacienteId);
                    pnlFormulario.Visible = true;
                    lblFormTitulo.Text = "Editar Paciente";
                    divMensaje.Visible = false;
                }
                else if (e.CommandName == "CustomDelete")
                {
                    EliminarPaciente(pacienteId);
                    CargarPacientes(txtBuscarPaciente.Text.Trim());
                }
            }
           
        }
        protected void gvPacientes_Sorting(object sender, GridViewSortEventArgs e)
        {
            
            string newSortExpression = e.SortExpression;

           
            if (this.SortExpression == newSortExpression)
            {
                this.SortDirection = (this.SortDirection == "ASC") ? "DESC" : "ASC";
            }
            else
            {
                this.SortDirection = "ASC";
            }
                       
            this.SortExpression = newSortExpression;

            CargarPacientes();
        }
        protected void gvPacientes_RowCreated(object sender, GridViewRowEventArgs e)
        {
           if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Controls.Count > 0 && cell.Controls[0] is LinkButton)
                    {
                        LinkButton sortButton = (LinkButton)cell.Controls[0];

                        if (sortButton.CommandArgument == this.SortExpression)
                        {
                            string sortIcon = "";
                            if (this.SortDirection == "ASC")
                            {
                                sortIcon = " <i class='bi bi-caret-up-fill'></i>";
                            }
                            else
                            {
                                sortIcon = " <i class='bi bi-caret-down-fill'></i>";
                            }

                            cell.Controls.Add(new LiteralControl(sortIcon));
                        }
                    }
                }
            }
        }

        #endregion

        #region MÉTODOS AUXILIARES

        void LimpiarFormulario()
        {
            hfPacienteId.Value = "0";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtFechaNacimiento.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }

        void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            divMensaje.Attributes["class"] = $"alert alert-{tipo} alert-dismissible fade show"; 
            divMensaje.Visible = true;
            lblMensaje.Text += "<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>";
        }

        #endregion
    }
}