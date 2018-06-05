using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class GuildPage : System.Web.UI.MasterPage
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userinfo"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"Select ComName from ParameterSetting where ComNumber=1");
                    cmd.Connection = cn;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Lbl_CompanyName.Text = dr["ComName"].ToString();
                        }
                    }
                }
                UserInfo tmpUserInfo = null;
                if (Session["userinfo"] is UserInfo)
                {
                    tmpUserInfo = (UserInfo)Session["userinfo"];
                    Lb_UserInfo.Text =tmpUserInfo.Department+"/"+tmpUserInfo.position+"/"+ tmpUserInfo.Name;
                    
                        //判斷代理期限是否失效
                        using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand(@"Select EndTime From AgentInfo Where EID=@EID");
                        cmd.Connection = cn;
                        cmd.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                DateTime strDate_EndTime = DateTime.Parse(dr["EndTime"].ToString());
                                DateTime Now = DateTime.Now;
                                if ((Now - strDate_EndTime).TotalSeconds < 0)
                                {

                                    using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                    {
                                        cn2.Open();
                                        SqlCommand cmd2 = new SqlCommand(@"Update UserInfo Set agent=@agent Where EID=@EID");
                                        cmd2.Connection = cn2;
                                        cmd2.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                                        cmd2.Parameters.AddWithValue("@agent", "");
                                        cmd2.ExecuteNonQuery();
                                        SqlCommand cmd3 = new SqlCommand(@"Delete from AgentInfo Where EID=@EID");
                                        cmd3.Connection = cn2;
                                        cmd3.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                                        cmd3.ExecuteNonQuery();
                                    }
                                }

                            }
                            cn.Close();
                        }


                    }
                    //判斷_給予權限_期限是否失效
                    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand(@"Select Date_End From Warrant Where R_EID=@R_EID and effective=@effective");
                        cmd.Connection = cn;
                        cmd.Parameters.AddWithValue("@R_EID", tmpUserInfo.EID);
                        cmd.Parameters.AddWithValue("@effective", 1);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {   //暫時權限
                            if (dr.Read())
                            {
                                DateTime strDate_EndTime = DateTime.Parse(dr["Date_End"].ToString());
                                DateTime Now = DateTime.Now;
                                if ((Now - strDate_EndTime).TotalSeconds < 0)
                                {

                                    using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                    {
                                        cn2.Open();
                                        SqlCommand cmd2 = new SqlCommand(@"Update UserInfo Set temp_Permission=@temp_Permission Where EID=@EID");
                                        cmd2.Connection = cn2;
                                        cmd2.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                                        cmd2.Parameters.AddWithValue("@temp_Permission", "");
                                        cmd2.ExecuteNonQuery();
                                        SqlCommand cmd3 = new SqlCommand(@"Update Warrant Set effective=@effective Where R_EID=@R_EID");
                                        cmd3.Connection = cn2;
                                        cmd3.Parameters.AddWithValue("@R_EID", tmpUserInfo.EID);
                                        cmd3.Parameters.AddWithValue("@effective", 0);
                                        cmd3.ExecuteNonQuery();
                                    }

                                    if (tmpUserInfo.Permission < 5)
                                    {
                                        LinkButton1.Visible = true;

                                    }
                                }
                                else
                                {
                                    if (tmpUserInfo.temp_Permission < 5)
                                    {
                                        LinkButton1.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                if (tmpUserInfo.Permission < 5)
                                {
                                    LinkButton1.Visible = true;
                                }

                            }
                            cn.Close();


                        }
                    }

                    //判斷該員工是否離職
                    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand(@"Select job From UserInfo Where EID=@EID");
                        cmd.Connection = cn;
                        cmd.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                if (dr["job"].ToString() == "1" )
                                {

                                }
                                else
                                {
                                    Session["userinfo"] = null;
                                    Response.Write("<script>alert('該員工已離職!');location.href='Home.aspx';</script>");
                                   
                                }
                            }
                        }
                        cn.Close();
                    }
                }
            }
        }

        #region 登出
        protected void Lb_Logout_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                UserInfo tmpUserInfo;
                tmpUserInfo = (UserInfo)Session["userinfo"];
                SqlCommand cmd2 = new SqlCommand(@"Update UserInfo Set KeyAddress=@KeyAddress Where EID=@EID");
                cmd2.Connection = cn;
                cn.Open();
                cmd2.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                cmd2.Parameters.AddWithValue("@KeyAddress", "");
                cmd2.ExecuteNonQuery();
                cn.Close();
            }
                Session["UserInfo"] = null;
            Response.Redirect("Home.aspx");

        }
        #endregion

        #region  各項連結
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectMemberData.aspx");
        }

        protected void Lb_Draft_Click(object sender, EventArgs e)
        {
            Response.Redirect("Draft.aspx");
        }

        protected void Lb_All_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllPage.aspx");
        }
        #endregion
    }

}