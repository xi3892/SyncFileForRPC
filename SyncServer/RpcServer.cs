using System;
using NewLife.Log;
using NewLife.Net;
using NewLife.Remoting;

namespace SyncServer
{
    /// <summary>远程通信服务</summary>
    public class RpcServer : ApiServer
    {
        /// <summary>固定监听端口</summary>
        /// <param name="port">端口</param>
        public RpcServer(Int32 port) : base(port)
        {
            Log = XTrace.Log;

            StatPeriod = 60;
            ShowError = true;
        }

        #region 方法
        /// <summary>开始服务</summary>
        public override void Start()
        {
            if (EnsureCreate() is NetServer ns)
            {
                ns.Log = Log;
                ns.SessionLog = Log;
                ns.SocketLog = Log;
            }

            base.Start();
        }
        #endregion
    }
}