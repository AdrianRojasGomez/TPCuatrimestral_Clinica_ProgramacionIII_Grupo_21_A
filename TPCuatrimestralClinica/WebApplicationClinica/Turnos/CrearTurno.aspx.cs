using Dominio;
using Negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class CrearTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlDatosPaciente.Visible = false;
                pnlAgregarPaciente.Visible = false;
                lblMensajeError.Visible = false;
                btnIrAgregarPaciente.Visible = false;

            }
        }

        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            pnlDatosPaciente.Visible = false;
            pnlAgregarPaciente.Visible = false;
            lblMensajeError.Visible = false;
            btnIrAgregarPaciente.Visible = false;
            lblPacienteEstado.CssClass = "badge bg-secondary";
            lblPacienteEstado.Text = "Buscando...";

            string dni = txtDocumento.Text.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                lblMensajeError.Text = "Debe ingresar un DNI.";
                lblMensajeError.Visible = true;
                lblPacienteEstado.Text = "Error";
                lblPacienteEstado.CssClass = "badge bg-danger";
                return;
            }

            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                Paciente pacienteEncontrado = negocio.BuscarPorDni(dni);

                if (pacienteEncontrado != null)
                {
                    pnlDatosPaciente.Visible = true;
                    lblPacienteEstado.CssClass = "badge bg-success";
                    lblPacienteEstado.Text = "Paciente Encontrado";
                    txtNombrePaciente.Text = pacienteEncontrado.Nombre;
                    txtApellidoPaciente.Text = pacienteEncontrado.Apellido;
                    txtEmailPaciente.Text = pacienteEncontrado.Email;
                    txtTelefonoPaciente.Text = pacienteEncontrado.Telefono;
                    ViewState["IdPaciente"] = pacienteEncontrado.IdPaciente;
                }
                else
                {
                    lblPacienteEstado.CssClass = "badge bg-danger";
                    lblPacienteEstado.Text = "Paciente no registrado";
                    lblMensajeError.Text = "El DNI ingresado no pertenece a un paciente. Puede agregarlo.";
                    lblMensajeError.Visible = true;
                    btnIrAgregarPaciente.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblPacienteEstado.CssClass = "badge bg-danger";
                lblPacienteEstado.Text = "Error";
                lblMensajeError.Text = "Error al buscar en la base de datos: " + ex.Message;
                lblMensajeError.Visible = true;
            }
        }

        protected void btnIrAgregarPaciente_Click(object sender, EventArgs e)
        {
            pnlAgregarPaciente.Visible = true;
            pnlDatosPaciente.Visible = false;
            lblMensajeError.Visible = false;
            btnIrAgregarPaciente.Visible = false; 
            txtNuevoDni.Text = txtDocumento.Text.Trim();
        }

        protected void btnGuardarNuevoPaciente_Click(object sender, EventArgs e)
        {
           
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                Paciente pac = new Paciente();
                pac.Dni = txtNuevoDni.Text.Trim();
                pac.Nombre = txtNuevoNombre.Text.Trim();
                pac.Apellido = txtNuevoApellido.Text.Trim();
                pac.Email = txtNuevoEmail.Text.Trim();
                pac.Telefono = txtNuevoTelefono.Text.Trim();
                pac.Direccion = txtNuevoDireccion.Text.Trim();
                if (!string.IsNullOrEmpty(txtNuevoFechaNacimiento.Text))
                {
                    pac.FechaNacimiento = Convert.ToDateTime(txtNuevoFechaNacimiento.Text);
                }

                PacienteNegocio negocio = new PacienteNegocio();
                negocio.GuardarNuevo(pac);

                Paciente pacienteGuardado = negocio.BuscarPorDni(pac.Dni);

                pnlAgregarPaciente.Visible = false; 
                pnlDatosPaciente.Visible = true; 
                lblPacienteEstado.CssClass = "badge bg-success";
                lblPacienteEstado.Text = "Guardado y Seleccionado";
                txtNombrePaciente.Text = pacienteGuardado.Nombre;
                txtApellidoPaciente.Text = pacienteGuardado.Apellido;
                txtEmailPaciente.Text = pacienteGuardado.Email;
                txtTelefonoPaciente.Text = pacienteGuardado.Telefono;
                ViewState["IdPaciente"] = pacienteGuardado.IdPaciente;
            }
            catch (Exception ex)
            {
                lblMensajeNuevoPaciente.Text = ex.Message;
                lblMensajeNuevoPaciente.Visible = true;
            }
        }

        protected void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            pnlAgregarPaciente.Visible = false;
            lblMensajeError.Visible = true;
            btnIrAgregarPaciente.Visible = true;
        }

       

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
           
            if (ViewState["IdPaciente"] == null)
            {
                lblMensajeError.Text = "Debe buscar y encontrar un paciente válido antes de guardar.";
                lblMensajeError.Visible = true;
                return;
            }

           
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mainmenu.aspx");
        }
    }
}