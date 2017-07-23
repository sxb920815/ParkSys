using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTP
{
    ///<summary>    
    ///模块编号：
    ///作用：
    ///作者：
    ///编写日期：
    ///</summary>
    public class PlateInfo
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual string SBBH { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime JGSJ { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public virtual string HPHM { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public virtual string FullPath { get; set; }
    }
}
