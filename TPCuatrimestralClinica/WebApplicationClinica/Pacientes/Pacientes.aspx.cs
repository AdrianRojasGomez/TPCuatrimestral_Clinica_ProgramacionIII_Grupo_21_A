using Dominio;
using Negocio;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class Pacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPacientes();
                divMensaje.Visible = false;
            }
        }

        #region MÉTODOS DE DATOS (LLAMAN A NEGOCIO)

        void CargarPacientes(string filtro = "")
        {
            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                DataTable dt = negocio.Listar(filtro);

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
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar pacientes: {ex.Message}", "danger");
            }
        }

        void CargarDatosPaciente(int id)
        {
            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                Paciente pac = negocio.BuscarPorId(id);

                if (pac != null)
                {
                    hfPacienteId.Value = pac.IdPaciente.ToString();
                    txtNombre.Text = pac.Nombre;
                    txtApellido.Text = pac.Apellido;
                    txtDni.Text = pac.Dni;
                    txtEmail.Text = pac.Email;
                    txtTelefono.Text = pac.Telefono;
                    txtDireccion.Text = pac.Direccion;

                    if (pac.FechaNacimiento != null)
                    {
                        txtFechaNacimiento.Text = ((DateTime)pac.FechaNacimiento).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        txtFechaNacimiento.Text = "";
                    }
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
                PacienteNegocio negocio = new PacienteNegocio();
                negocio.EliminarLogico(id);
                MostrarMensaje("Paciente eliminado correctamente.", "success");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "danger");
            }
        }

        void ReactivarPaciente(int id)
        {
            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                negocio.ReactivarLogico(id);
                MostrarMensaje("Paciente reactivado correctamente.", "success");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "danger");
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
                Paciente pac = new Paciente();
                pac.IdPaciente = Convert.ToInt32(hfPacienteId.Value);
                pac.Dni = txtDni.Text.Trim();
                pac.Apellido = txtApellido.Text.Trim();
                pac.Nombre = txtNombre.Text.Trim();
                pac.Email = txtEmail.Text.Trim();
                pac.Telefono = txtTelefono.Text.Trim();
                pac.Direccion = txtDireccion.Text.Trim();

                if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
                {
                    pac.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
                }

                PacienteNegocio negocio = new PacienteNegocio();
                if (pac.IdPaciente == 0)
                {
                    negocio.GuardarNuevo(pac);
                }
                else
                {
                    negocio.Modificar(pac);
                }

                MostrarMensaje(pac.IdPaciente == 0 ? "Paciente creado exitosamente." : "Paciente actualizado exitosamente.", "success");
                pnlFormulario.Visible = false;
                CargarPacientes();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "danger");
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
            
            if (!int.TryParse(e.CommandArgument?.ToString(), out int pacienteId))
            {
                return;
            }

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
            else if (e.CommandName == "ReactivarPaciente") 
            {
                ReactivarPaciente(pacienteId);
                CargarPacientes(txtBuscarPaciente.Text.Trim());
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
                            string sortIcon = (this.SortDirection == "ASC")
                                ? " <i class='bi bi-caret-up-fill'></i>"
                                : " <i class='bi bi-caret-down-fill'></i>";
                            cell.Controls.Add(new LiteralControl(sortIcon));
                        }
                    }
                }
            }

            else if (e.Row.RowType == DataControlRowType.Pager)
            {
                // Aquí va tu código para el paginador de Bootstrap
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