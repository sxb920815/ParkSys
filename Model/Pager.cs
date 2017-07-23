using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Pager<T> : Pager where T : class
    {
        /// <summary>
        /// 列表数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
    public class Pager
    {
        /// <summary>
        /// 当前页（从1开始）
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 页面尺寸
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int Count { get; set; }

        public int PageCount
        {
            get
            {
                return Count / Size + (Count % Size > 0 ? 1 : 0);
            }
        }
    }
}
