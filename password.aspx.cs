using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class password : System.Web.UI.Page
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "變更密碼";
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_ID.Text = "帳號："+tmpUserInfo.UserID;
                    }
                }
                
            }
        }

        protected void Btn_Next_Click(object sender, EventArgs e)
        {

            UserInfo tmpUserInfo = null;
            tmpUserInfo = (UserInfo)Session["userinfo"];
            Lbl_ID.Text = tmpUserInfo.UserID;
            if (tmpUserInfo.Pwd== Txt_Password.Text)
            {
                Response.Redirect("ChangePwd.aspx");
            }
            else
            {
                Label2.Visible = true;
            }

}
    }
}