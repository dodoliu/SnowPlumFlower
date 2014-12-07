/**  版本信息模板在安装目录下，可自行修改。
* BeetleUser.cs
*
* 功 能： N/A
* 类 名： BeetleUser
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-29 18:49:01   N/A    初版
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
	/// 数据访问类:BeetleUser
	/// </summary>
	public partial class BeetleUser:IBeetleUser
	{
		public BeetleUser()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return OleDBHelper.GetMaxID("ID", "BeetleUser"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BeetleUser");
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
        public bool Add(SPF.OleDB.Model.BeetleUserInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BeetleUser(");
			strSql.Append("BUSid,BRSid,BUName,BUPassword,BUDisplayName,BUStatus,BUCreateDate,BUUpdateDate,BUDesc)");
			strSql.Append(" values (");
			strSql.Append("@BUSid,@BRSid,@BUName,@BUPassword,@BUDisplayName,@BUStatus,@BUCreateDate,@BUUpdateDate,@BUDesc)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@BUSid", OleDbType.VarChar,50),
					new OleDbParameter("@BRSid", OleDbType.VarChar,50),
					new OleDbParameter("@BUName", OleDbType.VarChar,50),
					new OleDbParameter("@BUPassword", OleDbType.VarChar,50),
					new OleDbParameter("@BUDisplayName", OleDbType.VarChar,50),
					new OleDbParameter("@BUStatus", OleDbType.SmallInt),
					new OleDbParameter("@BUCreateDate", OleDbType.Date),
					new OleDbParameter("@BUUpdateDate", OleDbType.Date),
					new OleDbParameter("@BUDesc", OleDbType.VarChar,255)};
			parameters[0].Value = model.BUSid;
            parameters[1].Value = model.BRSid; 
            parameters[2].Value = model.BUName == null ? "" : model.BUName;
			parameters[3].Value = model.BUPassword;
			parameters[4].Value = model.BUDisplayName;
			parameters[5].Value = model.BUStatus;
			parameters[6].Value = model.BUCreateDate;
			parameters[7].Value = model.BUUpdateDate;
            parameters[8].Value = model.BUDesc == null ? "" : model.BUDesc;

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
        public bool Update(SPF.OleDB.Model.BeetleUserInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BeetleUser set ");
            //strSql.Append("BUSid=@BUSid,");
			strSql.Append("BRSid=@BRSid,");
            //strSql.Append("BUName=@BUName,");
			strSql.Append("BUPassword=@BUPassword,");
			strSql.Append("BUDisplayName=@BUDisplayName,");
			strSql.Append("BUStatus=@BUStatus,");
            //strSql.Append("BUCreateDate=@BUCreateDate,");
			strSql.Append("BUUpdateDate=@BUUpdateDate,");
			strSql.Append("BUDesc=@BUDesc");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
                    //new OleDbParameter("@BUSid", OleDbType.VarChar,50),
					new OleDbParameter("@BRSid", OleDbType.VarChar,50),
                    //new OleDbParameter("@BUName", OleDbType.VarChar,50),
					new OleDbParameter("@BUPassword", OleDbType.VarChar,50),
					new OleDbParameter("@BUDisplayName", OleDbType.VarChar,50),
					new OleDbParameter("@BUStatus", OleDbType.SmallInt),
                    //new OleDbParameter("@BUCreateDate", OleDbType.Date),
					new OleDbParameter("@BUUpdateDate", OleDbType.Date),
					new OleDbParameter("@BUDesc", OleDbType.VarChar,255),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            //parameters[0].Value = model.BUSid;
            parameters[0].Value = model.BRSid;
            //parameters[2].Value = model.BUName;
			parameters[1].Value = model.BUPassword;
			parameters[2].Value = model.BUDisplayName;
			parameters[3].Value = model.BUStatus;
            //parameters[6].Value = model.BUCreateDate;
			parameters[4].Value = model.BUUpdateDate;
			parameters[5].Value = model.BUDesc;
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
			strSql.Append("delete from BeetleUser ");
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
			strSql.Append("delete from BeetleUser ");
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
        public SPF.OleDB.Model.BeetleUserInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,BUSid,BRSid,BUName,BUPassword,BUDisplayName,BUStatus,BUCreateDate,BUUpdateDate,BUDesc from BeetleUser ");
            strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

            SPF.OleDB.Model.BeetleUserInfo model = new SPF.OleDB.Model.BeetleUserInfo();
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
        public SPF.OleDB.Model.BeetleUserInfo GetModelByUserName(string userName, string userPwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,BUSid,BRSid,BUName,BUPassword,BUDisplayName,BUStatus,BUCreateDate,BUUpdateDate,BUDesc from BeetleUser ");
            strSql.Append(" where BUName=@BUName");
            strSql.Append(" and BUPassword=@BUPassword");
            strSql.Append(" and BUStatus=1");
            OleDbParameter[] parameters = {
					new OleDbParameter("@BUName", OleDbType.VarChar,50),
					new OleDbParameter("@BUPassword", OleDbType.VarChar,50)
			};
            parameters[0].Value = userName;
            parameters[1].Value = userPwd;

            SPF.OleDB.Model.BeetleUserInfo model = new SPF.OleDB.Model.BeetleUserInfo();
            DataSet ds = OleDBHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public SPF.OleDB.Model.BeetleUserInfo DataRowToModel(DataRow row)
		{
            SPF.OleDB.Model.BeetleUserInfo model = new SPF.OleDB.Model.BeetleUserInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["BUSid"]!=null)
				{
					model.BUSid=row["BUSid"].ToString();
				}
				if(row["BRSid"]!=null)
				{
					model.BRSid=row["BRSid"].ToString();
				}
				if(row["BUName"]!=null)
				{
					model.BUName=row["BUName"].ToString();
				}
				if(row["BUPassword"]!=null)
				{
					model.BUPassword=row["BUPassword"].ToString();
				}
				if(row["BUDisplayName"]!=null)
				{
					model.BUDisplayName=row["BUDisplayName"].ToString();
				}
                if (row["BUStatus"] != null && row["BUStatus"].ToString() != "")
                {
                    model.BUStatus = (short)row["BUStatus"];
                }
				if(row["BUCreateDate"]!=null && row["BUCreateDate"].ToString()!="")
				{
					model.BUCreateDate=DateTime.Parse(row["BUCreateDate"].ToString());
				}
				if(row["BUUpdateDate"]!=null && row["BUUpdateDate"].ToString()!="")
				{
					model.BUUpdateDate=DateTime.Parse(row["BUUpdateDate"].ToString());
				}
				if(row["BUDesc"]!=null)
				{
					model.BUDesc=row["BUDesc"].ToString();
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
			strSql.Append("select ID,BUSid,BRSid,BUName,BUPassword,BUDisplayName,BUStatus,BUCreateDate,BUUpdateDate,BUDesc ");
			strSql.Append(" FROM BeetleUser ");
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
			strSql.Append("select count(1) FROM BeetleUser ");
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
			strSql.Append(")AS Row, T.*  from BeetleUser T ");
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
			parameters[0].Value = "BeetleUser";
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

