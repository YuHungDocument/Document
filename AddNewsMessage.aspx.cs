using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class AddNewsMessage : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(@"Insert Into  News(NType,NTitle,Context,Date)VALUES(@NType,@NTitle,@Context,@Date)");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@NType", Dp_Type.Text);
                cmd.Parameters.AddWithValue("@NTitle", Txt_Title.Text);
                cmd.Parameters.AddWithValue("@Context", txt_Connect.Text);
                cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('已成功新增訊息!');location.href='AddNews.aspx';</script>");
            }
        }
    }
}