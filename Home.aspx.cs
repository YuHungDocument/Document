using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class Home : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bind();
                ImgView1();
                ImgView2();
                ImgView3();
                bind2();
            }
        }

        public void bind()
        {
            string sqlstr = "select top 5 NID,Date,NTitle from News order by NID desc";
            //string sqlstr = "select top 10 BID,CONCAT(Department, '　', BTitle) as bull,CONVERT(varchar(10) , Date, 111 ) as ConDate from Bulletin";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            myda.Fill(myds, "New");
            GridView1.DataSource = myds;
            GridView1.DataBind();
            sqlcon.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton LB = (LinkButton)e.Row.FindControl("Lb_Title");
                e.Row.Attributes.Add("OnMouseover", "this.style.background='#E1FFE1'");
                e.Row.Attributes.Add("OnMouseout", "this.style.background='#FFFFFF'");
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
                string keyId = GridView1.DataKeys[index].Value.ToString();
                Session["NID"] = keyId;
                Response.Redirect("NewsDetail.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("News.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select top 1 * from (select top 2 * from Product order by PID desc) as t order by PID");
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Session["PID"] = dr["PID"].ToString();
                    }
                }
            }
            Response.Redirect("ProductDetail.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select Top 1 * from Product order by PID desc");
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Session["PID"] = dr["PID"].ToString();
                    }
                }
            }
            Response.Redirect("ProductDetail.aspx");
        }

        #region 顯示產品資料bind2()
        public void bind2()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select Top 1 * from Product order by PID desc");
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        Lbl_Img1.Text = dr["ProductName"].ToString();
                        Lbl_Price1.Text = dr["ProductPrice"].ToString();
                    }
                }
                SqlCommand cmd2 = new SqlCommand("select top 1 * from (select top 2 * from Product order by PID desc) as t order by PID");
                cmd2.Connection = cn;
                using (SqlDataReader dr = cmd2.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_Img2.Text = dr["ProductName"].ToString();
                        Lbl_Price2.Text = dr["ProductPrice"].ToString();
                    }
                }
                SqlCommand cmd3 = new SqlCommand("select top 1 * from (select top 3 * from Product order by PID desc) as t order by PID");
                cmd3.Connection = cn;
                using (SqlDataReader dr = cmd3.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_Img3.Text = dr["ProductName"].ToString();
                        Lbl_Price3.Text = dr["ProductPrice"].ToString();
                    }
                }
            }
        }
        #endregion


        #region 顯示圖片1
        public void ImgView1()
        {
            using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Top 1 ProductImg from Product order by PID desc");
                cmd.Connection = con;

                byte[] bytes = (byte[])cmd.ExecuteScalar();
                string strBase64 = Convert.ToBase64String(bytes);
                Image1.ImageUrl = "data:Image/png;base64," + strBase64;
            }
        }
        #endregion

        #region 顯示圖片2
        public void ImgView2()
        {
            using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select top 1 ProductImg from (select top 2 * from Product order by PID desc) as t order by PID");
                cmd.Connection = con;

                byte[] bytes = (byte[])cmd.ExecuteScalar();
                string strBase64 = Convert.ToBase64String(bytes);
                Image2.ImageUrl = "data:Image/png;base64," + strBase64;
            }
        }
        #endregion

        #region 顯示圖片3
        public void ImgView3()
        {
            using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select top 1 ProductImg from (select top 3 * from Product order by PID desc) as t order by PID");
                cmd.Connection = con;
                
                byte[] bytes = (byte[])cmd.ExecuteScalar();
                string strBase64 = Convert.ToBase64String(bytes);
                Image3.ImageUrl = "data:Image/png;base64," + strBase64;
            }
        }
        #endregion

        protected void Button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select top 1 * from (select top 3 * from Product order by PID desc) as t order by PID");
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Session["PID"] = dr["PID"].ToString();
                    }
                }
            }
            Response.Redirect("ProductDetail.aspx");
        }

        protected void Lbl_More_Click(object sender, EventArgs e)
        {
            Response.Redirect("Product.aspx");
        }
    }
}