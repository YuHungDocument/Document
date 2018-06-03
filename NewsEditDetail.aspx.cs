using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class NewsEditDetail : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bind();
            }
        }

        protected void Btn_Edit_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Update News Set NTitle=@NTitle,NType=@NType,Context=@Context Where NID=@NID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@NTitle",Txt_Title.Text);
                cmd.Parameters.AddWithValue("@NType", Ddl_Type.SelectedValue);
                cmd.Parameters.AddWithValue("@Context",Txt_Context.Text);
                cmd.Parameters.AddWithValue("@NID", Session["EditNID"].ToString());
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('修改成功');location.href='NewsEdit.aspx';</script>");
            }
        }
        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From News Where NID=@NID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@NID",Session["EditNID"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        Txt_Title.Text = dr["NTitle"].ToString();
                        Ddl_Type.SelectedValue = dr["NType"].ToString();
                        Txt_Context.Text = dr["Context"].ToString();
                    }
                }
            }
        }

        protected void Btn_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsEdit.aspx");
        }
    }
}