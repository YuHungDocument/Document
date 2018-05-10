using System;
using System.Collections.Generic;
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
    public partial class ChangeAgent : System.Web.UI.Page
    {
        string txtKey;
        string txtIV;
        string txt_Ciphertext_Text;
        string txt_Ciphertext_Proposition;
        string txt_PKmessage;
        string txt_PKmessage2;
        string work;
        string KeyAddress;
        string txt_RSAhash_Text;
        string txt_RSAhash_Proposition;
        String Date2;
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "代理人變更";
                }
            }
        }
        //AES加密功能
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


        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            string Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                SqlCommand cmd3 = new SqlCommand(@"Insert INTO Fil(SID,EID,Date,Text,Title,Proposition,Type,YOS,AESkey,AESiv,txt_RSAhash_Text,txt_RSAhash_Proposition,IsEnd)VALUES(@SID,@EID,@Date,@Text,@Title,@Proposition,@Type,@YOS,@AESkey,@AESiv,@txt_RSAhash_Text,@txt_RSAhash_Proposition,@IsEnd)");
                cn2.Open();
                cmd3.Connection = cn2;
                //建立一個 AES 演算法
                SymmetricAlgorithm symAlgorithm = new AesCryptoServiceProvider();
                txtKey = Convert.ToBase64String(symAlgorithm.Key);     //hFYPyIK3uSQ=
                txtIV = Convert.ToBase64String(symAlgorithm.IV);       //oeZlJhiaZB0=
                                                                       //對稱加密

                if (CheckBox1.Checked)
                {
                    work = CheckBox1.Text;
                }
                if (CheckBox2.Checked)
                {
                    work += "," + CheckBox2.Text;
                }
                txt_Ciphertext_Text = AESEncryption(txtKey, txtIV, "被代理單位:" + DropDownList1.Text + "被代理人員:" + DropDownList2.Text + "\r\n" + "代理職務:" + work + "\r\n" + "開始代理時間:" + Request.Form["d3"] + "\r\n" + "結束代理時間:" + Request.Form["d4"] + "\r\n" + "代理單位:" + DropDownList7.Text + "代理人:" + DropDownList8.Text + "");
                txt_Ciphertext_Proposition = AESEncryption(txtKey, txtIV, "");
                //發文者私鑰加密訊息摘要
                SqlCommand cmdfindkeyaddress = new SqlCommand(@"Select KeyAddress From UserInfo Where EID=@EID");
                cmdfindkeyaddress.Connection = cn2;
                cmdfindkeyaddress.Parameters.AddWithValue("@EID", DropDownList9.Text);
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
                    Date2 = DateTime.Today.Year.ToString();
                    if (DateTime.Today.Month < 10)
                        Date2 += "0" + DateTime.Today.Month.ToString();
                    else Date2 += DateTime.Today.Month.ToString();
                    if (DateTime.Today.Day < 10)
                        Date2 += "0" + DateTime.Today.Day.ToString();
                    else
                        Date2 += DateTime.Today.Day.ToString();
                    byte[] content_txt_Ciphertext_Text = Encoding.UTF8.GetBytes(txt_Ciphertext_Text + Date2);
                    byte[] content_txt_Ciphertext_Proposition = Encoding.UTF8.GetBytes(txt_Ciphertext_Proposition + Date2);
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

                cmd3.Parameters.AddWithValue("@SID", Date);
                cmd3.Parameters.AddWithValue("@EID", DropDownList9.Text);
                cmd3.Parameters.AddWithValue("@Date", Date2);
                cmd3.Parameters.AddWithValue("@Text", txt_Ciphertext_Text);
                cmd3.Parameters.AddWithValue("@Title", "代理人同意函");
                cmd3.Parameters.AddWithValue("@Proposition", txt_Ciphertext_Proposition);
                cmd3.Parameters.AddWithValue("@Type", "代理人設定");
                cmd3.Parameters.AddWithValue("@YOS", "10");
                cmd3.Parameters.AddWithValue("@AESkey", txtKey);
                cmd3.Parameters.AddWithValue("@AESiv", txtIV);
                cmd3.Parameters.AddWithValue("@txt_RSAhash_Text", txt_RSAhash_Text);
                cmd3.Parameters.AddWithValue("@txt_RSAhash_Proposition", txt_RSAhash_Proposition);
                cmd3.Parameters.AddWithValue("@IsEnd", "0");
                cmd3.ExecuteNonQuery();
                //cmd4.ExecuteNonQuery();
            }
            using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                //找尋接收者PK並加密KEY
                SqlCommand cmduserInfo = new SqlCommand(@"select UserInfo.PK from UserInfo LEFT JOIN Detail ON UserInfo.EID=Detail.EID where (UserInfo.EID=@EID)");
                SqlCommand cmduserInfo2 = new SqlCommand(@"select UserInfo.PK from UserInfo LEFT JOIN Detail ON UserInfo.EID=Detail.EID where (UserInfo.EID=@EID)");
                cn3.Open();
                cmduserInfo.Connection = cn3;
                cmduserInfo.Connection = cn3;
                cmduserInfo.Parameters.AddWithValue("@EID", DropDownList9.Text);
                using (SqlDataReader dr = cmduserInfo.ExecuteReader())
                {
                    if (dr.Read())
                    {
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
                cn3.Open();
                cmduserInfo2.Parameters.AddWithValue("@EID", DropDownList10.Text);
                cmduserInfo2.Connection = cn3;
                using (SqlDataReader dr2 = cmduserInfo2.ExecuteReader())
                {
                    if (dr2.Read())
                    {
                        string PK = dr2["PK"].ToString();
                        //以接收者PK加密KEY
                        // 建立 RSA 演算法物件的執行個體，並匯入先前建立的公鑰
                        RSACryptoServiceProvider rsaProviderReceiver = new RSACryptoServiceProvider();
                        rsaProviderReceiver.FromXmlString(PK);

                        // 將資料加密
                        byte[] bytePlain = Encoding.UTF8.GetBytes(txtKey);
                        byte[] byteCipher = rsaProviderReceiver.Encrypt(bytePlain, false);

                        // 將加密後的資料，轉 Base64 格式輸入
                        txt_PKmessage2 = Convert.ToBase64String(byteCipher);
                    }
                }
                cn3.Close();
                //寫回資料庫                        
                SqlCommand cmd = new SqlCommand(@"Insert INTO Detail(SID,Lvl,EID,Department,status,sign,look,RSAkey)VALUES(@SID,@Lvl,@EID,@Department,@status,@sign,@look,@RSAkey)");
                SqlCommand cmd2 = new SqlCommand(@"Insert INTO Detail(SID,Lvl,EID,Department,status,sign,look,RSAkey)VALUES(@SID,@Lvl,@EID,@Department,@status,@sign,@look,@RSAkey)");
                cn3.Open();
                cmd.Connection = cn3;
                cmd.Parameters.AddWithValue("@SID", Date);
                cmd.Parameters.AddWithValue("@Lvl", "1");
                cmd.Parameters.AddWithValue("@EID", DropDownList9.Text);
                cmd.Parameters.AddWithValue("@Department", DropDownList1.Text);
                cmd.Parameters.AddWithValue("@status", "1");
                cmd.Parameters.AddWithValue("@RSAkey", txt_PKmessage);
                cmd.Parameters.AddWithValue("@look", 1);
                cmd.Parameters.AddWithValue("@sign", 1);
                cmd.ExecuteNonQuery();
                cn3.Close();
                cn3.Open();
                cmd2.Connection = cn3;
                cmd2.Parameters.AddWithValue("@SID", Date);
                cmd2.Parameters.AddWithValue("@Lvl", "1");
                cmd2.Parameters.AddWithValue("@EID", DropDownList10.Text);
                cmd2.Parameters.AddWithValue("@Department", DropDownList7.Text);
                cmd2.Parameters.AddWithValue("@status", "1");
                cmd2.Parameters.AddWithValue("@RSAkey", txt_PKmessage2);
                cmd2.Parameters.AddWithValue("@look", 1);
                cmd2.Parameters.AddWithValue("@sign", 0);
                cmd2.ExecuteNonQuery();
                cn3.Close();
                Response.Write("<script>alert('代理人同意函已發送!');location.href='SetAgent.aspx';</script>");

                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmdEID = new SqlCommand(@"Select agent From UserInfo  Where EID=@EID");
                    cmdEID.Connection = cn;
                    cmdEID.Parameters.AddWithValue("@EID", DropDownList2.Text);
                    using (SqlDataReader dr = cmdEID.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            using (SqlConnection cnUpate = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                cnUpate.Open();
                                SqlCommand cmd3 = new SqlCommand(@"Update tempAgentInfo Set agent=@agent,AgentName=@AgentName,StartTime=@StartTime,EndTime=@EndTime,send=@send,receive=@receive Where EID=@EID");
                                cmd3.Connection = cnUpate;
                                cmd3.Parameters.AddWithValue("@EID", DropDownList9.Text);
                                cmd3.Parameters.AddWithValue("@agent", DropDownList10.Text);
                                cmd3.Parameters.AddWithValue("@AgentName", DropDownList8.Text);
                                string date1 = Request.Form["d3"];
                                string date2 = Request.Form["d4"];
                                cmd3.Parameters.AddWithValue("@StartTime", date1);
                                cmd3.Parameters.AddWithValue("@EndTime", date2);
                                if (CheckBox1.Checked)
                                {
                                    cmd3.Parameters.AddWithValue("@send", 1);
                                }
                                else
                                {
                                    cmd3.Parameters.AddWithValue("@send", 0);
                                }
                                if (CheckBox2.Checked)
                                {
                                    cmd3.Parameters.AddWithValue("@receive", 1);
                                }
                                else
                                {
                                    cmd3.Parameters.AddWithValue("@receive", 0);
                                }
                                cmd3.ExecuteNonQuery();
                                cnUpate.Close();

                            }
                        }
                        else
                        {
                            using (SqlConnection cnUpate = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                cnUpate.Open();
                                SqlCommand cmd3 = new SqlCommand(@"Insert Into tempAgentInfo (EID,agent,AgentName,StartTime,EndTime,send,receive) Values (@EID,@agent,@AgentName,@StartTime,@EndTime,@send,@receive)");
                                cmd3.Connection = cnUpate;
                                cmd3.Parameters.AddWithValue("@EID", DropDownList9.Text);
                                cmd3.Parameters.AddWithValue("@agent", DropDownList10.Text);
                                cmd3.Parameters.AddWithValue("@AgentName", DropDownList8.Text);
                                string date1 = Request.Form["d3"];
                                string date2 = Request.Form["d4"];
                                cmd3.Parameters.AddWithValue("@StartTime", date1);
                                cmd3.Parameters.AddWithValue("@EndTime", date2);
                                if (CheckBox1.Checked)
                                {
                                    cmd3.Parameters.AddWithValue("@send", 1);
                                }
                                else
                                {
                                    cmd3.Parameters.AddWithValue("@send", 0);
                                }
                                if (CheckBox2.Checked)
                                {
                                    cmd3.Parameters.AddWithValue("@receive", 1);
                                }
                                else
                                {
                                    cmd3.Parameters.AddWithValue("@receive", 0);
                                }
                                cmd3.ExecuteNonQuery();
                                cnUpate.Close();
                            }
                        }
                    }
                }
            }

        }

        protected void DropDownList2_DataBound(object sender, EventArgs e)
        {
            DropDownList9.DataBind();
        }

        protected void DropDownList8_DataBound(object sender, EventArgs e)
        {
            DropDownList10.DataBind();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetAgent.aspx");
        }
    }
}