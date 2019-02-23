using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SyncClient;
using SysncEntity;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var o = Console.ReadLine();

            var Name = "Test" + o;
            typeof(Program).InvokeMember(Name, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic, null, null, null);

            Console.WriteLine("ok");
            Console.ReadLine();
        }

        /// <summary>
        ///  测试序列化和反序列化
        /// </summary>
        static void Test1()
        {
            var list = Helper.ReadPK2("f:\\图片.rar");

            foreach (var item in list)
            {
                Helper.Write2(item, "F:\\123");

            }

        }

        /// <summary>
        ///  测试rpc接收
        /// </summary>
        static void Test2()
        {
            // 初始化发送客户端
            var client = new Client();
            // 发送文件
            var result = client.SendBigFilePK("f:\\图片.rar");

            Console.WriteLine(result);
        }
    }
}
