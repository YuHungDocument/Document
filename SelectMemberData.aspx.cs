using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ManagerPage : System.Web.UI.Page
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
                    UserInfo tmpUserInfo;
                    if (Session["userinfo"] is UserInfo)
                    {
                        ((Label)this.Master.FindControl("Lb_Title")).Text = "後台管理";

                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        TB_Inquire.Text = "" + tmpUserInfo.Department + "";
                        if (tmpUserInfo.Permission == 3)
                        {
                            //bind3();

                        }
                        else if (tmpUserInfo.Permission == 4)
                        {
                            bind4();
                        }
                        else if (tmpUserInfo.Permission == 2)
                        {

                        }
                        else if (tmpUserInfo.Permission == 1)
                        {
                            //bind3();
                           
                        }
                    }

                }
            }
        }
        public void bind3()
        {

            string sqlstr = @"select Eid,Name,Department,position,Gender,Address,Email,Tel,Cel,Birthday from UserInfo ";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            //cmd.Parameters.AddWithValue("@position", TextBox1.Text);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "UserInfo");
            Menu.DataSource = myds;
            Menu.DataBind();
            sqlcon.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TB_Inquire.Text))
            {
                string sqlstr = @"select Eid,Name,Department,position,Gender,Address,Email,Tel,Cel,Birthday from UserInfo where position = '" + TB_Inquire.Text + "' OR Department = '" + TB_Inquire.Text + "' OR  EID = '" + TB_Inquire.Text + "' OR  Name = '" + TB_Inquire.Text + "'";
                SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
                SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
                //cmd.Parameters.AddWithValue("@position", TextBox1.Text);
                DataSet myds = new DataSet();
                sqlcon.Open();
                SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

                myda.Fill(myds, "UserInfo");
                Menu.DataSource = myds;
                Menu.DataBind();
                sqlcon.Close();
            }
            else
            {
                string sqlstr = @"select Eid,Name,Department,position,Gender,Address,Email,Tel,Cel,Birthday from UserInfo ";

                SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
                SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
                //cmd.Parameters.AddWithValue("@position", TextBox1.Text);
                DataSet myds = new DataSet();
                sqlcon.Open();
                SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

                myda.Fill(myds, "UserInfo");
                Menu.DataSource = myds;
                Menu.DataBind();
                sqlcon.Close();
            }
        }

        public void bind4()
        {
            TB_Inquire.ReadOnly = true;
            Button1.Visible = false;
            string sqlstr = @"select Eid,Name,Department,position,Gender,Address,Email,Tel,Cel,Birthday from UserInfo where  Department = '" + TB_Inquire.Text + "' ";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            //cmd.Parameters.AddWithValue("@position", TextBox1.Text);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "UserInfo");
            Menu.DataSource = myds;
            Menu.DataBind();
            sqlcon.Close();
        }

        public void bind1()
        {
            string sqlstr = @"select TN from TypeGroup where Tp = '" + DropDownList1.SelectedValue + "' ";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
           
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "UserInfo");
            TG.DataSource = myds;
            TG.DataBind();
            sqlcon.Close();
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
                    SqlCommand cmd3 = new SqlCommand(@"select TN  from TypeGroup where TN = @TN");
                    cmd3.Connection = cn;
                    cmd3.Parameters.AddWithValue("@TN", TextBox2.Text);
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
                            bind1();
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