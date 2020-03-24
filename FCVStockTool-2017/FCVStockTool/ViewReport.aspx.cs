using FCVStockTool.Services;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FCVStockTool
{
    public partial class ViewReport : System.Web.UI.Page
    {
        ReportService vr;
        int UserID = 0;
        protected override void Render(HtmlTextWriter writer)
        {
            // Stop Caching in IE
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            Response.Cache.SetNoStore();
            base.Render(writer);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
           // Page.Title = "View Report" + Request.QueryString["report"] != null ? " - " + Request.QueryString["report"].ToString() : "";


            //get login cookie
            //HttpCookie logincookie = this.Context.Request.Cookies["sc-login"];
            //if (logincookie != null)
            //{
            //    //get userid
            //    UserID = int.Parse(logincookie["id"]);
            //}
            //else
            //{
            //    Response.Redirect("~/Unauthentication.aspx");
            //}

            vr = new ReportService();
            if (!IsPostBack)
            {
                string reportID = "";
                if (Request.QueryString["reportid"] != null) reportID = Request.QueryString["reportid"].ToString();
                ReportViewer1.Reset();
                vr.ViewReport(ReportViewer1, reportID);
                //List<ReportParameter> parameters = new List<ReportParameter>();

                //foreach (ReportParameterInfo param in ReportViewer1.ServerReport.GetParameters())
                //{
                //    ReportParameter reparam = null;
                //    if (param.Name.ToLower().Contains("userid"))
                //    {
                //        reparam = new ReportParameter(param.Name, UserID.ToString());
                //        reparam.Visible = false;
                //    }
                //    if (reparam != null) parameters.Add(reparam);
                //}
                //ReportViewer1.ServerReport.SetParameters(parameters);
            }
        }
    }
}