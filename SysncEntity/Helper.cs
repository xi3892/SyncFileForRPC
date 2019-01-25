using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NewLife.Data;

namespace SysncEntity
{
    class Helper
    {
        /// <summary>写入文件</summary>
        /// <param name="pk"></param>
        public static void Write(Packet pk, String fileName, FileStream fs)
        {
            using (var bw = new BinaryWriter(fs))
            {
                var ms = new MemoryStream();
                var stream = pk.GetStream();
                var br = new BinaryReader(stream);

                var fname = br.ReadString();
                var index = br.ReadInt32();
                var count = br.ReadInt32();
                var data = br.ReadBytes(count);

                if (!fname.EndsWithIgnoreCase(fileName)) return;

                fs.Position = index * 1024 * 1024;
                bw.Write(data);
                bw.Flush();
                bw.Dispose();
                bw.Close();
            }
        }

        /// <summary>从文件写入Packet</summary>
        /// <param name="pathFile">文件路径</param>
        /// <param name="name">自定义文件名</param>
        /// <param name="size">默认1024k</param>
        /// <returns></returns>
        public static IList<Packet> Read(String pathFile, String name = "", Int32 size = 1024 * 1024)
        {
            if (pathFile.IsNullOrEmpty() || !File.Exists(pathFile)) return new List<Packet>();

            using (var fs = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
            {
                var pklist = new List<Packet>();
                var offsetIndex = 0;
                var ms = new MemoryStream();
                var bw = new BinaryWriter(ms);
                var fname = (new FileInfo(pathFile)).Name;
                fname = fname.Substring(0, fname.LastIndexOf("."));

                /*
                 * 写入顺序
                 * 1.文件名
                 * 2.序号
                 * 3.长度
                 * 4.数据
                 */
                var inum = 0;
                while (true)
                {
                    ms = new MemoryStream();
                    var buffer = new Byte[size];
                    var cindex = fs.Read(buffer, offsetIndex, size);

                    // 文件名
                    if (name.IsNullOrEmpty())
                    {
                        bw.Write(fname);
                    }
                    else
                    {
                        bw.Write(name);
                    }
                    // 序号
                    bw.Write(inum);
                    bw.Write(buffer.Length);
                    bw.Write(buffer);

                    var pk = new Packet(ms);
                    pklist.Add(pk);

                    if (cindex <= 0) break;
                    offsetIndex += size;
                    Interlocked.Increment(ref inum);
                }

                return pklist;
            }
        }
    }
}
