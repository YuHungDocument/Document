using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bind();
            }
        }

        #region 讀資料
        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From Product Where PID=@PID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PID", Session["PID"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_Price.Text = "建議售價：" + dr["ProductPrice"].ToString();
                        Lbl_Type.Text = "車款：" + dr["ProductType"].ToString();
                        Lbl_Name.Text = dr["ProductName"].ToString();
                        Lbl_NamePath.Text = dr["ProductName"].ToString();
                        Lbl_Context.Text = dr["ProductContext"].ToString();
                    }
                }
            }
            ImgView();
        }
        #endregion

        #region 顯示圖片
        public void ImgView()
        {
            using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ProductImg from Product Where PID=@PID");
                cmd.Connection = con;


                cmd.Parameters.AddWithValue("@PID", Session["PID"].ToString());



                byte[] bytes = (byte[])cmd.ExecuteScalar();
                string strBase64 = Convert.ToBase64String(bytes);
                Image1.ImageUrl = "data:Image/png;base64," + strBase64;
            }
        }
        #endregion
    }
}