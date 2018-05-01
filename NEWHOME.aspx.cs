using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class NEWHOME : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["userinfo"] == null)
                {
                    Pel_Login.Visible = true;
                    Pel_UserInfo.Visible = false;
                }
                else
                {
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_Name.Text = tmpUserInfo.Name;
                        Lbl_Eid.Text = tmpUserInfo.EID;
                        Lbl_DpAndPos.Text = tmpUserInfo.Department + "/" + tmpUserInfo.position;
                    }
                    Pel_Login.Visible = false;
                    Pel_UserInfo.Visible = true;
                }
            }
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(@"Select EID,Name,Department,position,Gender,UserID,Pwd,Email,Tel,Cel,Birthday,Address,Permission,temp_Permission
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

                        if (!string.IsNullOrWhiteSpace(dr["temp_Permission"].ToString()))
                        {
                            tmpuserinfo.temp_Permission = int.Parse(dr["temp_Permission"].ToString());
                        }
                        else
                        {
                            tmpuserinfo.Permission = int.Parse(dr["Permission"].ToString());
                        }


                        Session["userinfo"] = tmpuserinfo;
                        Response.Redirect("Home.aspx");
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton LB = (LinkButton)e.Row.FindControl("Lb_Title");
                e.Row.Attributes.Add("OnMouseover", "this.style.fontWeight='900';");
                e.Row.Attributes.Add("OnMouseout", "this.style.fontWeight='normal';");
            }
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
                Session["BID"] = keyId;
                Response.Redirect("BulletinDetail.aspx");
            }
        }
    }
}