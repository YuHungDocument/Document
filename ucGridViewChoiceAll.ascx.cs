using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ucGridViewChoiceAll : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// 

        /// 核取方塊的名稱
        /// 

        public string CheckBoxName { get; set; }
        /// 

        /// 設定Header字串
        /// 

        public string HeaderText { set { cbxChoice.Text = value; } }

        protected void Page_PreRender(object sender , EventArgs e)
        {
            //找出Gridview之中的CheckBox名稱
            GridView GridView2 = (GridView)this.Parent.Parent.Parent.Parent;
            string strCheckBoxsName = "";
            for(int i =0; i<GridView2.Rows.Count;i++)
            {
                CheckBox cbx = (CheckBox)GridView2.Rows[i].FindControl(this.CheckBoxName);
                strCheckBoxsName += cbx.ClientID + ",";
            }

            //調整陣列值
            if (strCheckBoxsName != "")
                strCheckBoxsName = strCheckBoxsName.Substring(0, strCheckBoxsName.Length - 1);

            //變更陣列值
            hidCheckBoxs.Value = strCheckBoxsName;
            string strClickScript = "document.getElementById('" + hidCheckBoxs.ClientID + "').value = '" + strCheckBoxsName + "';";
            ScriptManager.RegisterStartupScript(this, this.GetType(), this.ClientID, strClickScript, true);
            cbxChoice.Attributes.Add("onclick", "funChoiceAll" + this.ClientID + "(this);");

            //註冊script
            string strScript = "function funChoiceAll" + this.ClientID + "(obj){";
            strScript += " var strCheckBoxs = document.getElementById('" + hidCheckBoxs.ClientID + "').value.split(',');";
            strScript += " var i;";
            strScript += " for (i=0; i<strCheckBoxs.length; i++){ ";
            strScript += " document.getElementById(strCheckBoxs[i]).checked = obj.checked;}}";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fun" + this.ClientID, strScript, true);
        }
    }
}