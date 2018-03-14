using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace WebApplication1
{
    public partial class UserPage1 : System.Web.UI.Page
    {
        
        DbHelper tmpdbhelper = new DbHelper();
        string key;
        string RSAkey;
        string AESiv;
        string KeyAddress;
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
                    
                    ((LinkButton)this.Master.FindControl("Lb_Wait")).BackColor = Color.Gray;
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_EID.Text = tmpUserInfo.EID;
                        bind();
                    }
                    
                }
            }
        }





        public void bind()
        {
            string sqlstr = @"select SID from Detail where EID='" + Lbl_EID.Text + "' and look=1 union Select SID from Fil Where EID='"+ Lbl_EID.Text + "' order by SID desc";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "Fil");
            Menu.DataSource = myds;
            Menu.DataBind();
            sqlcon.Close();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("OnMouseover", "this.style.background='#E1FFE1'");
                e.Row.Attributes.Add("OnMouseout", "this.style.background='#FFFFFF'");
                LinkButton LB = (LinkButton)e.Row.FindControl("LinkButton1");
                Label Date = (Label)e.Row.FindControl("Lbl_Date");
                Label DeadLine = (Label)e.Row.FindControl("Lbl_DeadLine");
                Label Type = (Label)e.Row.FindControl("Lbl_Type");
                Label Dep = (Label)e.Row.FindControl("Lbl_Dep");
                Label Peo = (Label)e.Row.FindControl("Lbl_Peo");
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Select * from Fil Where SID=@SID");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@SID", LB.Text);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            LB.ToolTip = "公文文號：" + dr["SID"].ToString();
                            LB.Text = dr["Title"].ToString();
                            DateTime strDate = DateTime.Parse(dr["Date"].ToString());                            
                            Date.Text = String.Format("{0:yyyy/MM/dd}", strDate);
                            DateTime strDeadLine = DateTime.Parse(dr["DeadLine"].ToString());
                            DeadLine.Text = String.Format("{0:yyyy/MM/dd}", strDeadLine);
                            if(DateTime.Compare(DateTime.Parse( dr["DeadLine"].ToString()), DateTime.Now) < 0)
                             {
                                e.Row.Attributes.Add("OnMouseout", "this.style.background='#F9B7C2'");
                                e.Row.Attributes.Add("style", "background-color:#F9B7C2");
                            }
                            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                cn2.Open();
                                SqlCommand cmd2 = new SqlCommand("Select * from UserInfo Where EID=@EID");
                                cmd2.Connection = cn2;
                                cmd2.Parameters.AddWithValue("@EID", dr["EID"].ToString());
                                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                {
                                    if (dr2.Read())
                                    {
                                        Dep.Text = dr2["Department"].ToString();
                                        Peo.Text = dr2["Name"].ToString();
                                    }
                                }
                            }
                            Type.Text = dr["Type"].ToString();

                        }
                    }
                }
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
                string keyId = Menu.DataKeys[index].Value.ToString();

                Session["keyId"] = keyId;
                Response.Redirect("DocumentDetail.aspx");
            }
        }

        protected void Btn_Select_Click(object sender, EventArgs e)
        {
            string date1 = Request.Form["d1"];
            string date2 = Request.Form["d2"];
            string date3 = Request.Form["d3"];
            string date4 = Request.Form["d4"];
            string searchingstr = "select SID from Fil Where EID=@EID";
            string wherestr = null;
            string SID = Txt_SID.Text;
            string Type = Ddl_Type.SelectedValue;
            string Title = Txt_Title.Text;
            DataSet ds = new DataSet();
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = tmpdbhelper.DB_CnStr;
            scn.Open();
            SqlCommand scmd = new SqlCommand();
            scmd.Connection = scn;
            if(!string.IsNullOrWhiteSpace(Request.Form["d1"]))
            {
                if(!string.IsNullOrWhiteSpace(Request.Form["d2"]))
                {
                    wherestr = " and Date Between'" + date1 + "'AND'" + date2 + "'";
                }
                else
                {
                    wherestr = " and Date >='"+ date1 + "'";
                }
            }
            else if(!string.IsNullOrWhiteSpace(Request.Form["d2"]))
            {
                wherestr = " and Date <='" + date2 + "'";
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["d3"]))
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["d4"]))
                {
                    wherestr = wherestr+ " and DeadLine Between'" + date3 + "'AND'" + date4 + "'";
                }
                else
                {
                    wherestr = wherestr + " and DeadLine >='" + date3 + "'";
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.Form["d4"]))
            {
                wherestr = wherestr + " and DeadLine <='" + date4 + "'";
            }
            if(!string.IsNullOrWhiteSpace(Txt_SID.Text))
            {
                wherestr = wherestr + " and ( SID Like'%" + SID + "%')";
            }
            if (Type != "--請選擇公文種類--")
            {
                wherestr = wherestr + " and (Type='" + Type + "')";
            }

            if(!string.IsNullOrWhiteSpace(Txt_Title.Text))
            {
                wherestr = wherestr + " and (Title Like'%" + Title + "%')";
            }

            scmd.CommandText = searchingstr + wherestr+" order by SID desc";
            scmd.Parameters.AddWithValue("@EID", Lbl_EID.Text);
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(ds, "Fil");
            Menu.DataSource = ds;
            Menu.DataBind();

        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }
    }
}