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
namespace SPF.OleDB.Model
{
	/// <summary>
	/// BeetlePic:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BeetlePic
	{
		public BeetlePic()
		{}
		#region Model
		private int _id;
		private string _bccsid="";
		private string _bpname="";
		private string _bpurl="";
		private short _bpstatus;
		private string _bpdemo="";
		private DateTime _bcccreatetime= DateTime.Now;
		private DateTime _bccupdatetime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 分类内容sid
		/// </summary>
		public string BCCSid
		{
			set{ _bccsid=value;}
			get{return _bccsid;}
		}
		/// <summary>
		/// 图片名称
		/// </summary>
		public string BPName
		{
			set{ _bpname=value;}
			get{return _bpname;}
		}
		/// <summary>
		/// 图片路径
		/// </summary>
		public string BPUrl
		{
			set{ _bpurl=value;}
			get{return _bpurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public short BPStatus
		{
			set{ _bpstatus=value;}
			get{return _bpstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BPDemo
		{
			set{ _bpdemo=value;}
			get{return _bpdemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime BCCCreateTime
		{
			set{ _bcccreatetime=value;}
			get{return _bcccreatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime BCCUpdateTime
		{
			set{ _bccupdatetime=value;}
			get{return _bccupdatetime;}
		}
		#endregion Model

	}
}

