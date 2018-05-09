using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class setInfo : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)//get
            {
                bind();
                if (Session["userinfo"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                   
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "修改個資";
                }
               
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserName.Text)
              && !string.IsNullOrWhiteSpace(Email.Text)
              && !string.IsNullOrWhiteSpace(Tel.Text)
              && !string.IsNullOrWhiteSpace(Cel.Text)
              )
            {
                string tmpSQL = @"UpDate UserInfo Set Name=@Name,Email=@Email,Tel=@Tel,Cel=@Cel where UserID=@UserID";//建立SQL語法修改輸入的資料
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))//建立連線
                {
                    cn.Open();//開啟
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = tmpSQL;
                    //把物件的值抓進參數裡面↓
                    cmd.Parameters.AddWithValue("@UserID", LbId.Text);
                    cmd.Parameters.AddWithValue("@Name", UserName.Text);
                    cmd.Parameters.AddWithValue("@Email", Email.Text);
                    cmd.Parameters.AddWithValue("@Tel", Tel.Text);
                    cmd.Parameters.AddWithValue("@Cel", Cel.Text);
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                }
                using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))//建立連線
                {
                    cn2.Open();
                    SqlCommand cmd2 = new SqlCommand(@"Select EID,Name,UserID,Pwd,Email,Tel,Cel,Birthday
                                                   From UserInfo Where UserID=@UserID");
                    cmd2.Connection = cn2;
                    cmd2.Parameters.AddWithValue("@UserID", LbId.Text);
                    using (SqlDataReader dr = cmd2.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            UserInfo tmpuserinfo = new UserInfo();
                            tmpuserinfo.EID = dr["EID"].ToString();
                            tmpuserinfo.Name = dr["Name"].ToString();
                            tmpuserinfo.UserID = dr["UserID"].ToString();
                            tmpuserinfo.Pwd = dr["Pwd"].ToString();
                            tmpuserinfo.Tel = dr["Tel"].ToString();
                            tmpuserinfo.Email = dr["Email"].ToString();
                            tmpuserinfo.Birthday = dr["Birthday"].ToString();
                            tmpuserinfo.Cel = dr["Cel"].ToString();

                            Session["userinfo"] = tmpuserinfo;
                           
                        }
                        Response.Write("<script>alert('資料修改成功!');location.href='setinfo.aspx';</script>");
                    }
                }
            }
        }
        public void bind()
        {

            if (Session["UserInfo"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                UserInfo tmpUserInfo = null;
                if (Session["userinfo"] is UserInfo)
                {
                    tmpUserInfo = (UserInfo)Session["userinfo"];
                    LbId.Text = tmpUserInfo.UserID;
                    UserName.Text = tmpUserInfo.Name;
                    Email.Text = tmpUserInfo.Email;
                    Tel.Text = tmpUserInfo.Tel;
                    Cel.Text = tmpUserInfo.Cel;
                }
            }



        }
    }
}