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
    public partial class editbullitin : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
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
                    ((Label)this.Master.FindControl("Lb_Title")).Text = "公告列表";

                }
                bind();

            }

        }

        public void bind()
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
            bind();
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
                bind();
            }
        }

        public void bind2()
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bulletin.aspx");
        }
    }
}
