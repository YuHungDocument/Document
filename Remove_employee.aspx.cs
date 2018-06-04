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
    public partial class Remove_employee : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            UserInfo tmpUserInfo = null;

            if (!Page.IsPostBack)
            {
                if (Session["userinfo"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {

                    if (Session["userinfo"] is UserInfo)
                    {
                        ((Label)this.Master.FindControl("Lb_Title")).Text = "刪除已離職員工";

                        tmpUserInfo = (UserInfo)Session["userinfo"];

                        if (tmpUserInfo.Permission != 3)
                        {
                            Response.Redirect("back_mainpage.aspx");
                        }
                    }
                    
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand("Update UserInfo  set job =@job where EID=@EID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EID", DropDownList2.SelectedValue);
                cmd.Parameters.AddWithValue("@job", '0');
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            Response.Write("<script>alert('您已刪除該員工!');location.href='Home.aspx';</script>");
        }

        //private void GetData()
        //{
        //    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
        //    {
        //        cn.Open();

        //        SqlCommand cmd = new SqlCommand("Select EID,Name,Department,position where Department = @Dp and EID=@EID");
        //        cmd.Connection = cn;
        //        cmd.Parameters.AddWithValue("@Dp",DropDownList1.SelectedValue);
        //        cmd.Parameters.AddWithValue("@EID", DropDownList2.SelectedValue);

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {

        //        }

        //    }
        //}


    }
}