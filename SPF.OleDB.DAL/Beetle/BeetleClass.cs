/**  版本信息模板在安装目录下，可自行修改。
* BeetleClass.cs
*
* 功 能： N/A
* 类 名： BeetleClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-09 20:01:14   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using SPF.DBUtility;
using SPF.OleDB.IDAL;
namespace SPF.OleDB.DAL
{
	/// <summary>
	/// 数据访问类:BeetleClass
	/// </summary>
	public partial class BeetleClass:IBeetleClass
	{
		public BeetleClass()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return OleDBHelper.GetMaxID("ID", "BeetleClass"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BeetleClass");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			return OleDBHelper.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(SPF.OleDB.Model.BeetleClassInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BeetleClass(");
			strSql.Append("BCSid,BCName,BCType,BCCreateTime,BCUpdateTime,BCStatus,BCDesc,BCUrl)");
			strSql.Append(" values (");
			strSql.Append("@BCSid,@BCName,@BCType,@BCCreateTime,@BCUpdateTime,@BCStatus,@BCDesc,@BCUrl)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@BCSid", OleDbType.VarChar,50),
					new OleDbParameter("@BCName", OleDbType.VarChar,50),
					new OleDbParameter("@BCType", OleDbType.SmallInt),
					new OleDbParameter("@BCCreateTime", OleDbType.Date),
					new OleDbParameter("@BCUpdateTime", OleDbType.Date),
					new OleDbParameter("@BCStatus", OleDbType.SmallInt),
					new OleDbParameter("@BCDesc", OleDbType.VarChar,255),
					new OleDbParameter("@BCUrl", OleDbType.VarChar,255)};
			parameters[0].Value = model.BCSid;
            parameters[1].Value = model.BCName == null ? "" : model.BCName;
			parameters[2].Value = model.BCType;
			parameters[3].Value = model.BCCreateTime;
			parameters[4].Value = model.BCUpdateTime;
			parameters[5].Value = model.BCStatus;
            parameters[6].Value = model.BCDesc == null ? "" : model.BCDesc;
            parameters[7].Value = model.BCUrl == null ? "" : model.BCUrl;

			int rows=OleDBHelper.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(SPF.OleDB.Model.BeetleClassInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BeetleClass set ");
            //strSql.Append("BCSid=@BCSid,");
			strSql.Append("BCName=@BCName,");
			strSql.Append("BCType=@BCType,");
            //strSql.Append("BCCreateTime=@BCCreateTime,");
			strSql.Append("BCUpdateTime=@BCUpdateTime,");
			strSql.Append("BCStatus=@BCStatus,");
			strSql.Append("BCDesc=@BCDesc,");
			strSql.Append("BCUrl=@BCUrl");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
                    //new OleDbParameter("@BCSid", OleDbType.VarChar,50),
					new OleDbParameter("@BCName", OleDbType.VarChar,50),
					new OleDbParameter("@BCType", OleDbType.SmallInt),
                    //new OleDbParameter("@BCCreateTime", OleDbType.Date),
					new OleDbParameter("@BCUpdateTime", OleDbType.Date),
					new OleDbParameter("@BCStatus", OleDbType.SmallInt),
					new OleDbParameter("@BCDesc", OleDbType.VarChar,255),
					new OleDbParameter("@BCUrl", OleDbType.VarChar,255),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            //parameters[0].Value = model.BCSid;
            parameters[0].Value = model.BCName == null ? "" : model.BCName;
			parameters[1].Value = model.BCType;
            //parameters[2].Value = model.BCCreateTime;
			parameters[2].Value = model.BCUpdateTime;
			parameters[3].Value = model.BCStatus;
            parameters[4].Value = model.BCDesc == null ? "" : model.BCDesc;
            parameters[5].Value = model.BCUrl == null ? "" : model.BCUrl;
			parameters[6].Value = model.ID;

			int rows=OleDBHelper.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from BeetleClass ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			int rows=OleDBHelper.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from BeetleClass ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=OleDBHelper.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public SPF.OleDB.Model.BeetleClassInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,BCSid,BCName,BCType,BCCreateTime,BCUpdateTime,BCStatus,BCDesc,BCUrl from BeetleClass ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

            SPF.OleDB.Model.BeetleClassInfo model = new SPF.OleDB.Model.BeetleClassInfo();
			DataSet ds=OleDBHelper.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public SPF.OleDB.Model.BeetleClassInfo DataRowToModel(DataRow row)
		{
            SPF.OleDB.Model.BeetleClassInfo model = new SPF.OleDB.Model.BeetleClassInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["BCSid"]!=null)
				{
					model.BCSid=row["BCSid"].ToString();
				}
				if(row["BCName"]!=null)
				{
					model.BCName=row["BCName"].ToString();
                }
                if (row["BCType"] != null && row["BCType"].ToString() != "")
                {
                    model.BCType = (short)row["BCType"];
                }
				if(row["BCCreateTime"]!=null && row["BCCreateTime"].ToString()!="")
				{
					model.BCCreateTime=DateTime.Parse(row["BCCreateTime"].ToString());
				}
				if(row["BCUpdateTime"]!=null && row["BCUpdateTime"].ToString()!="")
				{
					model.BCUpdateTime=DateTime.Parse(row["BCUpdateTime"].ToString());
				}
                if (row["BCStatus"] != null && row["BCStatus"].ToString() != "")
                {
                    model.BCStatus = (short)row["BCStatus"];
                }
				if(row["BCDesc"]!=null)
				{
					model.BCDesc=row["BCDesc"].ToString();
				}
				if(row["BCUrl"]!=null)
				{
					model.BCUrl=row["BCUrl"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,BCSid,BCName,BCType,BCCreateTime,BCUpdateTime,BCStatus,BCDesc,BCUrl ");
			strSql.Append(" FROM BeetleClass ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return OleDBHelper.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM BeetleClass ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            object obj = OleDBHelper.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from BeetleClass T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return OleDBHelper.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			OleDbParameter[] parameters = {
					new OleDbParameter("@tblName", OleDbType.VarChar, 255),
					new OleDbParameter("@fldName", OleDbType.VarChar, 255),
					new OleDbParameter("@PageSize", OleDbType.Integer),
					new OleDbParameter("@PageIndex", OleDbType.Integer),
					new OleDbParameter("@IsReCount", OleDbType.Boolean),
					new OleDbParameter("@OrderType", OleDbType.Boolean),
					new OleDbParameter("@strWhere", OleDbType.VarChar,1000),
					};
			parameters[0].Value = "BeetleClass";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return OleDBHelper.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

