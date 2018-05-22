using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Inset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductAdd.aspx");
        }

        protected void Lb_Edit_Click(object sender, EventArgs e)
        {
            DataListItem dli = (sender as LinkButton).NamingContainer as DataListItem;
            Label PID = dli.FindControl("Lbl_ID") as Label;

            Session["PID"] = PID.Text;
            Response.Redirect("ProductView.aspx");
        }
    }
}