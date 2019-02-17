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
    public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pklist"></param>
        /// <param name="rootPath"></param>
        public static void Write(IList<Packet> pklist, String rootPath)
        {
            var fname = pklist.FirstOrDefault().ToStr();
            var path = Path.Combine(rootPath, fname + ".jpg");

            using (var fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                pklist.RemoveAt(0);

                foreach (var pk in pklist)
                {
                    var ms = pk.GetStream();
                    fs.Write(ms.ReadBytes());
                }
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pklist"></param>
        ///// <param name="rootPath"></param>
        //public static void Write(IList<Byte[]> pklist, String rootPath)
        //{
        //    var fname = (pklist.FirstOrDefault()).ToStr();
        //    var path = Path.Combine(rootPath, fname + ".jpg");

        //    using (var fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
        //    {
        //        pklist.RemoveAt(0);

        //        foreach (var pk in pklist)
        //        {
        //            var ms = pk.GetStream();
        //            fs.Write(ms.ReadBytes());
        //        }
        //    }
        //}

        /// <summary>从文件写入Packet</summary>
        /// <param name="pathFile">文件路径</param>
        /// <param name="name">自定义文件名</param>
        /// <param name="size">默认1024k</param>
        /// <returns></returns>
        public static IList<Byte[]> Read(String pathFile, String name = "", Int32 size = 1024 * 1024)
        {
            if (pathFile.IsNullOrEmpty() || !File.Exists(pathFile)) return new List<Byte[]>();

            using (var fs = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
            {
                var pklist = new List<Byte[]>();
                var fname = name.IsNullOrEmpty() ? (new FileInfo(pathFile)).Name : name;
                if (fname.LastIndexOf(".") > -1) fname = fname.Substring(0, fname.LastIndexOf("."));

                /*
                 * 写入顺序
                 * 1.文件名
                 * 2.数据
                 */
                pklist.Add(fname.GetBytes());

                while (true)
                {
                    if (fs.Position == fs.Length) break;

                    var buffer = (fs.Length - fs.Position < size) ? new Byte[fs.Length - fs.Position] : new Byte[size];
                    fs.Read(buffer, 0, buffer.Length);

                    var pk = buffer;
                    pklist.Add(pk);
                }

                return pklist;
            }
        }
    }
}
