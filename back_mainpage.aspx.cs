using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Back_mainpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DbHelper tmpdbhelper = new DbHelper();
            UserInfo tmpUserInfo = null;
            if (Session["userinfo"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {

                ((Label)this.Master.FindControl("Lb_Title")).Text = "後台管理";
                tmpUserInfo = (UserInfo)Session["userinfo"];
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

                                if (tmpUserInfo.Permission == 5)
                                {
                                    Response.Redirect("Home.aspx");
                                }
                            }
                            else
                            {
                                if (tmpUserInfo.temp_Permission == 5)
                                {
                                    Response.Redirect("Home.aspx");
                                }
                            }
                        }
                        else
                        {
                            if (tmpUserInfo.Permission == 5)
                            {
                                Response.Redirect("Home.aspx");
                            }

                        }
                        cn.Close();


                    }
                }
            }
        }
    }
}