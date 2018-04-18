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
        }
        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From Bulletin Where BID=@BID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@BID",Session["BID"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        Lbl_Title.Text = dr["BTitle"].ToString();
                        Lbl_Dep.Text = dr["Department"].ToString();
                        Lbl_Date.Text = dr["Date"].ToString();
                        Lbl_Context.Text = dr["Context"].ToString();
                    }
                }
            }
        }
    }
}