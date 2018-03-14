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
                UserInfo tmpUserInfo = null;
                if (Session["userinfo"] is UserInfo)
                {
                    tmpUserInfo = (UserInfo)Session["userinfo"];
                    LinkButton2.Text = tmpUserInfo.Name;
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
                                        cmd2.Connection = cn;
                                        cmd2.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                                        cmd2.Parameters.AddWithValue("@agent", "");
                                        cmd2.ExecuteNonQuery();
                                        SqlCommand cmd3 = new SqlCommand(@"Delete *from AgentInfo Where EID=@EID");
                                        cmd3.Connection = cn2;
                                        cmd3.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                                        cmd3.ExecuteNonQuery();
                                    }
                                }

                            }
                            cn.Close();
                        }


                    }
                }
            }
        }

        protected void Lb_Logout_Click(object sender, EventArgs e)
        {
            Session["UserInfo"] = null;
            Response.Redirect("Home.aspx");
        }
    }
}