using Dominio;
using Negocio;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class GestionTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTurnos();
                divMensaje.Visible = false;
            }
        }

        #region MÉTODOS DE DATOS (LLAMAN A NEGOCIO)

        void CargarTurnos(string filtro = "")
        {
            try
            {
                TurnoNegocio negocio = new TurnoNegocio();
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
                        
                        dv.Sort = "FechaInicio ASC, HoraInicio ASC";
                    }
                    gvTurnos.DataSource = dv;
                }
                else
                {
                    gvTurnos.DataSource = dt;
                }
                gvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar turnos: {ex.Message}", "danger");
            }
        }

        void CancelarTurno(int idTurno)
        {
            try
            {
                TurnoNegocio negocio = new TurnoNegocio();
                negocio.Cancelar(idTurno);
                MostrarMensaje("Turno cancelado correctamente.", "success");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "danger");
            }
        }

        #endregion

        #region EVENTOS DE BOTONES

        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            // Redirige a la página que ya creamos para agregar turnos
            Response.Redirect("~/CrearTurno.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTurnos(txtBuscarTurno.Text.Trim());
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

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            CargarTurnos(txtBuscarTurno.Text.Trim()); 
        }

        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Atender" || e.CommandName == "CancelarTurno")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Atender")
                {
                   
                    Response.Redirect($"~/Turnos/AtenderTurno.aspx?id={idTurno}");
                }
                else if (e.CommandName == "CancelarTurno")
                {
                    CancelarTurno(idTurno);
                    CargarTurnos(txtBuscarTurno.Text.Trim()); 
                }
            }
        }

        protected void gvTurnos_Sorting(object sender, GridViewSortEventArgs e)
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
            CargarTurnos(txtBuscarTurno.Text.Trim()); 
        }

        protected void gvTurnos_RowCreated(object sender, GridViewRowEventArgs e)
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
                
            }
        }

        #endregion

        #region MÉTODOS AUXILIARES

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