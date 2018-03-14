using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public class DbHelper
    {
        /// <summary>
        /// 儲存資料
        /// </summary>
        /// <param name="tmpSQL">SQL語法</param>
        /// <param name="temParameters">參數集合</param>
        /// <returns>是否成功</returns>
        private string _DB_CnStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=電子公文;Integrated Security=true; pooling=false;";//建立DB_CnStr的屬性

        public string DB_CnStr
        {
            get { return _DB_CnStr; }
            set { _DB_CnStr = value; }
        }

        public bool ExecuteSQL(string tmpSQL, List<ListItem> tmpList)//ExecuteSQL是自己定義的名稱
        {
            #region 方法ListItem
            bool returnValue = false;
            //string tmpSQL = @"Delete From Students Where id=@id";
            using (SqlConnection cn = new SqlConnection(DB_CnStr))
            {
                cn.Open();//連線
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = tmpSQL;
                //cmd.Parameters.AddWithValue("@id", TextBox3.Text);
                foreach (ListItem item in tmpList)//foreach是有幾筆資料就執行幾次迴圈
                {
                    cmd.Parameters.AddWithValue(item.Text, item.Value);
                }
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();//執行
            }
            return returnValue;
            #endregion
        }
    }
}