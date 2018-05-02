using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Back_mainpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userinfo"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {

                ((Label)this.Master.FindControl("Lb_Title")).Text = "後台管理";
            }
        }
    }
}