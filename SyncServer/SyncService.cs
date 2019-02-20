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
        [Api(nameof(SendFilePK))]
        public Boolean SendFilePK(Packet pkList)
        {
            var set = Setting.Current;

            Helper.Write(pkList, set.RootPath);

            return true;
        }

        /// <summary></summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        [Api(nameof(SendFilePK2))]
        public Boolean SendFilePK2(Packet pk)
        {
            var set = Setting.Current;
            Helper.Write2(pk, set.RootPath);

            return true;
        }

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
