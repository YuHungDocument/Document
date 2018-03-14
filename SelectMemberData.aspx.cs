using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ManagerPage : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
                        bind();
                    }

                }
            }
        }
        public void bind()
        {
            string sqlstr = @"select Eid,Name,Department,position,Gender,Address,Email,Tel,Cel,Birthday from UserInfo";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "UserInfo");
            Menu.DataSource = myds;
            Menu.DataBind();
            sqlcon.Close();
        }
    }
}