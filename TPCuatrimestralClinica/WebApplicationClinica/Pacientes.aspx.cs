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

        void CargarPacientes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT IdPaciente, Dni, Apellido, Nombre, Email, Telefono FROM Pacientes ORDER BY Apellido, Nombre";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvPacientes.DataSource = dt;
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
                MostrarMensaje($"Error al eliminar paciente: {ex.Message}", "danger");
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
            try
            {
                
                int pacienteId = Convert.ToInt32(hfPacienteId.Value);

                string query;
                if (pacienteId == 0)
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

                   
                    if (pacienteId != 0)
                    {
                        cmd.Parameters.AddWithValue("@IdPaciente", pacienteId);
                    }

                   
                    cmd.Parameters.AddWithValue("@Dni", txtDni.Text.Trim());
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
                        cmd.Parameters.AddWithValue("@FechaNacimiento", txtFechaNacimiento.Text);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();

                    
                    MostrarMensaje(pacienteId == 0 ? "Paciente creado exitosamente." : "Paciente actualizado exitosamente.", "success");
                }
            }
            catch (Exception ex)
            {
               
                MostrarMensaje($"Error al guardar paciente: {ex.Message}", "danger");
            }
            finally
            {
                
                pnlFormulario.Visible = false;
                CargarPacientes();
            }
        }

        #endregion

        #region EVENTOS DEL GRIDVIEW

        /// <summary>
        /// Se dispara al presionar 'Editar' o 'Eliminar' en la grilla.
        /// </summary>
        protected void gvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtenemos el ID del paciente desde CommandArgument
            int pacienteId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                // Cargar datos para editar
                CargarDatosPaciente(pacienteId);
                pnlFormulario.Visible = true;
                lblFormTitulo.Text = "Editar Paciente";
                divMensaje.Visible = false;
            }
            else if (e.CommandName == "Delete")
            {
                // Eliminar paciente
                EliminarPaciente(pacienteId);
                // Refrescamos la grilla
                CargarPacientes();
            }
        }

        #endregion

        #region MÉTODOS AUXILIARES

        /// <summary>
        /// Limpia todos los campos del formulario.
        /// </summary>
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

        /// <summary>
        /// Muestra el panel de mensajes con un color y texto específicos.
        /// </summary>
        /// <param name="mensaje">Texto a mostrar.</param>
        /// <param name="tipo">"success" (verde), "danger" (rojo) o "info" (azul)</param>
        void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            // Cambiamos la clase CSS del div para que muestre el color correcto
            divMensaje.Attributes["class"] = $"alert alert-{tipo}";
            divMensaje.Visible = true;
        }

        #endregion
    }
}