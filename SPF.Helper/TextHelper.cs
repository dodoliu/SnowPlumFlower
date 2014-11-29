using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CM = System.Configuration.ConfigurationManager;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace SPF.Helper
{
    /// <summary>
    /// 文本处理类
    /// </summary>
    public class TextHelper
    {
        private static Dictionary<string, string> mapping = new Dictionary<string, string>();

        static TextHelper()
        {
            //!， '，(，)，*，-，.，_，~，
            mapping.Add("/","!-!");
            mapping.Add("=", "-!-");
            mapping.Add("+", "*!*");
        }

        #region 获取webconfig当中appSetting中的配置项
        /// <summary>
        /// 获取webconfig当中appSetting中的配置项
        /// </summary>
        /// <param name="key">配置项的键</param>
        /// <returns>配置项的值</returns>
        public static string GetConfigItem(string key)
        {
            string result = string.Empty;
            try
            {
                result = CM.AppSettings[key];
            }
            catch { }
            return result;
        }
        #endregion

        #region 获取webconfig当中appSetting中的配置项
        /// <summary>
        /// 获取webconfig当中appSetting中的配置项
        /// </summary>
        /// <param name="key">配置项的键</param>
        /// <returns>配置项的值</returns>
        public static T GetConfigItem<T>(string key)
        {

            try
            {
                return (T)Convert.ChangeType(CM.AppSettings[key], typeof(T));
            }
            catch
            {

                return (T)Convert.ChangeType(DBNull.Value, typeof(T)); ;
            }
        }
        #endregion

        #region 获取数据库配置项
        /// <summary>
        /// 获取数据库配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDataConfigItem(string key)
        {
            string result = string.Empty;
            try
            {
                result = CM.ConnectionStrings[key].ConnectionString;
            }
            catch { }
            return result;
        }
        #endregion

        #region 对指定的字符串,进行md5加密散列值计算
        /// <summary>
        /// 对指定的字符串,进行md5加密散列值计算
        /// </summary>
        /// <param name="source">需要md5加密的字符串值</param>
        /// <returns>md5值</returns>
        public static string MD5(string source)
        {
            byte[] result = Encoding.Default.GetBytes(source);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return ByteArrayToHexString(md5.ComputeHash(result));
        }
        #endregion

        #region 把对应的字节值转换为对应的字符串
        /// <summary>
        /// 把对应的字节值转换为对应的字符串
        /// </summary>
        /// <param name="values">字节值</param>
        /// <returns>字符串</returns>
        private static string ByteArrayToHexString(byte[] values)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte value in values)
            {
                sb.AppendFormat("{0:X2}", value);
            }
            return sb.ToString();
        }
        #endregion
        
        #region 字符串转成 column in ('','','','',)的条件字符串
        /// <summary>
        ///  字符串转成 column in ('','','','',)的条件字符串
        /// </summary>
        /// <param name="strings">字符串</param>
        /// <param name="split">拆分字符</param>
        /// <returns></returns>
        public static string ConvertStringArrayToWhere(string strings, string split)
        {
            string[] array = strings.Split(split.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return ConvertStringArrayToWhere(array);
        }

        /// <summary>
        /// 组合数组为('','','','',)的条件字符串
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>query的条件</returns>
        public static string ConvertStringArrayToWhere(string[] array)
        {
            //组合guid为query的条件
            StringBuilder where = new StringBuilder();
            where.Append("('");
            where.Append(string.Join("','", array));
            where.Append("')");

            return where.ToString();
        }

        /// <summary>
        /// 组合数组为('','','','',)的条件字符串
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>query的条件</returns>
        public static string ConvertStringArrayToWhere(IEnumerable<string> array)
        {
            return ConvertStringArrayToWhere(array.ToArray());
        }

        /// <summary>
        /// 组合数组为('','','','',)的条件字符串
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>query的条件</returns>
        public static string ConvertIntArrayToWhere(IEnumerable<int> array)
        {
            //组合guid为query的条件
            StringBuilder where = new StringBuilder();
            where.Append("('");
            where.Append(string.Join("','", array));
            where.Append("')");

            return where.ToString();
        }
        /// <summary>
        /// 把字符串转换为ModID的条件字符串
        /// </summary>
        /// <param name="strings">品牌ID逗号分割字符串</param>
        /// <param name="split">分隔符</param>
        /// <param name="Mod">余数</param>
        /// <returns>把字符串转换为ModID的条件字符串</returns>
        public static string ConvertIntArrayToModIDWhere(string strings, string split, int Mod = 50)
        {
            string[] array = strings.Split(split.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return ConvertIntArrayToModIDWhere(array, Mod);
        }

        /// <summary>
        /// 组合数组为('','','','',)的条件字符串
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="Mod">余数</param>
        /// <returns>query的条件</returns>
        public static string ConvertIntArrayToModIDWhere(IEnumerable<int> array, int Mod = 50)
        {
            //组合guid为query的条件
            List<int> temp = array.ToList();

            for (int i = 0; i < temp.Count; i++)
            {
                temp[i] = temp[i] % Mod;
            }

            StringBuilder where = new StringBuilder();
            where.Append("('");
            where.Append(string.Join("','", temp));
            where.Append("')");

            return where.ToString();
        }

        /// <summary>
        /// 组合数组为('','','','',)的条件字符串
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="Mod">余数</param>
        /// <returns>query的条件</returns>
        public static string ConvertIntArrayToModIDWhere(IEnumerable<string> array, int Mod = 50)
        {
            //组合guid为query的条件
            List<int> temp = array.Select(p=>Convert.ToInt32(p)).ToList();

            for (int i = 0; i < temp.Count; i++)
            {
                temp[i] = temp[i] % Mod;
            }

            StringBuilder where = new StringBuilder();
            where.Append("('");
            where.Append(string.Join("','", temp));
            where.Append("')");

            return where.ToString();
        }
        #endregion

        #region 转化为PinYin匹配的条件
        /// <summary>
        /// 查询关键字
        /// </summary>
        /// <param name="searchTxt"></param>
        /// <returns></returns>
        public static string ConvertStringToPinYinWhereString(string searchTxt)
        {
            StringBuilder where = new StringBuilder();

            if (!string.IsNullOrEmpty(searchTxt))
            {
                char[] chararray = searchTxt.ToCharArray();

                where.Append("%");
                where.Append(string.Join("%", chararray));
                where.Append("%");
            }

            return where.ToString();
        }
        #endregion

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encode">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            byte[] bytes = encode.GetBytes(source);
            string encodeString = "";
            try
            {
                encodeString = Convert.ToBase64String(bytes);
            }
            catch
            {
                encodeString = source;
            }
            return encodeString;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encode">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(Encoding encode, string result)
        {
            string decodeString = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decodeString = encode.GetString(bytes);
            }
            catch
            {
                decodeString = result;
            }
            return decodeString;
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }


        private string key = "teeinapi";
        private string iv = "request+";
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public string EncryptByDES(string sourceString)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());  
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>  
        /// 对DES加密后的字符串进行解密  
        /// </summary>  
        /// <param name="encryptedString">待解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public string Decrypt(string encryptedString)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    byte[] inData = Convert.FromBase64String(encryptedString);
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 判断字符串是否是中文
        /// </summary>
        /// <param name="chars">字符串</param>
        /// <param name="RegType">true:全部是中文；false:包含有中文</param>
        /// <returns></returns>
        private static bool IsChinese(string chars, bool RegType)
        {
            if (RegType)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(chars, @"^([\u4e00-\u9fa5]|[\uff01-\uff60]|\u3000){1,}$");
            }
            else
            {
                return System.Text.RegularExpressions.Regex.IsMatch(chars, @"([\u4e00-\u9fa5]|[\uff01-\uff60]|\u3000){1,}");
            }
        }

        /// <summary>
        /// 截取字符串(按字节)
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="length">截取的字节长度</param>
        /// <returns></returns>
        public static string SubStr(string s, int length)
        {
            if (string.IsNullOrWhiteSpace(s) || length < 1)
            {
                return "";
            }
            if (Encoding.GetEncoding("GB2312").GetBytes(s).Length <= length)
            {
                return s;
            }
            // 全部是中文
            if (IsChinese(s, true))
            {
                return s.Substring(0, length / 2);
            }
            // 如果不含有中文
            if (!IsChinese(s, false))
            {
                return s.Substring(0, length);
            }
            string str = "";
            int num = length / 2;
            int num2 = length;
            while (true)
            {
                str = str + s.Substring(str.Length, num);
                num2 = length - Encoding.GetEncoding("GB2312").GetBytes(str).Length;
                if (num2 <= 1)
                {
                    if ((num2 == 1) && (Encoding.GetEncoding("GB2312").GetBytes(s.Substring(str.Length, 1)).Length == 1))
                    {
                        str = str + s.Substring(str.Length, 1);
                    }
                    return str;
                }
                num = num2 / 2;
            }
        }

        /// <summary>
        /// 截取指定长度的字节数，并在末尾追加指定字符，比如“...”
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <param name="tailString"></param>
        /// <returns></returns>
        public static string SubStr(string s, int length, string tailString)
        {
            if (string.IsNullOrWhiteSpace(s) || length < 1)
            {
                return "";
            }
            if (string.IsNullOrWhiteSpace(tailString))
            {
                tailString = "...";
            }
            if (Encoding.GetEncoding("GB2312").GetBytes(s).Length > length)
            {
                return SubStr(s, length) + tailString;
            }
            return s;
        }
        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int ToInt(object o)
        {
            if(o != null)
                return ToInt(o.ToString());

            return 0;
        }
        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(string s)
        {
            int result = 0;
            if(int.TryParse(s,out result))
                return result;

            return 0;
        }
        /// <summary>
        /// 转换为UnixTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double ToUnixTimestamp(DateTime dt) {
            DateTime start = new DateTime(1970, 1, 1).ToLocalTime();
            return dt.Subtract(start).TotalMilliseconds;
        }
        /// <summary>
        /// 时间搓转换为时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToDatetime(double timestamp) {
            DateTime start = new DateTime(1970, 1, 1).ToLocalTime();
            return start.AddMilliseconds(timestamp);
        }
        /// <summary>
        /// 根据当前的请求获取网站的跟路径
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string BaseUrl(HttpRequestBase content)
        {
            string baseUrl = string.Empty;

            if (string.IsNullOrWhiteSpace(content.ApplicationPath) || content.ApplicationPath.Equals("/"))
            {
                baseUrl = string.Format("http://{0}:{1}/", content.Url.Host, content.Url.Port);
            }
            else
            {
                baseUrl = string.Format("http://{0}:{1}/{2}/", content.Url.Host, content.Url.Port, content.ApplicationPath.TrimStart('/'));
            }

            return baseUrl;
        }
        /// <summary>
        /// XmlDocument转换为字符串
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static string XmlDocument2String(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }

        /// <summary>
        /// json字符串转换为Xml对象
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static XmlDocument Json2Xml(string sJson)
        {
            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            Dictionary<string, object> Dic = (Dictionary<string, object>)oSerializer.DeserializeObject(sJson);
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDec;
            xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", "yes");
            doc.InsertBefore(xmlDec, doc.DocumentElement);
            XmlElement nRoot = doc.CreateElement("root");
            doc.AppendChild(nRoot);
            foreach (KeyValuePair<string, object> item in Dic)
            {
                try
                {
                    XmlElement element = doc.CreateElement(item.Key);
                    KeyValue2Xml(element, item);
                    nRoot.AppendChild(element);
                }
                catch(Exception e)
                {
                    new ExceptionHelper().LogException(e);
                }

            }
            return doc;
        }
        /// <summary>
        /// KeyValue结构转换为Xml
        /// </summary>
        /// <param name="node"></param>
        /// <param name="Source"></param>
        private static void KeyValue2Xml(XmlElement node, KeyValuePair<string, object> Source)
        {
            object kValue = Source.Value;
            if (kValue != null)
            {
                if (kValue.GetType() == typeof(Dictionary<string, object>))
                {
                    foreach (KeyValuePair<string, object> item in kValue as Dictionary<string, object>)
                    {
                        XmlElement element = node.OwnerDocument.CreateElement(item.Key);
                        KeyValue2Xml(element, item);
                        node.AppendChild(element);
                    }
                }
                else if (kValue.GetType() == typeof(object[]))
                {
                    object[] o = kValue as object[];
                    for (int i = 0; i < o.Length; i++)
                    {
                        XmlElement xitem = node.OwnerDocument.CreateElement("Item");
                        KeyValuePair<string, object> item = new KeyValuePair<string, object>("Item", o[i]);
                        KeyValue2Xml(xitem, item);
                        node.AppendChild(xitem);
                    }

                }
                else
                {
                    XmlText text = node.OwnerDocument.CreateTextNode(kValue.ToString());
                    node.AppendChild(text);
                }
            }
            else
            {
                XmlText text = node.OwnerDocument.CreateTextNode("");
                node.AppendChild(text);
            }
        }


        #region ========加密========
        private static string secret = "#!12^0#@";
        private static Byte[] _key;
        private static Byte[] IV = new Byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static string UrlEncrypt(string strToEncrypt)
        {
            return UrlEncrypt(strToEncrypt, secret);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strToEncrypt">要加密的字符串</param>
        /// <param name="strEncryptKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string UrlEncrypt(string strToEncrypt, string strEncryptKey)
        {
            if (!string.IsNullOrEmpty(strToEncrypt))
            {
                try
                {
                    _key = Encoding.UTF8.GetBytes(strEncryptKey.Substring(0, 8));
                    Byte[] inputByteArray = Encoding.UTF8.GetBytes(strToEncrypt);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(_key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    string result = Convert.ToBase64String(ms.ToArray());

                    foreach (var item in mapping)
                    {
                        result = result.Replace(item.Key, item.Value);
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    new ExceptionHelper().LogException(ex);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region ========解密========
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strToDecrypt"></param>
        /// <returns></returns>
        public static string UrlDecrypt(string strToDecrypt)
        {
            return UrlDecrypt(strToDecrypt, secret);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strToDecrypt">要解密的字符串</param>
        /// <param name="strEncryptKey">密钥，必须与加密的密钥相同</param>
        /// <returns>解密后的字符串</returns>
        public static string UrlDecrypt(string strToDecrypt, string strEncryptKey)
        {
            if (!string.IsNullOrEmpty(strToDecrypt))
            {
                foreach (var item in mapping)
                {
                    strToDecrypt = strToDecrypt.Replace(item.Value, item.Key);
                }
                //strToDecrypt = strToDecrypt.Replace(" ", "+");//如果去除此部分的代码就会出现上面出现所说的情况，出错或者解密出来的数据变成空值。
                try
                {
                    _key = Encoding.UTF8.GetBytes(strEncryptKey.Substring(0, 8));
                    Byte[] inputByteArray = Convert.FromBase64String(strToDecrypt);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(_key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    new ExceptionHelper().LogException(ex);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取post过来的xml参数
        /// <summary>
        /// 获取post过来的xml参数
        /// </summary>
        /// <returns></returns>
        public XDocument GetPostXmlData()
        {
            XDocument doc = null;
            if (System.Web.HttpContext.Current != null)
            {
                //读取参数
                using (StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Request.InputStream, Encoding.UTF8))
                {
                    try
                    {
                        string responseXml = sr.ReadToEnd();

                        FileLogHelper.Instance.Log(responseXml);

                        //转换为XDocument
                        doc = XDocument.Parse(responseXml);

                    }
                    catch (Exception e)
                    {
                        new ExceptionHelper().LogException(e);
                    }
                }
            }

            return doc;
        } 
        #endregion

        /// <summary>
        /// 根据当前的请求获取网站的跟路径
        /// </summary>
        /// <returns></returns>
        public static string BaseUrl()
        {
            string baseUrl = string.Empty;
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (string.IsNullOrWhiteSpace(request.ApplicationPath) || request.ApplicationPath.Equals("/"))
            {
                if (request.Url.Port == 80)
                {
                    baseUrl = string.Format("http://{0}/", request.Url.Host);
                }
                else
                {
                    baseUrl = string.Format("http://{0}:{1}/", request.Url.Host, request.Url.Port);
                }
            }
            else
            {
                if (request.Url.Port == 80)
                {
                    baseUrl = string.Format("http://{0}/{1}/", request.Url.Host, request.ApplicationPath.TrimStart('/'));
                }
                else
                {
                    baseUrl = string.Format("http://{0}:{1}/{2}/", request.Url.Host, request.Url.Port, request.ApplicationPath.TrimStart('/'));
                }
            }

            return baseUrl;
        }
        /// <summary>
        /// 获取当前请求的域名
        /// </summary>
        /// <returns></returns>
        public static string BaseUrlDomain()
        {
            string baseUrl = string.Empty;
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (request.Url.Port == 80)
            {
                baseUrl = string.Format("http://{0}/", request.Url.Host);
            }
            else
            {
                baseUrl = string.Format("http://{0}:{1}/", request.Url.Host, request.Url.Port);
            }
            return baseUrl;
        }
        
        #region 调试模式再输出
        /// <summary>
        /// 调试模式再输出
        /// </summary>
        /// <param name="message">信息</param>
        public static void DebugWriteLine(string message)
        {
            #if DEBUG
            Console.WriteLine(message);
            #endif
        }
        #endregion

        /// <summary>
        /// 根据当前的请求获取网站的跟路径
        /// </summary>
        /// <returns></returns>
        public static string BaseUrlPath()
        {
            string baseUrl = string.Empty;
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (string.IsNullOrWhiteSpace(request.ApplicationPath) || request.ApplicationPath.Equals("/"))
            {
                if (request.Url.Port == 80)
                {
                    baseUrl = string.Format("http://{0}/{1}", request.Url.Host, request.Url.LocalPath.TrimStart('/'));
                }
                else
                {
                    baseUrl = string.Format("http://{0}:{1}/{2}", request.Url.Host, request.Url.Port, request.Url.LocalPath.TrimStart('/'));
                }
            }
            else
            {
                if (request.Url.Port == 80)
                {
                    baseUrl = string.Format("http://{0}/{1}/{2}", request.Url.Host, request.ApplicationPath.TrimStart('/'), request.Url.LocalPath.TrimStart('/'));
                }
                else
                {
                    baseUrl = string.Format("http://{0}:{1}/{2}/{3}", request.Url.Host, request.Url.Port, request.ApplicationPath.TrimStart('/'), request.Url.LocalPath.TrimStart('/'));
                }
            }

            return baseUrl;
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">长度,默认长度43</param>
        /// <returns></returns>
        public static string RandomString(int length=43)
        {
            string source = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, 42);
                result += source[index];
            }

            return result;
        }
    }
}
