using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null) { Response.Redirect("~/Login y Usuarios/Login.aspx"); return; }

            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            if (usuarioLogueado.TipoUsuario != TipoUsuario.Admin)
            {
                Session.Clear();
                Response.Redirect("~/Login y Usuarios/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarGrillaUsuario();
                txtFiltradoUsario.Attributes["oninput"] = $"liveFilter('{txtFiltradoUsario.UniqueID}')";
            }
        }

        // ACCIÓN: GUARDAR O EDITAR
        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtNombreUsuario.Text) || string.IsNullOrWhiteSpace(TxtPassword.Text))
                {
                    lblMensaje.Text = "⚠️ Complete usuario y contraseña.";
                    lblMensaje.CssClass = "text-danger";
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = new Usuario();

                usuario.NombreUsuario = TxtNombreUsuario.Text;
                usuario.Password = TxtPassword.Text;
                usuario.TipoUsuario = (TipoUsuario)int.Parse(ddlTipoUsuario.SelectedValue);

                
                if (string.IsNullOrEmpty(hfIdUsuario.Value))
                {
                    
                    // Lógica de Médico
                    if (usuario.TipoUsuario == TipoUsuario.Medico)
                    {
                        if (Session["idMedicoCreado"] != null)
                        {
                            usuario.IdMedicoAsociado = int.Parse(Session["idMedicoCreado"].ToString());
                            negocio.GuardarUsuario(usuario);
                            Session["idMedicoCreado"] = null;
                        }
                        else
                        {
                            lblMensaje.Text = "⚠️ Para crear un Médico nuevo, primero debe darlo de alta en la sección Médicos.";
                            return;
                        }
                    }
                    else
                    {
                        
                        negocio.GuardarUsuario(usuario);
                    }
                    lblMensaje.Text = "✅ Usuario Creado.";
                }
                else
                {
                    // --- ES MODIFICACIÓN ---
                    usuario.IdUsuario = int.Parse(hfIdUsuario.Value);

                    // Aquí permitimos cambiar nombre, pass y ROL.
                    // no estamos asignando ID Medico aquí para simplificar.
                    negocio.ActualizarUsuario(usuario);

                    lblMensaje.Text = "✏️ Usuario Modificado Exitosamente.";
                }

               
                LimpiarFormulario();
                CargarGrillaUsuario();
                lblMensaje.CssClass = "text-success fw-bold";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }

        //  EDITAR (Desde la Grilla)
        protected void gvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);

                
                List<Usuario> lista = (List<Usuario>)Session["listausuario"];
                Usuario seleccionado = lista.Find(x => x.IdUsuario == idUsuario);

                if (seleccionado != null)
                {
                    
                    TxtNombreUsuario.Text = seleccionado.NombreUsuario;
                    TxtPassword.Text = seleccionado.Password;
                    ddlTipoUsuario.SelectedValue = ((int)seleccionado.TipoUsuario).ToString();

                   
                    hfIdUsuario.Value = seleccionado.IdUsuario.ToString();

                    btnGuardarUsuario.Text = "Modificar Usuario";
                    btnGuardarUsuario.CssClass = "btn btn-warning me-3";
                }
            }
        }

        // BAJA LÓGICA 
        protected void btnGuardarInactivacion_Click(object sender, EventArgs e)
        {
            
            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            int idUsuario = Convert.ToInt32(gvUsuario.DataKeys[fila.RowIndex].Value);

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarEstadoUsuario(idUsuario, false); 

            CargarGrillaUsuario();
        }

        // ALTA LÓGICA 
        protected void btnActivarUsuario_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            int idUsuario = Convert.ToInt32(gvUsuario.DataKeys[fila.RowIndex].Value);

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarEstadoUsuario(idUsuario, true); 

            CargarGrillaUsuario();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            TxtNombreUsuario.Text = "";
            TxtPassword.Text = "";
            ddlTipoUsuario.SelectedIndex = 0;
            hfIdUsuario.Value = ""; 
            btnGuardarUsuario.Text = "Guardar Usuario";
            btnGuardarUsuario.CssClass = "btn btn-primary me-3";
            lblMensaje.Text = "";
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

        protected void gvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void txtFiltradoUsario_TextChanged(object sender, EventArgs e)
        {
            List<Usuario> lista = (List<Usuario>)Session["listausuario"];
            string filtro = txtFiltradoUsario.Text.ToUpper();
            List<Usuario> listafiltrada;

            if (string.IsNullOrWhiteSpace(filtro))
                listafiltrada = lista;
            else
                listafiltrada = lista.FindAll(x => x.NombreUsuario != null && x.NombreUsuario.ToUpper().Contains(filtro));

            gvUsuario.DataSource = listafiltrada;
            gvUsuario.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Medicos/AgregarMedico.aspx");
        }
    }
}