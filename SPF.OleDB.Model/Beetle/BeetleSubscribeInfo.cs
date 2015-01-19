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
namespace SPF.OleDB.Model
{
	/// <summary>
	/// BeetleSubscribe:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BeetleSubscribeInfo
	{
        public BeetleSubscribeInfo()
		{}
		#region Model
		private int _id;
		private string _subssid="";
		private string _subscontact="";
		private string _subsinformation="";
		private DateTime _subsreservedate= DateTime.Now;
		private string _subshousetype="";
		private string _subshouseaddress="";
		private string _subsfloorarea="";
		private string _subsstyle="";
		private string _subsmemo="";
		private short _subsstatus;
		private DateTime _subscreatedate= DateTime.Now;
		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// SID
		/// </summary>
		public string SubsSid
		{
			set{ _subssid=value;}
			get{return _subssid;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string SubsContact
		{
			set{ _subscontact=value;}
			get{return _subscontact;}
		}
		/// <summary>
		/// 联系方式
		/// </summary>
		public string SubsInformation
		{
			set{ _subsinformation=value;}
			get{return _subsinformation;}
		}
		/// <summary>
		/// 预定日期
		/// </summary>
		public DateTime SubsReserveDate
		{
			set{ _subsreservedate=value;}
			get{return _subsreservedate;}
		}
		/// <summary>
		/// 户型
		/// </summary>
		public string SubsHouseType
		{
			set{ _subshousetype=value;}
			get{return _subshousetype;}
		}
		/// <summary>
		/// 小区地址
		/// </summary>
		public string SubsHouseAddress
		{
			set{ _subshouseaddress=value;}
			get{return _subshouseaddress;}
		}
		/// <summary>
		/// 建筑面积
		/// </summary>
		public string SubsFloorArea
		{
			set{ _subsfloorarea=value;}
			get{return _subsfloorarea;}
		}
		/// <summary>
		/// 期望风格
		/// </summary>
		public string SubsStyle
		{
			set{ _subsstyle=value;}
			get{return _subsstyle;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string SubsMemo
		{
			set{ _subsmemo=value;}
			get{return _subsmemo;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public short SubsStatus
		{
			set{ _subsstatus=value;}
			get{return _subsstatus;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime SubsCreateDate
		{
			set{ _subscreatedate=value;}
			get{return _subscreatedate;}
		}
		#endregion Model

	}
}

