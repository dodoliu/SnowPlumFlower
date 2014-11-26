/**  版本信息模板在安装目录下，可自行修改。
* BeetleClassContent.cs
*
* 功 能： N/A
* 类 名： BeetleClassContent
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-09 20:01:15   N/A    初版
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
	/// 数据访问类:BeetleClassContent
	/// </summary>
	public partial class BeetleClassContent:IBeetleClassContent
	{
		public BeetleClassContent()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return OleDBHelper.GetMaxID("ID", "BeetleClassContent"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BeetleClassContent");
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
        public bool Add(SPF.OleDB.Model.BeetleClassContentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BeetleClassContent(");
			strSql.Append("BCSid,BCCSid,BCCName,BCCPic,BCCCreateTime,BCCUpdateTime,BCCStatus,BCCDesc,BCCType,BCCUrl)");
			strSql.Append(" values (");
			strSql.Append("@BCSid,@BCCSid,@BCCName,@BCCPic,@BCCCreateTime,@BCCUpdateTime,@BCCStatus,@BCCDesc,@BCCType,@BCCUrl)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@BCSid", OleDbType.VarChar,50),
					new OleDbParameter("@BCCSid", OleDbType.VarChar,50),
					new OleDbParameter("@BCCName", OleDbType.VarChar,255),
					new OleDbParameter("@BCCPic", OleDbType.VarChar,255),
					new OleDbParameter("@BCCCreateTime", OleDbType.Date),
					new OleDbParameter("@BCCUpdateTime", OleDbType.Date),
					new OleDbParameter("@BCCStatus", OleDbType.SmallInt),
					new OleDbParameter("@BCCDesc", OleDbType.VarChar,255),
					new OleDbParameter("@BCCType", OleDbType.SmallInt),
					new OleDbParameter("@BCCUrl", OleDbType.VarChar,255)};
			parameters[0].Value = model.BCSid;
			parameters[1].Value = model.BCCSid;
            parameters[2].Value = model.BCCName == null ? "" : model.BCCName;
            parameters[3].Value = model.BCCPic == null ? "" : model.BCCPic;
			parameters[4].Value = model.BCCCreateTime;
			parameters[5].Value = model.BCCUpdateTime;
			parameters[6].Value = model.BCCStatus;
            parameters[7].Value = model.BCCDesc == null ? "" : model.BCCDesc;
			parameters[8].Value = model.BCCType;
            parameters[9].Value = model.BCCUrl == null ? "" : model.BCCUrl;

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
        public bool Update(SPF.OleDB.Model.BeetleClassContentInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BeetleClassContent set ");
			strSql.Append("BCSid=@BCSid,");
            //strSql.Append("BCCSid=@BCCSid,");
			strSql.Append("BCCName=@BCCName,");
			strSql.Append("BCCPic=@BCCPic,");
            //strSql.Append("BCCCreateTime=@BCCCreateTime,");
			strSql.Append("BCCUpdateTime=@BCCUpdateTime,");
			strSql.Append("BCCStatus=@BCCStatus,");
			strSql.Append("BCCDesc=@BCCDesc,");
			strSql.Append("BCCType=@BCCType,");
			strSql.Append("BCCUrl=@BCCUrl");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@BCSid", OleDbType.VarChar,50),
                    //new OleDbParameter("@BCCSid", OleDbType.VarChar,50),
					new OleDbParameter("@BCCName", OleDbType.VarChar,255),
					new OleDbParameter("@BCCPic", OleDbType.VarChar,255),
                    //new OleDbParameter("@BCCCreateTime", OleDbType.Date),
					new OleDbParameter("@BCCUpdateTime", OleDbType.Date),
					new OleDbParameter("@BCCStatus", OleDbType.SmallInt),
					new OleDbParameter("@BCCDesc", OleDbType.VarChar,255),
					new OleDbParameter("@BCCType", OleDbType.SmallInt),
					new OleDbParameter("@BCCUrl", OleDbType.VarChar,255),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.BCSid;
            //parameters[1].Value = model.BCCSid;
            parameters[1].Value = model.BCCName == null ? "" : model.BCCName;
            parameters[2].Value = model.BCCPic == null ? "" : model.BCCPic;
            //parameters[3].Value = model.BCCCreateTime;
			parameters[3].Value = model.BCCUpdateTime;
			parameters[4].Value = model.BCCStatus;
            parameters[5].Value = model.BCCDesc == null ? "" : model.BCCDesc;
			parameters[6].Value = model.BCCType;
            parameters[7].Value = model.BCCUrl == null ? "" : model.BCCUrl;
			parameters[8].Value = model.ID;

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
			strSql.Append("delete from BeetleClassContent ");
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
			strSql.Append("delete from BeetleClassContent ");
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
        public SPF.OleDB.Model.BeetleClassContentInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,BCSid,BCCSid,BCCName,BCCPic,BCCCreateTime,BCCUpdateTime,BCCStatus,BCCDesc,BCCType,BCCUrl from BeetleClassContent ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

            SPF.OleDB.Model.BeetleClassContentInfo model = new SPF.OleDB.Model.BeetleClassContentInfo();
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
        public SPF.OleDB.Model.BeetleClassContentInfo DataRowToModel(DataRow row)
		{
            SPF.OleDB.Model.BeetleClassContentInfo model = new SPF.OleDB.Model.BeetleClassContentInfo();
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
				if(row["BCCSid"]!=null)
				{
					model.BCCSid=row["BCCSid"].ToString();
				}
				if(row["BCCName"]!=null)
				{
					model.BCCName=row["BCCName"].ToString();
				}
				if(row["BCCPic"]!=null)
				{
					model.BCCPic=row["BCCPic"].ToString();
				}
				if(row["BCCCreateTime"]!=null && row["BCCCreateTime"].ToString()!="")
				{
					model.BCCCreateTime=DateTime.Parse(row["BCCCreateTime"].ToString());
				}
				if(row["BCCUpdateTime"]!=null && row["BCCUpdateTime"].ToString()!="")
				{
					model.BCCUpdateTime=DateTime.Parse(row["BCCUpdateTime"].ToString());
                }
                if (row["BCCStatus"] != null && row["BCCStatus"].ToString() != "")
                {
                    model.BCCStatus = (short)row["BCCStatus"];
                }
				if(row["BCCDesc"]!=null)
				{
					model.BCCDesc=row["BCCDesc"].ToString();
                }
                if (row["BCCType"] != null && row["BCCType"].ToString() != "")
                {
                    model.BCCType = (short)row["BCCType"];
                }
				if(row["BCCUrl"]!=null)
				{
					model.BCCUrl=row["BCCUrl"].ToString();
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
			strSql.Append("select ID,BCSid,BCCSid,BCCName,BCCPic,BCCCreateTime,BCCUpdateTime,BCCStatus,BCCDesc,BCCType,BCCUrl ");
			strSql.Append(" FROM BeetleClassContent ");
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
			strSql.Append("select count(1) FROM BeetleClassContent ");
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
			strSql.Append(")AS Row, T.*  from BeetleClassContent T ");
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
			parameters[0].Value = "BeetleClassContent";
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

