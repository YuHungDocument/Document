using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class verification : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Send_Click(object sender, EventArgs e)
        {
            SHA256 sha256_1 = new SHA256CryptoServiceProvider();//建立一個SHA256
            byte[] source_1 = Encoding.Default.GetBytes(TextBox1.Text);//將字串轉為Byte[]
            byte[] crypto_1 = sha256_1.ComputeHash(source_1);//進行SHA256加密
            string result_1 = Convert.ToBase64String(crypto_1);//把加密後的字串從Byte[]轉為字串
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From UserInfo Where verification=@verification");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@verification",result_1);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        // 建立 RSA 演算法物件的執行個體
                        RSACryptoServiceProvider rsaProviderReceiver = new RSACryptoServiceProvider();

                        // 匯出金鑰組。 
                        RSAParameters publicKeyReceiver = rsaProviderReceiver.ExportParameters(false);    //匯出金鑰，參數 false 表示不含私鑰。
                        RSAParameters privateKeyReceiver = rsaProviderReceiver.ExportParameters(true);    //匯出金鑰

                        // 以 XML 格式匯出金鑰組。 
                        string txt_PublicKeyrsaProviderReceiver = rsaProviderReceiver.ToXmlString(false);              //以 XML 格式匯出金鑰，參數 false 表示不含私鑰。
                        string txt_PrivateKeyrsaProviderReceiver = rsaProviderReceiver.ToXmlString(true);              //以 XML 格式匯出金鑰
                                                                                                                       // 建立 CspParameters 物件
                        CspParameters cspParaReceiver = new CspParameters();

                        // 指定 KeyContainerName
                        cspParaReceiver.KeyContainerName = "AsymmetricExampleReceiver";

                        // 建立 RSA 演算法物件的執行個體
                        rsaProviderReceiver = new RSACryptoServiceProvider(cspParaReceiver);

                        // 設定 PersistKeyInCsp 屬性為 true。
                        rsaProviderReceiver.PersistKeyInCsp = true;       // 指出金鑰是否應保存 (Persist) 在密碼編譯服務供應者 (CSP) 中。
                                                                          // 上面最後一行程式碼，.NET Framework 會自動建立金鑰容器並儲存金鑰。
                                                                          // 若再度執行相同的應用程式，.NET Framework 會偵測同名的金鑰容器是否已存，若存在，則取出存於其中的私密金鑰
                        SaveKey2File(txt_PrivateKeyrsaProviderReceiver, @"D:\" + dr["EID"].ToString() + ".txt");
                        using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                        {
                            cn2.Open();
                            SqlCommand cmd2 = new SqlCommand("Update UserInfo Set PK=@PK Where EID=@EID and verification=@verification");
                            cmd2.Connection = cn2;
                            cmd2.Parameters.AddWithValue("@EID",dr["EID"].ToString());
                            cmd2.Parameters.AddWithValue("@verification", result_1);
                            cmd2.Parameters.AddWithValue("@PK", txt_PublicKeyrsaProviderReceiver);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    Response.Write("<script>alert('金鑰建立成功!可至D槽查看');location.href='Home.aspx';</script>");
                }
            }


        }

        static void SaveKey2File(string PrivateKeyrsaProviderReceiver, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            fs.Write(Encoding.UTF8.GetBytes(PrivateKeyrsaProviderReceiver), 0, PrivateKeyrsaProviderReceiver.Length);
            fs.Close();
            fs.Dispose();
        }
    }
}