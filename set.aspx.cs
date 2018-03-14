using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class set : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        string UserID;
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "帳戶設定";
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        UserID = tmpUserInfo.UserID;
                    }
                    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand(@"Select ChangePwdTime From UserInfo Where UserID=@UserID");
                        cmd.Connection = cn;
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                if (dr["ChangePwdTime"].ToString() == "")
                                {
                                    Lbl_ChangePwdTime.Text += "尚無密碼變更紀錄";
                                }
                                else
                                {
                                    DateTime strDate = DateTime.Parse(dr["ChangePwdTime"].ToString());
                                    Lbl_ChangePwdTime.Text += String.Format("{0:yyyy/MM/dd}", strDate); ;
                                }
                            }
                        }

                    }

                }
            }
        }
    }
}