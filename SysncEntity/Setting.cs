using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Xml;

namespace SysncEntity
{
    [XmlConfigFile("Config/SendFileSetting.config", 15000)]
    public class Setting : XmlConfig<Setting>
    {
        #region 属性
        /// <summary>服务端地址</summary>
        [Description("服务端地址")]
        public String Server { get; set; } = "tcp://127.0.0.1:7788";
        #endregion

        #region 构造
        /// <summary>实例化</summary>
        public Setting()
        {
        }
        #endregion
    }
}
