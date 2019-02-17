using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Net;
using NewLife.Remoting;

namespace SyncServer
{
    class Program
    {
        static void Main(string[] args) => MyService.ServiceMain();
    }

    class MyService : AgentServiceBase<MyService>
    {
        public MyService()
        {
            // 注册菜单，在控制台菜单中按 t 可以执行Test函数，主要用于临时处理数据
            AddMenu('t', "数据测试", Test);
        }

        ApiServer _Server;
        private void Init()
        {
            var sc = _Server;
            if (sc == null)
            {
                var set = Setting.Current;

                sc = new ApiServer(set.Port);
                var ns = sc.EnsureCreate() as NetServer;
                sc.ShowError = true;

                if (Setting.Current.Debug)
                {
                    sc.Log = XTrace.Log;
                    ns.Log = XTrace.Log;
                    ns.LogSend = true;
                    ns.LogReceive = true;
                    sc.EncoderLog = XTrace.Log;
                }

                // 注册服务
                sc.Register<SyncService>();
                sc.Start();

                _Server = sc;
            }
        }

        /// <summary>服务启动</summary>
        /// <remarks>
        /// 安装Windows服务后，服务启动会执行一次该方法。
        /// 控制台菜单按5进入循环调试也会执行该方法。
        /// </remarks>
        protected override void StartWork(String reason)
        {
            Init();

            base.StartWork(reason);
        }

        /// <summary>服务停止</summary>
        /// <remarks>
        /// 安装Windows服务后，服务停止会执行该方法。
        /// 控制台菜单按5进入循环调试，任意键结束时也会执行该方法。
        /// </remarks>
        protected override void StopWork(String reason)
        {
            base.StopWork(reason);

            _Server.TryDispose();
            _Server = null;
        }

        /// <summary>数据测试</summary>
        public void Test()
        {

        }
    }
}
