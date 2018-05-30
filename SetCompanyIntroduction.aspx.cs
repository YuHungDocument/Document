using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class SetCompanyIntroduction : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["managerID"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"Select TN from TypeGroup where  Tp=@Tp and TID=0");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@Tp", "SCI");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txt_CI.Text = dr["TN"].ToString();
                        }
                    }
                }
            }
        }
        protected void Btn_Edit_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE TypeGroup SET TN=@TN  where  Tp=@Tp and TID=0");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Tp", "SCI");
                cmd.Parameters.AddWithValue("@TN", txt_CI.Text);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('修改成功!');location.href='SetCompanyIntroduction.aspx';</script>");
            }
        }
    }
}