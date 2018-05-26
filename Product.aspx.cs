using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(! Page.IsPostBack)
            {
                DataList1.DataBind();

            }
           
        }
        protected void TNLb_Click(object sender, EventArgs e)
        {
            DataListItem dli = (sender as LinkButton).NamingContainer as DataListItem;
            LinkButton BT = dli.FindControl("TNLb") as LinkButton;
            Session["BT"] = BT.Text;
            SqlDataSource1.SelectCommand = "SELECT * FROM [Product] Where [ProductType]='" + Session["BT"] + "'";
            Lbl_Type.Text = Session["BT"].ToString();

        }

        protected void TNALL_Click(object sender, EventArgs e)
        {
            Response.Redirect("product.aspx");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DataListItem dli = (sender as ImageButton).NamingContainer as DataListItem;
            Label PID = dli.FindControl("Lbl_PID") as Label;

            Session["PID"] = PID.Text;
            Response.Redirect("ProductDetail.aspx");
        }
    }
}