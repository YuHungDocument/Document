using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Connect2 : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Lb_send_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                if (Dp_Title.SelectedIndex == 0 || Txt_Name.Text == "" || Txt_Tel.Text == "" || Txt_mail.Text == "" || Txt_message.Text == "")
                {
                    Response.Write(" <script language=JavaScript> alert( ' 尚有必填資料(*)未填，請再次確認! '); </script> ");
                }
                else
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"Insert Into  Connect(Main,Name,Company,Tel,mail,message)VALUES(@Main,@Name,@Company,@Tel,@mail,@message)");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@Main", Dp_Title.Text);
                    cmd.Parameters.AddWithValue("@Name", Txt_Name.Text);
                    cmd.Parameters.AddWithValue("@Company", Txt_Company.Text);
                    cmd.Parameters.AddWithValue("@Tel", Txt_Tel.Text);
                    cmd.Parameters.AddWithValue("@mail", Txt_mail.Text);
                    cmd.Parameters.AddWithValue("@message", Txt_message.Text);
                    cmd.ExecuteNonQuery();

                    Response.Write("<script>alert('已成功送出，我們將盡快回復您的問題!');location.href='Connect.aspx';</script>");
                }
            }
        }
    }
}