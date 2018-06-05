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
    public partial class Forget : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Go_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * From UserInfo Where EID=@EID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EID", Txt_Num.Text);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        MailMessage msg = new MailMessage();
                        //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
                        msg.To.Add(string.Join(",", dr["Email"].ToString()));
                        msg.From = new MailAddress("yuhungsystem@gmail.com", "宇鴻科技", System.Text.Encoding.UTF8);
                        //郵件標題 
                        msg.Subject = "忘記密碼";
                        //郵件標題編碼  
                        msg.SubjectEncoding = System.Text.Encoding.UTF8;
                        //郵件內容
                        msg.Body = "您的密碼為：" + dr["Pwd"].ToString();
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

                        Response.Write("<script>alert('密碼已經寄送到您的信箱!請前往察看');location.href='Login.aspx';</script>");
                    }
                    else
                    {
                        cn.Close();
                        cn.Open();
                        SqlCommand cmd2 = new SqlCommand("Select * From UserInfo Where UserID=@UserID");
                        cmd2.Connection = cn;
                        cmd2.Parameters.AddWithValue("@UserID", Txt_Num.Text);
                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                        {
                            if (dr2.Read())
                            {
                                MailMessage msg = new MailMessage();
                                //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
                                msg.To.Add(string.Join(",", dr2["Email"].ToString()));
                                msg.From = new MailAddress("yuhungsystem@gmail.com", "宇鴻科技", System.Text.Encoding.UTF8);
                                //郵件標題 
                                msg.Subject = "忘記密碼";
                                //郵件標題編碼  
                                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                                //郵件內容
                                msg.Body = "您的密碼為：" + dr2["Pwd"].ToString();
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

                                Response.Write("<script>alert('密碼已經寄送到您的信箱!請前往察看');location.href='Login.aspx';</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('查無此工號或帳號')</script>");
                            }
                        }
                    }
                }


            }
        }
    }
}