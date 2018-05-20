using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = FileUpload1.PostedFile.FileName;

            string type = name.Substring(name.LastIndexOf(".") + 1);

            FileStream fs = File.OpenRead(name);

            byte[] content = new byte[fs.Length];

            fs.Read(content, 0, content.Length);

            fs.Close();



            SqlConnection conn = new SqlConnection(tmpdbhelper.DB_CnStr);

            SqlCommand cmd = conn.CreateCommand();

            conn.Open();

            cmd.CommandText = "insert into Product(ProductImg) values (@ProductImg)";

            cmd.CommandType = CommandType.Text;



            if (type == "jpg" || type == "gif" || type == "bmp" || type == "png")

            {

                SqlParameter para = cmd.Parameters.Add("@ProductImg", SqlDbType.Image);

                para.Value = content;

                cmd.ExecuteNonQuery();

            }
        }
    }
}