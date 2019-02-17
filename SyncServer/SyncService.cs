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
    public class SyncService
    {
        /// <summary>发送文件</summary>
        /// <param name="pkList"></param>
        [Api(nameof(SendFile))]
        public Boolean SendFile(IList<byte[]> pkList)
        {
            var set = Setting.Current;

            var list = new List<Packet>();
            foreach (var item in pkList)
            {
                list.Add(item);
            }

            Helper.Write(list, set.RootPath);

            return true;
        }
    }
}
