using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombreUsuario.Text) ||
     string.IsNullOrWhiteSpace(TxtPassword.Text) ||
     string.IsNullOrWhiteSpace(ddlTipoUsuario.SelectedValue))
            {
                lblMensaje.Text = "⚠️ Complete todos los campos.";
                lblMensaje.CssClass = "text-danger fw-semibold";
                return;
            }

            try
            {
                Usuario usuario = new Usuario();
                usuario.NombreUsuario = TxtNombreUsuario.Text.Trim();
                usuario.Password = TxtPassword.Text.Trim();
                usuario.TipoUsuario = (TipoUsuario)int.Parse(ddlTipoUsuario.SelectedValue);
                

                usuario.Medico = null;

               
                int idMedico;
                if (usuario.TipoUsuario == TipoUsuario.Medico &&
                    Session["idMedicoCreado"] != null &&
                    int.TryParse(Session["idMedicoCreado"].ToString(), out idMedico))
                {
                    usuario.Medico = new Medico();
                    usuario.Medico.IdMedico = idMedico;
                    usuario.IdMedicoAsociado = idMedico;

                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.GuardarUsuario(usuario);

                lblMensaje.Text = "✅ Usuario creado correctamente.";
                lblMensaje.CssClass = "text-success fw-semibold";

                TxtNombreUsuario.Text = string.Empty;
                TxtPassword.Text = string.Empty;
                ddlTipoUsuario.SelectedIndex = 0;
            }
            catch
            {
                lblMensaje.Text = "❌ Ocurrió un error al crear el usuario.";
                lblMensaje.CssClass = "text-danger fw-semibold";
            }
        }
    }
}