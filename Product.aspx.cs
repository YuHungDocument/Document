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

        }

        protected void TNALL_Click(object sender, EventArgs e)
        {
            Response.Redirect("product.aspx");
        }
    }
}