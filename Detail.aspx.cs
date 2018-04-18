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
    public partial class Detail : System.Web.UI.Page
    {
        #region 一連串宣告
        string KeyAddress;
        string RSAkey;
        string key;
        string AESiv;
        string IsEnd;
        string senderPK;
        string txt_RSAhash_Text;
        string txt_RSAhash_Proposition;
        string txt_Text;
        string txt_Proposition;
        string stringdate;
        DateTime txtDate;
        byte[] encryptedText;
        DbHelper tmpdbhelper = new DbHelper();
        #endregion
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
                    
                    #region 內文
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "內文";
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_EID.Text = tmpUserInfo.EID;
                        bind();
                        bind2();
                        using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn.Open();
                            SqlCommand cmd = new SqlCommand(@"Select EID From Fil  Where SID=@SID");
                            cmd.Connection = cn;
                            cmd.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Lbl_SenderEID.Text = dr["EID"].ToString();
                                    using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                    {
                                        cn2.Open();
                                        SqlCommand cmd2 = new SqlCommand(@"Select Name From UserInfo  Where EID=@EID");
                                        SqlCommand cmd3 = new SqlCommand(@"Select path,sign,recheckKey,comment From Detail  Where SID=@SID And EID=@EID");
                                        cmd2.Connection = cn2;
                                        cmd2.Parameters.AddWithValue("@EID", dr["EID"].ToString());
                                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                        {
                                            if (dr2.Read())
                                            {
                                                Lbl_SenderName.Text = "主辦人姓名：" + dr2["Name"].ToString();
                                            }

                                            cn2.Close();
                                        }
                                        cn2.Open();
                                        cmd3.Connection = cn2;
                                        cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                                        cmd3.Parameters.AddWithValue("@SID", Lbl_SID.Text);

                                        using (SqlDataReader dr3 = cmd3.ExecuteReader())
                                        {
                                            if (dr3.Read())
                                            {
                                                if (dr3["sign"].ToString() == "1")
                                                {
                                                    Txt_Enterpassword.Visible = false;
                                                    Btn_check.Visible = false;
                                                }
                                                if (dr3["path"].ToString() == "1")
                                                {
                                                    bind3();
                                                }
                                                if(dr3["comment"].ToString()=="1")
                                                {
                                                    Pel_Comment.Visible = true;
                                                }
                                            }

                                            cn2.Close();

                                        }
                                    }
                                }
                                cn.Close();
                            }

                            FillData();
                            if (Lbl_Type.Text == "公文類型：代理人設定")
                            {
                                if (Lbl_SenderEID.Text == Lbl_EID.Text)
                                {
                                    Txt_Enterpassword.Visible = false;
                                    Btn_check.Visible = false;

                                }
                            }
                        }
                        using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn.Open();
                            SqlCommand cmd = new SqlCommand(@"SELECT COUNT (sign)  FROM Detail WHERE SID = @SID and sign=1");
                            SqlCommand cmd2 = new SqlCommand(@"SELECT COUNT (SID) FROM Detail WHERE SID = @SID");
                            cmd.Connection = cn;
                            cmd2.Connection = cn;
                            cmd.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                            cmd2.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                            object RecordNum = cmd.ExecuteScalar();
                            string R1 = RecordNum.ToString();
                            object RecordNum2 = cmd2.ExecuteScalar();
                            string R2 = RecordNum2.ToString();
                            SqlCommand cmd3 = new SqlCommand(@"SELECT IsEnd FROM Fil WHERE SID = @SID");
                            cmd3.Connection = cn;
                            cmd3.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                            using (SqlDataReader dr3 = cmd3.ExecuteReader())
                            {
                                if (dr3.Read())
                                {
                                    IsEnd = dr3["IsEnd"].ToString();
                                }
                            }
                                    if (R1 == R2 && Lbl_SenderEID.Text== Lbl_EID.Text && IsEnd!="1")
                            {
                                BtnEnd.Visible = true;
                            }
                        }
                    }
                    #endregion
                }
            }
        }

        #region 顯示投票統計bind3
        public void bind3()
        {
            using (SqlConnection pathcn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                pathcn.Open();
                SqlCommand pathcmd = new SqlCommand("Select Type From Fil Where SID='" + Lbl_SID.Text + "'");
                pathcmd.Connection = pathcn;
                using (SqlDataReader pathdr = pathcmd.ExecuteReader())
                {
                    if(pathdr.Read())
                    {
                        if(pathdr["Type"].ToString()=="投票")
                        {
                            string sqlstr = "select * from Vote where SID='" + Lbl_SID.Text + "'";
                            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
                            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
                            DataSet myds = new DataSet();
                            sqlcon.Open();
                            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
                            myda.Fill(myds, "Vote");
                            Gv_Total.DataSource = myds;
                            Gv_Total.DataBind();
                            sqlcon.Close();
                        }
                        else
                        {
                            string sqlstr = "Select Detail.SID,Detail.Lvl,UserInfo.Department,UserInfo.Name,Detail.sign,Detail.signtime From Detail Left join UserInfo On Detail.EID = UserInfo.EID Where Detail.SID = '" + Lbl_SID.Text + "'";
                            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
                            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
                            DataSet myds = new DataSet();
                            sqlcon.Open();
                            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
                            myda.Fill(myds, "Detail");
                            Gv_path.DataSource = myds;
                            Gv_path.DataBind();
                            sqlcon.Close();
                        }
                    }
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
                SqlCommand cmd = new SqlCommand("select Name,FNO from Document where SID=@SID", con);
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

        #region AES解密功能
        public string AESDecryption(string Key, string IV, string CipherText)
        {
            try
            {
                UTF32Encoding utf32Encoding = new UTF32Encoding();
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
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    //執行解密

                    byte[] encryptTextBytes = Convert.FromBase64String(CipherText);
                    encryptedText = decryptor.TransformFinalBlock(encryptTextBytes, 0, encryptTextBytes.Length);
                   
                }
            }
            catch
            {
                Response.Write("<script>alert('資料已遭竄改!,簽章失敗');location.href='WaitDocument.aspx';</script>");
            }

            if(encryptedText!=null)
            {
                return Encoding.Unicode.GetString(encryptedText);
               
            }
            else
            {
                Response.Write("<script>alert('資料已遭竄改!,簽章失敗');location.href='WaitDocument.aspx';</script>");
                return "";
               
            }
            
        }

        public byte[] AESDecryptionFile(string Key, string IV, string CipherText)
        {
            UTF32Encoding utf32Encoding = new UTF32Encoding();
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
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                //執行解密
                byte[] encryptTextBytes = Convert.FromBase64String(CipherText);

                byte[] encryptedText = decryptor.TransformFinalBlock(encryptTextBytes, 0, encryptTextBytes.Length);
                return encryptedText;
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
                SqlCommand cmd = new SqlCommand("select Name,FNO,DocumentContent,Extn from Document where FNO = @FNO and SID = @SID", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FNO", SqlDbType.Int).Value = FNO;
                cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);
            }
            string name = dt.Rows[0]["Name"].ToString();
            //檔案解密
            #region 找出金鑰位址
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmdfindkeyaddress = new SqlCommand(@"Select KeyAddress From UserInfo Where EID=@EID");
                cmdfindkeyaddress.Connection = cn;
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
                #endregion

                try
                {
                    StreamReader str = new StreamReader(@"" + KeyAddress + "");
                    string ReadAll = str.ReadToEnd();
                    // 建立 RSA 演算法物件的執行個體，並匯入先前建立的私鑰
                    RSACryptoServiceProvider rsaProviderReceiver = new RSACryptoServiceProvider();
                    rsaProviderReceiver.FromXmlString(ReadAll);
                    try
                    {

                        SqlCommand cmd3 = new SqlCommand(@"Select RSAkey From Detail Where EID=@EID and SID=@SID");
                        cmd3.Connection = cn;
                        cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                        cmd3.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                        using (SqlDataReader dr2 = cmd3.ExecuteReader())
                        {
                            if (dr2.Read())
                            {
                                RSAkey = dr2["RSAkey"].ToString();
                            }
                        }

                        // 將資料解密

                        byte[] byteCipher = Convert.FromBase64String(RSAkey);
                        byte[] bytePlain = rsaProviderReceiver.Decrypt(byteCipher, false);

                        // 將解密後的資料，轉 UTF8 格式輸入
                        key = Encoding.UTF8.GetString(bytePlain);
                    }
                    catch
                    {
                        Response.Write("<script>alert('解密失敗!');location.href='WaitDocument.aspx';</script>");
                    }
                }
                catch
                {
                    Response.Write("<script>alert('此位置找無金鑰，請從新設定!');location.href='KeyAddress.aspx';</script>");
                }


                if (key != null)
                {
                    //找到解密iv

                    SqlCommand cmd5 = new SqlCommand(@"Select AESiv From Fil Where SID=@SID");
                    cmd5.Connection = cn;
                    cmd5.Parameters.AddWithValue("@SID", Session["keyId"].ToString());

                    using (SqlDataReader dr3 = cmd5.ExecuteReader())
                    {
                        if (dr3.Read())
                        {
                            AESiv = dr3["AESiv"].ToString();
                        }
                    }

                    //對稱解密
                    string document = dt.Rows[0]["DocumentContent"].ToString();
                    byte[] documentBytes = AESDecryptionFile(key, AESiv, document);

                    Response.ClearContent();
                    Response.ContentType = "application/octetstream";
                    Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", name));
                    Response.AddHeader("Content-Length", documentBytes.Length.ToString());

                    Response.BinaryWrite(documentBytes);
                    Response.Flush();
                    Response.Close();
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

                #region 讀公文資料
                SqlCommand cmd = new SqlCommand("Select * from Fil Where SID=@SID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        DateTime strDate = DateTime.Parse(dr["Date"].ToString());

                        Lbl_SID.Text = dr["SID"].ToString();
                        Lbl_Title.Text = dr["Title"].ToString();
                        Lbl_Date.Text = String.Format("{0:yyyy/MM/dd}", strDate);
                        stringdate = strDate.ToString("yyyyMMdd");
                        Lbl_Text.Text = dr["Text"].ToString();
                        Lbl_Type.Text = "公文類型：" + dr["Type"].ToString();
                        Lbl_Proposition.Text = dr["Proposition"].ToString();
                        if (dr["Type"].ToString() == "投票")
                        {

                            using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                cn3.Open();
                                SqlCommand choosecmd = new SqlCommand(@"Select choose From Detail Where EID=@EID and SID=@SID");
                                choosecmd.Connection = cn3;
                                choosecmd.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                                choosecmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                                using (SqlDataReader dr2 = choosecmd.ExecuteReader())
                                {
                                    if (dr2.Read())
                                    {
                                        using (SqlConnection Vmcn = new SqlConnection(tmpdbhelper.DB_CnStr))
                                        {
                                            Vmcn.Open();
                                            SqlCommand Vmcmd = new SqlCommand(@"Select Vname From Vote Where number=@number and SID=@SID");
                                            Vmcmd.Connection = Vmcn;
                                            Vmcmd.Parameters.AddWithValue("@number", dr2["choose"].ToString());
                                            Vmcmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                                            using (SqlDataReader drVm = Vmcmd.ExecuteReader())
                                            {
                                                if (drVm.Read())
                                                {
                                                    Lbl_Choose.Text = drVm["Vname"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                cn3.Close();
                            }
                            Pel_selectwatch.Visible = true;
                            bind2();
                        }
                        else
                        {
                            Pel_Choose.Visible = false;
                            bind2();
                        }
                    }
                }
                #endregion

                #region 判斷簽核是否完成
                SqlCommand cmd2 = new SqlCommand(@"Select * From Detail Where SID=@SID AND EID=@EID");
                cmd2.Connection = cn;
                cmd2.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                cmd2.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                    if (dr2.Read())
                    {
                        if (dr2["sign"].ToString() == "0" || dr2["choose"] != null)

                        {
                            Pnl_sign.Visible = true;

                        }
                        else
                        {
                            Pel_Choose.Visible = true;
                            Pnl_sign.Visible = false;
                        }
                    }
                #endregion                                

                #region 找出金鑰位址
                SqlCommand cmdfindkeyaddress = new SqlCommand(@"Select KeyAddress From UserInfo Where EID=@EID");
                cmdfindkeyaddress.Connection = cn;
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
                #endregion

                #region 解密
                try
                {
                    StreamReader str = new StreamReader(@"" + KeyAddress + "");
                    string ReadAll = str.ReadToEnd();
                    // 建立 RSA 演算法物件的執行個體，並匯入先前建立的私鑰
                    RSACryptoServiceProvider rsaProviderReceiver = new RSACryptoServiceProvider();
                    rsaProviderReceiver.FromXmlString(ReadAll);
                    try
                    {

                        SqlCommand cmd3 = new SqlCommand(@"Select RSAkey From Detail Where EID=@EID and SID=@SID");
                        cmd3.Connection = cn;
                        cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                        cmd3.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                        using (SqlDataReader dr2 = cmd3.ExecuteReader())
                        {
                            if (dr2.Read())
                            {
                                RSAkey = dr2["RSAkey"].ToString();
                            }
                        }

                        // 將資料解密

                        byte[] byteCipher = Convert.FromBase64String(RSAkey);
                        byte[] bytePlain = rsaProviderReceiver.Decrypt(byteCipher, false);

                        // 將解密後的資料，轉 UTF8 格式輸入
                        key = Encoding.UTF8.GetString(bytePlain);
                    }
                    catch
                    {
                        Response.Write("<script>alert('解密失敗!');location.href='WaitDocument.aspx';</script>");
                    }
                }
                catch
                {
                    Response.Write("<script>alert('此位置找無金鑰，請從新設定!');location.href='KeyAddress.aspx';</script>");
                }


                if (key != null)
                {
                    //找到解密iv

                    SqlCommand cmd5 = new SqlCommand(@"Select AESiv From Fil Where SID=@SID");
                    cmd5.Connection = cn;
                    cmd5.Parameters.AddWithValue("@SID", Session["keyId"].ToString());

                    using (SqlDataReader dr3 = cmd5.ExecuteReader())
                    {
                        if (dr3.Read())
                        {
                            AESiv = dr3["AESiv"].ToString();
                        }
                    }

                    //對稱解密

                    Lbl_Text.Text = AESDecryption(key, AESiv, Lbl_Text.Text);
                    Lbl_Proposition.Text = AESDecryption(key, AESiv, Lbl_Proposition.Text);
                    Lbl_Choose.Text = AESDecryption(key, AESiv, Lbl_Choose.Text);

                }
                #endregion
            }
        }
        #endregion

        #region 讀取Vote投票內容 bind2()
        public void bind2()
        {
            string sqlstr = "select * from Vote where SID='" + Session["keyId"].ToString() + "'";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            myda.Fill(myds, "Vote");
            GridView2.DataSource = myds;
                    GridView2.DataBind();
                    sqlcon.Close();

            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandText = "Select * From Vote Where SID=@SID";
                cmd.Connection = cn2;
                cmd.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                cn2.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DropDownList1.Items.Clear();
                    while (dr.Read())//讀取資料有幾筆讀幾筆只能往下讀不能往上讀
                    {
                        DropDownList1.Items.Add(dr["number"].ToString());//下拉式選單
                    }
                }
            }
        }
            
        

        #endregion

        #region 簽核
        protected void Btn_check_Click(object sender, EventArgs e)
        {
            if (Session["ischeck"]==null)
                {
                Session["ischeck"] = "";
            }
            string ischeck= Session["ischeck"].ToString();
            if (ischeck != "1"|| ischeck == "")
            {
                using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn2.Open();
                    SqlCommand cmd3 = new SqlCommand(@"Select recheckKey From Detail  Where SID=@SID And EID=@EID");
                    cmd3.Connection = cn2;
                    cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                    cmd3.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                    using (SqlDataReader dr3 = cmd3.ExecuteReader())
                    {
                        if (dr3.Read())
                        {
                            if (dr3["recheckKey"].ToString() == "1")
                            {
                                using (SqlConnection cnaddress = new SqlConnection(tmpdbhelper.DB_CnStr))
                                {
                                    cnaddress.Open();
                                    SqlCommand cmdfindkeyaddress = new SqlCommand(@"Update UserInfo Set KeyAddress=@KeyAddress Where EID=@EID");
                                    cmdfindkeyaddress.Connection = cnaddress;
                                    cmdfindkeyaddress.Parameters.AddWithValue("@KeyAddress", "");
                                    cmdfindkeyaddress.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                                    KeyAddress = "";
                                    cmdfindkeyaddress.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    cn2.Close();
                }
                if (KeyAddress == "")
                {
                    Session["ischeck"] = "1";
                    Response.Write("<script>alert('請重新確認金鑰位置!');location.href='KeyAddress.aspx';</script>");
                }
            }
            using (SqlConnection cnsign = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cnsign.Open();
                SqlCommand cmdsign = new SqlCommand(@"Select PK From UserInfo Where EID=@EID");
                SqlCommand cmdhash = new SqlCommand(@"Select txt_RSAhash_Text,txt_RSAhash_Proposition,Text,Proposition,Date From Fil Where SID=@SID");
                cmdsign.Connection = cnsign;
                cmdhash.Connection = cnsign;
                cmdsign.Parameters.AddWithValue("@EID", Lbl_SenderEID.Text);
                cmdhash.Parameters.AddWithValue("@SID", Session["keyId"].ToString());

                using (SqlDataReader dr2 = cmdsign.ExecuteReader())
                {
                    if (dr2.Read())
                    {
                        senderPK = dr2["PK"].ToString();
                    }
                }
                using (SqlDataReader drhash = cmdhash.ExecuteReader())
                {
                    if (drhash.Read())
                    {
                        txt_RSAhash_Text = drhash["txt_RSAhash_Text"].ToString();
                        txt_RSAhash_Proposition = drhash["txt_RSAhash_Proposition"].ToString();
                        txt_Text= drhash["Text"].ToString();
                        txt_Proposition= drhash["Proposition"].ToString();
                        txtDate= DateTime.Parse(drhash["Date"].ToString());
                        stringdate = txtDate.ToString("yyyyMMdd");
                    }
                }
                // 1) 建立RSA數位簽章演算法物件
                RSACryptoServiceProvider verifier = new RSACryptoServiceProvider();
                verifier.FromXmlString(senderPK);     //讀取公開金鑰
                // 2) 讀取接收到的本文資料
                byte[] content_Text = Encoding.UTF8.GetBytes(txt_Text + stringdate);
                byte[] content_Proposition = Encoding.UTF8.GetBytes(txt_Proposition + stringdate);
                // 3) 讀取接收到的簽章資料

                byte[] signature_Text = Convert.FromBase64String(txt_RSAhash_Text);
                byte[] signature_Proposition = Convert.FromBase64String(txt_RSAhash_Proposition);
                // 將資料解密
                // 4) 呼叫 VerifyData 方法, 驗證本文與簽章是否相符
                if (verifier.VerifyData(content_Text, new SHA1CryptoServiceProvider(), signature_Text) && verifier.VerifyData(content_Proposition, new SHA1CryptoServiceProvider(), signature_Proposition))
                {
                    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn.Open();

                        SqlCommand cmd = new SqlCommand(@"Select Pwd From UserInfo Where EID=@EID AND Pwd = @Pwd");
                        cmd.Connection = cn;
                        cmd.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                        cmd.Parameters.AddWithValue("@Pwd", Txt_Enterpassword.Text);

                        SqlCommand sqlcmd = new SqlCommand("Select * from Fil Where SID=@SID");
                        sqlcmd.Connection = cn;
                        sqlcmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);

                        using (SqlDataReader dr2 = sqlcmd.ExecuteReader())
                        {
                            if (dr2.Read())
                            {
                                if (dr2["Type"].ToString() == "投票")
                                {
                                    using (SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr))
                                    {
                                        sqlcon.Open();
                                        SqlCommand choosecmd = new SqlCommand("Update Detail set choose=@choose where EID=@EID and SID=@SID");
                                        choosecmd.Parameters.AddWithValue("@choose", DropDownList1.SelectedValue);
                                        choosecmd.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                                        choosecmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                                        choosecmd.Connection = sqlcon;
                                        choosecmd.ExecuteNonQuery();


                                        SqlCommand selecttotalcmd = new SqlCommand("Select * from Vote where SID=@SID and number=@number");
                                        selecttotalcmd.Parameters.AddWithValue("SID", Lbl_SID.Text);
                                        selecttotalcmd.Parameters.AddWithValue("number", DropDownList1.SelectedValue);
                                        selecttotalcmd.Connection = sqlcon;
                                        using (SqlDataReader totaldr = selecttotalcmd.ExecuteReader())
                                        {

                                            if (totaldr.Read())
                                            {
                                                Session["total"] = int.Parse(totaldr["Total"].ToString());
                                                Session["total"] = int.Parse(Session["total"].ToString()) + 1;
                                                using (SqlConnection upcn = new SqlConnection(tmpdbhelper.DB_CnStr))
                                                {
                                                    upcn.Open();
                                                    SqlCommand upcmd = new SqlCommand("Update Vote set Total=@Total Where number=@number");
                                                    upcmd.Parameters.AddWithValue("@Total", Session["total"].ToString());
                                                    upcmd.Parameters.AddWithValue("number", DropDownList1.SelectedValue);
                                                    upcmd.Connection = upcn;
                                                    upcmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {

                            if (dr.Read())
                            {
                                using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                {
                                    cn2.Open();
                                    SqlCommand cmd2 = new SqlCommand(@"UPDATE Detail Set sign=1,signtime=@signtime Where SID=@SID AND EID=@EID");
                                    cmd2.Connection = cn2;
                                    cmd2.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                                    cmd2.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                                    cmd2.Parameters.AddWithValue("@signtime", System.DateTime.Now);
                                    cmd2.ExecuteNonQuery();
                                }
                                using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                {
                                    cn3.Open();
                                    SqlCommand cmd3 = new SqlCommand("Select * From Detail Where SID=@SID and look=1");
                                    cmd3.Connection = cn3;
                                    cmd3.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                                    using (SqlDataReader dr2 = cmd3.ExecuteReader())
                                    {
                                        Session["i"] = 0;
                                        Session["j"] = 0;
                                        while (dr2.Read())
                                        {
                                            if (dr2["sign"].ToString() == "0")
                                            {
                                                Session["i"] = int.Parse(Session["i"].ToString()) + 1;
                                            }
                                            Session["j"] = dr2["Lvl"].ToString();
                                        }
                                        if (Session["i"].ToString() == "0")
                                        {
                                            Session["j"] = int.Parse(Session["j"].ToString()) + 1;
                                            using (SqlConnection cn4 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                            {
                                                cn4.Open();
                                                SqlCommand cmd4 = new SqlCommand("UPDATE Detail Set look=1 Where SID=@SID and Lvl='" + Session["j"].ToString() + "'");
                                                cmd4.Connection = cn4;
                                                cmd4.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                                                cmd4.ExecuteNonQuery();
                                            }
                                        }
                                        if (Lbl_Type.Text == "公文類型：代理人設定")
                                        {
                                            using (SqlConnection cnAgent = new SqlConnection(tmpdbhelper.DB_CnStr))
                                            {
                                                cnAgent.Open();
                                                SqlCommand cmdAgent = new SqlCommand(@"Select agent From UserInfo  Where EID=@EID");
                                                cmdAgent.Connection = cnAgent;
                                                cmdAgent.Parameters.AddWithValue("@EID", Lbl_SenderEID.Text);
                                                using (SqlDataReader drAgent = cmdAgent.ExecuteReader())
                                                {
                                                    if (drAgent.Read())
                                                    {
                                                        using (SqlConnection cnUpate = new SqlConnection(tmpdbhelper.DB_CnStr))
                                                        {
                                                            cnUpate.Open();
                                                            SqlCommand cmd2 = new SqlCommand(@"Update UserInfo Set agent=@agent Where EID=@EID");
                                                            cmd2.Connection = cnUpate;
                                                            cmd2.Parameters.AddWithValue("@EID", Lbl_SenderEID.Text);
                                                            cmd2.Parameters.AddWithValue("@agent", Lbl_EID.Text);
                                                            cmd2.ExecuteNonQuery();
                                                            SqlCommand cmd4 = new SqlCommand(@"Insert Into AgentInfo (agent,EID,AgentName,StartTime,EndTime,send,receive) Select agent,EID,AgentName,StartTime,EndTime,send,receive From tempAgentInfo Where EID = @EID");
                                                            cmd4.Connection = cnUpate;
                                                            cmd4.Parameters.AddWithValue("@EID", Lbl_SenderEID.Text);
                                                            cmd4.ExecuteNonQuery();
                                                            cnUpate.Close();
                                                        }
                                                    }
                                                    using (SqlConnection cnUpate = new SqlConnection(tmpdbhelper.DB_CnStr))
                                                    {
                                                        cnUpate.Open();
                                                        SqlCommand cmd2 = new SqlCommand(@"Delete From tempAgentInfo  Where EID=@EID");
                                                        cmd2.Connection = cnUpate;
                                                        cmd2.Parameters.AddWithValue("@EID", Lbl_SenderEID.Text);
                                                        cmd2.ExecuteNonQuery();
                                                        cnUpate.Close();
                                                    }

                                                }
                                                cnAgent.Close();
                                            }
                                        }

                                        Response.Write("<script language=javascript>alert('簽核成功!')</script>");
                                        Response.Write("<script language=javascript>window.location.href='Detail.aspx'</script>");
                                    }
                                }
                            }
                            else
                                Lbl_Eorr.Visible = true;
                        }
                    }
                }
                else
                    Response.Write("<script>alert('資料已遭竄改!,簽章失敗');location.href='Detail.aspx';</script>");
            }

        }
        #endregion

        #region 投票總數解密
        protected void Gv_Total_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    #region 找出金鑰位址
                    SqlCommand cmdfindkeyaddress = new SqlCommand(@"Select KeyAddress From UserInfo Where EID=@EID");
                cmdfindkeyaddress.Connection = cn;
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
                #endregion

                try
                {
                    StreamReader str = new StreamReader(@"" + KeyAddress + "");
                    string ReadAll = str.ReadToEnd();
                    // 建立 RSA 演算法物件的執行個體，並匯入先前建立的私鑰
                    RSACryptoServiceProvider rsaProviderReceiver = new RSACryptoServiceProvider();
                    rsaProviderReceiver.FromXmlString(ReadAll);
                    try
                    {

                        SqlCommand cmd3 = new SqlCommand(@"Select RSAkey From Detail Where EID=@EID and SID=@SID");
                        cmd3.Connection = cn;
                        cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                        cmd3.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                        using (SqlDataReader dr2 = cmd3.ExecuteReader())
                        {
                            if (dr2.Read())
                            {
                                RSAkey = dr2["RSAkey"].ToString();
                            }
                        }

                        // 將資料解密

                        byte[] byteCipher = Convert.FromBase64String(RSAkey);
                        byte[] bytePlain = rsaProviderReceiver.Decrypt(byteCipher, false);

                        // 將解密後的資料，轉 UTF8 格式輸入
                        key = Encoding.UTF8.GetString(bytePlain);
                    }
                    catch
                    {
                        Response.Write("<script>alert('解密失敗!');location.href='WaitDocument.aspx';</script>");
                    }
                }
                catch
                {
                    Response.Write("<script>alert('此位置找無金鑰，請從新設定!');location.href='KeyAddress.aspx';</script>");
                }


                    if (key != null)
                    {
                        //找到解密iv

                        SqlCommand cmd5 = new SqlCommand(@"Select AESiv From Fil Where SID=@SID");
                        cmd5.Connection = cn;
                        cmd5.Parameters.AddWithValue("@SID", Session["keyId"].ToString());

                        using (SqlDataReader dr3 = cmd5.ExecuteReader())
                        {
                            if (dr3.Read())
                            {
                                AESiv = dr3["AESiv"].ToString();
                            }
                        }

                        //對稱解密
                        Label Lbl_number = (Label)e.Row.FindControl("Lbl_number");
                        Label Lbl_Vname = (Label)e.Row.FindControl("Lbl_Vname");
                        using (SqlConnection cnVote = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cnVote.Open();
                            SqlCommand cmd = new SqlCommand("Select * from Vote where SID=@SID and number=@number");
                            cmd.Connection = cnVote;
                            cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                            cmd.Parameters.AddWithValue("@number", Lbl_number.Text);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Lbl_Vname.Text = AESDecryption(key, AESiv, dr["Vname"].ToString());
                                }
                            }
                            cnVote.Close();



                        }
                    }

                    cn.Close();
                }
            }

        }
        #endregion

        #region 投票選項解密
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    #region 找出金鑰位址
                    SqlCommand cmdfindkeyaddress = new SqlCommand(@"Select KeyAddress From UserInfo Where EID=@EID");
                    cmdfindkeyaddress.Connection = cn;
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
                    #endregion

                    try
                    {
                        StreamReader str = new StreamReader(@"" + KeyAddress + "");
                        string ReadAll = str.ReadToEnd();
                        // 建立 RSA 演算法物件的執行個體，並匯入先前建立的私鑰
                        RSACryptoServiceProvider rsaProviderReceiver = new RSACryptoServiceProvider();
                        rsaProviderReceiver.FromXmlString(ReadAll);
                        try
                        {

                            SqlCommand cmd3 = new SqlCommand(@"Select RSAkey From Detail Where EID=@EID and SID=@SID");
                            cmd3.Connection = cn;
                            cmd3.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                            cmd3.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                            using (SqlDataReader dr2 = cmd3.ExecuteReader())
                            {
                                if (dr2.Read())
                                {
                                    RSAkey = dr2["RSAkey"].ToString();
                                }
                            }

                            // 將資料解密

                            byte[] byteCipher = Convert.FromBase64String(RSAkey);
                            byte[] bytePlain = rsaProviderReceiver.Decrypt(byteCipher, false);

                            // 將解密後的資料，轉 UTF8 格式輸入
                            key = Encoding.UTF8.GetString(bytePlain);
                        }
                        catch
                        {
                            Response.Write("<script>alert('解密失敗!');location.href='WaitDocument.aspx';</script>");
                        }
                    }
                    catch
                    {
                        Response.Write("<script>alert('此位置找無金鑰，請從新設定!');location.href='KeyAddress.aspx';</script>");
                    }


                    if (key != null)
                    {
                        //找到解密iv

                        SqlCommand cmd5 = new SqlCommand(@"Select AESiv From Fil Where SID=@SID");
                        cmd5.Connection = cn;
                        cmd5.Parameters.AddWithValue("@SID", Session["keyId"].ToString());

                        using (SqlDataReader dr3 = cmd5.ExecuteReader())
                        {
                            if (dr3.Read())
                            {
                                AESiv = dr3["AESiv"].ToString();
                            }
                        }

                        //對稱解密
                        Label Lb_number = (Label)e.Row.FindControl("Lb_Num");
                        Label Lb_Vname = (Label)e.Row.FindControl("Lb_Vname");
                        using (SqlConnection cnVote = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cnVote.Open();
                            SqlCommand cmd = new SqlCommand("Select * from Vote where SID=@SID and number=@number");
                            cmd.Connection = cnVote;
                            cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                            cmd.Parameters.AddWithValue("@number", Lb_number.Text);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Lb_Vname.Text = AESDecryption(key, AESiv, dr["Vname"].ToString());
                                }
                            }
                            cnVote.Close();



                        }
                    }

                    cn.Close();
                }
            }
        }
        #endregion

        #region 改變簽核或未簽核和文字
        protected void Gv_path_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Select * From Detail Where SID=@SID");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@SID",Lbl_SID.Text);
                    Label sign = (Label)e.Row.FindControl("Lbl_sign");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(dr.Read())
                        {
                            if(sign.Text == "0")
                            {
                                sign.Text = "未簽核";
                                sign.ForeColor = Color.Red;                                
                            }
                            else
                            {
                                sign.Text = "已簽核";
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 歸檔
        protected void BtnEnd_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("UpDate Fil SET IsEnd=1 Where SID=@SID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
                cmd.ExecuteNonQuery();
                cn.Close();
                Response.Write("<script language=javascript>alert('已成功歸檔!')</script>");
                Response.Write("<script language=javascript>window.location.href='Detail.aspx'</script>");
            }


        }

        #endregion

        #region 排序
        protected void Lb_Sort_Click(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "SELECT * FROM[Comment] WHERE([SID] = @SID) ORDER BY[CID] ASC";
            Lb_Dsort.Visible = true;
            Lb_Sort.Visible = false;
        }

        protected void Lb_Dsort_Click(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "SELECT * FROM[Comment] WHERE([SID] = @SID) ORDER BY[CID] DESC";
            Lb_Dsort.Visible = false;
            Lb_Sort.Visible = true;
        }
        #endregion

        #region 評論
        protected void Btn_Comment_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Comment (SID,Name,UserComment,Date) Values (@SID,@Name,@UserComment,@Date)");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SID",Lbl_SID.Text);
                cmd.Parameters.AddWithValue("@Name", Lbl_EID.Text);
                cmd.Parameters.AddWithValue("@UserComment", Txt_comment.Text);
                cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                cmd.ExecuteNonQuery();
                Response.Redirect("Detail.aspx");
            }
        }
        #endregion
    }
}