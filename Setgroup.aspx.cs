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
    public partial class Setgroup : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bind();
            }
             ((Label)this.Master.FindControl("Lb_Title")).Text = "群組設定";

        }

        public void bind()
        {
            UserInfo tmpUserInfo = null;
            if (Session["userinfo"] is UserInfo)
            {
                tmpUserInfo = (UserInfo)Session["userinfo"];
                
            }
            string sqlstr = "Select * from Record Where EID='" + tmpUserInfo.EID + "' Order by ID desc";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            myda.Fill(myds, "Record");
            GridView1.DataSource = myds;
            GridView1.DataBind();
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
                Session["EditNID"] = keyId;
                Response.Redirect("NewsEditDetail.aspx");
            }
            if (e.CommandName == "DelData")
            {
                int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                string keyId = GridView1.DataKeys[index].Value.ToString();
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Delete From News Where NID=@NID");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@NID", keyId);
                    cmd.ExecuteNonQuery();
                    bind();
                }
            }
        }
    }
}