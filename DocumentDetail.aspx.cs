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
    public partial class DocumentDetail : System.Web.UI.Page
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

                UserInfo tmpUserInfo = null;
                if (Session["userinfo"] is UserInfo)
                {
                    tmpUserInfo = (UserInfo)Session["userinfo"];
                    Lbl_EID.Text = tmpUserInfo.EID;
                    bind();
                    bind2();
                }
            }
        }
        //AES解密功能
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
                #region 判斷投票完成或者是不是投票類型公文
                SqlCommand cmd2 = new SqlCommand(@"Select * From Detail Where SID=@SID AND EID=@EID");
                cmd2.Connection = cn;
                cmd2.Parameters.AddWithValue("@SID", Session["keyId"].ToString());
                cmd2.Parameters.AddWithValue("@EID", Lbl_EID.Text);
                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                    if (dr2.Read())
                    {
                        if (dr2["sign"].ToString() != "0")

                        {
                            Pel_Choose.Visible = true;
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
                        Response.Write("<script>alert('解密失敗!');location.href='UserPage.aspx';</script>");
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
    }
}