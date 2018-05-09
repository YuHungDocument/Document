using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Home1 : System.Web.UI.MasterPage
    {
        DbHelper tmpdbhelper = new DbHelper();

        public object ClientScript { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"Select ComName from ParameterSetting where ComNumber=1");
                    cmd.Connection = cn;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Label1.Text = dr["ComName"].ToString();
                        }
                    }
                }
                if (Session["userinfo"] == null)
                {
                    LinkButton.Visible = true;                    
                    lissy.Style.Add("display", "none");
                }
                else
                {
                    LinkButton.Visible = false;                    
                    UserInfo tmpUserInfo = null;
                    lissy.Style.Add("display", "block");
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_Name.Text ="歡迎!"+tmpUserInfo.Name;
                    }

                }
            }

        }

        protected void Lb_Logout_Click(object sender, EventArgs e)
        {
            Session["userinfo"] = null;
            Response.Redirect("Home.aspx");
        }

    }
}