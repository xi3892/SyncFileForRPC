using System;
using System.IO;
using System.Text.RegularExpressions;
using NewLife;
using NewLife.Log;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
//using ApiClientX = NewLife.Remoting.ApiClient;

namespace SysncEntity
{
    /// <summary>远程调用客户端，带连接池</summary>
    public class RpcClient : ApiClient
    {
        #region 属性
        #endregion

        #region 构造
        /// <summary>实例化</summary>
        public RpcClient()
        {
            Log = XTrace.Log;

            //Timeout = 3_000;
            StatPeriod = 60;
            ShowError = true;

            Init();
        }

        private void Init()
        {

        }
        #endregion

        #region 方法
        /// <summary>打开客户端</summary>
        /// <returns></returns>
        public override Boolean Open()
        {
            var svrs = Servers;
            if (svrs == null || svrs.Length == 0) RegisterConfig();

            if (!base.Open()) return false;

            return true;
        }

        /// <summary>注册配置</summary>
        protected void RegisterConfig()
        {
            //var name = ConfigName;
            //if (name.IsNullOrEmpty()) throw new ArgumentNullException(nameof(ConfigName));

            var set = Setting.Current;

            var addr = set.Server;
            if (!addr.IsNullOrEmpty()) Servers = addr.Split(",");
        }
        #endregion

        #region 辅助
        /// <summary>远程结点地址</summary>
        public NetUri Remote
        {
            get
            {
                Open();
                return GetClient(true)?.Remote;
            }
        }

        /// <summary>从资源池借出内存流。用完后使用Put归还</summary>
        /// <returns></returns>
        protected virtual MemoryStream GetMemory() => NewLife.Collections.Pool.MemoryStream.Get();

        /// <summary>
        /// 校验是否是正确的地址格式
        /// </summary>
        /// <param name="address">tcp://1.2.3.4:56789</param>
        /// <returns></returns>
        public static Boolean checkRpcServerAddress(String address)
        {
            if (address.IsNullOrEmpty()) return false;

            var reg = "tcp://\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}:\\d{1,5}";
            if (Regex.IsMatch(address, reg))
            {
                var pp = Regex.Match(address, reg);
                return address.Length == pp.Length;
            }
            return false;
        }
        #endregion
    }
}