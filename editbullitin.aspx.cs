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
    public partial class editbullitin : System.Web.UI.Page
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "公告列表";

                }
                bind();

            }

        }
        public void bind()
        {
            string sqlstr = "select BID,Date,BTitle from Bulletin order by BID desc";
            //string sqlstr = "select top 10 BID,CONCAT(Department, '　', BTitle) as bull,CONVERT(varchar(10) , Date, 111 ) as ConDate from Bulletin";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            myda.Fill(myds, "Bulletin");
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
                Session["EditBID"] = keyId;
                Response.Redirect("editbullitinDetail.aspx");
            }
            if (e.CommandName == "DelData")
            {
                int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                string keyId = GridView1.DataKeys[index].Value.ToString();
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Bulletin Where BID=@BID");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@BID", keyId);
                    cmd.ExecuteNonQuery();
                    bind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bulletin.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("back_mainpage.aspx");
        }
 
    }
}
