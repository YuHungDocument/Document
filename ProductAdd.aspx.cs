using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class ProductAdd : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {                
                Lbl_Eorr.Visible = false;
            }
        }

        //protected void Btn_UpLoad_Click(object sender, EventArgs e)
        //{
        //    HttpPostedFile postedFile = FileUpload1.PostedFile;
        //    string filename = Path.GetFileName(postedFile.FileName);
        //    string fileExtension = Path.GetExtension(filename);
        //    int fileSize = postedFile.ContentLength;

        //    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif"
        //        || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp")
        //    {
        //        Stream stream = postedFile.InputStream;
        //        BinaryReader binaryReader = new BinaryReader(stream);
        //        Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

        //        using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
        //        {
        //            SqlCommand cmd = new SqlCommand("UploadImagePre", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            SqlParameter paramImageData = new SqlParameter()
        //            {
        //                ParameterName = "@ProductImg",
        //                Value = bytes
        //            };
        //            cmd.Parameters.Add(paramImageData);

        //            SqlParameter paramNewId = new SqlParameter()
        //            {
        //                ParameterName = "@NewId",
        //                Value = -1,
        //                Direction = ParameterDirection.Output
                       
        //        };                    
        //            cmd.Parameters.Add(paramNewId);
                    
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();

        //            lblMessage.Visible = true;
        //            lblMessage.ForeColor = System.Drawing.Color.Green;
        //            lblMessage.Text = "成功上傳圖檔";

        //            bind();
        //        }
        //    }
        //}

        public void ImgView()
        {
            using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ProductImg from ProductImgPreview Where PID=@PID");
                SqlCommand lastcmd = new SqlCommand("Select Max(PID) as PIDLast From ProductImgPreview");
                lastcmd.Connection = con;
                using (SqlDataReader dr = lastcmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        Session["ID"] = int.Parse(dr["PIDLast"].ToString());
                        cmd.Parameters.AddWithValue("@PID", Session["ID"]);
                        
                    }
                }
                    cmd.Connection = con;                



                byte[] bytes = (byte[])cmd.ExecuteScalar();
                string strBase64 = Convert.ToBase64String(bytes);
                Image1.ImageUrl = "data:Image/png;base64," + strBase64;
            }
        }

        protected void Btn_Insert_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue != "--請選擇車款--")
            {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(filename);
            int fileSize = postedFile.ContentLength;


                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    SqlCommand cmd = new SqlCommand("spUploadImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramName = new SqlParameter()
                    {
                        ParameterName = @"ProductName",
                        Value = Txt_Name.Text
                    };
                    cmd.Parameters.Add(paramName);

                    SqlParameter paramSize = new SqlParameter()
                    {
                        ParameterName = "@ProductType",
                        Value = DropDownList1.SelectedValue
                    };
                    cmd.Parameters.Add(paramSize);

                    SqlParameter paramPrice = new SqlParameter()
                    {
                        ParameterName = "@ProductPrice",
                        Value = Txt_Price.Text
                    };
                    cmd.Parameters.Add(paramPrice);

                    SqlParameter paramContext = new SqlParameter()
                    {
                        ParameterName = "@ProductContext",
                        Value = CKEditorControl1.Text
                    };
                    cmd.Parameters.Add(paramContext);

                    SqlParameter paramImageData = new SqlParameter()
                    {
                        ParameterName = "@ProductImg",
                        Value = bytes
                    };
                    cmd.Parameters.Add(paramImageData);

                    SqlParameter paramNewId = new SqlParameter()
                    {
                        ParameterName = "@NewId",
                        Value = -1,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramNewId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('新增產品成功!');location.href='ProductEdit.aspx';</script>");
                }

            }
            else
            {
                Lbl_Eorr.Visible = true;
                Lbl_Eorr.ForeColor = System.Drawing.Color.Red;
                Lbl_Eorr.Text = "資料輸入未完全";
            }
        }

        protected void Btn_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductEdit.aspx");
        }
    }
}