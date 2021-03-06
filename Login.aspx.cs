﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //public void Select_signtime()
        //{
        //    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
        //    {
        //        UserInfo tmpuserinfo = new UserInfo();
        //        cn.Open();
        //        SqlCommand cmd0 = new SqlCommand(@"select SID from Fil where EID = @EID and Title = @Title");
        //        cmd0.Connection = cn;
        //        cmd0.Parameters.AddWithValue("@EID", tmpuserinfo.EID);
        //        cmd0.Parameters.AddWithValue("@Title", "授予權限同意函");

        //        using (SqlDataReader drsign = cmd0.ExecuteReader())
        //        {
        //            if (drsign.Read())
        //            {
        //                SqlCommand cmdsign = new SqlCommand(@"select Detail.signtime from Detail left JOIN Fil ON Fil.SID=Detail.SID where (Detail.SID=@SID AND Detail.EID !=@EID);");

        //                cmdsign.Connection = cn;
        //                cmdsign.Parameters.AddWithValue("@SID", drsign);
        //                cmdsign.Parameters.AddWithValue("@EID", tmpuserinfo.EID);

        //                using (SqlDataReader drsign1 = cmdsign.ExecuteReader())
        //                {
        //                    if (drsign1.Read())
        //                    {
                                
        //                    }
        //                }
        //            }
        //        }

        //    }
        //}

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            if(Txt_ID.Text=="123456"&& Txt_Password.Text == "123456")
            {
                Session["managerID"] = "123456";
                Response.Redirect("NewsEdit.aspx");
            }
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(@"Select EID,Name,Department,position,Gender,UserID,Pwd,Email,Tel,Cel,Birthday,Address,Permission,temp_Permission,job
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
                        tmpuserinfo.Address = dr["job"].ToString();
                        if (!string.IsNullOrWhiteSpace(dr["temp_Permission"].ToString()))
                        {
                            tmpuserinfo.temp_Permission = int.Parse(dr["temp_Permission"].ToString());
                        }
                        else
                        {
                            tmpuserinfo.Permission = int.Parse(dr["Permission"].ToString());
                        }




                        Session["userinfo"] = tmpuserinfo;
                        Response.Redirect("MainPage.aspx");
                    }
                    else
                        Label2.Visible = true;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forget.aspx");
        }
    }
}