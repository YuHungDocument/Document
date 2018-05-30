using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class SetNT : System.Web.UI.Page
    {
        DbHelper tmpdbhelper = new DbHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["managerID"] == null)
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            string TN;
            TN = ((TextBox)GridView1.FooterRow.Cells[0].
                FindControl("tbTNFooter")).Text;
            /* 新增資料驗證作業 
               ...
               ...
               ...
            */

            /* 更新資料 */
            using (SqlConnection cn = new SqlConnection(tmpdbhelper.DB_CnStr))//使用using可以確保close
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();//設定一個叫cmd的命令實體
                SqlCommand lastTID = new SqlCommand("Select Max(TID)+1 as TIDLast From TypeGroup");
                lastTID.Connection = cn;
                using (SqlDataReader dr = lastTID.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Session["TID"] = int.Parse(dr["TIDLast"].ToString());
                        cmd.Parameters.AddWithValue("@TID", Session["TID"]);

                    }
                }

                cmd.Connection = cn;
                cmd.CommandText = @"Insert Into TypeGroup(Tp,TID,TN)VALUES(@Tp,@TID,@TN) ";
                cmd.Parameters.AddWithValue("@TN", TN);
                cmd.Parameters.AddWithValue("@Tp", "NT");



                cmd.ExecuteNonQuery();
                cn.Close();
                Response.Write("<script>alert('新增項目成功!');location.href='SetNT.aspx';</script>");
            }

        }
        protected void lbCancelSave_Click(object sender, EventArgs e)
        {
            GridView1.FooterRow.Visible = false;
        }
        protected void lbInsert_Click(object sender, EventArgs e)
        {
            GridView1.FooterRow.Visible = true;
        }
    }
}