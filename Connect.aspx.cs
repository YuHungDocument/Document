using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Connect2 : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected void Btn_Send_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                if (Dp_Title.SelectedIndex == 0 || Txt_Name.Text == "" || Txt_Tel.Text == "" || Txt_mail.Text == "" || Txt_message.Text == "")
                {
                    Response.Write(" <script language=JavaScript> alert( ' 尚有必填資料(*)未填，請再次確認! '); </script> ");
                }
                else
                {
                    MailMessage msg = new MailMessage();
                    //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
                    msg.To.Add(string.Join(",", "yuhungsystem@gmail.com"));
                    msg.From = new MailAddress(Txt_mail.Text, Txt_mail.Text, System.Text.Encoding.UTF8);
                    //郵件標題 
                    msg.Subject = Dp_Title.SelectedValue;
                    //郵件標題編碼  
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;
                    //郵件內容
                    msg.Body = "<p>姓名:" + Txt_Name.Text + "</p>" + "<p>公司:" + Txt_Company.Text + "</p>" + "<p>電話:" + Txt_Tel.Text + "</p>" + "<p>訊息:" + Txt_message.Text + "</p>";
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


                    Response.Write("<script>alert('已成功送出，我們將盡快回復您的問題!');location.href='Connect.aspx';</script>");
                }
            }
        }
    }
}