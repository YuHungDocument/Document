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


namespace WebApplication1
{
    public partial class Detail : System.Web.UI.Page
    {
        string KeyAddress;
        string RSAkey;
        string key;
        string AESiv;
        DbHelper tmpdbhelper = new DbHelper();
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
                   ((Label)this.Master.FindControl("Lb_Title")).Text = "內文";
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_EID.Text = tmpUserInfo.EID;
                        bind();
                        bind2();
                    }
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
                                    cmd2.Connection = cn2;
                                    cmd2.Parameters.AddWithValue("@EID", dr["EID"].ToString());
                                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                    {
                                        if (dr2.Read())
                                        {
                                            Lbl_SenderName.Text = "主辦人姓名" + dr2["Name"].ToString();
                                        }
                                        cn2.Close();
                                    }
                                }
                            }
                        }
                        cn.Close();
                    }
                    if (Lbl_Type.Text == "公文類型：代理人設定")
                    {
                        if (Lbl_SenderEID.Text == Lbl_EID.Text)
                        {
                            Txt_Enterpassword.Visible = false;
                            Btn_check.Visible = false;
                        }
                    }
                }
            }
        }

        #region AES解密功能
        public string AESDecryption(string Key, string IV, string CipherText)
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
                return Encoding.Unicode.GetString(encryptedText);
            }
        }
        #endregion


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

                        Lbl_SID.Text = "文號：" + dr["SID"].ToString();
                        Lbl_Title.Text = dr["Title"].ToString();
                        Lbl_Date.Text = String.Format("{0:yyyy/MM/dd}", strDate);
                        Lbl_Text.Text = dr["Text"].ToString();
                        Lbl_Type.Text = "公文類型：" + dr["Type"].ToString();
                        Lbl_Proposition.Text = dr["Proposition"].ToString();
                        if (dr["Type"].ToString() == "投票")
                        {
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
                        if (dr2["sign"].ToString() == "0")

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

                #region 判斷可不可看進度

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

                }
            }
        }
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

            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandText = "Select * From Vote Where SID=@SID";
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                cn.Open();

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
                            SqlCommand cmd2 = new SqlCommand(@"UPDATE Detail Set sign=1 Where SID=@SID AND EID=@EID");
                            cmd2.Connection = cn2;
                            cmd2.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                            cmd2.Parameters.AddWithValue("@SID", Lbl_SID.Text);
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
        #endregion
    }
}