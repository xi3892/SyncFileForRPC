using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Data;
using NewLife.Log;
using NewLife.Remoting;
using SysncEntity;

namespace SyncServer
{
    [Api("Sync")]
    class SyncService
    {
        /// <summary>发送文件</summary>
        /// <param name="pkList"></param>
        [Api(nameof(SendFile))]
        public Boolean SendFile(IList<Packet> pkList)
        {
            var set = Setting.Current;

            Helper.Write(pkList, set.RootPath);

            return true;
        }
    }
}
