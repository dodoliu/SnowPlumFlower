/**  版本信息模板在安装目录下，可自行修改。
* BeetlePic.cs
*
* 功 能： N/A
* 类 名： BeetlePic
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-23 20:48:06   N/A    初版
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
	/// 数据访问类:BeetlePic
	/// </summary>
	public partial class BeetlePic:IBeetlePic
	{
		public BeetlePic()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return OleDBHelper.GetMaxID("ID", "BeetlePic"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BeetlePic");
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
        public bool Add(SPF.OleDB.Model.BeetlePicInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BeetlePic(");
			strSql.Append("BCCSid,BPName,BPUrl,BPStatus,BPDemo,BCCCreateTime,BCCUpdateTime)");
			strSql.Append(" values (");
			strSql.Append("@BCCSid,@BPName,@BPUrl,@BPStatus,@BPDemo,@BCCCreateTime,@BCCUpdateTime)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@BCCSid", OleDbType.VarChar,50),
					new OleDbParameter("@BPName", OleDbType.VarChar,255),
					new OleDbParameter("@BPUrl", OleDbType.VarChar,255),
					new OleDbParameter("@BPStatus", OleDbType.SmallInt),
					new OleDbParameter("@BPDemo", OleDbType.VarChar,255),
					new OleDbParameter("@BCCCreateTime", OleDbType.Date),
					new OleDbParameter("@BCCUpdateTime", OleDbType.Date)};
            parameters[0].Value = model.BCCSid;
		    parameters[1].Value = model.BPName == null ? "" : model.BPName;
            parameters[2].Value = model.BPUrl == null ? "" : model.BPUrl;
			parameters[3].Value = model.BPStatus;
            parameters[4].Value = model.BPDemo == null ? "" : model.BPDemo;
			parameters[5].Value = model.BCCCreateTime;
			parameters[6].Value = model.BCCUpdateTime;

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
        public bool Update(SPF.OleDB.Model.BeetlePicInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BeetlePic set ");
            //strSql.Append("BCCSid=@BCCSid,");
			strSql.Append("BPName=@BPName,");
			strSql.Append("BPUrl=@BPUrl,");
			strSql.Append("BPStatus=@BPStatus,");
			strSql.Append("BPDemo=@BPDemo,");
            //strSql.Append("BCCCreateTime=@BCCCreateTime,");
			strSql.Append("BCCUpdateTime=@BCCUpdateTime");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
                    //new OleDbParameter("@BCCSid", OleDbType.VarChar,50),
					new OleDbParameter("@BPName", OleDbType.VarChar,255),
					new OleDbParameter("@BPUrl", OleDbType.VarChar,255),
					new OleDbParameter("@BPStatus", OleDbType.SmallInt),
					new OleDbParameter("@BPDemo", OleDbType.VarChar,255),
                    //new OleDbParameter("@BCCCreateTime", OleDbType.Date),
					new OleDbParameter("@BCCUpdateTime", OleDbType.Date),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            //parameters[0].Value = model.BCCSid;
            parameters[0].Value = model.BPName == null ? "" : model.BPName;
            parameters[1].Value = model.BPUrl == null ? "" : model.BPUrl;
			parameters[2].Value = model.BPStatus;
            parameters[3].Value = model.BPDemo == null ? "" : model.BPDemo;
            //parameters[4].Value = model.BCCCreateTime;
			parameters[4].Value = model.BCCUpdateTime;
			parameters[5].Value = model.ID;

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
			strSql.Append("delete from BeetlePic ");
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
			strSql.Append("delete from BeetlePic ");
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
        public SPF.OleDB.Model.BeetlePicInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,BCCSid,BPName,BPUrl,BPStatus,BPDemo,BCCCreateTime,BCCUpdateTime from BeetlePic ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

            SPF.OleDB.Model.BeetlePicInfo model = new SPF.OleDB.Model.BeetlePicInfo();
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
        public SPF.OleDB.Model.BeetlePicInfo DataRowToModel(DataRow row)
		{
            SPF.OleDB.Model.BeetlePicInfo model = new SPF.OleDB.Model.BeetlePicInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["BCCSid"]!=null)
				{
					model.BCCSid=row["BCCSid"].ToString();
				}
				if(row["BPName"]!=null)
				{
					model.BPName=row["BPName"].ToString();
				}
				if(row["BPUrl"]!=null)
				{
					model.BPUrl=row["BPUrl"].ToString();
                }
                if (row["BPStatus"] != null && row["BPStatus"].ToString() != "")
                {
                    model.BPStatus = (short)row["BPStatus"];
                }
				if(row["BPDemo"]!=null)
				{
					model.BPDemo=row["BPDemo"].ToString();
				}
				if(row["BCCCreateTime"]!=null && row["BCCCreateTime"].ToString()!="")
				{
					model.BCCCreateTime=DateTime.Parse(row["BCCCreateTime"].ToString());
				}
				if(row["BCCUpdateTime"]!=null && row["BCCUpdateTime"].ToString()!="")
				{
					model.BCCUpdateTime=DateTime.Parse(row["BCCUpdateTime"].ToString());
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
			strSql.Append("select ID,BCCSid,BPName,BPUrl,BPStatus,BPDemo,BCCCreateTime,BCCUpdateTime ");
			strSql.Append(" FROM BeetlePic ");
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
			strSql.Append("select count(1) FROM BeetlePic ");
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
			strSql.Append(")AS Row, T.*  from BeetlePic T ");
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
			parameters[0].Value = "BeetlePic";
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

