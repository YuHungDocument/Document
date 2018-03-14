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
    public partial class HostVote : System.Web.UI.Page
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
                ((LinkButton)this.Master.FindControl("Lb_Host")).BackColor = Color.White;
                ((LinkButton)this.Master.FindControl("Lb_Host")).ForeColor = Color.Black;
                ((Label)this.Master.FindControl("Lb_Title")).Text = "主辦投票";
                UserInfo tmpUserInfo = null;
                if (Session["userinfo"] is UserInfo)
                {
                    tmpUserInfo = (UserInfo)Session["userinfo"];
                    Lbl_EID.Text = tmpUserInfo.EID;
                    bind();
                }
            }
        }

        private DataTable GetData()
        {
            dt = new DataTable();

            string sqlstr = "Select * From Fil Where EID='" + Lbl_EID.Text + "' and Type='投票' order by SID desc";

            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);

            myda.Fill(myds, "Fil");

            Menu.DataSource = myds;
            Menu.DataBind();
            sqlcon.Close();
            myda.Fill(dt);
            return dt;
        }
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
        /// <summary>
        /// 換頁
        /// </summary>
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Menu.PageIndex = e.NewPageIndex;
            bind2();
        }
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
                    index = Menu.PageIndex + 1;
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
        //換分頁數
        protected void Change_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(TxtPageSize.Text) > 0)
            {

                Menu.PageSize = Convert.ToInt32(TxtPageSize.Text);
                bind();
            }
        }
        public void bind()
        {
            dt = new DataTable();
            string sqlstr = "Select * From Fil Where EID='" + Lbl_EID.Text + "' and Type='投票' order by SID desc";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            myda.Fill(myds, "Fil");
            Menu.DataSource = myds;
            myda.Fill(dt);
            Menu.DataBind();
            lbl_1.Text = Menu.PageSize.ToString();
            lbl_2.Text = dt.Rows.Count.ToString();
            lbl_3.Text = index.ToString();
            lbl_4.Text = Menu.PageCount.ToString();
            sqlcon.Close();
        }
        public void bind2()
        {
            dt = new DataTable();
            string date1 = Request.Form["d1"];
            string date2 = Request.Form["d2"];
            string date3 = Request.Form["d3"];
            string date4 = Request.Form["d4"];
            string searchingstr = "Select * From Fil Where EID='" + Lbl_EID.Text + "' and Type='投票'";
            string wherestr = null;
            string SID = Txt_SID.Text;
            string Type = Ddl_Type.SelectedValue;
            string Title = Txt_Title.Text;
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
            if (!string.IsNullOrWhiteSpace(Request.Form["d3"]))
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["d4"]))
                {
                    wherestr = wherestr + " and DeadLine Between'" + date3 + "'AND'" + date4 + "'";
                }
                else
                {
                    wherestr = wherestr + " and DeadLine >='" + date3 + "'";
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.Form["d4"]))
            {
                wherestr = wherestr + " and DeadLine <='" + date4 + "'";
            }
            if (!string.IsNullOrWhiteSpace(Txt_SID.Text))
            {
                wherestr = wherestr + " and ( SID Like'%" + SID + "%')";
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
            sda.Fill(ds, "Fil");
            Menu.DataSource = ds;
            Menu.EmptyDataText = "查無此項目";
            sda.Fill(dt);
            Menu.DataBind();
            lbl_1.Text = Menu.PageSize.ToString();
            lbl_2.Text = dt.Rows.Count.ToString();
            lbl_3.Text = index.ToString();
            lbl_4.Text = Menu.PageCount.ToString();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelData")
            {
                //這樣就可以讀到RowIndex
                int index = ((GridViewRow)
                ((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                //這樣就可以取得Keys值了
                string keyId = Menu.DataKeys[index].Value.ToString();

                Session["keyId"] = keyId;
                Response.Redirect("DocumentDetail.aspx");
            }
        }

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
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            LB.ToolTip = "公文文號：" + dr["SID"].ToString();
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

        protected void Btn_Select_Click(object sender, EventArgs e)
        {
            bind2();
        }

        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WaitProcess.aspx");
        }
    }
}