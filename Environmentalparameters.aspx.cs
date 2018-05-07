using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Environmentalparameters : System.Web.UI.Page
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "系統參數設定";
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                    }
                    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand(@"Select ComName from ParameterSetting where ComNumber=1");
                        cmd.Connection = cn;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                Lbl_ComName.Text = dr["ComName"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(btn_change.Text== "變更公司名稱")
            {
                btn_change.Text = "確定更改";
                Txt_ComName.Visible = true;
            }
            else
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE ParameterSetting Set ComName=@ComName where ComNumber=1");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@ComName", Txt_ComName.Text);
                    cmd.ExecuteNonQuery();
                    Response.Redirect("Environmentalparameters.aspx");
                }
            }
            
        }
    }
}