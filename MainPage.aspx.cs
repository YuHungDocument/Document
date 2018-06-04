using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class MainPage : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        int Wdc,Votec,HWdc,HVotec,EDocc,EVotec = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["userinfo"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "主頁面";
                    #region 內文
                    UserInfo tmpUserInfo = null;
                    if (Session["userinfo"] is UserInfo)
                    {
                        tmpUserInfo = (UserInfo)Session["userinfo"];
                        Lbl_EID.Text = tmpUserInfo.EID;
                        bind();
                    }
                    #endregion
                }
            }
        }
        public void bind()
        {
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text+ "' and Detail.look=1 and Detail.sign=0 and Fil.Type!='投票'");
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Wdc = int.Parse(Wdc.ToString()) + 1;
                    }
                }
                SqlCommand votecmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Detail.look=1 and Detail.sign=0 and Fil.Type='投票'");
                votecmd.Connection = cn;
                using (SqlDataReader dr = votecmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Votec = int.Parse(Votec.ToString()) + 1;
                    }
                }

                SqlCommand NewDccmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Detail.isread='0' and Fil.Type!='投票' and Detail.look!='0'");
                NewDccmd.Connection = cn;
                using (SqlDataReader dr = NewDccmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_DocNew.Visible = true;
                    }
                }

                SqlCommand NewVocmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Detail.isread='0' and Fil.Type='投票' and Detail.look!='0'");
                NewVocmd.Connection = cn;
                using (SqlDataReader dr = NewVocmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_VoteNew.Visible = true;
                    }
                }

                SqlCommand HDoccmd = new SqlCommand("Select * from Fil Where EID='" + Lbl_EID.Text + "' and Type!='投票' and IsEnd='0'");
                HDoccmd.Connection = cn;
                using (SqlDataReader dr = HDoccmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        HWdc = int.Parse(HWdc.ToString()) + 1;
                    }
                }

                SqlCommand NewHDocNew = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Fil.EID='" + Lbl_EID.Text + "' and Detail.EID='"+Lbl_EID.Text+"' and Detail.isread='0' and Fil.Type!='投票'");
                NewHDocNew.Connection = cn;
                using (SqlDataReader dr = NewHDocNew.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_HDocNew.Visible = true;
                    }
                }

                SqlCommand NewHVoteNew = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Fil.EID='" + Lbl_EID.Text + "' and Detail.EID='" + Lbl_EID.Text + "' and Detail.isread='0' and Fil.Type='投票'");
                NewHVoteNew.Connection = cn;
                using (SqlDataReader dr = NewHVoteNew.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_HVoteNew.Visible = true;
                    }
                }

                SqlCommand HVotecmd = new SqlCommand("Select * from Fil Where EID='" + Lbl_EID.Text + "' and Type='投票' and IsEnd='0'");
                HVotecmd.Connection = cn;
                using (SqlDataReader dr = HVotecmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        HVotec = int.Parse(HVotec.ToString()) + 1;
                    }
                }

                SqlCommand EDoccmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Fil.IsEnd=1 and Fil.Type!='投票'");
                EDoccmd.Connection = cn;
                using (SqlDataReader dr = EDoccmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EDocc = int.Parse(EDocc.ToString()) + 1;
                    }
                }

                SqlCommand EVotecmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Fil.IsEnd=1 and Fil.Type='投票'");
                EVotecmd.Connection = cn;
                using (SqlDataReader dr = EVotecmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EVotec = int.Parse(EVotec.ToString()) + 1;
                    }
                }
                Lbl_EndVote.Text = EVotec.ToString();
                Lbl_EndDoc.Text = EDocc.ToString();
                Lbl_Hvote.Text =HVotec.ToString();
                Lbl_Doc.Text = Wdc.ToString();
                Lbl_Vote.Text = Votec.ToString();
                Lbl_HDoc.Text = HWdc.ToString();
            }
        }
    }
}