using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Bulletin : System.Web.UI.Page
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "新增公告";
                   
                }
            }
        }
        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                UserInfo tmpUserInfo = null;
                if (Session["userinfo"] is UserInfo)
                {
                    tmpUserInfo = (UserInfo)Session["userinfo"];

                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"Insert Into  Bulletin(BTitle,Context,Dp,Date,EID)VALUES(@BTitle,@Context,@Dp,@Date,@EID)");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@BTitle", Txt_Title.Text);
                    cmd.Parameters.AddWithValue("@Context", txt_Connect.Text);
                    cmd.Parameters.AddWithValue("@Dp", tmpUserInfo.Department);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                    cmd.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('已成功新增公告!');location.href='editbullitin.aspx';</script>");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("editbullitin.aspx");
        }
    }
}