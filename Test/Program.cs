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

        static void Test1()
        {
            var list = Helper.Read("F:\\DSC_4015.JPG");

            //Helper.Write(list, "F:\\123");

            //using (var fs = new FileStream("F:\\DSC_123.jpg", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    foreach (var item in list)
            //    {
            //        Helper.Write(item, "DSC_123.JPG", fs);
            //    }
            //}
        }

        static void Test2()
        {
            var client = new Client();
            var result = client.SendFileF("F:\\DSC_4015.JPG");

            Console.WriteLine(result);
        }

        static void Test3()
        {
            var client = new Client();
            var result = client.t();
            Console.WriteLine(result);
        }
    }
}
