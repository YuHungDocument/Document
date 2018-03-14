using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class GuildPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userinfo"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                UserInfo tmpUserInfo = null;
                if (Session["userinfo"] is UserInfo)
                {
                    tmpUserInfo = (UserInfo)Session["userinfo"];
                    LinkButton2.Text = tmpUserInfo.Name;
                }
            }
        }

        protected void Lb_Logout_Click(object sender, EventArgs e)
        {
            Session["UserInfo"] = null;
            Response.Redirect("Home.aspx");
        }
    }
}