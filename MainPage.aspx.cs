using System;
using System.Collections.Generic;
using System.Data;
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
                bind3();
            }
        }
        public void bind3()
        {
            string sqlstr = "Select * from Bulletin Order by BID desc";
            SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
            SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlcon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
            myda.Fill(myds, "Bulletin");
            GridView1.DataSource = myds;
            GridView1.DataBind();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = GridView1.TopPagerRow;

            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("Ddl_Page");
            DropDownList TypeList = (DropDownList)pagerRow.Cells[0].FindControl("Ddl_Type");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("Lbl_View");
            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < GridView1.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    // If the ListItem object matches the currently selected
                    // page, flag the ListItem object as being selected. Because
                    // the DropDownList control is recreated each time the pager
                    // row gets created, this will persist the selected item in
                    // the DropDownList control.   
                    if (i == GridView1.PageIndex)
                    {
                        item.Selected = true;
                    }

                    // Add the ListItem object to the Items collection of the 
                    // DropDownList.
                    pageList.Items.Add(item);

                }

            }

            if (pageLabel != null)
            {

                // Calculate the current page number.
                int currentPage = GridView1.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = " 共 " + GridView1.PageCount.ToString() + " 頁 ";
            }

        }



        protected void Ddl_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Retrieve the pager row.
            GridViewRow pagerRow = GridView1.TopPagerRow;

            // Retrieve the PageDropDownList DropDownList from the pager row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("Ddl_Page");

            // Set the PageIndex property to display that page selected by the user.
            GridView1.PageIndex = pageList.SelectedIndex;
            bind3();
        }

        protected void Ddl_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Ddl_Type.SelectedValue != "全部部門")
            {
                string sqlstr = "Select * from Bulletin Where Dp='" + Ddl_Type.SelectedValue + "' Order by BID desc";

                SqlConnection sqlcon = new SqlConnection(tmpdbhelper.DB_CnStr);
                SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
                DataSet myds = new DataSet();
                sqlcon.Open();
                SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon);
                myda.Fill(myds, "Bulletin");
                GridView1.DataSource = myds;
                GridView1.DataBind();
            }
            else
            {
                bind3();
            }
        }

        public void bind4()
        {


        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            GridView grid = (GridView)sender;
            if (grid != null)
            {
                GridViewRow pagerRow = (GridViewRow)grid.TopPagerRow;
                if (pagerRow != null)
                {
                    pagerRow.Visible = true;
                }
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelData")
            {
                //這樣就可以讀到RowIndex
                int index = ((GridViewRow)
                ((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                //這樣就可以取得Keys值了
                string keyId = GridView1.DataKeys[index].Value.ToString();
                Session["BID"] = keyId;
                Response.Redirect("BulletinDetail.aspx");
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

                SqlCommand NewDccmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Detail.isread='0' and Fil.Type!='投票' and Detail.look!='0' and Detail.status='1'");
                NewDccmd.Connection = cn;
                using (SqlDataReader dr = NewDccmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Lbl_DocNew.Visible = true;
                    }
                }

                SqlCommand NewVocmd = new SqlCommand("Select * from Detail left join Fil On Fil.SID = Detail.SID Where Detail.EID='" + Lbl_EID.Text + "' and Detail.isread='0' and Fil.Type='投票' and Detail.look!='0' and Detail.status='1'");
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