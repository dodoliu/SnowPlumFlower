/**  版本信息模板在安装目录下，可自行修改。
* BeetleSubscribe.cs
*
* 功 能： N/A
* 类 名： BeetleSubscribe
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-01-19 22:38:07   N/A    初版
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
using SPF.OleDB.IDAL;
using SPF.DBUtility;//Please add references
namespace SPF.OleDB.DAL
{
	/// <summary>
	/// 数据访问类:BeetleSubscribe
	/// </summary>
	public partial class BeetleSubscribe:IBeetleSubscribe
	{
		public BeetleSubscribe()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return OleDBHelper.GetMaxID("ID", "BeetleSubscribe"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BeetleSubscribe");
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
        public bool Add(SPF.OleDB.Model.BeetleSubscribeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BeetleSubscribe(");
			strSql.Append("SubsSid,SubsContact,SubsInformation,SubsReserveDate,SubsHouseType,SubsHouseAddress,SubsFloorArea,SubsStyle,SubsMemo,SubsStatus,SubsCreateDate)");
			strSql.Append(" values (");
			strSql.Append("@SubsSid,@SubsContact,@SubsInformation,@SubsReserveDate,@SubsHouseType,@SubsHouseAddress,@SubsFloorArea,@SubsStyle,@SubsMemo,@SubsStatus,@SubsCreateDate)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@SubsSid", OleDbType.VarChar,50),
					new OleDbParameter("@SubsContact", OleDbType.VarChar,0),
					new OleDbParameter("@SubsInformation", OleDbType.VarChar,255),
					new OleDbParameter("@SubsReserveDate", OleDbType.Date),
					new OleDbParameter("@SubsHouseType", OleDbType.VarChar,50),
					new OleDbParameter("@SubsHouseAddress", OleDbType.VarChar,255),
					new OleDbParameter("@SubsFloorArea", OleDbType.VarChar,50),
					new OleDbParameter("@SubsStyle", OleDbType.VarChar,50),
					new OleDbParameter("@SubsMemo", OleDbType.VarChar,255),
					new OleDbParameter("@SubsStatus", OleDbType.SmallInt),
					new OleDbParameter("@SubsCreateDate", OleDbType.Date)};
			parameters[0].Value = model.SubsSid;
			parameters[1].Value = model.SubsContact;
			parameters[2].Value = model.SubsInformation;
			parameters[3].Value = model.SubsReserveDate;
			parameters[4].Value = model.SubsHouseType;
			parameters[5].Value = model.SubsHouseAddress;
			parameters[6].Value = model.SubsFloorArea;
			parameters[7].Value = model.SubsStyle;
			parameters[8].Value = model.SubsMemo;
			parameters[9].Value = model.SubsStatus;
			parameters[10].Value = model.SubsCreateDate;

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
        public bool Update(SPF.OleDB.Model.BeetleSubscribeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BeetleSubscribe set ");
			strSql.Append("SubsSid=@SubsSid,");
			strSql.Append("SubsContact=@SubsContact,");
			strSql.Append("SubsInformation=@SubsInformation,");
			strSql.Append("SubsReserveDate=@SubsReserveDate,");
			strSql.Append("SubsHouseType=@SubsHouseType,");
			strSql.Append("SubsHouseAddress=@SubsHouseAddress,");
			strSql.Append("SubsFloorArea=@SubsFloorArea,");
			strSql.Append("SubsStyle=@SubsStyle,");
			strSql.Append("SubsMemo=@SubsMemo,");
			strSql.Append("SubsStatus=@SubsStatus,");
			strSql.Append("SubsCreateDate=@SubsCreateDate");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@SubsSid", OleDbType.VarChar,50),
					new OleDbParameter("@SubsContact", OleDbType.VarChar,0),
					new OleDbParameter("@SubsInformation", OleDbType.VarChar,255),
					new OleDbParameter("@SubsReserveDate", OleDbType.Date),
					new OleDbParameter("@SubsHouseType", OleDbType.VarChar,50),
					new OleDbParameter("@SubsHouseAddress", OleDbType.VarChar,255),
					new OleDbParameter("@SubsFloorArea", OleDbType.VarChar,50),
					new OleDbParameter("@SubsStyle", OleDbType.VarChar,50),
					new OleDbParameter("@SubsMemo", OleDbType.VarChar,255),
					new OleDbParameter("@SubsStatus", OleDbType.SmallInt),
					new OleDbParameter("@SubsCreateDate", OleDbType.Date),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.SubsSid;
			parameters[1].Value = model.SubsContact;
			parameters[2].Value = model.SubsInformation;
			parameters[3].Value = model.SubsReserveDate;
			parameters[4].Value = model.SubsHouseType;
			parameters[5].Value = model.SubsHouseAddress;
			parameters[6].Value = model.SubsFloorArea;
			parameters[7].Value = model.SubsStyle;
			parameters[8].Value = model.SubsMemo;
			parameters[9].Value = model.SubsStatus;
			parameters[10].Value = model.SubsCreateDate;
			parameters[11].Value = model.ID;

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
			strSql.Append("delete from BeetleSubscribe ");
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
			strSql.Append("delete from BeetleSubscribe ");
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
        public SPF.OleDB.Model.BeetleSubscribeInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,SubsSid,SubsContact,SubsInformation,SubsReserveDate,SubsHouseType,SubsHouseAddress,SubsFloorArea,SubsStyle,SubsMemo,SubsStatus,SubsCreateDate from BeetleSubscribe ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

            SPF.OleDB.Model.BeetleSubscribeInfo model = new SPF.OleDB.Model.BeetleSubscribeInfo();
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
        public SPF.OleDB.Model.BeetleSubscribeInfo DataRowToModel(DataRow row)
		{
            SPF.OleDB.Model.BeetleSubscribeInfo model = new SPF.OleDB.Model.BeetleSubscribeInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["SubsSid"]!=null)
				{
					model.SubsSid=row["SubsSid"].ToString();
				}
				if(row["SubsContact"]!=null)
				{
					model.SubsContact=row["SubsContact"].ToString();
				}
				if(row["SubsInformation"]!=null)
				{
					model.SubsInformation=row["SubsInformation"].ToString();
				}
				if(row["SubsReserveDate"]!=null && row["SubsReserveDate"].ToString()!="")
				{
					model.SubsReserveDate=DateTime.Parse(row["SubsReserveDate"].ToString());
				}
				if(row["SubsHouseType"]!=null)
				{
					model.SubsHouseType=row["SubsHouseType"].ToString();
				}
				if(row["SubsHouseAddress"]!=null)
				{
					model.SubsHouseAddress=row["SubsHouseAddress"].ToString();
				}
				if(row["SubsFloorArea"]!=null)
				{
					model.SubsFloorArea=row["SubsFloorArea"].ToString();
				}
				if(row["SubsStyle"]!=null)
				{
					model.SubsStyle=row["SubsStyle"].ToString();
				}
				if(row["SubsMemo"]!=null)
				{
					model.SubsMemo=row["SubsMemo"].ToString();
				}
					//model.SubsStatus=row["SubsStatus"].ToString();
				if(row["SubsCreateDate"]!=null && row["SubsCreateDate"].ToString()!="")
				{
					model.SubsCreateDate=DateTime.Parse(row["SubsCreateDate"].ToString());
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
			strSql.Append("select ID,SubsSid,SubsContact,SubsInformation,SubsReserveDate,SubsHouseType,SubsHouseAddress,SubsFloorArea,SubsStyle,SubsMemo,SubsStatus,SubsCreateDate ");
			strSql.Append(" FROM BeetleSubscribe ");
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
			strSql.Append("select count(1) FROM BeetleSubscribe ");
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
			strSql.Append(")AS Row, T.*  from BeetleSubscribe T ");
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
			parameters[0].Value = "BeetleSubscribe";
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

