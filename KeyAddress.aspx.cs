﻿using System;
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
                    ViewState["URL"] = Request.UrlReferrer.ToString();
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "金鑰設定";
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
                    Response.Write("<script>alert('修改成功!'); location.href='"+ViewState["URL"]+"'; </script>");

                }
            }
            else
            {
                Response.Write("<script>alert('請輸入根目錄!');location.href='KeyAddress.aspx';</script>");
            }
        }
    }
}