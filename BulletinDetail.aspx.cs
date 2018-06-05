using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class BulletinDetail : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bind();
            }
             ((Label)this.Master.FindControl("Lb_Title")).Text = "公告內文";

        }
        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From Bulletin Where BID=@BID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@BID", Session["BID"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        Lbl_Title.Text = dr["BTitle"].ToString();
                        DateTime date = Convert.ToDateTime(dr["Date"].ToString());
                        Lbl_Date.Text = date.ToString("yyyy-MM-dd");
                        Lbl_Context.Text = dr["Context"].ToString();

                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            Response.Redirect("editbullitin.aspx");
        }
    }
}