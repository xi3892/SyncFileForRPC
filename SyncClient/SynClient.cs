﻿using System;
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
            var pkList = Helper.Read(path);
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
            var pkList = Helper.ReadPK(path);
            if (pkList == null) return false;

            var result = await InvokeAsync<Boolean>("Sync/SendFilePK", new { pkList });

            return result;
        }

        /// <summary></summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Boolean SendFile(String path, String fileName = "")
        {
            return AsyncSendFile(path, fileName).Result;
        }
    }
}
