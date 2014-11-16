using System;
using System.Reflection;
using System.Configuration;
using SPF.Helper;
using SPF.OleDB.IDAL;
namespace SPF.OleDB.DALFactory
{
	/// <summary>
	/// 抽象工厂模式创建DAL。
	/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) 
	/// DataCache类在导出代码的文件夹里
	/// <appSettings> 
	/// <add key="DAL" value="SPF.OleDB.OleDbDAL" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
	/// </appSettings> 
	/// </summary>
	public sealed class BeetleDataAccess
	{
		private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
		/// <summary>
		/// 创建对象或从缓存获取
		/// </summary>
		public static object CreateObject(string AssemblyPath,string ClassNamespace)
		{
			object objType = DataCache.GetCache(ClassNamespace);//从缓存读取
			if (objType == null)
			{
				try
				{
					objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
					DataCache.SetCache(ClassNamespace, objType);// 写入缓存
				}
				catch
				{}
			}
			return objType;
		}
		/// <summary>
		/// 创建数据层接口
		/// </summary>
		//public static t Create(string ClassName)
		//{
			//string ClassNamespace = AssemblyPath +"."+ ClassName;
			//object objType = CreateObject(AssemblyPath, ClassNamespace);
			//return (t)objType;
		//}
		/// <summary>
		/// 创建BeetleClass数据层接口。
		/// </summary>
		public static SPF.OleDB.IDAL.IBeetleClass CreateBeetleClass()
		{

			string ClassNamespace = AssemblyPath +".BeetleClass";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (SPF.OleDB.IDAL.IBeetleClass)objType;
		}
        /// <summary>
        /// 创建BeetleClassContent数据层接口。
        /// </summary>
        public static SPF.OleDB.IDAL.IBeetleClassContent CreateBeetleClassContent()
        {

            string ClassNamespace = AssemblyPath + ".BeetleClassContent";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SPF.OleDB.IDAL.IBeetleClassContent)objType;
        }
	}
}
