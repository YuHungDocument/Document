using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bind();
            }
        }
        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From News Where NID=@NID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@NID", Session["NID"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_Title.Text = dr["NTitle"].ToString();
                        DateTime date = Convert.ToDateTime(dr["Date"].ToString());
                        Lbl_Date.Text = date.ToString("yyyy-MM-dd");
                        Lbl_Context.Text = dr["Context"].ToString();

                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("News.aspx");
        }
    }
}