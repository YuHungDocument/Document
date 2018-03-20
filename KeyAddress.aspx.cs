using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class KeyAddress : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        String EID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["userinfo"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "設定金鑰位置";
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserInfo tmpUserInfo;
            tmpUserInfo = (UserInfo)Session["userinfo"];
            EID = tmpUserInfo.EID;
            string address = FileUpload1.PostedFile.FileName;
            string strdr = root.Text;
            if (!string.IsNullOrWhiteSpace(root.Text))
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE UserInfo SET KeyAddress = @KeyAddress WHERE EID = @EID");

                    cmd.Connection = cn;
                    address = strdr+ ":\\" + address;
                    cmd.Parameters.AddWithValue("@KeyAddress", address);
                    cmd.Parameters.AddWithValue("@EID", EID);
                    cmd.ExecuteNonQuery();//執行命令
                    Response.Write("<script>alert('金鑰建立成功!');location.href='WaitDocument.aspx';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('請輸入根目錄!');location.href='KeyAddress.aspx';</script>");
            }
        }
    }
}