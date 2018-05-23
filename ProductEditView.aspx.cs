using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

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
                        Txt_Name.Text = dr["ProductName"].ToString();
                        Txt_Price.Text = dr["ProductPrice"].ToString();
                        DropDownList1.SelectedValue = dr["ProductType"].ToString();
                        CKEditorControl1.Text = dr["ProductContext"].ToString();
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

        #region 返回
        protected void Btn_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductEdit.aspx");
        }
        #endregion

        #region 更新資料
        protected void Btn_UpLoad_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(filename);
            int fileSize = postedFile.ContentLength;
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                
                if (FileUpload1.HasFile)
                {
                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif"
                     || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp")
                    {
                        Stream stream = postedFile.InputStream;
                        BinaryReader binaryReader = new BinaryReader(stream);
                        Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                        SqlCommand cmd = new SqlCommand("Update Product set ProductName=@ProductName,ProductType=@ProductType,ProductPrice=@ProductPrice,ProductContext=@ProductContext,ProductImg=@ProductImg Where PID=@PID");
                        cmd.Parameters.AddWithValue("@ProductName", Txt_Name.Text);
                        cmd.Parameters.AddWithValue("@ProductType", DropDownList1.SelectedValue);
                        cmd.Parameters.AddWithValue("@ProductPrice", Txt_Price.Text);
                        cmd.Parameters.AddWithValue("@ProductContext", CKEditorControl1.Text);
                        cmd.Parameters.AddWithValue("@ProductImg", bytes);
                        cmd.Parameters.AddWithValue("@PID", Session["PID"].ToString());
                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Update Product set ProductName=@ProductName,ProductType=@ProductType,ProductPrice=@ProductPrice,ProductContext=@ProductContext Where PID=@PID");
                    cmd.Parameters.AddWithValue("@ProductName", Txt_Name.Text);
                    cmd.Parameters.AddWithValue("@ProductType", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProductPrice", Txt_Price.Text);
                    cmd.Parameters.AddWithValue("@ProductContext", CKEditorControl1.Text);
                    cmd.Parameters.AddWithValue("@PID", Session["PID"].ToString());
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                    
                }

                Response.Write("<script>alert('修改產品成功!');location.href='ProductEdit.aspx';</script>");
            }

        }
        #endregion

        protected void Btn_Delete_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Delete From Product Where PID=@PID");
                cmd.Parameters.AddWithValue("@PID",Session["PID"].ToString());
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                Response.Write("<script>alert('成功刪除該產品!');location.href='ProductEdit.aspx';</script>");
            }
        }
    }
}