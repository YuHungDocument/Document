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
    public partial class News : System.Web.UI.Page
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
            string sqlstr = "Select * from News Order by NID desc";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            myda.Fill(myds, "News");
            GridView1.DataSource = myds;
            GridView1.DataBind();
        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (GridView1.PageIndex == 0)
            {
                LinkButton Lb_Frist = (LinkButton)pagerRow.FindControl("Lb_Frist");
                Lb_Frist.Visible = false;  //--目前位於第一頁，所以需隱形，看不見。無法繼續「上一頁」。
            }
        }
    }
}