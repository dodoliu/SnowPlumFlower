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
namespace SPF.OleDB.Model
{
	/// <summary>
	/// BeetleUser:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BeetleUserInfo
	{
        public BeetleUserInfo()
		{}
		#region Model
		private int _id;
		private string _busid="";
		private string _brsid="";
		private string _buname="";
		private string _bupassword="";
		private string _budisplayname="";
		private short _bustatus;
		private DateTime _bucreatedate= DateTime.Now;
		private DateTime _buupdatedate= DateTime.Now;
		private string _budesc="";
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// SID
		/// </summary>
		public string BUSid
		{
			set{ _busid=value;}
			get{return _busid;}
		}
		/// <summary>
		/// 角色sid
		/// </summary>
		public string BRSid
		{
			set{ _brsid=value;}
			get{return _brsid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string BUName
		{
			set{ _buname=value;}
			get{return _buname;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string BUPassword
		{
			set{ _bupassword=value;}
			get{return _bupassword;}
		}
		/// <summary>
		/// 显示名
		/// </summary>
		public string BUDisplayName
		{
			set{ _budisplayname=value;}
			get{return _budisplayname;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public short BUStatus
		{
			set{ _bustatus=value;}
			get{return _bustatus;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime BUCreateDate
		{
			set{ _bucreatedate=value;}
			get{return _bucreatedate;}
		}
		/// <summary>
		/// 更新日期
		/// </summary>
		public DateTime BUUpdateDate
		{
			set{ _buupdatedate=value;}
			get{return _buupdatedate;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string BUDesc
		{
			set{ _budesc=value;}
			get{return _budesc;}
		}
		#endregion Model

	}
}

