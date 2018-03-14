using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Home : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(@"Select EID,Name,Department,position,Gender,UserID,Pwd,Email,Tel,Cel,Birthday,Address,Permission
                                                  From UserInfo Where UserID=@UserID And Pwd=@Pwd");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@UserID", Txt_ID.Text);
                cmd.Parameters.AddWithValue("@Pwd", Txt_Password.Text);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        UserInfo tmpuserinfo = new UserInfo();
                        tmpuserinfo.EID = dr["EID"].ToString();
                        tmpuserinfo.Name = dr["Name"].ToString();
                        tmpuserinfo.Department = dr["Department"].ToString();
                        tmpuserinfo.position = dr["position"].ToString();
                        tmpuserinfo.Gender = dr["Gender"].ToString();
                        tmpuserinfo.UserID = dr["UserID"].ToString();
                        tmpuserinfo.Pwd = dr["Pwd"].ToString();
                        tmpuserinfo.Tel = dr["Tel"].ToString();
                        tmpuserinfo.Email = dr["Email"].ToString();
                        tmpuserinfo.Birthday = dr["Birthday"].ToString();
                        tmpuserinfo.Cel = dr["Cel"].ToString();
                        tmpuserinfo.Address = dr["Address"].ToString();
                        tmpuserinfo.Permission = int.Parse(dr["Permission"].ToString());


                        Session["userinfo"] = tmpuserinfo;
                        Response.Redirect("WaitDocument.aspx");
                    }
                    else
                        Label2.Visible = true;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}