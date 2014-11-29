using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;

namespace SPF.Helper
{
    /// <summary>
    /// 文件日志类(属性可配置)
    /// </summary>
    public class FileLogHelper
    {
        /** 配置文件中可配置项
        <configuration>
          <appSettings>
            <!--是否启用日志记录功能，默认为true-->
            <add key="LogIsEnabled" value="true"/>
            <!--是否同时记录堆栈信息(文件,方法,行号,列号)，默认为true-->
            <add key="IsRecordStack" value="true"/>
            <!--单个日志文件的最大限制(单位：MB)，默认为10(范围[1-10])-->
            <add key="LogFileMaxLimit" value="10"/>
          </appSettings>
        </configuration>
         */

        private static FileLogHelper _instance;
        private static readonly object lockObject = new object();
        private static Dictionary<string,IList<string>> _data;

        private bool IsEnabledLog = true; // 是否启用日志功能
        private bool IsRecordStackInfo = true; // 是否记录堆栈信息
        private long fileMaxLenth; // 日志文件的最大长度(字节)
        private FileStream fs;
        private string filePath;

        /// <summary>
        /// 单例模式
        /// </summary>
        public static FileLogHelper Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new FileLogHelper();
                    }
                    return _instance;
                }
            }
        }


        /// <summary>
        /// 加载配置文件信息
        /// </summary>
        private FileLogHelper()
        {
            NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string isEnabled = appSettings["LogIsEnabled"];
            string isRecordStack = appSettings["IsRecordStack"];
            string maxLimit = appSettings["LogFileMaxLimit"];

            bool enabled;
            if (bool.TryParse(isEnabled, out enabled))
            {
                IsEnabledLog = enabled;
            }
            else
            {
                IsEnabledLog = true; // 默认启用日志功能
            }

            bool recordStack;
            if (bool.TryParse(isRecordStack, out recordStack))
            {
                IsRecordStackInfo = recordStack;
            }
            else
            {
                IsRecordStackInfo = false; // 默认记录堆栈信息
            }

            int fileMaxLimit, max;
            if (int.TryParse(maxLimit, out max))
            {
                fileMaxLimit = Math.Max(1, max); // 限制最小5M
                fileMaxLimit = Math.Min(10, max); // 限制最大20M
            }
            else
            {
                fileMaxLimit = 10; // 默认20M
            }
            fileMaxLenth = fileMaxLimit * 1024 * 1024;
            _data = new Dictionary<string, IList<string>>();
        }

        /// <summary>
        /// 记录文件日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="logfilePrdfix">log文件名称的前缀</param>
        /// <param name="isLogTime">是否记录日志的中间时间</param>
        /// <param name="listcount">批量写入的检查点</param>
        public void Log(string content,string logfilePrdfix="", bool isLogTime = true,int listcount = 10)
        {
            if (!IsEnabledLog || string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            try
            {
                StringBuilder sb = new StringBuilder();

                if (isLogTime)
                {
                    sb.Append("-----------------------------------------------------------------------------------\r\n");
                    sb.AppendFormat("【时间】{0}\r\n", DateTime.Now.ToString("HH:mm:ss"));
                    if (IsRecordStackInfo)
                    {
                        sb.Append("【堆栈信息】\r\n");
                        #region 获取堆栈信息
                        StackTrace trace = new StackTrace(true);
                        StackFrame[] frames = trace.GetFrames();
                        int maxLenth = frames.Select(c => c.GetMethod().Name.Length).Max();
                        int tabNum = (maxLenth + 7) / 8;
                        sb.Append("\t方法");
                        //并行计算
                        for (int i = 0; i < tabNum; i++)
                        {
                            sb.Append("\t");
                        }
                        sb.Append("行号\t列号\t文件\r\n");
                        foreach (StackFrame frame in frames)
                        {
                            sb.AppendFormat("\t{0}", frame.GetMethod().Name);
                            int num = (int)Math.Ceiling((decimal)(tabNum * 8 - frame.GetMethod().Name.Length) / 8);
                            //并行计算
                            //并行计算
                            for (int i = 0; i < num; i++)
                            {
                                sb.Append("\t");
                            }
                            sb.AppendFormat("{0}\t{1}\t{2}\r\n", frame.GetFileLineNumber(), frame.GetFileColumnNumber(), frame.GetFileName());
                        }
                        #endregion
                    }
                    sb.Append("【内容】\r\n\t" + content + "\r\n\r\n");
                }
                else
                {
                    sb.Append(content);
                }

                lock (lockObject)
                {
                    if (!_data.ContainsKey(logfilePrdfix))
                    {
                        _data.Add(logfilePrdfix, new List<string>());
                    }

                    _data[logfilePrdfix].Add(sb.ToString());

                    if (_data[logfilePrdfix].Count > listcount)
                    {
                        Log(_data[logfilePrdfix], logfilePrdfix);
                        _data[logfilePrdfix].Clear();
                    }
                }
            }
            catch (Exception e)
            {
                e.Data["Method"] = "Log";
                new ExceptionHelper().LogException(e);
            }
        }

        /// <summary>
        /// 记录文件日志
        /// </summary>
        /// <param name="contents">日志内容</param>
        /// <param name="logfilePrdfix">日志文件前缀</param>
        public void Log(IList<string> contents, string logfilePrdfix = "")
        {
            if (!IsEnabledLog || contents == null || contents.Count == 0)
            {
                return;
            }
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("-----------------------------------------------------------------------------------\r\n");
                sb.AppendFormat("【时间】{0}\r\n", DateTime.Now.ToString("HH:mm:ss"));
                if (IsRecordStackInfo)
                {
                    sb.Append("【堆栈信息】\r\n");
                    #region 获取堆栈信息
                    StackTrace trace = new StackTrace(true);
                    StackFrame[] frames = trace.GetFrames();
                    int maxLenth = frames.Select(c => c.GetMethod().Name.Length).Max();
                    int tabNum = (maxLenth + 7) / 8;
                    sb.Append("\t方法");
                    //并行计算
                    for (int i = 0; i < tabNum; i++)
                    {
                        sb.Append("\t");
                    }
                    sb.Append("行号\t列号\t文件\r\n");
                    foreach (StackFrame frame in frames)
                    {
                        sb.AppendFormat("\t{0}", frame.GetMethod().Name);
                        int num = (int)Math.Ceiling((decimal)(tabNum * 8 - frame.GetMethod().Name.Length) / 8);
                        //并行计算
                        for (int i = 0; i < num; i++)
                        {
                            sb.Append("\t");
                        }
                        sb.AppendFormat("{0}\t{1}\t{2}\r\n", frame.GetFileLineNumber(), frame.GetFileColumnNumber(), frame.GetFileName());
                    }
                    #endregion
                }
                sb.Append("【内容】\r\n");


                for (int i = 0; i < contents.Count; i++)
                {
                    sb.Append(contents[i] + "\r\n");
                }
                filePath = GetLogFilePath(logfilePrdfix);
                fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                byte[] data = System.Text.Encoding.UTF8.GetBytes(sb.ToString()); // Encoding.GetBytes(string s); s必须实例化
                fs.Write(data, 0, data.Length);
                fs.Close();
                
            }
            catch (Exception e)
            {
                new ExceptionHelper().LogException(e);
            }
        }

        /// <summary>
        /// 取得当前所应操作的日志文件的路径
        /// </summary>
        /// <param name="logfilePrdfix"></param>
        /// <returns></returns>
        public string GetLogFilePath(string logfilePrdfix="")
        {
            string strFilePath = string.Empty;
            string strYearMonthDay = DateTime.Now.ToString("yyyyMMdd");
            string logSavePath = string.Format("{0}\\log", System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\".ToCharArray()));
            // 判断日志保存目录是否存在，不存在则新建
            if (!System.IO.Directory.Exists(logSavePath))
            {
                System.IO.Directory.CreateDirectory(logSavePath);
            }

            //判断当天是否已有日志文件 
            string[] strFilesArray = Directory.GetFiles(logSavePath, string.Format("{0}log_" + strYearMonthDay + "_*.txt", logfilePrdfix));
            if (strFilesArray.Length == 0)
            {
                strFilePath = string.Format("{0}\\{2}log_{1}_1.txt", logSavePath, strYearMonthDay, logfilePrdfix);

                //之前没有当日的日志文件，则需要新建 
                using (File.CreateText(strFilePath))
                {

                }
            }
            else
            {
                int maxOrder = 1, fileOrder;
                string fileName = null;

                #region 获取编号最大的日志文件
                for (int i = 0; i < strFilesArray.Length; i++)
                {
                    fileName = strFilesArray[i].Trim();
                    fileName = fileName.Substring(fileName.LastIndexOf('_') + 1);
                    fileName = fileName.Replace(".txt", "");
                    fileOrder = Convert.ToInt32(fileName);
                    if (fileOrder > maxOrder)
                    {
                        maxOrder = fileOrder;
                    }
                }
                #endregion

                strFilePath = string.Format("{0}\\{3}log_{1}_{2}.txt", logSavePath, strYearMonthDay, maxOrder, logfilePrdfix);
                //判断最新文件(即编号最大)是否超容 
                FileInfo fileInfo = new FileInfo(strFilePath);
                if (fileInfo.Length >= fileMaxLenth)
                {
                    //超容了,则新建之
                    int newOrder = maxOrder + 1;
                    strFilePath = string.Format("{0}\\{3}log_{1}_{2}.txt", logSavePath, strYearMonthDay, newOrder, logfilePrdfix);
                    using (File.CreateText(strFilePath))
                    {

                    }
                }
            }
            return strFilePath;
        }

        /// <summary>
        /// /获取指定文件夹的大小 
        /// </summary>
        /// <param name="dir">目录实例</param>
        /// <returns></returns>
        public static long FolderSize(System.IO.DirectoryInfo dir)
        {
            if (dir == null || !dir.Exists)
            {
                return 0;
            }
            long size = 0;
            FileInfo[] files = dir.GetFiles();
            foreach (System.IO.FileInfo info in files)
            {
                size += info.Length;
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo dirinfo in dirs)
            {
                size += FolderSize(dirinfo);
            }
            return size;
        }
    }
}
