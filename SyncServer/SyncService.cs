using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Data;
using NewLife.Log;
using NewLife.Remoting;

namespace SyncServer
{
    [Api("Sync")]
    class SyncService
    {
        /// <summary>发送文件</summary>
        /// <param name="pk"></param>
        [Api(nameof(SendFile))]
        public Boolean SendFile(Packet pk)
        {



            return true;
        }
    }
}
