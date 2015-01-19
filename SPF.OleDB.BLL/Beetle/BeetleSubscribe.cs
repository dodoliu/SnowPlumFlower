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
using System.Collections.Generic;
using SPF.DBUtility;
using SPF.OleDB.Model;
using SPF.OleDB.DALFactory;
using SPF.OleDB.IDAL;
using SPF.Helper;
namespace SPF.OleDB.BLL
{
	/// <summary>
	/// BeetleSubscribe
	/// </summary>
	public partial class BeetleSubscribe
	{
        private readonly IBeetleSubscribe dal = BeetleDataAccess.CreateBeetleSubscribe();
		public BeetleSubscribe()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SPF.OleDB.Model.BeetleSubscribeInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(SPF.OleDB.Model.BeetleSubscribeInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public SPF.OleDB.Model.BeetleSubscribeInfo GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        public SPF.OleDB.Model.BeetleSubscribeInfo GetModelByCache(int ID)
		{
			
			string CacheKey = "BeetleSubscribeModel-" + ID;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
            return (SPF.OleDB.Model.BeetleSubscribeInfo)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<SPF.OleDB.Model.BeetleSubscribeInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<SPF.OleDB.Model.BeetleSubscribeInfo> DataTableToList(DataTable dt)
		{
            List<SPF.OleDB.Model.BeetleSubscribeInfo> modelList = new List<SPF.OleDB.Model.BeetleSubscribeInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                SPF.OleDB.Model.BeetleSubscribeInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
        //}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

