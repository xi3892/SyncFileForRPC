using System;
using System.Collections.Generic;
using System.IO;
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
        public Client()
        {
            RegisterConfig();
        }

        /// <summary>异步发送文件</summary>
        /// <param name="path">发送文件路径</param>
        /// <param name="fileName">指定保存文件名称(默认发送文件名)</param>
        /// <returns></returns>
        public async Task<Boolean> AsyncSendFile(String path, String fileName = "")
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"指定路径【{path}】文件不存在!");
            var pkList = Helper.Read(path, fileName);
            if (pkList == null) return false;

            var result = await InvokeAsync<Boolean>("Sync/SendFile", new { pkList });

            return result;
        }

        /// <summary>异步发送文件</summary>
        /// <param name="path">发送文件路径</param>
        /// <param name="fileName">指定保存文件名称(默认发送文件名)</param>
        /// <returns></returns>
        public async Task<Boolean> AsyncSendFilePK(String path, String fileName = "")
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"指定路径【{path}】文件不存在!");
            var pkList = Helper.ReadPK(path, fileName);
            if (pkList == null) return false;

            var result = await InvokeAsync<Boolean>("Sync/SendFilePK", new { pkList });

            return result;
        }

        /// <summary>发送文件（采用字节流）</summary>
        /// <param name="path">文件路径</param>
        /// <param name="fileName">指定保存文件名称(默认发送文件名)</param>
        /// <returns></returns>
        public Boolean SendFile(String path, String fileName = "")
        {
            return AsyncSendFile(path, fileName).Result;
        }

        /// <summary>发送文件（采用packet参数）</summary>
        /// <param name="path">文件路径</param>
        /// <param name="fileName">指定保存文件名称(默认发送文件名)</param>
        /// <returns></returns>
        public Boolean SendFilePK(String path, String fileName = "")
        {
            return AsyncSendFilePK(path, fileName).Result;
        }

        /// <summary>异步发送文件</summary>
        /// <param name="path">发送文件路径</param>
        /// <param name="fileName">指定保存文件名称(默认发送文件名)</param>
        /// <returns></returns>
        public Boolean SendFilePK2(String path, String fileName = "")
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"指定路径【{path}】文件不存在!");

            var pkList = Helper.ReadPK2(path, fileName);
            Helper.ReadAndWrite(this, path, fileName);
            if (pkList == null || pkList.Count < 1) return false;

            var flag = true;
            foreach (var pk in pkList)
            {
                // 切记传递PACKET对象，而不能以匿名对象成员属性方式传递，否则会被json序列化
                flag = InvokeAsync<Boolean>("Sync/SendFilePK2", pk).Result;
            }

            return flag;
        }

        /// <summary>边读边发送，减少内存占用（适用大文件）</summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Boolean SendBigFilePK(String path, String fileName = "")
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"指定路径【{path}】文件不存在!");

            return Helper.ReadAndWrite(this, path, fileName);
        }
    }
}
