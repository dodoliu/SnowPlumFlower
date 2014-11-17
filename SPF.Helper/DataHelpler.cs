using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SPF.DBUtility;

namespace SPF.Helper
{
    /// <summary>
    /// 数据辅助类
    /// </summary>
    public class DataHelpler
    {
        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="iPageCount">每页条数</param>
        /// <param name="iPageIndex">当前页</param>
        /// <param name="strTableName">需要分页的表名</param>
        /// <param name="strIndexColumn">索引字段</param>
        /// <returns></returns>
        public DataTable FindDataByPage(int iPageSize = 10, int iPageIndex = 0, string strTableName = "", string strIndexColumn = "")
        {
            //select top 10 *
            //from AppConfig
            //where AppConfigID >=
            //(select MAX(AppConfigID)
            //from
            //(select top (10*(9-1)-1) AppConfigID
            //from AppConfig
            //order by AppConfigID)
            //as T)
            //order by AppConfigID

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select top {0} * ", iPageSize);
            sb.AppendFormat(" from {0} ", strTableName);
            sb.AppendFormat(" where [{0}] >= ", strIndexColumn);
            sb.AppendFormat(" (select Max([{0}]) from ", strIndexColumn);
            sb.AppendFormat(" (select top ({0}*({1}-1)-1) [{2}] ", iPageSize, iPageIndex, strIndexColumn);
            sb.AppendFormat(" from {0} ", strTableName);
            sb.AppendFormat(" order by [{0}]) as T) ", strIndexColumn);
            sb.AppendFormat(" order by [{0}] ", strIndexColumn);

            DataSet ds = OleDBHelper.Query(sb.ToString());
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 获取每个查询的总记录数
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strIndexColumn"></param>
        /// <returns></returns>
        public int FindCount(string strTableName = "", string strIndexColumn = "", string strWhere = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select count({0}) from {1}",strIndexColumn, strTableName);
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                sb.AppendFormat(" where {0} ", strWhere);
            }
            int iCount = Convert.ToInt32(OleDBHelper.GetSingle(sb.ToString()));

            return iCount;
        }
    }
}
