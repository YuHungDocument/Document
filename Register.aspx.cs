using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        string verification;
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "註冊員工帳號";
                   
                }
            }
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }
        static void SaveKey2File(string PrivateKeyrsaProviderReceiver, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            fs.Write(Encoding.UTF8.GetBytes(PrivateKeyrsaProviderReceiver), 0, PrivateKeyrsaProviderReceiver.Length);
            fs.Close();
            fs.Dispose();            
        }

        public string CreateRandomCode(int Number)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < Number; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(Number);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
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
                    string tmpSQL = @"Insert Into UserInfo (EID,Name,UserID,Pwd,Gender,Email,Tel,Cel,Birthday,PK,Department,position,Permission,job,verification) 
                                       Values (@EID,@Name,@UserID2,@Pwd,@Gender,@Email,@Tel,@Cel,@Birthday,@PK,@Department,@position,@Permission,@job,@verification)";//建立SQL語法新增輸入的資料
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


                            verification = CreateRandomCode(10);
                            SHA256 sha256_1 = new SHA256CryptoServiceProvider();//建立一個SHA256
                            byte[] source_1 = Encoding.Default.GetBytes(verification.ToString());//將字串轉為Byte[]
                            byte[] crypto_1 = sha256_1.ComputeHash(source_1);//進行SHA256加密
                            string result_1 = Convert.ToBase64String(crypto_1);//把加密後的字串從Byte[]轉為字串
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
                            cmd.Parameters.AddWithValue("@PK", "");
                            cmd.Parameters.AddWithValue("@Department", Department.SelectedValue);
                            cmd.Parameters.AddWithValue("@position", position.SelectedValue);
                            cmd.Parameters.AddWithValue("@Permission", "1");
                            cmd.Parameters.AddWithValue("@job", "1");
                            cmd.Parameters.AddWithValue("@verification", result_1);
                            cmd.ExecuteNonQuery();//執行命令
                            

                            MailMessage msg = new MailMessage();
                            //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
                            msg.To.Add(Email.Text);
                            msg.From = new MailAddress("yuhungsystem@gmail.com", "系統通知", System.Text.Encoding.UTF8);
                            //郵件標題 
                            msg.Subject = "新進員工通知";
                            //郵件標題編碼  
                            msg.SubjectEncoding = System.Text.Encoding.UTF8;
                            //郵件內容
                            msg.Body = @"<p>您好!恭喜加入宇鴻自行車</p><p>公司將給予您一組公文專用的帳號密碼與私鑰(檔案位於附件)</p><p>您可透過此帳號密碼與私鑰進行公文處理動作</p><p>您的帳號："+Id.Text+"</p><p>密碼："+Password.Text+"</p><p>您的金鑰驗證碼為："+ verification.ToString() + "</p><p><a href='http://localhost:65241/verification.aspx'>點選此連結</a>)輸入驗證碼獲取金鑰</p>";
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

                            Response.Write("<script>alert('帳戶建立成功!');location.href='Register.aspx';</script>");
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