/**  版本信息模板在安装目录下，可自行修改。
* BeetleClass.cs
*
* 功 能： N/A
* 类 名： BeetleClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-09 20:01:14   N/A    初版
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
    /// BeetleClass:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class BeetleClassInfo
    {
        public BeetleClassInfo()
        { }
        #region Model
        private int _id;
        private string _bcsid = "";
        private string _bcname = "";
        private short _bctype;
        private DateTime _bccreatetime = DateTime.Now;
        private DateTime _bcupdatetime = DateTime.Now;
        private short _bcstatus;
        private string _bcdesc = "";
        private string _bcurl = "";
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// sid
        /// </summary>
        public string BCSid
        {
            set { _bcsid = value; }
            get { return _bcsid; }
        }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string BCName
        {
            set { _bcname = value; }
            get { return _bcname; }
        }
        /// <summary>
        /// 分类类型(显示在什么地方)(1:为主分类)
        /// </summary>
        public short BCType
        {
            set { _bctype = value; }
            get { return _bctype; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime BCCreateTime
        {
            set { _bccreatetime = value; }
            get { return _bccreatetime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime BCUpdateTime
        {
            set { _bcupdatetime = value; }
            get { return _bcupdatetime; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public short BCStatus
        {
            set { _bcstatus = value; }
            get { return _bcstatus; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string BCDesc
        {
            set { _bcdesc = value; }
            get { return _bcdesc; }
        }
        /// <summary>
        /// 分类对应的链接
        /// </summary>
        public string BCUrl
        {
            set { _bcurl = value; }
            get { return _bcurl; }
        }
        #endregion Model

    }
}

