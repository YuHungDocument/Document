using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class UserPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lb_UserInfo.Text = tmpUserInfo.Department + "/" + tmpUserInfo.position + "/" + tmpUserInfo.Name;
                        if (tmpUserInfo.Permission < 5)
                        {
                            Lb_Manager.Enabled = true;
                        }
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["UserInfo"] = null;
            Response.Redirect("Home.aspx");
        }

        protected void Lb_Wait_Click(object sender, EventArgs e)
        {
            Response.Redirect("WaitProcess.aspx");
        }

        protected void Lb_Host_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hostdocument.aspx");
        }

        protected void Lb_Write_Click(object sender, EventArgs e)
        {
            Response.Redirect("sender.aspx");
        }

        protected void Lb_Manager_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectMemberData.aspx");
        }
    }
}