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
namespace SPF.OleDB.Model
{
	/// <summary>
	/// BeetleRole:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BeetleRoleInfo
	{
        public BeetleRoleInfo()
		{}
		#region Model
		private int _id;
		private string _brsid="";
		private string _brname="";
		private short _brstatus;
		private DateTime? _brcreatedate= DateTime.Now;
		private DateTime? _brupdatedate= DateTime.Now;
		private string _brdesc="";
		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// sid
		/// </summary>
		public string BRSid
		{
			set{ _brsid=value;}
			get{return _brsid;}
		}
		/// <summary>
		/// 角色名
		/// </summary>
		public string BRName
		{
			set{ _brname=value;}
			get{return _brname;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public short BRStatus
		{
			set{ _brstatus=value;}
			get{return _brstatus;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? BRCreateDate
		{
			set{ _brcreatedate=value;}
			get{return _brcreatedate;}
		}
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? BRUpdateDate
		{
			set{ _brupdatedate=value;}
			get{return _brupdatedate;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string BRDesc
		{
			set{ _brdesc=value;}
			get{return _brdesc;}
		}
		#endregion Model

	}
}

