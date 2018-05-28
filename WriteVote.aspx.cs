﻿using System;
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
using System.Net.Mail;

namespace WebApplication1
{
    public partial class WriteVote : System.Web.UI.Page
    {
        #region 一連串宣告
        DbHelper tmpdbhelper = new DbHelper();
        string txtKey;
        string txtIV;
        string txt_Ciphertext_Text;
        string txt_Ciphertext_DocumentContent;
        string txt_Ciphertext_Proposition;
        string txt_PKmessage;
        string listmail;
        private SqlConnection connection;
        private SqlCommand command;
        string KeyAddress;
        string txt_RSAhash_Text;
        string txt_RSAhash_Proposition;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Btn_Save.Attributes["onclick"] = "this.disabled = true;this.value = '資料送出中..';" + Page.ClientScript.GetPostBackEventReference(Btn_Save, "");
                UserInfo tmpUserInfo = null;
                bind2();
                if (Session["userinfo"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    //設定還原頁
                    ViewState["URL"] = Request.UrlReferrer.ToString();
                    #region 內容
                    Session["number"] = 1;
                    Session["max"] = 1;
                    ((LinkButton)this.Master.FindControl("Lb_Write")).BackColor = Color.White;
                    ((LinkButton)this.Master.FindControl("Lb_Write")).ForeColor = Color.Black;
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "撰寫投票";
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_EID.Text = tmpUserInfo.EID;
                        Lbl_SID.Text = DateTime.Now.ToString("yyyyMMddhhmmss");
                        Lbl_Sender.Text = tmpUserInfo.Name;
                        String Date = DateTime.Today.Year.ToString();
                        if (DateTime.Today.Month < 10)
                            Date += "0" + DateTime.Today.Month.ToString();
                        else Date += DateTime.Today.Month.ToString();
                        if (DateTime.Today.Day < 10)
                            Date += "0" + DateTime.Today.Day.ToString();
                        else
                            Date += DateTime.Today.Day.ToString();
                        Lbl_Date.Text = Date;

                        #region 找出金鑰位址
                        using (SqlConnection cnkey = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cnkey.Open();
                            SqlCommand cmdfindkeyaddress = new SqlCommand(@"Select KeyAddress From UserInfo Where EID=@EID");
                            cmdfindkeyaddress.Connection = cnkey;
                            cmdfindkeyaddress.Parameters.AddWithValue("@EID", Lbl_EID.Text);

                            using (SqlDataReader dr2 = cmdfindkeyaddress.ExecuteReader())
                            {
                                if (dr2.Read())
                                {
                                    KeyAddress = dr2["KeyAddress"].ToString();
                                }
                                if (KeyAddress == "")
                                {
                                    Response.Redirect("KeyAddress.aspx");
                                }
                            }
                        }
                        #endregion

                        

                        using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn.Open();
                            SqlCommand cmd2 = new SqlCommand("Insert Into Preview(SID,EID) Values(@SID,@EID)");
                            cmd2.Connection = cn;
                            cmd2.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                            cmd2.Parameters.AddWithValue("@EID", "");
                            cmd2.ExecuteNonQuery();
                            bind3();
                            SqlCommand votecmd = new SqlCommand("Insert Into Vote(SID,number) Values(@SID,@number)");
                            votecmd.Connection = cn;
                            votecmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                            votecmd.Parameters.AddWithValue("@number", int.Parse(Session["number"].ToString()));
                            votecmd.ExecuteNonQuery();
                            bind4();
                            SqlCommand cmd = new SqlCommand(@"Insert Into Preview(SID,Department,EID,Name) Values(@SID,@Department,@EID,@Name)");
                            cmd.Connection = cn;
                            cmd.Parameters.AddWithValue("@Department", tmpUserInfo.Department);
                            cmd.Parameters.AddWithValue("@EID", tmpUserInfo.EID);
                            cmd.Parameters.AddWithValue("@Name", tmpUserInfo.Name);
                            cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                            cmd.ExecuteNonQuery();
                            bind5();
                            cn.Close();
                        }
                    }
                    #endregion
                }
            }
        }

        #region AES加密功能
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
        #endregion

        #region 改變Gridview的Lvl(層級)時做的Update
        protected void Txt_Lvl_TextChanged(object sender, EventArgs e)
        {
            TextBox curTextBox = (TextBox)sender;
            int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                CheckBox Cb_sign = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_sign"));
                CheckBox Cb_path = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_path"));
                cn2.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl Where ID=@ID");
                cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_Lvl")).Text);
                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn2;
                cmd2.ExecuteNonQuery();
            }
            string Lvl = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_Lvl")).Text.Trim();
            string EID = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_EID")).Text.Trim();
            if (Lvl != "" || EID != "")
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into Preview(SID,EID) Values(@SID,@EID)");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@EID", "");
                    cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                    cmd.ExecuteNonQuery();
                    bind3();
                    using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn3.Open();
                        SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "' and EID!='" + Lbl_EID.Text + "'");
                        cmd3.Connection = cn3;
                        using (SqlDataReader dr2 = cmd3.ExecuteReader())
                        {
                            int i = 0;
                            while (dr2.Read())
                            {

                                ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
                                ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
                                ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
                                ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
                                if (dr2["status"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = false;
                                }

                                if (dr2["path"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = false;
                                }
                                if (dr2["Comment"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = false;
                                }
                                i = int.Parse(i.ToString()) + 1;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Gridview的輸入EID或名字找到人
        protected void Txt_EID_TextChanged(object sender, EventArgs e)
        {
            TextBox curTextBox = (TextBox)sender;
            int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            string UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text.Trim();
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
                        ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text = dr["Department"].ToString();
                        ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text = dr["Name"].ToString();
                        using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            CheckBox Cb_sign = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_sign"));
                            CheckBox Cb_path = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_path"));
                            cn2.Open();
                            SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name,status=@status,path=@path Where ID=@ID");
                            cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_Lvl")).Text);
                            cmd2.Parameters.AddWithValue("@Department", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text);
                            cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text);
                            cmd2.Parameters.AddWithValue("@Name", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text);
                            if (Cb_sign.Checked == true)
                            {
                                cmd2.Parameters.AddWithValue("@status", "1");
                            }
                            else
                            {
                                cmd2.Parameters.AddWithValue("@status", "0");
                            }
                            if (Cb_path.Checked == true)
                            {
                                cmd2.Parameters.AddWithValue("@path", "1");
                            }
                            else
                            {
                                cmd2.Parameters.AddWithValue("@path", "0");
                            }
                            cmd2.Parameters.AddWithValue("@ID", ID);
                            cmd2.Connection = cn2;
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        cn.Close();
                        cn.Open();
                        SqlCommand namemd = new SqlCommand("Select * from UserInfo Where Name=@Name");
                        namemd.Parameters.AddWithValue("@Name", UserEID);
                        namemd.Connection = cn;
                        using (SqlDataReader drname = namemd.ExecuteReader())
                        {
                            if (drname.Read())
                            {
                                ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = drname["EID"].ToString();
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text = drname["Department"].ToString();
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text = drname["Name"].ToString();
                                using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                {
                                    CheckBox ck = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_sign"));
                                    CheckBox Cb_path = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_path"));
                                    cn3.Open();
                                    SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name,status=@status,@path=path Where ID=@ID");
                                    cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_Lvl")).Text);
                                    cmd2.Parameters.AddWithValue("@Department", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text);
                                    cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text);
                                    cmd2.Parameters.AddWithValue("@Name", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text);
                                    if (ck.Checked == true)
                                    {
                                        cmd2.Parameters.AddWithValue("@status", "1");
                                    }
                                    else
                                    {
                                        cmd2.Parameters.AddWithValue("@status", "0");
                                    }
                                    if (Cb_path.Checked == true)
                                    {
                                        cmd2.Parameters.AddWithValue("@path", "1");
                                    }
                                    else
                                    {
                                        cmd2.Parameters.AddWithValue("@path", "0");
                                    }
                                    cmd2.Parameters.AddWithValue("@ID", ID);
                                    cmd2.Connection = cn3;
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }

            }
            string Lvl = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_Lvl")).Text.Trim();
            string EID = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_EID")).Text.Trim();
            if (EID != "")
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into Preview(SID,EID) Values(@SID,@EID)");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@EID", "");
                    cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                    cmd.ExecuteNonQuery();
                    bind3();
                    using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn3.Open();
                        SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "' and EID!='" + Lbl_EID.Text + "'");
                        cmd3.Connection = cn3;
                        using (SqlDataReader dr2 = cmd3.ExecuteReader())
                        {
                            int i = 0;
                            while (dr2.Read())
                            {

                                ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
                                ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
                                ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
                                ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
                                if (dr2["status"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = false;
                                }

                                if (dr2["path"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = false;
                                }
                                if (dr2["Comment"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = false;
                                }
                                i = int.Parse(i.ToString()) + 1;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 插入群組
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
        #endregion

        #region bind
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

                                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
                                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("EID")).Text = dr2["EID"].ToString();
                                    ((Label)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
                                    ((Label)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();

                                    ((DropDownList)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Ddl_status")).Text = dr2["status"].ToString();
                                    Session["i"] = int.Parse(Session["i"].ToString()) + 1;
                                }
                            }

                        }
                    }
                }
            }
        }
        #endregion

        #region bind2
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
        #endregion

        #region bind3
        public void bind3()
        {
            string sqlstr = "select * from Preview Where SID='" + Lbl_SID.Text + "' and EID!='" + Lbl_EID.Text + "'";

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
        #endregion

        #region bind4
        public void bind4()
        {
            string sqlstr = "select * from Vote Where SID='" + Lbl_SID.Text + "'";

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
        #endregion

        #region bind5
        public void bind5()
        {
            string sqlstr = "select * from Preview Where SID='" + Lbl_SID.Text + "' and EID='"+ Lbl_EID.Text +"'";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "Preview");

            GridView1.DataSource = myds;
            GridView1.DataBind();
            sqlcon.Close();
        }
        #endregion

        #region 上傳檔案
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
        #endregion

        #region 找暫存檔填寫到gridview
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
        #endregion

        #region 增加一列
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand("Insert Into Preview(SID,EID) Values(@SID,@EID)");
        //        cmd.Connection = cn;
        //        cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
        //        cmd.Parameters.AddWithValue("@EID", "");
        //        cmd.ExecuteNonQuery();
        //        bind3();
        //        using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
        //        {
        //            cn3.Open();
        //            SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "' and EID!='" + Lbl_EID.Text + "'");
        //            cmd3.Connection = cn3;
        //            using (SqlDataReader dr2 = cmd3.ExecuteReader())
        //            {
        //                Session["i"] = 0;
        //                while (dr2.Read())
        //                {

        //                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
        //                    ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
        //                    ((Label)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
        //                    ((Label)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
        //                    if (dr2["status"].ToString() == "1")
        //                    {
        //                        ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_sign")).Checked = true;
        //                    }
        //                    else
        //                    {
        //                        ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_sign")).Checked = false;
        //                    }

        //                    if (dr2["path"].ToString() == "1")
        //                    {
        //                        ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_path")).Checked = true;
        //                    }
        //                    else
        //                    {
        //                        ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_path")).Checked = false;
        //                    }

        //                    Session["i"] = int.Parse(Session["i"].ToString()) + 1;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

        #region 增加十列
        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
        //        {
        //            cn.Open();
        //            SqlCommand cmd = new SqlCommand("Insert Into Preview(SID,EID) Values(@SID,@EID)");
        //            cmd.Connection = cn;
        //            cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
        //            cmd.Parameters.AddWithValue("@EID", "");
        //            cmd.ExecuteNonQuery();
        //            bind3();
        //        }
        //    }
        //    using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
        //    {
        //        cn3.Open();
        //        SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "' and EID!='" + Lbl_EID.Text + "'");
        //        cmd3.Connection = cn3;
        //        using (SqlDataReader dr2 = cmd3.ExecuteReader())
        //        {
        //            Session["i"] = 0;
        //            while (dr2.Read())
        //            {

        //                ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
        //                ((TextBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
        //                ((Label)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
        //                ((Label)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
        //                if (dr2["status"].ToString() == "1")
        //                {
        //                    ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_sign")).Checked = true;
        //                }
        //                else
        //                {
        //                    ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_sign")).Checked = false;
        //                }

        //                if (dr2["path"].ToString() == "1")
        //                {
        //                    ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_path")).Checked = true;
        //                }
        //                else
        //                {
        //                    ((CheckBox)GridView2.Rows[int.Parse(Session["i"].ToString())].FindControl("Cb_path")).Checked = false;
        //                }
        //                Session["i"] = int.Parse(Session["i"].ToString()) + 1;
        //            }
        //        }
        //    }
        //}
        #endregion

        #region 增加新群組
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

                                string Lvl = ((TextBox)GridView2.Rows[i].FindControl("Txt_Lvl")).Text.Trim();
                                string EID = ((TextBox)GridView2.Rows[i].FindControl("Txt_EID")).Text.Trim();
                                string Department = ((Label)GridView2.Rows[i].FindControl("Lbl_Dep")).Text.Trim();
                                string Name = ((Label)GridView2.Rows[i].FindControl("Lbl_Name")).Text.Trim();
                                CheckBox Cb_sign = ((CheckBox)GridView1.Rows[0].FindControl("Cb_sign"));
                                CheckBox Cb_path = ((CheckBox)GridView1.Rows[0].FindControl("Cb_path"));
                                CheckBox Cb_comment = ((CheckBox)GridView1.Rows[0].FindControl("Cb_comment"));

                                if (!string.IsNullOrWhiteSpace(TextBox1.Text)
                                    && Lvl != ""
                                    && Department != ""
                                    && Department != "Dp1"
                                    && Name != "")
                                {
                                    //寫回資料庫

                                    SqlCommand cmd = new SqlCommand(@"Insert INTO UseGroup(ID,GpName,GID,Lvl,EID,Department,Name,status,path,Comment)VALUES(@ID,@GpName,@GID,@Lvl,@EID,@Department,@Name,@status,@path,@Comment)");

                                    cmd.Connection = cn;
                                    cmd.Parameters.AddWithValue("@GpName", TextBox1.Text);
                                    cmd.Parameters.AddWithValue("@Lvl", Lvl);
                                    cmd.Parameters.AddWithValue("@EID", EID);
                                    cmd.Parameters.AddWithValue("@ID", i + 1);
                                    cmd.Parameters.AddWithValue("@Department", Department);
                                    cmd.Parameters.AddWithValue("@Name", Name);
                                    if (Cb_sign.Checked == true)
                                    {
                                        cmd.Parameters.AddWithValue("@status", "1");
                                        cmd.Parameters.AddWithValue("@sign", 0);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@status", "0");
                                        cmd.Parameters.AddWithValue("@sign", 1);
                                    }
                                    if (Cb_path.Checked == true)
                                    {
                                        cmd.Parameters.AddWithValue("@path", "1");
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@path", "0");
                                    }
                                    if (Cb_comment.Checked == true)
                                    {
                                        cmd.Parameters.AddWithValue("@Comment", "1");
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Comment", "0");
                                    }
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
        #endregion

        #region 點選LinkButton插入群組
        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            i = int.Parse(GridView4.SelectedIndex.ToString());
            string GpName = ((LinkButton)GridView4.Rows[i].FindControl("LblName")).Text.Trim();
            Lbl_GpName.Text = GpName;

            bind();
        }
        #endregion

        #region 編輯群組
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
        #endregion

        #region 點選下載檔案
        protected void OpenDoc(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow gr = (GridViewRow)lnk.NamingContainer;

            int FNO = int.Parse(gv_showTempFile.DataKeys[gr.RowIndex].Value.ToString());
            Download(FNO);
        }
        #endregion

        #region 下載檔案coding
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
        #endregion

        #region 刪除檔案
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
            command.CommandText = "DELETE FROM tempDocument WHERE Name=@Name and SID=@SID ";

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
        #endregion        

        #region 增加投票選項
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["max"]= int.Parse(Session["max"].ToString()) + 1;
            Session["number"] = int.Parse(Session["max"].ToString());
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Insert Into Vote(SID,number) Values(@SID,@number)");
                cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                cmd.Parameters.AddWithValue("@number", int.Parse(Session["number"].ToString()));
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlCommand votecmd = new SqlCommand("Select * From Vote Where SID='" + Lbl_SID.Text + "'");
                votecmd.Connection = cn;

                    bind4();
                using (SqlDataReader dr = votecmd.ExecuteReader())
                {
                    int i = 0;
                    while (dr.Read())
                    {
                        ((TextBox)GridView5.Rows[i].FindControl("Txt_content")).Text = dr["Vname"].ToString();
                        i = int.Parse(i.ToString()) + 1;
                    }
                }
            }
        }
        #endregion

        #region 勾選sign時發生變化
        protected void Cb_sign_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            CheckBox ck = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_sign"));
            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn2.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set status=@status Where ID=@ID");

                if (ck.Checked == true)
                {
                    cmd2.Parameters.AddWithValue("@status", "1");
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@status", "0");
                }

                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn2;
                cmd2.ExecuteNonQuery();
            }
        }
        #endregion

        #region 勾選path時發生變化
        protected void Cb_path_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
            string UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text.Trim();
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            CheckBox ck = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_sign"));
            CheckBox ckp = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_path"));
            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn2.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name,status=@status,path=@path Where ID=@ID");
                cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_Lvl")).Text);
                cmd2.Parameters.AddWithValue("@Department", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text);
                cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text);
                cmd2.Parameters.AddWithValue("@Name", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text);
                if (ck.Checked == true)
                {
                    cmd2.Parameters.AddWithValue("@status", "1");
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@status", "0");
                }
                if (ckp.Checked == true)
                {
                    cmd2.Parameters.AddWithValue("@path", "1");
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@path", "0");
                }
                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn2;
                cmd2.ExecuteNonQuery();
            }
        }
        #endregion

        #region 勾選comment時發生變化
        protected void Cb_comment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
            string UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text.Trim();
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            CheckBox ck = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_comment"));
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set Comment=@Comment Where ID=@ID");
                if (ck.Checked == true)
                {
                    cmd2.Parameters.AddWithValue("@Comment", "1");
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@Comment", "0");
                }
                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn;
                cmd2.ExecuteNonQuery();
            }
        }
        #endregion

        #region 送出
        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            
            string SID = Lbl_SID.Text;
            string Lvl5 = ((TextBox)GridView1.Rows[0].FindControl("Txt_Lvl")).Text.Trim();
            string EID5 = ((Label)GridView1.Rows[0].FindControl("Lbl_EID")).Text.Trim();
            string Department5 = ((Label)GridView1.Rows[0].FindControl("Lbl_Dep")).Text.Trim();
            string Name5 = ((Label)GridView1.Rows[0].FindControl("Lbl_Name")).Text.Trim();
            CheckBox Cb_sign5 = ((CheckBox)GridView1.Rows[0].FindControl("Cb_sign"));
            CheckBox Cb_path5 = ((CheckBox)GridView1.Rows[0].FindControl("Cb_path"));
            CheckBox Cb_comment5 = ((CheckBox)GridView1.Rows[0].FindControl("Cb_comment"));

            if (!string.IsNullOrWhiteSpace(d1.Value)
                &&!string.IsNullOrWhiteSpace(Txt_Title.Text)
                && !string.IsNullOrWhiteSpace(Txt_Text.Text)
                && Lvl5 != ""
                )
            {
                using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    //SqlCommand cmd4 = new SqlCommand(@"update Fil set Fil.Name=Document.Name,Fil.DocumentContent=Document.DocumentContent,Fil.Extn=Document.Extn  from Document join Fil on Fil.SID=Document.SID");
                    SqlCommand cmd3 = new SqlCommand(@"Insert INTO Fil(SID,EID,Date,DeadLine,Text,Proposition,Title,Type,YOS,AESkey,AESiv,txt_RSAhash_Text,txt_RSAhash_Proposition,IsEnd)VALUES(@SID,@EID,@Date,@DeadLine,@Text,@Proposition,@Title,@Type,@YOS,@AESkey,@AESiv,@txt_RSAhash_Text,@txt_RSAhash_Proposition,@IsEnd)");
                    cn2.Open();
                    cmd3.Connection = cn2;
                    //cmd4.Connection = cn2;
                    //建立一個 AES 演算法
                    SymmetricAlgorithm symAlgorithm = new AesCryptoServiceProvider();
                    txtKey = Convert.ToBase64String(symAlgorithm.Key);     //hFYPyIK3uSQ=
                    txtIV = Convert.ToBase64String(symAlgorithm.IV);       //oeZlJhiaZB0=
                                                                           //對稱加密
                    txt_Ciphertext_Text = AESEncryption(txtKey, txtIV, Txt_Text.Text);
                    txt_Ciphertext_Proposition = AESEncryption(txtKey, txtIV, "");
                    //發文者私鑰加密訊息摘要
                    SqlCommand cmdfindkeyaddress = new SqlCommand(@"Select KeyAddress From UserInfo Where EID=@EID");
                    cmdfindkeyaddress.Connection = cn2;
                    cmdfindkeyaddress.Parameters.AddWithValue("@EID", Lbl_EID.Text);

                    using (SqlDataReader dr2 = cmdfindkeyaddress.ExecuteReader())
                    {
                        if (dr2.Read())
                        {
                            KeyAddress = dr2["KeyAddress"].ToString();
                        }
                        if (KeyAddress == "")
                        {
                            Response.Redirect("KeyAddress.aspx");
                        }
                    }

                    // 建立 RSA 演算法物件的執行個體，並匯入先前建立的私鑰
                    RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                        try
                        {
                            StreamReader str = new StreamReader(@"" + KeyAddress + "");
                        string ReadAll = str.ReadToEnd();
                        // 建立 RSA 演算法物件的執行個體，並匯入先前建立的私鑰
                        rsaProvider.FromXmlString(ReadAll);
                        // 2) 讀取本文資料
                        byte[] content_txt_Ciphertext_Text = Encoding.UTF8.GetBytes(txt_Ciphertext_Text + Lbl_Date.Text);
                        byte[] content_txt_Ciphertext_Proposition = Encoding.UTF8.GetBytes(txt_Ciphertext_Proposition + Lbl_Date.Text);
                        // 3) 呼叫 SignData 方法, 對本文進行簽章
                        byte[] signature_Text = rsaProvider.SignData(content_txt_Ciphertext_Text, new SHA1CryptoServiceProvider());  //指定一個雜湊法
                        byte[] signature_Proposition = rsaProvider.SignData(content_txt_Ciphertext_Proposition, new SHA1CryptoServiceProvider());  //指定一個雜湊法

                        // 輸出簽章 (使用 Base64 編碼）
                        txt_RSAhash_Text = Convert.ToBase64String(signature_Text);
                        txt_RSAhash_Proposition = Convert.ToBase64String(signature_Proposition);
                    }
                
                    catch
                    {
                        Response.Write("<script>alert('此位置找無金鑰，請從新設定!');location.href='KeyAddress.aspx';</script>");
                    }

                    cmd3.Parameters.AddWithValue("@SID", SID);
                    cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                    cmd3.Parameters.AddWithValue("@Date", Lbl_Date.Text);
                    cmd3.Parameters.AddWithValue("@DeadLine", d1.Value);
                    cmd3.Parameters.AddWithValue("@Text", txt_Ciphertext_Text);
                    cmd3.Parameters.AddWithValue("@Proposition", txt_Ciphertext_Proposition);
                    cmd3.Parameters.AddWithValue("@Title", Txt_Title.Text);
                    cmd3.Parameters.AddWithValue("@Type", "投票");
                    cmd3.Parameters.AddWithValue("@YOS", Ddp_YOS.SelectedValue);
                    cmd3.Parameters.AddWithValue("@AESkey", txtKey);
                    cmd3.Parameters.AddWithValue("@AESiv", txtIV);
                    cmd3.Parameters.AddWithValue("@txt_RSAhash_Proposition", txt_RSAhash_Proposition);
                    cmd3.Parameters.AddWithValue("@txt_RSAhash_Text", txt_RSAhash_Text);
                    cmd3.Parameters.AddWithValue("@IsEnd", "0");
                    //cmd4.ExecuteNonQuery();
                    cn2.Close();

                    using (SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        sqlcon.Open();
                        for (int i = 0; i < GridView5.Rows.Count; i++)
                        {
                            string Vname = ((TextBox)GridView5.Rows[i].FindControl("Txt_content")).Text.Trim();
                            string number = ((Label)GridView5.Rows[i].FindControl("Lbl_number")).Text.Trim();
                            if (Vname != "")
                            {
                                SqlCommand sqlcmd = new SqlCommand("Update Vote Set Vname=@Vname where SID=@SID and number=@number");
                                sqlcmd.Connection = sqlcon;
                                sqlcmd.Parameters.AddWithValue("@SID", SID);
                                sqlcmd.Parameters.AddWithValue("@number", number);
                                string txt_Vname = AESEncryption(txtKey, txtIV, Vname);
                                sqlcmd.Parameters.AddWithValue("@Vname", txt_Vname);
                                sqlcmd.ExecuteNonQuery();
                            }
                            else
                            {
                                Lbl_Eorr.Visible = true;
                            }
                        }
                    }

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        string Lvl = ((TextBox)GridView2.Rows[i].FindControl("Txt_Lvl")).Text.Trim();
                        string EID = ((TextBox)GridView2.Rows[i].FindControl("Txt_EID")).Text.Trim();
                        string Department = ((Label)GridView2.Rows[i].FindControl("Lbl_Dep")).Text.Trim();
                        string Name = ((Label)GridView2.Rows[i].FindControl("Lbl_Name")).Text.Trim();
                        CheckBox Cb_sign = ((CheckBox)GridView2.Rows[i].FindControl("Cb_sign"));
                        CheckBox Cb_path = ((CheckBox)GridView2.Rows[i].FindControl("Cb_path"));
                        CheckBox Cb_comment = ((CheckBox)GridView2.Rows[i].FindControl("Cb_comment"));
                        if (SID != "" && Lvl != "")
                        {
                            //找尋接收者PK並加密KEY
                            SqlCommand cmduserInfo1 = new SqlCommand(@"select UserInfo.PK from UserInfo LEFT JOIN Detail ON UserInfo.EID=Detail.EID where (UserInfo.EID=@EID)");
                            cn2.Open();
                            cmduserInfo1.Connection = cn2;
                            cmduserInfo1.Parameters.AddWithValue("@EID", EID);
                            using (SqlDataReader dr = cmduserInfo1.ExecuteReader())
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
                            cn2.Close();

                            using (SqlConnection cnsavefile = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                cn2.Open();
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
                                cn2.Close();
                            }
                            //寫回資料庫                        
                            SqlCommand cmdd = new SqlCommand(@"Insert INTO Detail(SID,Lvl,EID,Department,status,path,sign,Comment,look,RSAkey)VALUES(@SID,@Lvl,@EID,@Department,@status,@path,@sign,@Comment,@look,@RSAkey)");
                            cn2.Open();
                            cmdd.Connection = cn2;
                            cmdd.Parameters.AddWithValue("@SID", SID);
                            cmdd.Parameters.AddWithValue("@Lvl", Lvl);
                            cmdd.Parameters.AddWithValue("@EID", EID);
                            cmdd.Parameters.AddWithValue("@Department", Department);
                            cmdd.Parameters.AddWithValue("@RSAkey", txt_PKmessage);

                            if (Cb_sign.Checked == true)
                            {
                                cmdd.Parameters.AddWithValue("@status", "1");
                                cmdd.Parameters.AddWithValue("@sign", 0);
                            }
                            else
                            {
                                cmdd.Parameters.AddWithValue("@status", "0");
                                cmdd.Parameters.AddWithValue("@sign", 1);
                            }
                            if (Cb_path.Checked == true)
                            {
                                cmdd.Parameters.AddWithValue("@path", "1");
                            }
                            else
                            {
                                cmdd.Parameters.AddWithValue("@path", "0");
                            }
                            if (Cb_comment.Checked == true)
                            {
                                cmdd.Parameters.AddWithValue("@Comment", "1");
                            }
                            else
                            {
                                cmdd.Parameters.AddWithValue("@Comment", "0");
                            }
                            if (ChB_Check.Checked == true)
                            {
                                cmdd.Parameters.AddWithValue("@recheckKey", "1");
                            }
                            else
                            {
                                cmdd.Parameters.AddWithValue("@recheckKey", "0");
                            }
                            if (Lvl == "1")
                            {
                                cmdd.Parameters.AddWithValue("@look", 1);
                            }
                            else
                            {
                                cmdd.Parameters.AddWithValue("@look", 0);
                            }
                            
                            cmdd.ExecuteNonQuery();

                            using (SqlConnection mailcn = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                mailcn.Open();
                                SqlCommand mailcmd = new SqlCommand("Select UserInfo.Email From Detail Left join UserInfo On Detail.EID = UserInfo.EID Where Detail.SID = '" + Lbl_SID.Text + "'");
                                mailcmd.Connection = mailcn;
                                using (SqlDataReader maildr = mailcmd.ExecuteReader())
                                {
                                    while (maildr.Read())
                                    {
                                        if (listmail == null)
                                        {
                                            listmail = maildr["Email"].ToString();
                                        }
                                        else
                                        {
                                            listmail = listmail.ToString() + "," + maildr["Email"].ToString();
                                        }
                                    }

                                }

                            }
                            cn2.Close();

                        }
                        else
                        {
                            string LvlLast = ((TextBox)GridView2.Rows[GridView2.Rows.Count-1].FindControl("Txt_Lvl")).Text.Trim();
                            string EIDLast = ((TextBox)GridView2.Rows[GridView2.Rows.Count-1].FindControl("Txt_EID")).Text.Trim();
                            if(EIDLast!="" && LvlLast!="")
                            {
                                Lbl_Eorr.Visible = true;
                                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                                {
                                    cn.Open();
                                    SqlCommand cmddelete = new SqlCommand("Delete From Detail Where SID=@SID");
                                    cmddelete.Connection = cn;
                                    cmddelete.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                                    cmddelete.ExecuteNonQuery();
                                    cn.Close();
                                }
                                return;
                            }                           

                        }
                    }
                    //找尋接收者PK並加密KEY
                    SqlCommand cmduserInfo = new SqlCommand(@"select UserInfo.PK from UserInfo LEFT JOIN Detail ON UserInfo.EID=Detail.EID where (UserInfo.EID=@EID)");
                    cn2.Open();
                    cmduserInfo.Connection = cn2;
                    cmduserInfo.Parameters.AddWithValue("@EID", EID5);
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

                    //寫回資料庫 
                    SqlCommand cmd = new SqlCommand(@"Insert INTO Detail(SID,Lvl,EID,Department,status,path,sign,look,RSAkey,isAgent,isread,recheckKey,comment)VALUES(@SID,@Lvl,@EID,@Department,@status,@path,@sign,@look,@RSAkey,@isAgent,@isread,@recheckKey,@comment)");

                    cmd.Connection = cn2;
                    cmd.Parameters.AddWithValue("@SID", SID);
                    cmd.Parameters.AddWithValue("@Lvl", Lvl5);
                    cmd.Parameters.AddWithValue("@EID", EID5);
                    cmd.Parameters.AddWithValue("@Department", Department5);
                    cmd.Parameters.AddWithValue("@RSAkey", txt_PKmessage);
                    if (Cb_sign5.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@status", "1");
                        cmd.Parameters.AddWithValue("@sign", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@status", "0");
                        cmd.Parameters.AddWithValue("@sign", 1);
                    }
                    if (ChB_Check.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@recheckKey", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@recheckKey", "0");
                    }
                    if (Cb_comment5.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@comment", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@comment", "0");
                    }
                    if (Cb_path5.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@path", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@path", "0");
                    }
                    if (Lvl5 == "1")
                    {
                        cmd.Parameters.AddWithValue("@look", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@look", 0);
                    }

                    cmd.Parameters.AddWithValue("@isread", 0);
                    using (SqlConnection cnEID = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        SqlCommand cmdEID = new SqlCommand(@"Select agent From UserInfo Where EID=@EID");
                        cnEID.Open();
                        cmdEID.Connection = cnEID;
                        cmdEID.Parameters.AddWithValue("@EID", EID5);
                        using (SqlDataReader drEID = cmdEID.ExecuteReader())
                        {
                            if (drEID.Read())
                            {
                                if (drEID["agent"].ToString() != "")
                                {
                                    cmd.Parameters.AddWithValue("@isAgent", "1");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@isAgent", "0");
                                }
                            }
                            cnEID.Close();
                        }
                    }
                    cmd.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    MailMessage msg = new MailMessage();
                    //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
                    msg.To.Add(listmail.ToString());
                    msg.From = new MailAddress("yuhungsystem@gmail.com", "電子公文通知", System.Text.Encoding.UTF8);
                    //郵件標題 
                    msg.Subject = "新公文通知";
                    //郵件標題編碼  
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;
                    //郵件內容
                    msg.Body = "您有新公文需簽收請前往系統確認";
                    msg.IsBodyHtml = true;
                    msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
                    msg.Priority = MailPriority.Normal;//郵件優先級 
                                                       //建立 SmtpClient 物件 並設定 Gmail的smtp主機及Port 
                    #region 其它 Host
                    /*
                     *  outlook.com smtp.live.com port:25
                     *  yahoo smtp.mail.yahoo.com.tw port:465
                    */
                    #endregion
                    SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
                    //設定你的帳號密碼
                    MySmtp.Credentials = new System.Net.NetworkCredential("yuhungsystem@gmail.com", "lkxvbxebxzfkfdke");
                    //Gmial 的 smtp 使用 SSL
                    MySmtp.EnableSsl = true;
                    MySmtp.Send(msg);
                    Response.Redirect("WaitVote.aspx");
                }
            }
            else
            {
                Lbl_Eorr.Visible = true;
            }


        }
        #endregion

        #region 投票選項刪除
        protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int votecount = 0;
            using (SqlConnection ifcn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                
                ifcn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Vote Where SID='" + Lbl_SID.Text + "'");
                cmd.Connection = ifcn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        votecount = int.Parse(votecount.ToString()) + 1;
                    }
                }
            }
            if(votecount==1)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert('剩餘最後一個選項，不能刪除')", true);
            }
            else
            {
                string sqlstr = "delete from Vote where number=@number";
                SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
                SqlCommand sqlcom = new SqlCommand(sqlstr, sqlcon);
                //這樣就可以取得Keys值了
                string keyId = GridView5.DataKeys[e.RowIndex].Value.ToString();
                sqlcom.Parameters.AddWithValue("@number", int.Parse(keyId));
                sqlcon.Open();
                sqlcom.ExecuteNonQuery();

                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    Session["number"] = 1;
                    cn.Open();
                    SqlCommand cmdselect = new SqlCommand("Select * From Vote Where SID=@SID");
                    cmdselect.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                    cmdselect.Connection = cn;
                    using (SqlDataReader dr = cmdselect.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            using (SqlConnection cnupdate = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                Session["max"] = int.Parse(Session["number"].ToString());
                                cnupdate.Open();
                                SqlCommand cmdupdate = new SqlCommand("UpDate Vote Set number=@number where number=@nb");
                                cmdupdate.Connection = cnupdate;
                                cmdupdate.Parameters.AddWithValue("@number", int.Parse(Session["number"].ToString()));
                                cmdupdate.Parameters.AddWithValue("@nb", int.Parse(dr["number"].ToString()));
                                cmdupdate.ExecuteNonQuery();
                                Session["number"] = int.Parse(Session["number"].ToString()) + 1;
                            }
                        }
                    }
                    bind4();
                    SqlCommand votecmd = new SqlCommand("Select * From Vote Where SID='" + Lbl_SID.Text + "'");
                    votecmd.Connection = cn;
                    using (SqlDataReader dr = votecmd.ExecuteReader())
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            ((TextBox)GridView5.Rows[i].FindControl("Txt_content")).Text = dr["Vname"].ToString();
                            i = int.Parse(i.ToString()) + 1;
                        }
                    }
                }
            }

            
        }
        #endregion

        #region 投票內容存進資料庫
        protected void Txt_content_TextChanged(object sender, EventArgs e)
        {
            TextBox curTextBox = (TextBox)sender;
            int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            string number = ((Label)GridView5.Rows[gvRowIndex].FindControl("Lbl_number")).Text.Trim();
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Update Vote set Vname=@Vname where SID='"+Lbl_SID.Text+"' and number=@number");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@number", number);
                cmd.Parameters.AddWithValue("@Vname", ((TextBox)GridView5.Rows[gvRowIndex].FindControl("Txt_content")).Text);
                cmd.ExecuteNonQuery();

            }
        }
        #endregion
    }
}