/**  版本信息模板在安装目录下，可自行修改。
* BeetleRole.cs
*
* 功 能： N/A
* 类 名： BeetleRole
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-29 18:49:00   N/A    初版
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
namespace SPF.OleDB.OleDbDAL
{
	/// <summary>
	/// 数据访问类:BeetleRole
	/// </summary>
	public partial class BeetleRole:IBeetleRole
	{
		public BeetleRole()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return OleDBHelper.GetMaxID("ID", "BeetleRole"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BeetleRole");
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
        public bool Add(SPF.OleDB.Model.BeetleRoleInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BeetleRole(");
			strSql.Append("BRSid,BRName,BRStatus,BRCreateDate,BRUpdateDate,BRDesc)");
			strSql.Append(" values (");
			strSql.Append("@BRSid,@BRName,@BRStatus,@BRCreateDate,@BRUpdateDate,@BRDesc)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@BRSid", OleDbType.VarChar,50),
					new OleDbParameter("@BRName", OleDbType.VarChar,50),
					new OleDbParameter("@BRStatus", OleDbType.SmallInt),
					new OleDbParameter("@BRCreateDate", OleDbType.Date),
					new OleDbParameter("@BRUpdateDate", OleDbType.Date),
					new OleDbParameter("@BRDesc", OleDbType.VarChar,255)};
			parameters[0].Value = model.BRSid;
			parameters[1].Value = model.BRName;
			parameters[2].Value = model.BRStatus;
			parameters[3].Value = model.BRCreateDate;
			parameters[4].Value = model.BRUpdateDate;
			parameters[5].Value = model.BRDesc;

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
        public bool Update(SPF.OleDB.Model.BeetleRoleInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BeetleRole set ");
			strSql.Append("BRSid=@BRSid,");
			strSql.Append("BRName=@BRName,");
			strSql.Append("BRStatus=@BRStatus,");
			strSql.Append("BRCreateDate=@BRCreateDate,");
			strSql.Append("BRUpdateDate=@BRUpdateDate,");
			strSql.Append("BRDesc=@BRDesc");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@BRSid", OleDbType.VarChar,50),
					new OleDbParameter("@BRName", OleDbType.VarChar,50),
					new OleDbParameter("@BRStatus", OleDbType.SmallInt),
					new OleDbParameter("@BRCreateDate", OleDbType.Date),
					new OleDbParameter("@BRUpdateDate", OleDbType.Date),
					new OleDbParameter("@BRDesc", OleDbType.VarChar,255),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.BRSid;
			parameters[1].Value = model.BRName;
			parameters[2].Value = model.BRStatus;
			parameters[3].Value = model.BRCreateDate;
			parameters[4].Value = model.BRUpdateDate;
			parameters[5].Value = model.BRDesc;
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
			strSql.Append("delete from BeetleRole ");
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
			strSql.Append("delete from BeetleRole ");
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
        public SPF.OleDB.Model.BeetleRoleInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,BRSid,BRName,BRStatus,BRCreateDate,BRUpdateDate,BRDesc from BeetleRole ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

            SPF.OleDB.Model.BeetleRoleInfo model = new SPF.OleDB.Model.BeetleRoleInfo();
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
        public SPF.OleDB.Model.BeetleRoleInfo DataRowToModel(DataRow row)
		{
            SPF.OleDB.Model.BeetleRoleInfo model = new SPF.OleDB.Model.BeetleRoleInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["BRSid"]!=null)
				{
					model.BRSid=row["BRSid"].ToString();
				}
				if(row["BRName"]!=null)
				{
					model.BRName=row["BRName"].ToString();
				}
					//model.BRStatus=row["BRStatus"].ToString();
				if(row["BRCreateDate"]!=null && row["BRCreateDate"].ToString()!="")
				{
					model.BRCreateDate=DateTime.Parse(row["BRCreateDate"].ToString());
				}
				if(row["BRUpdateDate"]!=null && row["BRUpdateDate"].ToString()!="")
				{
					model.BRUpdateDate=DateTime.Parse(row["BRUpdateDate"].ToString());
				}
				if(row["BRDesc"]!=null)
				{
					model.BRDesc=row["BRDesc"].ToString();
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
			strSql.Append("select ID,BRSid,BRName,BRStatus,BRCreateDate,BRUpdateDate,BRDesc ");
			strSql.Append(" FROM BeetleRole ");
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
			strSql.Append("select count(1) FROM BeetleRole ");
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
			strSql.Append(")AS Row, T.*  from BeetleRole T ");
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
			parameters[0].Value = "BeetleRole";
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

