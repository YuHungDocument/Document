using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class Home : System.Web.UI.Page
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
            string sqlstr = "select top 5 NID,Date,NTitle from News";
            //string sqlstr = "select top 10 BID,CONCAT(Department, '　', BTitle) as bull,CONVERT(varchar(10) , Date, 111 ) as ConDate from Bulletin";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            myda.Fill(myds, "New");
            GridView1.DataSource = myds;
            GridView1.DataBind();
            sqlcon.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton LB = (LinkButton)e.Row.FindControl("Lb_Title");
                e.Row.Attributes.Add("OnMouseover", "this.style.background='#E1FFE1'");
                e.Row.Attributes.Add("OnMouseout", "this.style.background='#FFFFFF'");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelData")
            {
                //這樣就可以讀到RowIndex
                int index = ((GridViewRow)
                ((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                //這樣就可以取得Keys值了
                string keyId = GridView1.DataKeys[index].Value.ToString();
                Session["NID"] = keyId;
                Response.Redirect("BulletinDetail.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("News.aspx");
        }
    }
}