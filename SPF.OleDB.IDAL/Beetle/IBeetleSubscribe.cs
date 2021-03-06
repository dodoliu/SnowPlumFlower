﻿/**  版本信息模板在安装目录下，可自行修改。
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
namespace SPF.OleDB.IDAL
{
	/// <summary>
	/// 接口层BeetleSubscribe
	/// </summary>
	public interface IBeetleSubscribe
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
        bool Add(SPF.OleDB.Model.BeetleSubscribeInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
        bool Update(SPF.OleDB.Model.BeetleSubscribeInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        SPF.OleDB.Model.BeetleSubscribeInfo GetModel(int ID);
        SPF.OleDB.Model.BeetleSubscribeInfo GetModel(string strContact, string strInformation);
        SPF.OleDB.Model.BeetleSubscribeInfo DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
