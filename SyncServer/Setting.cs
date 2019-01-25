using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Xml;

namespace SyncServer
{
    /// <summary>配置</summary>
    [XmlConfigFile("Config/BillServer.config", 15000)]
    public class Setting : XmlConfig<Setting>
    {
        #region 属性
        /// <summary>调试开关。默认true</summary>
        [Description("调试开关。默认true")]
        public Boolean Debug { get; set; } = true;

        /// <summary>服务端口。默认7788</summary>
        [Description("服务端口。默认7788")]
        public Int32 Port { get; set; } = 7788;
        #endregion

        #region 构造
        /// <summary>实例化</summary>
        public Setting()
        {
        }
        #endregion
    }
}
