using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPF.Helper
{
    /// <summary>
    /// 方法的返回对象
    /// </summary>
    [Serializable]
   
    public class ReturnMessage
    {
        private IDictionary<string, object> m_Data = new Dictionary<string, object>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsSuccess">默认是true还是false</param>
        public ReturnMessage()
        {
            this.IsSuccess = false;
            this.Text = "";
            this.Message = "";
            this.ReturnUrl = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsSuccess">默认是true还是false</param>
        public ReturnMessage(bool IsSuccess)
        {
            this.IsSuccess = IsSuccess;
            this.Text = "";
            this.Message = "";
            this.ReturnUrl = "";
        }


        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回单项数据信息
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 返回跳转地址
        /// </summary>
        public string ReturnUrl { get; set; }

        [XmlIgnore]
        /// <summary>
        /// 返多项值,以字典形式返回
        /// </summary>
        public IDictionary<string, object> ResultData
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        [XmlIgnore]
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        public class DataItem
        {
            public string Key{ get; set; }
            public object Value { get; set; }
        }

    }
}
