using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class MainMenu : System.Web.UI.Page
    {
        #region Propiedades de Ordenamiento
        // Propiedades para manejar el orden de la grilla
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login y Usuarios/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                CargarTurnosProximos();
            }

            //if (Login.PuedeVerTurnos(Session) == TipoUsuario.Recepcion)
            //{               
            //    btnAgregarMedico.Visible = false;
            //    btnModificarMedico.Visible= false;
            //    btnListarMedico.Visible = true;
            //    btnBajaMedico.Visible = false;   

            //}

            else if (Login.PuedeVerTurnos(Session) == TipoUsuario.Medico)
            {
                Response.Redirect("~/Login y Usuarios/Login.aspx");

                return;


            }
            else if (Login.PuedeVerTurnos(Session) == TipoUsuario.SinDefinir) {


                Response.Redirect("~/Login y Usuarios/Login.aspx");

                return;
            
            }


                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

        }

        #region Métodos de Grilla "Turnos Próximos"
        void CargarTurnosProximos()
        {
            try
            {
                litErrorTurnos.Visible = false;
                TurnoNegocio negocio = new TurnoNegocio();
                DataTable dt = negocio.ListarTurnosDashboard();

                if (dt.Rows.Count > 0)
                {
                    pnlTurnosDashboard.Visible = true;
                    pnlNoTurnos.Visible = false;

                    DataView dv = dt.DefaultView;
                    if (!string.IsNullOrEmpty(this.SortExpression))
                    {
                        dv.Sort = string.Format("{0} {1}", this.SortExpression, this.SortDirection);
                    }
                    else
                    {
                        dv.Sort = "FechaInicio ASC, HoraInicio ASC";
                    }
                    gvTurnosProximos.DataSource = dv;
                }
                else
                {
                    pnlTurnosDashboard.Visible = false;
                    pnlNoTurnos.Visible = true;
                }
                gvTurnosProximos.DataBind();
            }
            catch (Exception ex)
            {
                pnlTurnosDashboard.Visible = false;
                pnlNoTurnos.Visible = false;
                litErrorTurnos.Visible = true;
                litErrorTurnos.Text = "<div class='alert alert-danger'><strong>Error:</strong> No se pudieron cargar los turnos. Por favor, intente recargar la página o contacte al administrador.</div>";
                          
                System.Diagnostics.Debug.WriteLine($"ERROR en CargarTurnosProximos: {ex.Message}");
            }
        }

        protected void gvTurnosProximos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnosProximos.PageIndex = e.NewPageIndex;
            CargarTurnosProximos();
        }

        protected void gvTurnosProximos_Sorting(object sender, GridViewSortEventArgs e)
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
            CargarTurnosProximos();
        }

        protected void gvTurnosProximos_RowCreated(object sender, GridViewRowEventArgs e)
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
        }

        #endregion

        protected void BtnAltaPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes/Pacientes.aspx");
        }

        protected void btnAgregarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("Medicos/AgregarMedico.aspx");
        }

        protected void btnModificarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("Medicos/AgregarMedico.aspx");
        }

        protected void btnListarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("Medicos/AgregarMedico.aspx");
        }

        protected void btnBajaMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("Medicos/AgregarMedico.aspx");
        }
    }
}