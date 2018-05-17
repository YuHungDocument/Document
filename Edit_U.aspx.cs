using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Edit_U : System.Web.UI.Page
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

                    if (Session["userinfo"] is UserInfo)
                    {
                        ((Label)this.Master.FindControl("Lb_Title")).Text = "部門管理";
                    }
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            
            
                if (!string.IsNullOrWhiteSpace(TextBox2.Text))
                {


                    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn.Open();

                        SqlCommand cmd2 = new SqlCommand(@"Select TID , TN from TypeGroup where Tp = @Tp ORDER BY TID DESC ");
                        cmd2.Connection = cn;
                        cmd2.Parameters.AddWithValue("@Tp", DropDownList1.SelectedValue);

                        using (SqlDataReader dr = cmd2.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                TextBox3.Text = dr["TID"].ToString();
                                TextBox4.Text = dr["TN"].ToString();

                            }

                        }
                        SqlCommand cmd3 = new SqlCommand(@"select TN  from TypeGroup where TN = @TN and Tp=@Tp");
                        cmd3.Connection = cn;
                        cmd3.Parameters.AddWithValue("@TN", TextBox2.Text);
                        cmd3.Parameters.AddWithValue("@Tp", "Dp");

                    using (SqlDataReader dr = cmd3.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                Response.Write(" <script language=JavaScript> alert( '已存在 '); </script> ");

                            }
                            else
                            {
                                dr.Close();
                                string sqlstr = @"Insert Into TypeGroup( TID, TN, Tp ) Values ( @TID,@TN ,@Tp) ";


                                SqlCommand cmd = new SqlCommand(sqlstr, cn);
                                cmd.CommandText = sqlstr;
                                cmd.Parameters.AddWithValue("@TN", TextBox2.Text);
                                cmd.Parameters.AddWithValue("@Tp", DropDownList1.SelectedValue);
                                cmd.Parameters.AddWithValue("@TID", int.Parse(TextBox3.Text) + 1);

                                cmd.ExecuteNonQuery();//執行命令

                                Response.Write(" <script language=JavaScript> alert( '動作完成 '); </script> ");

                            }
                        }
                    }
                }
                else
                {
                    Response.Write(" <script language=JavaScript> alert( '請勿空白 '); </script> ");
                }
            }

        }
    }
