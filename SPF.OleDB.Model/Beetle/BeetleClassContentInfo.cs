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
namespace SPF.OleDB.Model
{
    /// <summary>
    /// BeetleClassContent:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class BeetleClassContentInfo
    {
        public BeetleClassContentInfo()
        { }
        #region Model
        private int _id;
        private string _bcsid = "";
        private string _bccsid = "";
        private string _bccname = "";
        private string _bccpic = "";
        private DateTime _bcccreatetime = DateTime.Now;
        private DateTime _bccupdatetime = DateTime.Now;
        private short _bccstatus;
        private string _bccdesc = "";
        private short _bcctype;
        private string _bccurl = "";
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 分类表的外键
        /// </summary>
        public string BCSid
        {
            set { _bcsid = value; }
            get { return _bcsid; }
        }
        /// <summary>
        /// sid
        /// </summary>
        public string BCCSid
        {
            set { _bccsid = value; }
            get { return _bccsid; }
        }
        /// <summary>
        /// 内容标题
        /// </summary>
        public string BCCName
        {
            set { _bccname = value; }
            get { return _bccname; }
        }
        /// <summary>
        /// 内容示例图片
        /// </summary>
        public string BCCPic
        {
            set { _bccpic = value; }
            get { return _bccpic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BCCCreateTime
        {
            set { _bcccreatetime = value; }
            get { return _bcccreatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BCCUpdateTime
        {
            set { _bccupdatetime = value; }
            get { return _bccupdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public short BCCStatus
        {
            set { _bccstatus = value; }
            get { return _bccstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BCCDesc
        {
            set { _bccdesc = value; }
            get { return _bccdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public short BCCType
        {
            set { _bcctype = value; }
            get { return _bcctype; }
        }
        /// <summary>
        /// 对应的链接
        /// </summary>
        public string BCCUrl
        {
            set { _bccurl = value; }
            get { return _bccurl; }
        }
        #endregion Model

    }
}

