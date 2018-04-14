using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class WaitDocument : System.Web.UI.Page
    {
        int index = 1;
        private DataTable dt;
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userinfo"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                #region 內容
                ((LinkButton)this.Master.FindControl("Lb_WaitProcess")).BackColor = Color.White;
                ((LinkButton)this.Master.FindControl("Lb_WaitProcess")).ForeColor = Color.Black;
                ((Label)this.Master.FindControl("Lb_Title")).Text = "待處理公文";
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

        #region 獲取資料
        private DataTable GetData()
        {
            dt = new DataTable();

            string sqlstr = "Select Fil.Title,Fil.Type,Fil.Speed,Fil.Date,Fil.SID From Fil Left join Detail On Fil.SID = Detail.SID Where Detail.EID = '" + Lbl_EID.Text + "' and Detail.look = 1 and Detail.sign = 0 and Fil.Type != '投票' order by SID desc";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "Detail");

            Menu.DataSource = myds;
            Menu.DataBind();
            sqlcon.Close();
            myda.Fill(dt);
            return dt;
        }
        #endregion

        #region 取得排序資料
        /// <summary>
        /// 取得排序資料
        /// </summary>
        private void GVGetData(SortDirection pSortDirection,
            string pSortExpression)
        {
            DataTable _dt = GetData();

            string sSort = string.Empty;
            if (pSortDirection == SortDirection.Ascending)
            {
                sSort = string.Format("{0} {1}", pSortExpression, "DESC");
            }
            else
            {
                sSort = string.Format("{0} {1}", pSortExpression, "ASC");
            }

            DataView dv = _dt.DefaultView;
            dv.Sort = sSort;

            Menu.DataSource = dv;
            Menu.DataBind();

        }
        #endregion

        #region 換頁
        /// <summary>
        /// 換頁
        /// </summary>
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Menu.PageIndex = e.NewPageIndex;
            bind2();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        protected void Gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            string se = ViewState["se"] != null ?
                Convert.ToString(ViewState["se"]) : string.Empty;
            SortDirection sd = ViewState["sd"] != null ?
                (SortDirection)ViewState["sd"] : SortDirection.Ascending;

            if (string.IsNullOrEmpty(se))
            {
                se = e.SortExpression;
                sd = SortDirection.Ascending;
            }

            // 如果欄位與本來不同
            if (se != e.SortExpression)
            {
                // 切換為目前所指定欄位
                se = e.SortExpression;

                // 指定排列方式為升冪
                sd = SortDirection.Ascending;
            }
            // 如果欄位與本來相同
            else
            {
                // 切換升冪為降冪，降冪為升冪
                if (sd == SortDirection.Ascending)
                {
                    sd = SortDirection.Descending;
                }
                else
                    sd = SortDirection.Ascending;
            }

            // 紀錄欄位與排列方式 ( 升冪或降冪 )
            ViewState["se"] = se;
            ViewState["sd"] = sd;

            GVGetData(sd, se);
        }
        #endregion

        #region 分頁按鈕
        protected void Gridview_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                //新增LinkButton 
                Label label_Index = new Label();
                LinkButton Button_First = new LinkButton();
                LinkButton Button_Last = new LinkButton();
                LinkButton Button_Next = new LinkButton();
                LinkButton Button_Previous = new LinkButton();

                //第一頁
                Button_First.Text = "第一頁";
                Button_First.CommandName = "first";
                Button_First.ForeColor = Color.Blue;
                Button_First.Click += new EventHandler(PageButtonClick);
                Button_First.CausesValidation = false;

                //上一頁
                Button_Previous.Text = "上一頁";
                Button_Previous.CommandName = "previous";
                Button_Previous.ForeColor = Color.Blue;
                Button_Previous.Click += new EventHandler(PageButtonClick);
                Button_Previous.CausesValidation = false;

                //下一頁
                Button_Next.Text = "下一頁";
                Button_Next.CommandName = "next";
                Button_Next.ForeColor = Color.Blue;
                Button_Next.Click += new EventHandler(PageButtonClick);
                Button_Next.CausesValidation = false;

                //最後一頁
                Button_Last.Text = "最後一頁";
                Button_Last.CommandName = "last";
                Button_Last.ForeColor = Color.Blue;
                Button_Last.Click += new EventHandler(PageButtonClick);
                Button_Last.CausesValidation = false;


                e.Row.Controls[0].Controls[0].Controls[0].Controls[0].Controls.AddAt(0, (Button_First));
                e.Row.Controls[0].Controls[0].Controls[0].Controls[0].Controls.AddAt(1, (Button_Previous));

                int controlTmp = e.Row.Controls[0].Controls[0].Controls[0].Controls.Count - 1;
                e.Row.Controls[0].Controls[0].Controls[0].Controls[controlTmp].Controls.Add(Button_Next);
                e.Row.Controls[0].Controls[0].Controls[0].Controls[controlTmp].Controls.Add(Button_Last);
            }
        }
        #endregion

        #region 判斷按鈕類別
        protected void PageButtonClick(object sender, EventArgs e)
        {
            LinkButton clickedButton = ((LinkButton)sender);

            //第一頁
            if (clickedButton.CommandName == "first")
            {
                Menu.PageIndex = 0;
                index = 1;
            }

            //上一頁
            else if (clickedButton.CommandName == "previous")
            {
                if (Menu.PageIndex >= 1)
                {
                    Menu.PageIndex -= 1;
                    index =Menu.PageIndex+1;
                }
            }

            //下一頁
            else if (clickedButton.CommandName == "next")
            {
                if (Menu.PageIndex < Menu.PageCount - 1)
                {
                    Menu.PageIndex += 1;
                    index = Menu.PageIndex + 1;
                }
            }

            //最後一頁
            else if (clickedButton.CommandName == "last")
            {
                Menu.PageIndex = Menu.PageCount - 1;
                index = Menu.PageCount;
            }
            bind2();
        }
        #endregion

        #region 換分頁數
        protected void Change_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(TxtPageSize.Text) > 0)
            {

                Menu.PageSize = Convert.ToInt32(TxtPageSize.Text);
                bind();
            }
        }
        #endregion

        #region bind
        public void bind()
        {
            dt = new DataTable();
            string sqlstr = "Select  Fil.Title,Fil.Type,Fil.Speed,Fil.Date,Fil.SID From Fil Left join Detail On Fil.SID=Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Detail.look=1 and Detail.sign=0 and Fil.Type!='投票' order by SID desc";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            myda.Fill(myds, "Detail");
            Menu.DataSource = myds;
            myda.Fill(dt);
            Menu.DataBind();
            lbl_1.Text = Menu.PageSize.ToString();
            lbl_2.Text = dt.Rows.Count.ToString();
            lbl_3.Text = index.ToString();
            lbl_4.Text = Menu.PageCount.ToString();
            sqlcon.Close();
        }
        #endregion

        #region bind2
        public void bind2()
        {
            dt = new DataTable();
            string date1 = Request.Form["d1"];
            string date2 = Request.Form["d2"];
            string date3 = Request.Form["d3"];
            string date4 = Request.Form["d4"];
            string searchingstr = "Select  Fil.Title,Fil.Type,Fil.Speed,Fil.Date,Fil.SID From Fil Left join Detail On Fil.SID=Detail.SID Where Detail.EID=@EID and Detail.look=1 and Detail.sign=0 and Fil.Type!='投票'";
            string wherestr = null;
            string SID = Txt_SID.Text;
            string Type = Ddl_Type.SelectedValue;
            string Title = Txt_Title.Text;
            string Speed = Ddl_speed.SelectedValue;
            DataSet ds = new DataSet();
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = tmpdbhelper.DB_CnStr;
            scn.Open();
            SqlCommand scmd = new SqlCommand();
            scmd.Connection = scn;
            if (!string.IsNullOrWhiteSpace(Request.Form["d1"]))
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["d2"]))
                {
                    wherestr = " and Date Between'" + date1 + "'AND'" + date2 + "'";
                }
                else
                {
                    wherestr = " and Date >='" + date1 + "'";
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.Form["d2"]))
            {
                wherestr = " and Date <='" + date2 + "'";
            }

            if (!string.IsNullOrWhiteSpace(Txt_SID.Text))
            {
                wherestr = wherestr + " and ( SID Like'%" + SID + "%')";
            }

            if (Speed != "--請選擇速別--")
            {
                wherestr = wherestr + " and (Speed='" + Speed + "')";
            }

            if (Type != "--請選擇公文種類--")
            {
                wherestr = wherestr + " and (Type='" + Type + "')";
            }

            if (!string.IsNullOrWhiteSpace(Txt_Title.Text))
            {
                wherestr = wherestr + " and (Title Like'%" + Title + "%')";
            }

            scmd.CommandText = searchingstr + wherestr + " order by SID desc";
            scmd.Parameters.AddWithValue("@EID", Lbl_EID.Text);
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(ds, "Detail");
            Menu.DataSource = ds;
            Menu.EmptyDataText = "查無此項目";
            sda.Fill(dt);
            Menu.DataBind();
            lbl_1.Text = Menu.PageSize.ToString();
            lbl_2.Text = dt.Rows.Count.ToString();
            lbl_3.Text = index.ToString();
            lbl_4.Text = Menu.PageCount.ToString();
        }
        #endregion

        #region 點選進入Detail
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelData")
            {
                //這樣就可以讀到RowIndex
                int index = ((GridViewRow)
                ((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                //這樣就可以取得Keys值了
                string keyId = Menu.DataKeys[index].Value.ToString();
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update Detail set isread=1 where SID='"+ keyId + "' and EID='"+Lbl_EID.Text+"'");
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                }
                    Session["keyId"] = keyId;
                Response.Redirect("Detail.aspx");
            }
        }
        #endregion

        #region 判斷每一行然後做改變
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //這個if直接判斷gridview的每一列
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("OnMouseover", "this.style.background='#E1FFE1'");
                e.Row.Attributes.Add("OnMouseout", "this.style.background='#FFFFFF'");
                Label LB = (Label)e.Row.FindControl("Lbl_SID");
                Label Dep = (Label)e.Row.FindControl("Lbl_Dep");
                Label Peo = (Label)e.Row.FindControl("Lbl_Peo");
                using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Select * from Fil Where SID=@SID");
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@SID", LB.Text);
                    SqlCommand lookcmd = new SqlCommand("Select * From Detail Where isread!=1");
                    lookcmd.Connection = cn;
                    using (SqlDataReader dr = lookcmd.ExecuteReader())
                    {
                        if(dr.Read())
                        {
                            e.Row.Attributes.Add("style", "font-weight:bold");
                        }
                    }
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            LB.ToolTip = "公文文號：" + dr["SID"].ToString();
                            if (dr["Speed"].ToString() == "速件")
                            {
                                e.Row.Attributes.Add("OnMouseout", "this.style.background='#ADD8E6'");
                                e.Row.Attributes.Add("style", "background-color:#ADD8E6");
                            }

                            if (dr["Speed"].ToString() == "最速件")
                            {
                                e.Row.Attributes.Add("OnMouseout", "this.style.background='#FFCCCC'");
                                e.Row.Attributes.Add("style", "background-color:#FFCCCC");
                            }
                            #region 未來可能用到
                            //DateTime strDate = DateTime.Parse(dr["Date"].ToString());
                            //Date.Text = String.Format("{0:yyyy/MM/dd}", strDate);
                            //DateTime strDeadLine = DateTime.Parse(dr["DeadLine"].ToString());
                            //DeadLine.Text = String.Format("{0:yyyy/MM/dd}", strDeadLine);
                            //if (DateTime.Compare(DateTime.Parse(dr["DeadLine"].ToString()), DateTime.Now) < 0)
                            //{
                            //    e.Row.Attributes.Add("OnMouseout", "this.style.background='#F9B7C2'");
                            //    e.Row.Attributes.Add("style", "background-color:#F9B7C2");
                            //}
                            #endregion

                            using (SqlConnection cn2 = new SqlConnection(tmpdbhelper.DB_CnStr))
                            {
                                cn2.Open();
                                SqlCommand cmd2 = new SqlCommand("Select * from UserInfo Where EID=@EID");
                                cmd2.Connection = cn2;
                                cmd2.Parameters.AddWithValue("@EID", dr["EID"].ToString());
                                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                {
                                    if (dr2.Read())
                                    {
                                        Dep.Text = dr2["Department"].ToString();
                                        Peo.Text = dr2["Name"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 搜尋
        protected void Btn_Select_Click(object sender, EventArgs e)
        {
            bind2();
        }
        #endregion

        #region 顯示全部
        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WaitProcess.aspx");
        }
        #endregion
    }
}