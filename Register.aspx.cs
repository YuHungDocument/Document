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
    public partial class Register : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }
        static void SaveKey2File(string PrivateKeyrsaProviderReceiver, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            fs.Write(Encoding.UTF8.GetBytes(PrivateKeyrsaProviderReceiver), 0, PrivateKeyrsaProviderReceiver.Length);
            fs.Close();
            fs.Dispose();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Id.Text)
                && !string.IsNullOrWhiteSpace(Password.Text)
                && !string.IsNullOrWhiteSpace(ConfirmPassword.Text)
                && !string.IsNullOrWhiteSpace(UserName.Text)
                && !string.IsNullOrWhiteSpace(Email.Text)
                && Gender.SelectedIndex!=-1
                && Department.SelectedIndex != -1
                && position.SelectedIndex != -1
                && !string.IsNullOrWhiteSpace(Tel.Text)
                && !string.IsNullOrWhiteSpace(Cel.Text)
                && !string.IsNullOrWhiteSpace(Birthday.Text))
            //如果TextBox1跟TextBox2的text不是空白的跟DropDownList2的SelectedIndex不是-1就執行

            {
                if (Password.Text != ConfirmPassword.Text)
                {
                    Response.Write(" <script language=JavaScript> alert( ' 確認密碼不一致 '); </script> ");
                }
                else
                {
                    string tmpSQL = @"Insert Into UserInfo (EID,Name,UserID,Pwd,Gender,Email,Tel,Cel,Birthday,PK,Department,position,Permission) 
                                       Values (@EID,@Name,@UserID2,@Pwd,@Gender,@Email,@Tel,@Cel,@Birthday,@PK,@Department,@position,@Permission)";//建立SQL語法新增輸入的資料
                    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))//使用using可以確保close
                    {
                        SqlCommand cmd = new SqlCommand();//設定一個叫cmd的命令實體
                        cmd.Connection = cn;
                        cmd.CommandText = @"SELECT UserID FROM UserInfo WHERE UserID=@UserID";
                        cmd.Parameters.AddWithValue("@UserID", Id.Text);

                        cn.Open();
                        bool isExist = false;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                isExist = true;
                        }

                        if (!isExist)
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

                            //把物件的值抓進參數裡面↓
                            cmd.CommandText = tmpSQL;
                            cmd.Parameters.AddWithValue("@EID", DateTime.Now.ToString("yyyyMMddhhmmss"));
                            cmd.Parameters.AddWithValue("@Name", UserName.Text);
                            cmd.Parameters.AddWithValue("@UserID2", Id.Text);
                            cmd.Parameters.AddWithValue("@Pwd", Password.Text);
                            cmd.Parameters.AddWithValue("@Gender", Gender.SelectedValue);
                            cmd.Parameters.AddWithValue("@Email", Email.Text);
                            cmd.Parameters.AddWithValue("@Tel", Tel.Text);
                            cmd.Parameters.AddWithValue("@Cel", Cel.Text);
                            cmd.Parameters.AddWithValue("@Birthday", Birthday.Text);
                            cmd.Parameters.AddWithValue("@PK", txt_PublicKeyrsaProviderReceiver);
                            cmd.Parameters.AddWithValue("@Department", Department.SelectedValue);
                            cmd.Parameters.AddWithValue("@position", position.SelectedValue);
                            cmd.Parameters.AddWithValue("@Permission", "1");

                            cmd.ExecuteNonQuery();//執行命令
                            SaveKey2File(txt_PrivateKeyrsaProviderReceiver, @"D:\"+DateTime.Now.ToString("yyyyMMddhhmmss")+".txt");
                            Response.Write("<script>alert('帳戶建立成功!');location.href='Home.aspx';</script>");
                        }
                        else
                        {
                            Response.Write(" <script language=JavaScript> alert( ' 此帳戶已被申請 '); </script> ");
                            Id.Text = "";
                        }
                    }
                }
            }
            Response.Write(" <script language=JavaScript> alert( ' 尚有空的欄位未填寫 '); </script> ");
        }
    }
}