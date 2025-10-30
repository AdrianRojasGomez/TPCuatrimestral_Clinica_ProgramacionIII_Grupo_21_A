using System;
using System.Configuration; // Necesario para leer el Web.config
using System.Data;
using System.Data.SqlClient; // Necesario para SQL Server
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class Pacientes : System.Web.UI.Page
    {
        // Obtiene la cadena de conexión que definimos en Web.config
        readonly string connectionString = ConfigurationManager.ConnectionStrings["ClinicaConnection"].ConnectionString;

        // Se dispara CADA VEZ que se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {
            // IsPostBack es 'false' solo la primera vez que entras a la página.
            // Si es 'true', significa que apretaste un botón (ej: Guardar).
            if (!IsPostBack)
            {
                CargarPacientes();
                divMensaje.Visible = false;
            }
        }

        #region MÉTODOS DE DATOS (BD)

        /// <summary>
        /// Lee todos los pacientes de la BD y los carga en el GridView
        /// </summary>
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

        /// <summary>
        /// Carga los datos de un paciente específico en el formulario para editarlo.
        /// </summary>
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
                        // Llenamos el formulario
                        hfPacienteId.Value = reader["IdPaciente"].ToString();
                        txtNombre.Text = reader["Nombre"].ToString();
                        txtApellido.Text = reader["Apellido"].ToString();
                        txtDni.Text = reader["Dni"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();
                        txtDireccion.Text = reader["Direccion"].ToString();

                        // Manejamos la fecha, que puede ser nula (DBNull)
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

        /// <summary>
        /// Elimina un paciente de la BD por su ID.
        /// </summary>
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

        /// <summary>
        /// Prepara el formulario para crear un nuevo paciente.
        /// </summary>
        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = true;
            lblFormTitulo.Text = "Nuevo Paciente";
            LimpiarFormulario();
            divMensaje.Visible = false;
        }

        /// <summary>
        /// Oculta el formulario y limpia los campos.
        /// </summary>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = false;
            LimpiarFormulario();
            divMensaje.Visible = false;
        }

        /// <summary>
        /// Guarda un paciente (ya sea Nuevo o Edición)
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Leemos el ID del HiddenField. Si es 0, es NUEVO. Si no, es EDITAR.
                int pacienteId = Convert.ToInt32(hfPacienteId.Value);

                string query;
                if (pacienteId == 0)
                {
                    // Query de INSERT (Nuevo)
                    query = @"INSERT INTO Pacientes (Dni, Apellido, Nombre, FechaNacimiento, Email, Telefono, Direccion) 
                              VALUES (@Dni, @Apellido, @Nombre, @FechaNacimiento, @Email, @Telefono, @Direccion)";
                }
                else
                {
                    // Query de UPDATE (Editar)
                    query = @"UPDATE Pacientes 
                              SET Dni = @Dni, Apellido = @Apellido, Nombre = @Nombre, 
                                  FechaNacimiento = @FechaNacimiento, Email = @Email, 
                                  Telefono = @Telefono, Direccion = @Direccion
                              WHERE IdPaciente = @IdPaciente";
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregamos el ID solo si es un UPDATE
                    if (pacienteId != 0)
                    {
                        cmd.Parameters.AddWithValue("@IdPaciente", pacienteId);
                    }

                    // Agregamos los parámetros comunes
                    cmd.Parameters.AddWithValue("@Dni", txtDni.Text.Trim());
                    cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim());
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());

                    // Manejamos la fecha nula (si el campo está vacío, guardamos DBNull)
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

                    // Mostramos mensaje de éxito
                    MostrarMensaje(pacienteId == 0 ? "Paciente creado exitosamente." : "Paciente actualizado exitosamente.", "success");
                }
            }
            catch (Exception ex)
            {
                // Manejamos errores, como un DNI duplicado
                MostrarMensaje($"Error al guardar paciente: {ex.Message}", "danger");
            }
            finally
            {
                // Al final, ocultamos el formulario y recargamos la grilla
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