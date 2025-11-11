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


            if (!IsPostBack)
            {

                CargarGrillaUsuario();
                txtFiltradoUsario.Attributes["oninput"] = $"liveFilter('{txtFiltradoUsario.UniqueID}')";
                lblActivoCorrectamente.Visible = false;
                lblInactivoCorecto.Visible = false;
                btnVolver.Visible = false;



            }
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
                bool esMedico = usuario.TipoUsuario == TipoUsuario.Medico;

                if (esMedico)
                {
                    if (Session["idMedicoCreado"] == null ||
                        !int.TryParse(Session["idMedicoCreado"].ToString(), out idMedico))
                    {
                        lblMensaje.Text = "⚠️ Para crear un usuario de tipo Médico primero debe dar de alta el médico.";
                        lblMensaje.CssClass = "text-danger fw-semibold";
                        return;
                    }

                 
                    usuario.Medico = new Medico();
                    usuario.Medico.IdMedico = idMedico;
                    usuario.IdMedicoAsociado = idMedico;

                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.GuardarUsuario(usuario);

                    Session["idMedicoCreado"] = null;

                    lblMensaje.Text = "✅ Usuario creado correctamente.";
                    lblMensaje.CssClass = "text-success fw-semibold";
                    CargarGrillaUsuario();

                    TxtNombreUsuario.Text = string.Empty;
                    TxtPassword.Text = string.Empty;
                    ddlTipoUsuario.SelectedIndex = 0;
                }
            }
            catch
            {
                lblMensaje.Text = "❌ Ocurrió un error al crear el usuario.";
                lblMensaje.CssClass = "text-danger fw-semibold";
            }
        }

        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {

        }

        public void CargarGrillaUsuario()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            List<Usuario> lista = usuarioNegocio.listarusuario();
            Session.Add("listausuario", lista);
            gvUsuario.DataSource = lista;
            gvUsuario.DataBind();
        }

        protected void gvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuario.PageIndex = e.NewPageIndex;
            CargarGrillaUsuario();
        }

        protected void gvUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnActivar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer;


            btn.Visible = false;


            Button btnGuardar = (Button)fila.FindControl("btnGuardarInactivacion");
            if (btnGuardar != null)
                btnGuardar.Visible = true;

        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer;


            btn.Visible = false;

            Button btnGuardar = (Button)fila.FindControl("btnGuardarInactivacion");
            Button btnCncelar2 = (Button)fila.FindControl("btnCncelar2");
            if (btnGuardar != null)
                btnGuardar.Visible = true;

            if (btnCncelar2 != null)
                btnCncelar2.Visible = true;

        }

        protected void btnActivar_Click1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer;


            btn.Visible = false;



            Button btnGuardar = (Button)fila.FindControl("btnActivarUsuario");
            Button btnCancelar = (Button)fila.FindControl("btnCancelar");
            if (btnGuardar != null)
                btnGuardar.Visible = true;


            if (btnCancelar != null)
                btnCancelar.Visible = true;
        }

        protected void gvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnGuardarInactivacion = (Button)e.Row.FindControl("btnGuardarInactivacion");
                Button btnActivarUsuario = (Button)e.Row.FindControl("btnActivarUsuario");

                if (btnGuardarInactivacion != null)
                    btnGuardarInactivacion.Visible = false;

                if (btnActivarUsuario != null)
                    btnActivarUsuario.Visible = false;
            }

        }

        protected void btnGuardarInactivacion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            int idUsuario = Convert.ToInt32(gvUsuario.DataKeys[fila.RowIndex].Value);

            UsuarioNegocio negocio = new UsuarioNegocio();


            negocio.CambiarEstadoUsuario(idUsuario, false);
            lblActivoCorrectamente.Visible = false;
            lblInactivoCorecto.Visible = true;
            
            btnVolver.Visible = true;



            CargarGrillaUsuario();

        }

        protected void btnActivarUsuario_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            int idUsuario = Convert.ToInt32(gvUsuario.DataKeys[fila.RowIndex].Value);

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarEstadoUsuario(idUsuario, true);
            lblActivoCorrectamente.Visible = true;
            lblInactivoCorecto.Visible = false;
            btnVolver.Visible = true;
            
            CargarGrillaUsuario();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarGrillaUsuario();
        }

        protected void btnCncelar2_Click(object sender, EventArgs e)
        {
            CargarGrillaUsuario();
        }

        protected void txtFiltradoUsario_TextChanged(object sender, EventArgs e)
        {


            List<Usuario> lista = (List<Usuario>)Session["listausuario"];
            string filtro = txtFiltradoUsario.Text.ToUpper();

            List<Usuario> listafiltrada;

            if (string.IsNullOrWhiteSpace(filtro))
            {

                listafiltrada = lista;
            }
            else
            {

                listafiltrada = lista.FindAll(
                  x => x.NombreUsuario != null &&
                   x.NombreUsuario.ToUpper().Contains(filtro));





            }

            gvUsuario.DataSource = listafiltrada;
            gvUsuario.DataBind();

            txtFiltradoUsario.Focus();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            TxtNombreUsuario.Text = "";
            TxtPassword.Text = "";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            lblActivoCorrectamente.Visible = false;
            lblInactivoCorecto.Visible = false;
            btnVolver.Visible = false;

       
            CargarGrillaUsuario();

      
            txtFiltradoUsario.Text = string.Empty;
            txtFiltradoUsario.Focus();
        }
    }
}