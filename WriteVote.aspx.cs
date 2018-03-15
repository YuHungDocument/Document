using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;


namespace WebApplication1
{
    public partial class WriteVote : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        string txtKey;
        string txtIV;
        string txt_Ciphertext_Text;
        string txt_Ciphertext_Proposition;
        string txt_Ciphertext_DocumentContent;
        string txt_PKmessage;
        private SqlConnection connection;
        private SqlCommand command;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserInfo tmpUserInfo = null;
                bind2();
                if (Session["userinfo"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ((LinkButton)this.Master.FindControl("Lb_Write")).BackColor = Color.White;
                    ((LinkButton)this.Master.FindControl("Lb_Write")).ForeColor = Color.Black;
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "撰寫公文";
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_EID.Text = tmpUserInfo.EID;
                        Lbl_SID.Text = DateTime.Now.ToString("yyyyMMddhhmmss");
                        String Date = DateTime.Today.Year.ToString();
                        if (DateTime.Today.Month < 10)
                            Date += "0" + DateTime.Today.Month.ToString();
                        else Date += DateTime.Today.Month.ToString();
                        if (DateTime.Today.Day < 10)
                            Date += "0" + DateTime.Today.Day.ToString();
                        else
                            Date += DateTime.Today.Day.ToString();
                        Lbl_Date.Text = Date;
                        SqlCommand cmd = new SqlCommand(@"Select Name From UserInfo Where EID=@EID");

                        using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn.Open();
                            SqlCommand cmd2 = new SqlCommand("Insert Into Preview(SID) Values(@SID)");
                            cmd2.Connection = cn;
                            cmd2.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                            cmd2.ExecuteNonQuery();
                            bind3();
                            cmd.Connection = cn;
                            cmd.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Lbl_Sender.Text = dr["Name"].ToString();
                                }

                            }
                            cn.Close();
                        }
                    }
                }
            }
        }

        public string AESEncryption(string Key, string IV, string PlainText)
        {
            byte[] byte_Key = Encoding.UTF8.GetBytes(Key);
            byte[] byte_IV = Encoding.UTF8.GetBytes(IV);
            MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
            byte[] byte_KeyMD5 = provider_MD5.ComputeHash(byte_Key);
            byte[] byte_IVMD5 = provider_MD5.ComputeHash(byte_IV);
            using (Aes aesAlg = Aes.Create())
            {
                //加密金鑰(32 Byte)
                aesAlg.Key = byte_KeyMD5;
                //初始向量(Initial Vector, iv) 
                aesAlg.IV = byte_IVMD5;
                //加密器
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                //執行加密
                byte[] cryptTextBytes = Encoding.Unicode.GetBytes(PlainText);
                byte[] cryptedText = encryptor.TransformFinalBlock(cryptTextBytes, 0, cryptTextBytes.Length);
                return Convert.ToBase64String(cryptedText);
            }
        }
        public string AESEncryption(string Key, string IV, byte[] PlainText)
        {
            byte[] byte_Key = Encoding.UTF8.GetBytes(Key);
            byte[] byte_IV = Encoding.UTF8.GetBytes(IV);
            MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
            byte[] byte_KeyMD5 = provider_MD5.ComputeHash(byte_Key);
            byte[] byte_IVMD5 = provider_MD5.ComputeHash(byte_IV);
            using (Aes aesAlg = Aes.Create())
            {
                //加密金鑰(32 Byte)
                aesAlg.Key = byte_KeyMD5;
                //初始向量(Initial Vector, iv) 
                aesAlg.IV = byte_IVMD5;
                //加密器
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                //執行加密
                byte[] cryptedText = encryptor.TransformFinalBlock(PlainText, 0, PlainText.Length);
                return Convert.ToBase64String(cryptedText);
            }
        }
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox curTextBox = (TextBox)sender;
            int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            string UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox3")).Text.Trim();
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn2.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name,status=@status Where ID=@ID");
                cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox2")).Text);
                cmd2.Parameters.AddWithValue("@Department", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox4")).Text);
                cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox3")).Text);
                cmd2.Parameters.AddWithValue("@Name", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox5")).Text);
                cmd2.Parameters.AddWithValue("@status", ((DropDownList)GridView2.Rows[gvRowIndex].FindControl("Ddl_status")).SelectedValue);
                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn2;
                cmd2.ExecuteNonQuery();
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox curTextBox = (TextBox)sender;
            int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            string UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox3")).Text.Trim();
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * from UserInfo Where EID=@EID");
                cmd.Parameters.AddWithValue("@EID", UserEID);
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox4")).Text = dr["Department"].ToString();
                        ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox5")).Text = dr["Name"].ToString();
                        using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn2.Open();
                            SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name,status=@status Where ID=@ID");
                            cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox2")).Text);
                            cmd2.Parameters.AddWithValue("@Department", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox4")).Text);
                            cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox3")).Text);
                            cmd2.Parameters.AddWithValue("@Name", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox5")).Text);
                            cmd2.Parameters.AddWithValue("@status", ((DropDownList)GridView2.Rows[gvRowIndex].FindControl("Ddl_status")).SelectedValue);
                            cmd2.Parameters.AddWithValue("@ID", ID);
                            cmd2.Connection = cn2;
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox curTextBox = (TextBox)sender;
            int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            string UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox3")).Text.Trim();
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn2.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name,status=@status Where ID=@ID");
                cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox2")).Text);
                cmd2.Parameters.AddWithValue("@Department", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox4")).Text);
                cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox3")).Text);
                cmd2.Parameters.AddWithValue("@Name", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("TextBox5")).Text);
                cmd2.Parameters.AddWithValue("@status", ((DropDownList)GridView2.Rows[gvRowIndex].FindControl("Ddl_status")).SelectedValue);
                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn2;
                cmd2.ExecuteNonQuery();
            }
        }



        protected void Btn_Insert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)GridView4.Rows[i].FindControl("CheckBox1");
                if (cb.Checked == true)
                {
                    Lbl_GpName.Text = ((LinkButton)GridView4.Rows[i].FindControl("LblName")).Text.Trim();
                    bind();
                }
            }
        }

        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * from UseGroup Where GpName='" + Lbl_GpName.Text + "'");

                cmd.Connection = cn;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    Session["i"] = 0;

                    while (dr.Read())
                    {
                        using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn2.Open();
                            SqlCommand cmd2 = new SqlCommand("Insert Into Preview(SID,Lvl,Department,Name,EID,status) Values(@SID,@Lvl,@Department,@Name,@EID,@status)");
                            cmd2.Connection = cn2;

                            cmd2.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                            cmd2.Parameters.AddWithValue("@Lvl", dr["Lvl"].ToString());
                            cmd2.Parameters.AddWithValue("@Department", dr["Department"].ToString());
                            cmd2.Parameters.AddWithValue("@Name", dr["Name"].ToString());
                            cmd2.Parameters.AddWithValue("@EID", dr["EID"].ToString());
                            cmd2.Parameters.AddWithValue("@status", dr["status"].ToString());
                            cmd2.ExecuteNonQuery();

                            bind3();
                        }
                        using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn3.Open();
                            SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "'");
                            cmd3.Connection = cn3;
                            using (SqlDataReader dr2 = cmd3.ExecuteReader())
                            {
                                Session["i"] = 0;
                                while (dr2.Read())
                                {

                                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox2")).Text = dr2["Lvl"].ToString();
                                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox3")).Text = dr2["EID"].ToString();
                                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox4")).Text = dr2["Department"].ToString();
                                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox5")).Text = dr2["Name"].ToString();
                                    ((DropDownList)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Ddl_status")).Text = dr2["status"].ToString();
                                    Session["i"] = int.Parse(Session["i"].ToString()) + 1;
                                }
                            }

                        }
                    }
                }
            }
        }

        public void bind2()
        {
            string sqlstr = "select * from Record ";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            myda.Fill(myds, "Record");


            GridView4.DataSource = myds;
            GridView4.DataKeyNames = new string[] { "GID" };//主键
            GridView4.DataBind();
            sqlcon.Close();
        }

        public void bind3()
        {
            string sqlstr = "select * from Preview Where SID='" + Lbl_SID.Text + "'";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "Preview");

            GridView2.DataSource = myds;
            GridView2.DataBind();
            sqlcon.Close();


        }

        public void bind4()
        {
            string sqlstr = "select * from Vote Where VID='" + Session["VID"].ToString() + "'";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "Vote");

            GridView5.DataSource = myds;
            GridView5.DataBind();
            sqlcon.Close();


        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                int FNO_INDEX;
                if (Session["FNOSession"] == null)
                {
                    FNO_INDEX = 1;
                }
                else
                {
                    int FNOSession;
                    FNOSession = int.Parse(Session["FNOSession"].ToString());
                    FNO_INDEX = FNOSession;
                }

                if (fu_upload.FileName == "")
                {
                    Response.Write(" <script language=JavaScript> alert( ' 尚未選擇檔案 '); </script> ");
                }
                else
                {
                    FileInfo fi = new FileInfo(fu_upload.FileName);
                    byte[] DocumentContent = fu_upload.FileBytes;

                    string name = fi.Name;
                    string extn = fi.Extension;

                    SqlCommand cmd = new SqlCommand("SaveDoc", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

                    cmd.Parameters.Add("@DocumentContent", SqlDbType.VarBinary).Value = DocumentContent;
                    cmd.Parameters.Add("@Extn", SqlDbType.VarChar).Value = extn;
                    cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                    cmd.Parameters.AddWithValue("@FNO", FNO_INDEX);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    FNO_INDEX += 1;
                    Session["FNOSession"] = FNO_INDEX;
                    FillData();
                }
            }
        }
        private void FillData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                SqlCommand cmd = new SqlCommand("select Name,FNO from tempDocument where SID=@SID", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);
                con.Close();
            }
            if (dt.Rows.Count > 0)
            {
                gv_showTempFile.DataSource = dt;
                gv_showTempFile.DataBind();
            }
            else
            {
                gv_showTempFile.DataSource = dt;
                gv_showTempFile.DataBind();
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Insert Into Preview(SID) Values(@SID)");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                cmd.ExecuteNonQuery();
                bind3();
                using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn3.Open();
                    SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "'");
                    cmd3.Connection = cn3;
                    using (SqlDataReader dr2 = cmd3.ExecuteReader())
                    {
                        Session["i"] = 0;
                        while (dr2.Read())
                        {

                            ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox2")).Text = dr2["Lvl"].ToString();
                            ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox3")).Text = dr2["EID"].ToString();
                            ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox4")).Text = dr2["Department"].ToString();
                            ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox5")).Text = dr2["Name"].ToString();
                            ((DropDownList)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Ddl_status")).Text = dr2["status"].ToString();
                            Session["i"] = int.Parse(Session["i"].ToString()) + 1;
                        }
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into Preview(SID) Values(@SID)");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                    cmd.ExecuteNonQuery();
                    bind3();
                }
            }
            using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn3.Open();
                SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "'");
                cmd3.Connection = cn3;
                using (SqlDataReader dr2 = cmd3.ExecuteReader())
                {
                    Session["i"] = 0;
                    while (dr2.Read())
                    {

                        ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox2")).Text = dr2["Lvl"].ToString();
                        ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox3")).Text = dr2["EID"].ToString();
                        ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox4")).Text = dr2["Department"].ToString();
                        ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("TextBox5")).Text = dr2["Name"].ToString();
                        ((DropDownList)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Ddl_status")).Text = dr2["status"].ToString();
                        Session["i"] = int.Parse(Session["i"].ToString()) + 1;
                    }
                }
            }
        }

        protected void Btn_Newgroup_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                string ttSQL = "Select * From UseGroup Where GpName='" + TextBox1.Text + "'";
                cn.Open();
                SqlCommand cmd3 = new SqlCommand(ttSQL);
                cmd3.Connection = cn;
                using (SqlDataReader dr = cmd3.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Response.Write("<Script language='Javascript'>");
                        Response.Write("alert('此群組名稱已存在請重新輸入')");
                        Response.Write("</" + "Script>");

                    }
                    else
                    {
                        cn.Close();

                        cn.Open();
                        if (!string.IsNullOrWhiteSpace(TextBox1.Text))
                        {
                            string GID = DateTime.Now.ToString("yyyyMMddhhmmss");
                            SqlCommand cmd4 = new SqlCommand(@"Insert Into Record(GpName,GID)Values(@GpName,@GID)");
                            cmd4.Connection = cn;
                            cmd4.Parameters.AddWithValue("@GpName", TextBox1.Text);
                            cmd4.Parameters.AddWithValue("@GID", GID);
                            cmd4.ExecuteNonQuery();


                            for (int i = 0; i < GridView2.Rows.Count - 1; i++)
                            {

                                string Lvl = ((TextBox)GridView2.Rows[i].FindControl("TextBox2")).Text.Trim();
                                string EID = ((TextBox)GridView2.Rows[i].FindControl("TextBox3")).Text.Trim();
                                string Department = ((TextBox)GridView2.Rows[i].FindControl("TextBox4")).Text.Trim();
                                string Name = ((TextBox)GridView2.Rows[i].FindControl("TextBox5")).Text.Trim();
                                string status = ((DropDownList)GridView2.Rows[i].FindControl("Ddl_status")).Text.Trim();

                                if (!string.IsNullOrWhiteSpace(TextBox1.Text)
                                    && Lvl != ""
                                    && Department != ""
                                    && Department != "Dp1"
                                    && Name != ""
                                    && status != "")
                                {
                                    //寫回資料庫

                                    SqlCommand cmd = new SqlCommand(@"Insert INTO UseGroup(ID,GpName,GID,Lvl,EID,Department,Name,status)VALUES(@ID,@GpName,@GID,@Lvl,@EID,@Department,@Name,@status)");

                                    cmd.Connection = cn;
                                    cmd.Parameters.AddWithValue("@GpName", TextBox1.Text);
                                    cmd.Parameters.AddWithValue("@Lvl", Lvl);
                                    cmd.Parameters.AddWithValue("@EID", EID);
                                    cmd.Parameters.AddWithValue("@ID", i + 1);
                                    cmd.Parameters.AddWithValue("@Department", Department);
                                    cmd.Parameters.AddWithValue("@Name", Name);
                                    cmd.Parameters.AddWithValue("@status", status);
                                    cmd.Parameters.AddWithValue("@GID", GID);
                                    cmd.ExecuteNonQuery();


                                }
                            }
                            TextBox1.Text = "";
                            //Response.Write("<Script language='Javascript'>");
                            //Response.Write("alert('儲存成功，可到選擇群組查看!')");
                            //Response.Write("</" + "Script>");
                            bind2();
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('儲存成功，可到選擇群組查看!')", true);


                            cn.Close();
                        }
                        else
                        {
                            //Response.Write("<Script language='Javascript'>");
                            //Response.Write("alert('儲存失敗，請確認資料是否完善')");
                            //Response.Write("</" + "Script>");
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('儲存失敗，請確認資料是否完善')", true);

                        }
                    }
                }

            }
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            i = int.Parse(GridView4.SelectedIndex.ToString());
            string GpName = ((LinkButton)GridView4.Rows[i].FindControl("LblName")).Text.Trim();
            Lbl_GpName.Text = GpName;

            bind();
        }
        protected void Btn_editgroup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBox1.Text))
            {

                string tmpsql2 = "Update Record set GpName=@GpName where GID=@GID";
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Select * From UseGroup where GpName=@GpName");
                    cmd.Parameters.AddWithValue("@GpName", Lbl_GpName.Text);
                    cmd.Connection = cn;
                    cn.Close();
                    cn.Open();
                    Boolean bn = false;
                    string GID = null;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            bn = true;
                            GID = dr["GID"].ToString();

                        }

                    }
                    if (bn == true)
                    {

                        for (int i = 0; i < GridView2.Rows.Count - 1; i++)
                        {
                            SqlCommand cmd4 = new SqlCommand("Select * From UseGroup where GID=@GID");
                            string tmpsql = "Update UseGroup set GpName=@GpName,Lvl=@Lvl,EID=@EID,Name=@Name,Department=@Department,status=@status where ID=@ID And GID=@GID ";
                            string Lvl = ((TextBox)GridView2.Rows[i].FindControl("TextBox2")).Text.Trim();
                            string EID = ((TextBox)GridView2.Rows[i].FindControl("TextBox3")).Text.Trim();
                            string Department = ((TextBox)GridView2.Rows[i].FindControl("TextBox4")).Text.Trim();
                            string Name = ((TextBox)GridView2.Rows[i].FindControl("TextBox5")).Text.Trim();
                            string status = ((DropDownList)GridView2.Rows[i].FindControl("Ddl_status")).Text.Trim();


                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandText = tmpsql;
                            cmd2.Parameters.AddWithValue("@ID", i + 1);
                            cmd2.Parameters.AddWithValue("@GID", GID);
                            cmd2.Parameters.AddWithValue("@GpName", TextBox1.Text);
                            cmd2.Parameters.AddWithValue("@Lvl", Lvl);
                            cmd2.Parameters.AddWithValue("@EID", EID);
                            cmd2.Parameters.AddWithValue("@Name", Name);
                            cmd2.Parameters.AddWithValue("@Department", Department);
                            cmd2.Parameters.AddWithValue("@status", status);
                            cmd2.Connection = cn;
                            cmd2.ExecuteNonQuery();

                            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('修改成功')", true);
                        }

                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.CommandText = tmpsql2;
                        cmd3.Parameters.AddWithValue("@GID", GID);
                        cmd3.Parameters.AddWithValue("@GpName", TextBox1.Text);
                        cmd3.Connection = cn;
                        cmd3.ExecuteNonQuery();
                        bind2();
                    }


                }
            }
            else
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Select * From UseGroup where GpName=@GpName");
                    cmd.Parameters.AddWithValue("@GpName", Lbl_GpName.Text);
                    cmd.Connection = cn;
                    cn.Close();
                    cn.Open();
                    Boolean bn = false;
                    string GID = null;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            bn = true;
                            GID = dr["GID"].ToString();

                        }

                    }
                    if (bn == true)
                    {

                        for (int i = 0; i < GridView2.Rows.Count - 1; i++)
                        {
                            SqlCommand cmd4 = new SqlCommand("Select * From UseGroup where GID=@GID");
                            string tmpsql = "Update UseGroup set Lvl=@Lvl,EID=@EID,Name=@Name,Department=@Department,status=@status where ID=@ID And GID=@GID ";
                            string Lvl = ((TextBox)GridView2.Rows[i].FindControl("TextBox2")).Text.Trim();
                            string EID = ((TextBox)GridView2.Rows[i].FindControl("TextBox3")).Text.Trim();
                            string Department = ((TextBox)GridView2.Rows[i].FindControl("TextBox4")).Text.Trim();
                            string Name = ((TextBox)GridView2.Rows[i].FindControl("TextBox5")).Text.Trim();
                            string status = ((DropDownList)GridView2.Rows[i].FindControl("Ddl_status")).Text.Trim();


                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandText = tmpsql;
                            cmd2.Parameters.AddWithValue("@ID", i + 1);
                            cmd2.Parameters.AddWithValue("@GID", GID);
                            cmd2.Parameters.AddWithValue("@Lvl", Lvl);
                            cmd2.Parameters.AddWithValue("@EID", EID);
                            cmd2.Parameters.AddWithValue("@Name", Name);
                            cmd2.Parameters.AddWithValue("@Department", Department);
                            cmd2.Parameters.AddWithValue("@status", status);
                            cmd2.Connection = cn;
                            cmd2.ExecuteNonQuery();

                            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('修改成功')", true);
                        }

                    }


                }
            }
        }

        protected void OpenDoc(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow gr = (GridViewRow)lnk.NamingContainer;

            int FNO = int.Parse(gv_showTempFile.DataKeys[gr.RowIndex].Value.ToString());
            Download(FNO);
        }
        private void Download(int FNO)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                SqlCommand cmd = new SqlCommand("select Name,FNO,DocumentContent,Extn from tempDocument where FNO = @FNO and SID = @SID", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FNO", SqlDbType.Int).Value = FNO;
                cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);
            }
            string name = dt.Rows[0]["Name"].ToString();
            byte[] documentBytes = (byte[])dt.Rows[0]["DocumentContent"];

            Response.ClearContent();
            Response.ContentType = "application/octetstream";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", name));
            Response.AddHeader("Content-Length", documentBytes.Length.ToString());

            Response.BinaryWrite(documentBytes);
            Response.Flush();
            Response.Close();
        }


        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            connection = new SqlConnection(tmpdbhelper.DB_CnStr);
            command = new SqlCommand();
            command.Connection = connection;
            string strName;

            strName = ((LinkButton)gv_showTempFile.Rows[e.RowIndex].Cells[0].
                FindControl("btn_filename")).Text;

            /* 刪除資料 */
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Name", strName);
            command.Parameters.AddWithValue("@SID", Lbl_SID.Text);
            command.CommandText =
                "DELETE FROM tempDocument WHERE Name=@Name and SID=@SID ";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            int FNO_INDEX;
            int FNOSession;
            FNOSession = int.Parse(Session["FNOSession"].ToString());
            FNO_INDEX = FNOSession - 1;
            Session["FNOSession"] = FNO_INDEX;
            FillData();
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //宣告DropDownList


            DropDownList Ddl_sign;

            string sql1;

            DataView dv1;

            //要特別注意一下這邊，如果不用這個if包起來的話，RowDataBound會跑Header，Footer，Pager
            //我們的DropDownList是放在DataRow裡，所以只有在這邊才會找到DropDownList控制項
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //用FindControl(你的DropDownList的ID)，來找我們的DropDownList，記得要轉型喔!


                Ddl_sign = (DropDownList)e.Row.FindControl("Ddl_status");

                sql1 = "select * from TypeGroup where Tp=@Tp ";

                string Dp = Ddl_sign.SelectedValue;

                dv1 = GetDV3(sql1);

                //DropDownList要顯示的內容


                Ddl_sign.DataTextField = "TN";
                //DropDownList顯示內容對應的值


                Ddl_sign.DataValueField = "TID";
                //繫結DropDownList


                Ddl_sign.DataSource = dv1;


                Ddl_sign.DataBind();
            }
        }
        private DataView GetDV3(string sql)
        {

            SqlConnection sqlCon = new SqlConnection(tmpdbhelper.DB_CnStr);
            DataView dv;
            SqlDataAdapter sqlAdp = new SqlDataAdapter();
            SqlCommand cmd;
            DataSet ds = new DataSet();
            sqlCon.Open();
            cmd = new SqlCommand(sql, sqlCon);
            sqlAdp.SelectCommand = cmd;
            cmd.Parameters.AddWithValue("@Tp", "FS");
            sqlAdp.Fill(ds);
            dv = new DataView(ds.Tables[0]);
            return dv;
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            string SID = Lbl_SID.Text;
            if (Ddp_Type.SelectedValue == "投票")
            {
                using (SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    sqlcon.Open();
                    for (int i = 0; i < GridView5.Rows.Count; i++)
                    {
                        string Vname = ((TextBox)GridView5.Rows[i].FindControl("Txt_content")).Text.Trim();
                        string number = ((Label)GridView5.Rows[i].FindControl("Label1")).Text.Trim();
                        if (Vname != "")
                        {
                            SqlCommand sqlcmd = new SqlCommand("Update Vote Set Vname=@Vname where SID=@SID and number=@number");
                            sqlcmd.Connection = sqlcon;
                            sqlcmd.Parameters.AddWithValue("@SID", SID);
                            sqlcmd.Parameters.AddWithValue("@number", number);
                            sqlcmd.Parameters.AddWithValue("@Vname", Vname);
                            sqlcmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            if (Ddp_Type.SelectedValue != "FT0"
                && Ddl_Speed.SelectedValue != "--請選擇公文速別--"
                && !string.IsNullOrWhiteSpace(Txt_Title.Text)
                && !string.IsNullOrWhiteSpace(Txt_Text.Text)
                )
            {
                using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    //SqlCommand cmd4 = new SqlCommand(@"update Fil set Fil.Name=Document.Name,Fil.DocumentContent=Document.DocumentContent,Fil.Extn=Document.Extn  from Document join Fil on Fil.SID=Document.SID");
                    SqlCommand cmd3 = new SqlCommand(@"Insert INTO Fil(SID,EID,Date,Speed,Text,Title,Proposition,Type,YOS,AESkey,AESiv)VALUES(@SID,@EID,@Date,@Speed,@Text,@Title,@Proposition,@Type,@YOS,@AESkey,@AESiv)");
                    cn2.Open();
                    cmd3.Connection = cn2;
                    //cmd4.Connection = cn2;
                    //建立一個 AES 演算法
                    SymmetricAlgorithm symAlgorithm = new AesCryptoServiceProvider();
                    txtKey = Convert.ToBase64String(symAlgorithm.Key);     //hFYPyIK3uSQ=
                    txtIV = Convert.ToBase64String(symAlgorithm.IV);       //oeZlJhiaZB0=
                                                                           //對稱加密
                    txt_Ciphertext_Text = AESEncryption(txtKey, txtIV, Txt_Text.Text);
                    txt_Ciphertext_Proposition = AESEncryption(txtKey, txtIV, txt_Proposition.Text);
                    cmd3.Parameters.AddWithValue("@SID", SID);
                    cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                    cmd3.Parameters.AddWithValue("@Date", Lbl_Date.Text);
                    cmd3.Parameters.AddWithValue("@Speed", Ddl_Speed.SelectedValue);
                    cmd3.Parameters.AddWithValue("@Text", txt_Ciphertext_Text);
                    cmd3.Parameters.AddWithValue("@Title", Txt_Title.Text);
                    cmd3.Parameters.AddWithValue("@Proposition", txt_Ciphertext_Proposition);
                    cmd3.Parameters.AddWithValue("@Type", Ddp_Type.SelectedValue);
                    cmd3.Parameters.AddWithValue("@YOS", Ddp_YOS.SelectedValue);
                    cmd3.Parameters.AddWithValue("@AESkey", txtKey);
                    cmd3.Parameters.AddWithValue("@AESiv", txtIV);
                    cmd3.ExecuteNonQuery();
                    //cmd4.ExecuteNonQuery();
                }
                using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {


                        string Lvl = ((TextBox)GridView2.Rows[i].FindControl("TextBox2")).Text.Trim();
                        string EID = ((TextBox)GridView2.Rows[i].FindControl("TextBox3")).Text.Trim();
                        string Department = ((TextBox)GridView2.Rows[i].FindControl("TextBox4")).Text.Trim();
                        string Name = ((TextBox)GridView2.Rows[i].FindControl("TextBox5")).Text.Trim();
                        string status = ((DropDownList)GridView2.Rows[i].FindControl("Ddl_status")).Text.Trim();

                        if (SID != "" && Lvl != "" && Department != "" && Name != "" && status != "")
                        {
                            //找尋接收者PK並加密KEY
                            SqlCommand cmduserInfo = new SqlCommand(@"select UserInfo.PK from UserInfo LEFT JOIN Detail ON UserInfo.EID=Detail.EID where (UserInfo.EID=@EID)");
                            cn3.Open();
                            cmduserInfo.Connection = cn3;
                            cmduserInfo.Parameters.AddWithValue("@EID", EID);
                            using (SqlDataReader dr = cmduserInfo.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    using (SqlConnection cn4 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                    {
                                        SqlCommand cmdfindfile = new SqlCommand(@"select DocumentContent from tempDocument Where SID=@SID");

                                        cn4.Open();
                                        cmdfindfile.Connection = cn4;
                                        cmdfindfile.Parameters.AddWithValue("@SID", SID);

                                        using (SqlDataReader dr2 = cmdfindfile.ExecuteReader())
                                        {
                                            if (dr2.Read())
                                            {
                                                byte[] DocumentContent = (byte[])dr2["DocumentContent"];
                                                txt_Ciphertext_DocumentContent = AESEncryption(txtKey, txtIV, DocumentContent);
                                            }
                                        }
                                        cn4.Close();
                                    }
                                    string PK = dr["PK"].ToString();
                                    //以接收者PK加密KEY
                                    // 建立 RSA 演算法物件的執行個體，並匯入先前建立的公鑰
                                    RSACryptoServiceProvider rsaProviderReceiver = new RSACryptoServiceProvider();
                                    rsaProviderReceiver.FromXmlString(PK);

                                    // 將資料加密
                                    byte[] bytePlain = Encoding.UTF8.GetBytes(txtKey);
                                    byte[] byteCipher = rsaProviderReceiver.Encrypt(bytePlain, false);

                                    // 將加密後的資料，轉 Base64 格式輸入
                                    txt_PKmessage = Convert.ToBase64String(byteCipher);
                                }
                            }
                            cn3.Close();

                            using (SqlConnection cnsavefile = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                cn3.Open();


                                SqlCommand cmdsavefile = new SqlCommand(@"Insert INTO Document(FNO,Name,DocumentContent,Extn,SID)SELECT FNO,Name,@DocumentContent,Extn,SID FROM tempDocument Where SID=@SID ");
                                cnsavefile.Open();
                                SqlCommand cmddeletefile = new SqlCommand(@"DELETE FROM tempDocument WHERE SID=@SID");
                                SqlCommand cmd2 = new SqlCommand(@"Delete From Preview Where SID=@SID");
                                cmdsavefile.Connection = cnsavefile;
                                cmddeletefile.Connection = cnsavefile;
                                cmd2.Connection = cnsavefile;
                                cmdsavefile.Parameters.AddWithValue("@SID", SID);
                                if (txt_Ciphertext_DocumentContent == null)
                                {
                                    txt_Ciphertext_DocumentContent = "";
                                }
                                cmdsavefile.Parameters.AddWithValue("@DocumentContent", txt_Ciphertext_DocumentContent);
                                cmdsavefile.ExecuteNonQuery();
                                cmddeletefile.Parameters.AddWithValue("@SID", SID);
                                cmddeletefile.ExecuteNonQuery();
                                cmd2.Parameters.AddWithValue("@SID", SID);
                                cmd2.ExecuteNonQuery();
                                cn3.Close();
                            }
                            //寫回資料庫                        
                            SqlCommand cmd = new SqlCommand(@"Insert INTO Detail(SID,Lvl,EID,Department,status,sign,look,RSAkey)VALUES(@SID,@Lvl,@EID,@Department,@status,@sign,@look,@RSAkey)");
                            cn3.Open();
                            cmd.Connection = cn3;
                            cmd.Parameters.AddWithValue("@SID", SID);
                            cmd.Parameters.AddWithValue("@Lvl", Lvl);
                            cmd.Parameters.AddWithValue("@EID", EID);
                            cmd.Parameters.AddWithValue("@Department", Department);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@RSAkey", txt_PKmessage);

                            if (Lvl == "1")
                            {
                                cmd.Parameters.AddWithValue("@look", 1);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@look", 0);
                            }
                            cmd.Parameters.AddWithValue("@sign", 0);
                            cmd.ExecuteNonQuery();
                            cn3.Close();

                        }

                    }
                    Response.Redirect("sender.aspx");
                }
            }
            else
            {
                Lbl_Eorr.Visible = true;
            }


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["number"] = int.Parse(Session["number"].ToString()) + 1;
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Insert Into Vote(SID,VID,number) Values(@SID,@VID,@number)");
                cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                cmd.Parameters.AddWithValue("@VID", Session["VID"].ToString());
                cmd.Parameters.AddWithValue("@number", Session["number"].ToString());
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                bind4();
            }
        }
    }
}