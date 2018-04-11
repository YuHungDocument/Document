using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class HomeLogin : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
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
                    Lbl_Name.Text = tmpUserInfo.Name;
                    Lbl_Eid.Text = tmpUserInfo.EID;
                    Lbl_DpAndPos.Text = tmpUserInfo.Department + "/"+tmpUserInfo.position;
                        }
                    }
                }
    }
        }
