using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargargrillaespecialidaddes();


            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            TxtNombreEspecialidad.Text = string.Empty;


            lblMensajeEspecialidad.Text = string.Empty;
            lblMensajeEspecialidad.Visible = false;

            btnCancelar.Visible = true;
            btnGuardarEspecialidad.Visible = true;


            gvEspecialidades.EditIndex = -1;


            cargargrillaespecialidaddes();


            TxtNombreEspecialidad.Focus();
        }

        protected void TxtNombreEspecialidad_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvEspecilidades_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvEspecilidades_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvEspecilidades_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvEspecilidades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        public void cargargrillaespecialidaddes()
        {




            EspeciladadNegocio especiladadNegocio = new EspeciladadNegocio();

            List<Especialidad> lista = especiladadNegocio.Listar();
            Session.Add("listaespecialidad", lista);
            gvEspecialidades.DataSource = lista;
            gvEspecialidades.DataBind();



        }

        protected void btnGuardarEspecialidad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombreEspecialidad.Text))
            {
                lblMensajeEspecialidad.Visible = true;
                lblMensajeEspecialidad.Text = "⚠️ Debe ingresar un nombre de especialidad.";
                lblMensajeEspecialidad.CssClass = "alert alert-danger fw-semibold mt-2 text-start w-100";
                return;
            }


            string texto = TxtNombreEspecialidad.Text.Trim();


            Regex soloLetras = new Regex(@"^[a-zA-ZÁÉÍÓÚáéíóúÑñ ]+$");

            if (!soloLetras.IsMatch(texto))
            {
                lblMensajeEspecialidad.Visible = true;
                lblMensajeEspecialidad.Text = "⚠️ El nombre solo puede contener letras y espacios. No se permiten números ni símbolos.";
                lblMensajeEspecialidad.CssClass = "alert alert-danger fw-semibold mt-2 text-start w-100";
                return;
            }


                Especialidad especialidad = new Especialidad();
                EspeciladadNegocio especiladadNegocio = new EspeciladadNegocio();

                especialidad.Nombre = TxtNombreEspecialidad.Text.Trim();

                try
                {

                    especiladadNegocio.AgregarEspecialidad(especialidad);




                    TxtNombreEspecialidad.Text = string.Empty;



                    lblMensajeEspecialidad.Text = "✅ Especialidad guardada correctamente.";
                    lblMensajeEspecialidad.CssClass = "alert alert-success fw-semibold mt-2 text-start w-100";
                    lblMensajeEspecialidad.Visible = true;

                    btnCancelar.Visible = false;
                    btnLimpiar.Visible = true;
                    btnGuardarEspecialidad.Visible = false;

                    TxtNombreEspecialidad.Focus();
                    cargargrillaespecialidaddes();
                }
                catch (Exception)
                {
                    lblMensajeEspecialidad.Visible = true;
                    lblMensajeEspecialidad.Text = "❌ Ocurrió un error al guardar la especialidad. Intente nuevamente.";
                    lblMensajeEspecialidad.CssClass = "alert alert-danger fw-semibold mt-2 text-start w-100";

                    btnLimpiar.Visible = true;
                    btnCancelar.Visible = false;
                    btnGuardarEspecialidad.Visible = false;


                }


            
        }
    }
}