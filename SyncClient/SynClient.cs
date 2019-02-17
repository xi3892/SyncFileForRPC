using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Collections;
using NewLife.Data;
using NewLife.Net;
using NewLife.Remoting;
using SysncEntity;

namespace SyncClient
{
    /// <summary>客户端</summary>
    public class Client : RpcClient
    {
        /// <summary>异步获取运单信息</summary>
        /// <param name="code">单号、包号、车签号</param>
        /// <param name="extend">扩展，是否解包解车</param>
        /// <returns></returns>
        public async Task<Boolean> AsyncSendFile(IList<Packet> pk)
        {
            // 防止传递的单号为空的时候导致调用服务抛出异常
            if (pk == null) return false;

            var result = await InvokeAsync<Boolean>("Sync/SendFile", new { pk });

            return result;
        }

        /// <summary></summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Boolean SendFile(IList<Packet> pk)
        {
            return AsyncSendFile(pk).Result;
        }
    }
}
