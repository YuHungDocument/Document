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
                Lbl_GpName.Text= Session["Lbl_GpName"].ToString();
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
                        ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep2")).Text = dr["Department"].ToString();
                        ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name2")).Text = dr["Name"].ToString();
                        if (AgentEID != "")
                        {
                            ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = AgentEID;
                        }
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
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep2")).Text = drna["Department"].ToString();
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name2")).Text = drna["Name"].ToString();
                                if (AgentEID != "")
                                {
                                    ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = AgentEID;
                                }

                            }
                            else
                            {
                                ((TextBox)GridView2.Rows[gvRowIndex].FindControl("Txt_EID")).Text = "";
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Dep2")).Text = "";
                                ((Label)GridView2.Rows[gvRowIndex].FindControl("Lbl_Name2")).Text = "";
                            }
                        }
                    }
                }

                cn.Close();
            }
        }

        protected void Txt_EID_TextChanged1(object sender, EventArgs e)
        {
            string EID= ((TextBox)GridView2.FooterRow.Cells[0].FindControl("Txt_EID2")).Text;
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * from UserInfo Where EID=@EID");
                SqlCommand cmdAgentEID = new SqlCommand("Select * from UserInfo Where EID=@EID");
                cmdAgentEID.Connection = cn;
                cmdAgentEID.Parameters.AddWithValue("@EID", EID);
                using (SqlDataReader dr = cmdAgentEID.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        AgentEID = dr["agent"].ToString();
                        if (AgentEID != "")
                        {
                            cmd.Parameters.AddWithValue("@EID", AgentEID);
                            ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Agent")).Visible = true;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@EID", EID);
                        }

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@EID", EID);
                    }

                }
                cn.Close();
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Dep3")).Text = dr["Department"].ToString();
                        ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Name3")).Text = dr["Name"].ToString();
                        if (AgentEID != "")
                        {
                            ((TextBox)GridView2.FooterRow.Cells[0].FindControl("Txt_EID2")).Text = AgentEID;
                        }
                    }
                    else
                    {
                        cn.Close();
                        cn.Open();
                        SqlCommand namemd = new SqlCommand("Select * from UserInfo Where Name=@Name");

                        SqlCommand cmdAgentName = new SqlCommand("Select agent from UserInfo Where Name=@Name");
                        cmdAgentName.Connection = cn;
                        cmdAgentName.Parameters.AddWithValue("@Name", EID);
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
                                    ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Agent")).Visible = true;
                                }
                                else
                                {
                                    namemd.Parameters.AddWithValue("@Name", EID);
                                }

                            }
                            else
                            {
                                namemd.Parameters.AddWithValue("@Name", EID);
                            }
                        }
                        cn.Close();
                        cn.Open();
                        namemd.Connection = cn;
                        using (SqlDataReader drna = namemd.ExecuteReader())
                        {
                            if (drna.Read())
                            {
                                ((TextBox)GridView2.FooterRow.Cells[0].FindControl("Txt_EID2")).Text = drna["EID"].ToString();
                                ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Dep3")).Text = drna["Department"].ToString();
                                ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Name3")).Text = drna["Name"].ToString();
                                if (AgentEID != "")
                                {
                                    ((TextBox)GridView2.FooterRow.Cells[0].FindControl("Txt_EID2")).Text = AgentEID;
                                }

                            }
                            else
                            {
                                ((TextBox)GridView2.FooterRow.Cells[0].FindControl("Txt_EID2")).Text = "";
                                ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Dep3")).Text = "";
                                ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Name3")).Text = "";
                            }
                        }
                    }
                }

                cn.Close();
            }
        }
        #endregion

        #region 刪除Gridview列和更新
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DelData")
            {
                int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                string keyId = GridView2.DataKeys[index].Value.ToString();
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand Maxcmd = new SqlCommand("Select Max(ID) as MaxID From UseGroup Where GID=@GID");
                    Maxcmd.Connection = cn;
                    Maxcmd.Parameters.AddWithValue("@GID", Lbl_GID.Text);
                    using (SqlDataReader dr = Maxcmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["MaxID"].ToString() == keyId)
                            {
                                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('最後一列無法刪除')", true);
                            }
                            else
                            {
                                using (SqlConnection Incn = new SqlConnection(tmpdbhelper.DB_CnStr))
                                {
                                    Incn.Open();
                                    SqlCommand cmd = new SqlCommand("Delete From UseGroup Where ID=@ID and GID=@GID");
                                    cmd.Connection = Incn;
                                    cmd.Parameters.AddWithValue("@ID", keyId);
                                    cmd.Parameters.AddWithValue("@GID", Lbl_GID.Text);
                                    cmd.ExecuteNonQuery();
                                    bind3();
                                }

                            }
                        }
                    }
                }

            }
            if(e.CommandName=="UpdateData")
            {
                int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                string keyId = GridView2.DataKeys[index].Value.ToString();
                string ID = ((Label)GridView2.Rows[index].FindControl("Lbl_ID")).Text;
                string Lvl = ((TextBox)GridView2.Rows[index].FindControl("Txt_Lvl")).Text;
                string EID = ((TextBox)GridView2.Rows[index].FindControl("Txt_EID")).Text;
                string Dep = ((Label)GridView2.Rows[index].FindControl("Lbl_Dep2")).Text;
                string Name = ((Label)GridView2.Rows[index].FindControl("Lbl_Name2")).Text;
                CheckBox status = ((CheckBox)GridView2.Rows[index].FindControl("Cb_sign"));
                CheckBox path = ((CheckBox)GridView2.Rows[index].FindControl("Cb_path"));
                CheckBox comment = ((CheckBox)GridView2.Rows[index].FindControl("Cb_comment"));
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update UseGroup Set Lvl=@Lvl,EID=@EID,Department=@Department,Name=@Name,status=@status,path=@path,Comment=@Comment Where ID=@ID and GID=@GID");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@Lvl", Lvl);
                    cmd.Parameters.AddWithValue("@EID", EID);
                    cmd.Parameters.AddWithValue("@Department", Dep);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@GID", Lbl_GID.Text);
                    if(status.Checked==true)
                    {
                        cmd.Parameters.AddWithValue("@status", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@status", "0");
                    }
                    if (path.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@path", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@path", "0");
                    }
                    if (comment.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Comment", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Comment", "0");
                    }
                    cmd.ExecuteNonQuery();
                }
                GridView2.EditIndex = -1;
                bind();
                
            }

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

        #region 點選編輯 
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string Lvl = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_Lvl")).Text;
            string EID = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_EID")).Text;
            string Dep = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_Dep")).Text;
            string Name = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_Name")).Text;
            string status = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_sign")).Text;
            string path = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_path")).Text;
            string comment = ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_comment")).Text;
            GridView2.EditIndex = e.NewEditIndex;
           
            bind();
            ((TextBox)GridView2.Rows[e.NewEditIndex].FindControl("Txt_Lvl")).Text = Lvl;
            ((TextBox)GridView2.Rows[e.NewEditIndex].FindControl("Txt_EID")).Text = EID;
            ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_Dep2")).Text = Dep;
            ((Label)GridView2.Rows[e.NewEditIndex].FindControl("Lbl_Name2")).Text = Name;
            if(status=="1")
            {
                ((CheckBox)GridView2.Rows[e.NewEditIndex].FindControl("Cb_sign")).Checked = true;
            }
            else
            {
                ((CheckBox)GridView2.Rows[e.NewEditIndex].FindControl("Cb_sign")).Checked = false;
            }

            if (path == "1")
            {
                ((CheckBox)GridView2.Rows[e.NewEditIndex].FindControl("Cb_path")).Checked = true;
            }
            else
            {
                ((CheckBox)GridView2.Rows[e.NewEditIndex].FindControl("Cb_path")).Checked = false;
            }

            if (comment == "1")
            {
                ((CheckBox)GridView2.Rows[e.NewEditIndex].FindControl("Cb_comment")).Checked = true;
            }
            else
            {
                ((CheckBox)GridView2.Rows[e.NewEditIndex].FindControl("Cb_comment")).Checked = false;
            }
        }
        #endregion

        #region 取消編輯
        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            bind();
        }
        #endregion

        #region 增加人員按鈕
        protected void Lb_Insert_Click(object sender, EventArgs e)
        {
            GridView2.FooterRow.Visible = true;
        }
        #endregion

        #region 新增人員
        protected void Lb_InsertMember_Click(object sender, EventArgs e)
        {
            string EID = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("Txt_EID2")).Text;
            string Dep = ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Dep3")).Text;
            string Name = ((Label)GridView2.FooterRow.Cells[0].FindControl("Lbl_Name3")).Text;
            string Lvl = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("Txt_Lvl2")).Text;
            CheckBox status = ((CheckBox)GridView2.FooterRow.Cells[0].FindControl("Cb_sign2"));
            CheckBox path = ((CheckBox)GridView2.FooterRow.Cells[0].FindControl("Cb_path2"));
            CheckBox comment = ((CheckBox)GridView2.FooterRow.Cells[0].FindControl("Cb_comment2"));
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            {
                cn.Open();
                int ID = 0;
                SqlCommand cmd = new SqlCommand();//設定一個叫cmd的命令實體
                SqlCommand lastTID = new SqlCommand("Select Max(ID)+1 as IDLast From UseGroup Where GID=@GID");
                lastTID.Connection = cn;
                lastTID.Parameters.AddWithValue("@GID", Lbl_GID.Text);
                using (SqlDataReader dr = lastTID.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ID = int.Parse(dr["IDLast"].ToString());
                        cmd.Parameters.AddWithValue("@ID", ID);

                    }
                }
                cmd.CommandText="Insert Into UseGroup (ID,GID,GpName,Lvl,EID,Name,Department,status,path,Comment) Values (@ID,@GID,@GpName,@Lvl,@EID,@Name,@Department,@status,@path,@Comment)";
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("GID",Lbl_GID.Text);
                cmd.Parameters.AddWithValue("Lvl", Lvl);
                cmd.Parameters.AddWithValue("EID", EID);
                cmd.Parameters.AddWithValue("Name", Name);
                cmd.Parameters.AddWithValue("Department", Dep);
                cmd.Parameters.AddWithValue("@GpName", Lbl_GpName.Text);
                if(status.Checked==true)
                {
                    cmd.Parameters.AddWithValue("status", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("status", "0");
                }
                if (path.Checked == true)
                {
                    cmd.Parameters.AddWithValue("path", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("path", "0");
                }
                if (comment.Checked == true)
                {
                    cmd.Parameters.AddWithValue("Comment", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("Comment", "0");
                }
                cmd.ExecuteNonQuery();
            }
            GridView2.FooterRow.Visible = false;
            bind();
        }
        #endregion

        #region 增加人員取消
        protected void Lb_Canel_Click(object sender, EventArgs e)
        {
            GridView2.FooterRow.Visible = false;
        }
        #endregion

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
            //    {
            //        cn.Open();
            //        SqlCommand cmd = new SqlCommand("Select * From UseGroup Where GID=@GID");
            //        cmd.Connection = cn;
            //        cmd.Parameters.AddWithValue("@GID", Lbl_GID.Text);
            //        Label sign = (Label)e.Row.FindControl("Lbl_sign");
            //        Label path = (Label)e.Row.FindControl("Lbl_path");
            //        Label comment = (Label)e.Row.FindControl("Lbl_comment");
            //        CheckBox status2 = (CheckBox)e.Row.FindControl("Cb_sign");
            //        CheckBox path2 = (CheckBox)e.Row.FindControl("Cb_path");
            //        CheckBox comment2 = (CheckBox)e.Row.FindControl("Cb_comment");
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            if (dr.Read())
            //            {
            //                if(status2.Text!=null)
            //                {
            //                    if (sign.Text == "1")
            //                    {
            //                        sign.Text = "是";
            //                    }
            //                    else
            //                    {
            //                        sign.Text = "否";
            //                    }
            //                }

            //                //if(sign.Text=="是")
            //                //{
            //                //    status2.Checked = true;
            //                //}
            //                //else
            //                //{
            //                //    status2.Checked = false;
            //                //}
            //                if (path2.Text != null)
            //                {
            //                    if (path.Text == "1")
            //                    {
            //                        path.Text = "是";
            //                    }
            //                    else
            //                    {
            //                        path.Text = "否";
            //                    }
            //                }

            //                //if (path.Text == "是")
            //                //{
            //                //    path2.Checked = true;
            //                //}
            //                //else
            //                //{
            //                //    path2.Checked = false;
            //                //}
            //                if(comment2.Text!=null)
            //                {
            //                    if (comment.Text == "1")
            //                    {
            //                        comment.Text = "是";
            //                    }
            //                    else
            //                    {
            //                        comment.Text = "否";
            //                    }
            //                }

            //                //if (comment.Text == "是")
            //                //{
            //                //    comment2.Checked = true;
            //                //}
            //                //else
            //                //{
            //                //    comment2.Checked = false;
            //                //}
            //            }
            //        }
            //    }
            //}
        }

        protected void Btn_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("Setgroup.aspx");
        }
    }
}