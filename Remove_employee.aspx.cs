using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

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
                          
                            Response.Write("<script>alert('非人資部門無法使用!');location.href='back_mainpage.aspx';</script>");
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

       
        public void bind2()
        {
            
                    string sqlstr = "Select EID, Name, Department, position from UserInfo where Department ='"+DropDownList1.SelectedValue+"' AND EID = '"+DropDownList2.SelectedValue+"' ";

                SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
                SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            //cmd.Parameters.AddWithValue("@Dp", DropDownList1.SelectedValue);
            //cmd.Parameters.AddWithValue("@EID", DropDownList2.SelectedValue);
                DataSet myds = new DataSet();
                sqlcon.Open();  
                SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
               
                
                myda.Fill(myds, "UserInfo");

                show.DataSource = myds;
                show.DataBind();
                sqlcon.Close();

                
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            bind2();
        }
    }
}