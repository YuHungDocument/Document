using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class ProductEditView : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                Lbl_Eorr.Visible = false;
                bind();
            }
        }

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
                        Txt_Name.Text = dr["ProductName"].ToString();
                        Txt_Price.Text = dr["ProductPrice"].ToString();
                        DropDownList1.SelectedValue = dr["ProductType"].ToString();
                        CKEditorControl1.Text = dr["ProductContext"].ToString();
                    }
                }
            }
            ImgView();
        }

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

        protected void Btn_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductEdit.aspx");
        }



        protected void Btn_UpLoad_Click(object sender, EventArgs e)
        {

        }
    }
}