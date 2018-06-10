using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Net.Mail;

namespace WebApplication1
{
    public partial class SetGroupEdit : System.Web.UI.Page
    {
        #region 一連串宣告
        DbHelper tmpdbhelper = new DbHelper();
        string txtKey;
        string txtIV;
        string txt_Ciphertext_Text;
        string txt_Ciphertext_Proposition;
        string txt_Ciphertext_DocumentContent;
        string txt_PKmessage;
        private SqlConnection connection;
        private SqlCommand command;
        string AgentEID;
        string AgentName;
        string listmail;
        string KeyAddress;
        string txt_RSAhash_Text;
        string txt_RSAhash_Proposition;
        int IDcount = 0;
        int IDGO = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ((LinkButton)this.Master.FindControl("Lb_Set")).BackColor = Color.White;
                ((LinkButton)this.Master.FindControl("Lb_Set")).ForeColor = Color.Black;
                ((Label)this.Master.FindControl("Lb_Title")).Text = "群組設定";
                Txt_GpName.Text = Session["Lbl_GpName"].ToString();
                Lbl_GID.Text = Session["Lbl_GID"].ToString();
                bind();
            }
        }

        #region bind
        public void bind()
        {
            string sqlstr = "select * from UseGroup Where GID='" + Lbl_GID.Text + "'";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "UseGroup");

            GridView2.DataSource = myds;
            GridView2.DataBind();
            sqlcon.Close();

            //using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
            //{
            //    cn3.Open();
            //    SqlCommand cmd3 = new SqlCommand("Select * from UseGroup Where GID='" + Lbl_GID.Text + "'");
            //    cmd3.Connection = cn3;
            //    using (SqlDataReader dr2 = cmd3.ExecuteReader())
            //    {
            //        int CountPre = 0;
            //        while (dr2.Read())
            //        {

            //            ((TextBox)GridView2.Rows[CountPre].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
            //            ((TextBox)GridView2.Rows[CountPre].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
            //            ((Label)GridView2.Rows[CountPre].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
            //            ((Label)GridView2.Rows[CountPre].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
            //            CheckBox Cb_sign = ((CheckBox)GridView2.Rows[CountPre].FindControl("Cb_sign"));
            //            CheckBox Cb_path = ((CheckBox)GridView2.Rows[CountPre].FindControl("Cb_path"));
            //            CheckBox Cb_comment = ((CheckBox)GridView2.Rows[CountPre].FindControl("Cb_comment"));
            //            if (dr2["status"].ToString() == "1")
            //            {
            //                Cb_sign.Checked = true;
            //            }
            //            if (dr2["path"].ToString() == "1")
            //            {
            //                Cb_path.Checked = true;
            //            }
            //            if (dr2["Comment"].ToString() == "1")
            //            {
            //                Cb_comment.Checked = true;
            //            }
            //            CountPre = int.Parse(CountPre.ToString()) + 1;
            //        }
            //    }

            //}
        }
        #endregion

        #region 改變Gridview的Lvl(層級)時做的Update舊的可能會用到
        protected void Txt_Lvl_TextChanged(object sender, EventArgs e)
        {
            //TextBox curTextBox = (TextBox)sender;
            //int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            //string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            //TextBox Txt_Lvl = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_Lvl"));
            //using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            //{
            //    cn2.Open();
            //    SqlCommand cmd2 = new SqlCommand("Update UseGroup set Lvl=@Lvl Where ID=@ID  and GID=@GID");
            //    cmd2.Parameters.AddWithValue("@Lvl", Txt_Lvl.Text);
            //    cmd2.Parameters.AddWithValue("@GID",Lbl_GID.Text);
            //    cmd2.Parameters.AddWithValue("@ID", ID);
            //    cmd2.Connection = cn2;
            //    cmd2.ExecuteNonQuery();
            //}

            //string Lvl = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_Lvl")).Text.Trim();
            //string EID = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_EID")).Text.Trim();
            //if (Lvl != "" || EID != "")
            //{
            //    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            //    {
            //        cn.Open();
            //        SqlCommand cmd = new SqlCommand("Insert Into Preview(SID,EID) Values(@SID,@EID)");
            //        cmd.Connection = cn;
            //        cmd.Parameters.AddWithValue("@EID", "");
            //        cmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
            //        cmd.ExecuteNonQuery();
            //        bind3();
            //        using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
            //        {
            //            cn3.Open();
            //            SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "' and EID!='" + Lbl_EID.Text + "'");
            //            cmd3.Connection = cn3;
            //            using (SqlDataReader dr2 = cmd3.ExecuteReader())
            //            {
            //                int i = 0;
            //                while (dr2.Read())
            //                {

            //                    ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
            //                    ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
            //                    ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
            //                    ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
            //                    if (dr2["status"].ToString() == "1")
            //                    {
            //                        ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = true;
            //                    }
            //                    else
            //                    {
            //                        ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = false;
            //                    }

            //                    if (dr2["path"].ToString() == "1")
            //                    {
            //                        ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = true;
            //                    }
            //                    else
            //                    {
            //                        ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = false;
            //                    }
            //                    if (dr2["Comment"].ToString() == "1")
            //                    {
            //                        ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = true;
            //                    }
            //                    else
            //                    {
            //                        ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = false;
            //                    }
            //                    i = int.Parse(i.ToString()) + 1;
            //                }
            //            }
            //        }
            //    }
            //}
        }
        #endregion

        #region Gridview的輸入EID或名字找到人
        protected void Txt_EID_TextChanged(object sender, EventArgs e)
        {
            TextBox curTextBox = (TextBox)sender;
            int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
            TextBox UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID"));
            string GID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_ID")).Text.Trim();
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * from UserInfo Where EID=@EID");
                SqlCommand cmdAgentEID = new SqlCommand("Select * from UserInfo Where EID=@EID");
                cmdAgentEID.Connection = cn;
                cmdAgentEID.Parameters.AddWithValue("@EID", UserEID.Text);
                using (SqlDataReader dr = cmdAgentEID.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        AgentEID = dr["agent"].ToString();
                        if (AgentEID != "")
                        {
                            cmd.Parameters.AddWithValue("@EID", AgentEID);
                            ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Agent")).Visible = true;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@EID", UserEID.Text);
                        }

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@EID", UserEID.Text);
                    }

                }
                cn.Close();
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text = dr["Department"].ToString();
                        ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text = dr["Name"].ToString();
                        if (AgentEID != "")
                        {
                            ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = AgentEID;
                        }
                        //using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                        //{
                        //    cn2.Open();
                        //    SqlCommand cmd2 = new SqlCommand("Update UseGroup set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name Where ID=@ID");
                        //    cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_Lvl")).Text);
                        //    cmd2.Parameters.AddWithValue("@Department", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text);
                        //    cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text);
                        //    cmd2.Parameters.AddWithValue("@Name", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text);
                        //    cmd2.Parameters.AddWithValue("@ID", ID);
                        //    cmd2.Connection = cn2;
                        //    cmd2.ExecuteNonQuery();

                        //}
                    }
                    else
                    {
                        cn.Close();
                        cn.Open();
                        SqlCommand namemd = new SqlCommand("Select * from UserInfo Where Name=@Name");

                        SqlCommand cmdAgentName = new SqlCommand("Select agent from UserInfo Where Name=@Name");
                        cmdAgentName.Connection = cn;
                        cmdAgentName.Parameters.AddWithValue("@Name", UserEID.Text);
                        using (SqlDataReader drname = cmdAgentName.ExecuteReader())
                        {
                            if (drname.Read())
                            {
                                AgentEID = drname["agent"].ToString();
                                if (AgentEID != "")
                                {
                                    using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                    {
                                        SqlCommand cmdAgentName2 = new SqlCommand("Select Name from UserInfo Where EID=@EID");
                                        cmdAgentName2.Parameters.AddWithValue("@EID", AgentEID);
                                        cmdAgentName2.Connection = cn2;
                                        cn2.Open();
                                        using (SqlDataReader dr2 = cmdAgentName2.ExecuteReader())
                                        {

                                            if (dr2.Read())
                                            {
                                                AgentName = dr2["Name"].ToString();

                                            }
                                        }
                                        cn2.Close();
                                    }
                                    namemd.Parameters.AddWithValue("@Name", AgentName);
                                    ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Agent")).Visible = true;
                                }
                                else
                                {
                                    namemd.Parameters.AddWithValue("@Name", UserEID.Text);
                                }

                            }
                            else
                            {
                                namemd.Parameters.AddWithValue("@Name", UserEID.Text);
                            }
                        }
                        cn.Close();
                        cn.Open();
                        namemd.Connection = cn;
                        using (SqlDataReader drna = namemd.ExecuteReader())
                        {
                            if (drna.Read())
                            {
                                ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = drna["EID"].ToString();
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text = drna["Department"].ToString();
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text = drna["Name"].ToString();
                                if (AgentEID != "")
                                {
                                    ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = AgentEID;
                                }
                                using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                                {
                                    CheckBox ck = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_sign"));
                                    CheckBox Cb_path = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_path"));
                                    cn3.Open();
                                    SqlCommand cmd2 = new SqlCommand("Update Preview set Lvl=@Lvl,Department=@Department,EID=@EID,Name=@Name,status=@status,@path=path Where ID=@ID");
                                    cmd2.Parameters.AddWithValue("@Lvl", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_Lvl")).Text);
                                    cmd2.Parameters.AddWithValue("@Department", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text);
                                    cmd2.Parameters.AddWithValue("@EID", ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text);
                                    cmd2.Parameters.AddWithValue("@Name", ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text);
                                    if (ck.Checked == true)
                                    {
                                        cmd2.Parameters.AddWithValue("@status", "1");
                                    }
                                    else
                                    {
                                        cmd2.Parameters.AddWithValue("@status", "0");
                                    }
                                    if (Cb_path.Checked == true)
                                    {
                                        cmd2.Parameters.AddWithValue("@path", "1");
                                    }
                                    else
                                    {
                                        cmd2.Parameters.AddWithValue("@path", "0");
                                    }
                                    cmd2.Parameters.AddWithValue("@ID", ID);
                                    cmd2.Connection = cn3;
                                    cmd2.ExecuteNonQuery();

                                }
                            }
                            else
                            {
                                ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = "";
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep")).Text = "";
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name")).Text = "";
                            }
                        }
                    }
                }

                cn.Close();
            }
            string Lvl = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_Lvl")).Text.Trim();
            string EID = ((TextBox)GridView2.Rows[GridView2.Rows.Count - 1].FindControl("Txt_EID")).Text.Trim();
            if (EID != "")
            {
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into UseGroup(ID,GID,EID) Values(@ID,@GID,@EID)");
                    cmd.Connection = cn;
                    SqlCommand cmdcount = new SqlCommand("Select Max(ID) as IDcount From Preview");
                    cmdcount.Connection = cn;
                    using (SqlDataReader dr = cmdcount.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            IDcount = int.Parse(dr["IDcount"].ToString());
                        }
                        IDcount = int.Parse(IDcount.ToString()) + 1;
                    }
                    cmd.Parameters.AddWithValue("@ID", IDcount);
                    cmd.Parameters.AddWithValue("@EID", "");
                    cmd.Parameters.AddWithValue("@GID",Lbl_GID.Text);
                    cmd.ExecuteNonQuery();
                    bind3();
                    using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
                    {
                        cn3.Open();
                        SqlCommand cmd3 = new SqlCommand("Select * from UseGroup Where GID='" + Lbl_GID.Text + "'");
                        cmd3.Connection = cn3;
                        using (SqlDataReader dr2 = cmd3.ExecuteReader())
                        {
                            int i = 0;
                            while (dr2.Read())
                            {

                                ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
                                ((TextBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
                                ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
                                ((Label)GridView2.Rows[int.Parse(i.ToString())].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
                                if (dr2["status"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_sign")).Checked = false;
                                }

                                if (dr2["path"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_path")).Checked = false;
                                }
                                if (dr2["Comment"].ToString() == "1")
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)GridView2.Rows[int.Parse(i.ToString())].FindControl("Cb_comment")).Checked = false;
                                }
                                i = int.Parse(i.ToString()) + 1;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 勾選path時發生變化
        protected void Cb_path_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            CheckBox ckp = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_path"));
            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn2.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set path=@path Where ID=@ID");

                if (ckp.Checked == true)
                {
                    cmd2.Parameters.AddWithValue("@path", "1");
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@path", "0");
                }
                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn2;
                cmd2.ExecuteNonQuery();
            }
        }

        //protected void Cb_path_CheckedChanged1(object sender, EventArgs e)
        //{
        //    CheckBox CheckBox = (CheckBox)sender;
        //    int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
        //    string ID = ((Label)GridView5.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
        //    CheckBox ckp = ((CheckBox)GridView5.Rows[gvRowIndex].FindControl("Cb_path"));
        //    using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
        //    {
        //        cn2.Open();
        //        SqlCommand cmd2 = new SqlCommand("Update Preview set path=@path Where ID=@ID");

        //        if (ckp.Checked == true)
        //        {
        //            cmd2.Parameters.AddWithValue("@path", "1");
        //        }
        //        else
        //        {
        //            cmd2.Parameters.AddWithValue("@path", "0");
        //        }
        //        cmd2.Parameters.AddWithValue("@ID", ID);
        //        cmd2.Connection = cn2;
        //        cmd2.ExecuteNonQuery();
        //    }
        //}
        #endregion

        #region 勾選comment時發生變化
        protected void Cb_comment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
            string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            CheckBox ck = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_comment"));
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand("Update Preview set Comment=@Comment Where ID=@ID");
                if (ck.Checked == true)
                {
                    cmd2.Parameters.AddWithValue("@Comment", "1");
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@Comment", "0");
                }
                cmd2.Parameters.AddWithValue("@ID", ID);
                cmd2.Connection = cn;
                cmd2.ExecuteNonQuery();
            }
        }

        //protected void Cb_comment_CheckedChanged1(object sender, EventArgs e)
        //{
        //    CheckBox CheckBox = (CheckBox)sender;
        //    int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
        //    string ID = ((Label)GridView5.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
        //    CheckBox ck = ((CheckBox)GridView5.Rows[gvRowIndex].FindControl("Cb_comment"));
        //    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
        //    {
        //        cn.Open();
        //        SqlCommand cmd2 = new SqlCommand("Update Preview set Comment=@Comment Where ID=@ID");
        //        if (ck.Checked == true)
        //        {
        //            cmd2.Parameters.AddWithValue("@Comment", "1");
        //        }
        //        else
        //        {
        //            cmd2.Parameters.AddWithValue("@Comment", "0");
        //        }
        //        cmd2.Parameters.AddWithValue("@ID", ID);
        //        cmd2.Connection = cn;
        //        cmd2.ExecuteNonQuery();
        //    }
        //}
        #endregion

        #region 勾選sign時發生變化
        protected void Cb_sign_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox CheckBox = (CheckBox)sender;
            //int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
            //string UserEID = ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text.Trim();
            //string ID = ((Label)GridView2.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
            //CheckBox ck = ((CheckBox)GridView2.Rows[gvRowIndex].FindControl("Cb_sign"));
            //using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
            //{
            //    cn2.Open();
            //    SqlCommand cmd2 = new SqlCommand("Update Preview set status=@status Where ID=@ID");
            //    if (ck.Checked == true)
            //    {
            //        cmd2.Parameters.AddWithValue("@status", "1");
            //    }
            //    else
            //    {
            //        cmd2.Parameters.AddWithValue("@status", "0");
            //    }
            //    cmd2.Parameters.AddWithValue("@ID", ID);
            //    cmd2.Connection = cn2;
            //    cmd2.ExecuteNonQuery();
            //}
        }
        //protected void Cb_sign_CheckedChanged1(object sender, EventArgs e)
        //{
        //    CheckBox CheckBox = (CheckBox)sender;
        //    int gvRowIndex = (CheckBox.NamingContainer as GridViewRow).RowIndex;
        //    string ID = ((Label)GridView5.Rows[gvRowIndex].FindControl("Label1")).Text.Trim();
        //    CheckBox ck = ((CheckBox)GridView5.Rows[gvRowIndex].FindControl("Cb_sign"));
        //    using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
        //    {
        //        cn2.Open();
        //        SqlCommand cmd2 = new SqlCommand("Update Preview set status=@status Where ID=@ID");
        //        if (ck.Checked == true)
        //        {
        //            cmd2.Parameters.AddWithValue("@status", "1");
        //        }
        //        else
        //        {
        //            cmd2.Parameters.AddWithValue("@status", "0");
        //        }
        //        cmd2.Parameters.AddWithValue("@ID", ID);
        //        cmd2.Connection = cn2;
        //        cmd2.ExecuteNonQuery();
        //    }
        //}
        #endregion

        #region 刪除Gridview列
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "DelData")
            //{
            //    int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            //    string keyId = GridView2.DataKeys[index].Value.ToString();
            //    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            //    {
            //        cn.Open();
            //        SqlCommand Maxcmd = new SqlCommand("Select Max(ID) as MaxID From Preview Where SID=@SID");
            //        Maxcmd.Connection = cn;
            //        Maxcmd.Parameters.AddWithValue("@SID", Lbl_SID.Text);
            //        using (SqlDataReader dr = Maxcmd.ExecuteReader())
            //        {
            //            if (dr.Read())
            //            {
            //                if (dr["MaxID"].ToString() == keyId)
            //                {
            //                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('最後一列無法刪除')", true);
            //                }
            //                else
            //                {
            //                    using (SqlConnection Incn = new SqlConnection(tmpdbhelper.DB_CnStr))
            //                    {
            //                        Incn.Open();
            //                        SqlCommand cmd = new SqlCommand("Delete From Preview Where ID=@ID");
            //                        cmd.Connection = Incn;
            //                        cmd.Parameters.AddWithValue("@ID", keyId);
            //                        cmd.ExecuteNonQuery();
            //                        bind3();
            //                    }

            //                }
            //            }
            //        }

            //    }

            //    using (SqlConnection cn3 = new SqlConnection(tmpdbhelper.DB_CnStr))
            //    {
            //        cn3.Open();
            //        SqlCommand cmd3 = new SqlCommand("Select * from Preview Where SID='" + Lbl_SID.Text + "' and EID!='" + Lbl_EID.Text + "'");
            //        cmd3.Connection = cn3;
            //        using (SqlDataReader dr2 = cmd3.ExecuteReader())
            //        {
            //            int CountPre = 0;
            //            while (dr2.Read())
            //            {

            //                ((TextBox)GridView2.Rows[CountPre].FindControl("Txt_Lvl")).Text = dr2["Lvl"].ToString();
            //                ((TextBox)GridView2.Rows[CountPre].FindControl("Txt_EID")).Text = dr2["EID"].ToString();
            //                ((Label)GridView2.Rows[CountPre].FindControl("Lbl_Dep")).Text = dr2["Department"].ToString();
            //                ((Label)GridView2.Rows[CountPre].FindControl("Lbl_Name")).Text = dr2["Name"].ToString();
            //                CheckBox Cb_sign = ((CheckBox)GridView2.Rows[CountPre].FindControl("Cb_sign"));
            //                CheckBox Cb_path = ((CheckBox)GridView2.Rows[CountPre].FindControl("Cb_path"));
            //                CheckBox Cb_comment = ((CheckBox)GridView2.Rows[CountPre].FindControl("Cb_comment"));
            //                if (dr2["status"].ToString() == "1")
            //                {
            //                    Cb_sign.Checked = true;
            //                }
            //                if (dr2["path"].ToString() == "1")
            //                {
            //                    Cb_path.Checked = true;
            //                }
            //                if (dr2["Comment"].ToString() == "1")
            //                {
            //                    Cb_comment.Checked = true;
            //                }
            //                CountPre = int.Parse(CountPre.ToString()) + 1;
            //            }
            //        }

            //    }
            //}
        }
        #endregion

        #region bind3
        public void bind3()
        {
            string sqlstr = "select * from UseGroup Where GID='" + Lbl_GID.Text + "'";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "UseGroup");

            GridView2.DataSource = myds;
            GridView2.DataBind();
            sqlcon.Close();
        }
        #endregion

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            bind();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = Convert.ToInt32(e.Keys);
            string ID =((Label)GridView2.Rows[i].FindControl("Lbl_ID")).Text;
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Update From UseGroup Where ID=@ID and GID=@GID");
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ID",ID);
                cmd.Parameters.AddWithValue("@GID",Lbl_GID.Text);
            }
        }
    }
}