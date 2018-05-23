using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Management : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["managerID"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected void Lb_LogOut_Click(object sender, EventArgs e)
        {
            Session["managerID"] = null;
            Response.Redirect("Home.aspx");
        }
    }
}