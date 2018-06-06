using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class editbullitinDetail : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bind();
            }
        }
        protected void Btn_Edit_Click(object sender, EventArgs e)
        { UserInfo tmpUserInfo = null;
            if (Session["userinfo"] is UserInfo)
            {
                tmpUserInfo = (UserInfo)Session["userinfo"];

                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update Bulletin Set BTitle=@BTitle,Dp=@Dp,Context=@Context,EID=@EID Where BID=@BID");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@BTitle", Txt_Title.Text);
                    cmd.Parameters.AddWithValue("@Dp", tmpUserInfo.Department);
                    cmd.Parameters.AddWithValue("@Context", Txt_Context.Text);
                    cmd.Parameters.AddWithValue("@BID", Session["EditBID"].ToString());
                    cmd.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('修改成功');location.href='editbullitin.aspx';</script>");
                }
            }
        }
        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From Bulletin Where BID=@BID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@BID", Session["EditBID"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Txt_Title.Text = dr["BTitle"].ToString();
                        Txt_Context.Text = dr["Context"].ToString();
                    }
                }
            }
        }
    }
}