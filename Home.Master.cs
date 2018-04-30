using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Home1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userinfo"] == null)
            {
                Lb_Logout.Visible = false;
            }
            else
            {
                Lb_Logout.Visible = true;
            }
        }

        protected void Lb_Logout_Click(object sender, EventArgs e)
        {
            Session["userinfo"] = null;
            Response.Redirect("Home.aspx");
        }
    }
}