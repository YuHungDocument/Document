using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ChangeAgent : System.Web.UI.Page
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "代理人變更";
                }
            }
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(@"Select agent From UserInfo  Where EID=@EID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EID", DropDownList2.Text);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        using (SqlConnection cnUpate = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cnUpate.Open();
                            SqlCommand cmd2 = new SqlCommand(@"Update UserInfo Set agent=@agent Where EID=@EID");
                            cmd2.Connection = cnUpate;
                            cmd2.Parameters.AddWithValue("@EID", DropDownList9.Text);
                            cmd2.Parameters.AddWithValue("@agent", DropDownList10.Text);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand(@"Update AgentInfo Set agent=@agent,AgentName=@AgentName,StartTime=@StartTime,EndTime=@EndTime,send=@send,receive=@receive Where EID=@EID");
                            cmd3.Connection = cnUpate;
                            cmd3.Parameters.AddWithValue("@EID", DropDownList9.Text);
                            cmd3.Parameters.AddWithValue("@agent", DropDownList10.Text);
                            cmd3.Parameters.AddWithValue("@AgentName", DropDownList8.Text);
                            string date1 = Request.Form["d3"];
                            string date2 = Request.Form["d4"];
                            cmd3.Parameters.AddWithValue("@StartTime", date1);
                            cmd3.Parameters.AddWithValue("@EndTime", date2);
                            if (CheckBox1.Checked)
                            {
                                cmd3.Parameters.AddWithValue("@send", 1);
                            }
                            else
                            {
                                cmd3.Parameters.AddWithValue("@send", 0);
                            }
                            if (CheckBox2.Checked)
                            {
                                cmd3.Parameters.AddWithValue("@receive", 1);
                            }
                            else
                            {
                                cmd3.Parameters.AddWithValue("@receive", 0);
                            }
                            cmd3.ExecuteNonQuery();
                            cnUpate.Close();
                            Response.Write("<script>alert('代理人變更成功!');location.href='SetAgent.aspx';</script>");
                        }
                    }
                    else
                    {
                        using (SqlConnection cnUpate = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cnUpate.Open();
                            SqlCommand cmd2 = new SqlCommand(@"Update UserInfo Set agent=@agent Where EID=@EID");
                            cmd2.Connection = cnUpate;
                            cmd2.Parameters.AddWithValue("@EID", DropDownList9.Text);
                            cmd2.Parameters.AddWithValue("@agent", DropDownList10.Text);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand(@"Insert Into AgentInfo (EID,agent,AgentName,StartTime,EndTime,send,receive) Values (@EID,@agent,@AgentName,@StartTime,@EndTime,@send,@receive)");
                            cmd3.Connection = cnUpate;
                            cmd3.Parameters.AddWithValue("@EID", DropDownList9.Text);
                            cmd3.Parameters.AddWithValue("@agent", DropDownList10.Text);
                            cmd3.Parameters.AddWithValue("@AgentName", DropDownList8.Text);
                            string date1 = Request.Form["d3"];
                            string date2 = Request.Form["d4"];
                            cmd3.Parameters.AddWithValue("@StartTime", date1);
                            cmd3.Parameters.AddWithValue("@EndTime", date2);
                            if (CheckBox1.Checked)
                            {
                                cmd3.Parameters.AddWithValue("@send", 1);
                            }
                            else
                            {
                                cmd3.Parameters.AddWithValue("@send", 0);
                            }
                            if (CheckBox2.Checked)
                            {
                                cmd3.Parameters.AddWithValue("@receive", 1);
                            }
                            else
                            {
                                cmd3.Parameters.AddWithValue("@receive", 0);
                            }
                            cmd3.ExecuteNonQuery();
                            Response.Write("<script>alert('代理人變更成功!');location.href='SetAgent.aspx';</script>");
                        }
                    }
                    }
                    }
        }

        protected void DropDownList2_DataBound(object sender, EventArgs e)
        {
            DropDownList9.DataBind();
        }

        protected void DropDownList8_DataBound(object sender, EventArgs e)
        {
            DropDownList10.DataBind();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetAgent.aspx");
        }
    }
}