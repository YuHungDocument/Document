using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class AboutUs : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(@"Select TN from TypeGroup where  Tp=@Tp and TID=0");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Tp", "AU");
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_title1.Text= dr["TN"].ToString();
                    }
                }
                SqlCommand cmd1 = new SqlCommand(@"Select TN from TypeGroup where Tp=@Tp and TID=1");
                cmd1.Connection = cn;
                cmd1.Parameters.AddWithValue("@Tp", "AU");
                using (SqlDataReader dr = cmd1.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_context1.Text = dr["TN"].ToString();
                    }
                }
                SqlCommand cmd2 = new SqlCommand(@"Select TN from TypeGroup where Tp=@Tp and TID=2");
                cmd2.Connection = cn;
                cmd2.Parameters.AddWithValue("@Tp", "AU");
                using (SqlDataReader dr = cmd2.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_title2.Text = dr["TN"].ToString();
                    }
                }
                SqlCommand cmd3 = new SqlCommand(@"Select TN from TypeGroup where Tp=@Tp and TID=3");
                cmd3.Connection = cn;
                cmd3.Parameters.AddWithValue("@Tp", "AU");
                using (SqlDataReader dr = cmd3.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_context2.Text = dr["TN"].ToString();
                    }
                }
              
            }
        }
    }
}