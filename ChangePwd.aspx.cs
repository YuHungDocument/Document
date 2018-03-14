using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ChangePwd : System.Web.UI.Page
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "變更密碼";
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_ID.Text = tmpUserInfo.UserID;
                    }
                }
            }
        }

        protected void Btn_ChangePwd_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(@"Update UserInfo Set Pwd=@Pwd,ChangePwdTime=@ChangePwdTime Where UserID=@UserID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@UserID",Lbl_ID.Text);
                cmd.Parameters.AddWithValue("@Pwd", Txt_Pwd.Text);
                String Date = DateTime.Today.Year.ToString();
                //取得日期
                if (DateTime.Today.Month < 10)
                    Date += "0" + DateTime.Today.Month.ToString();
                else Date += DateTime.Today.Month.ToString();
                if (DateTime.Today.Day < 10)
                    Date += "0" + DateTime.Today.Day.ToString();
                else
                    Date += DateTime.Today.Day.ToString();
                //填入日期
                cmd.Parameters.AddWithValue("@ChangePwdTime", Date);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('密碼變更成功!');location.href='set.aspx';</script>");

            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("set.aspx");
        }
    }
}
