using System;
using System.Threading;

namespace Utilities
{
    /// <summary>
    /// 序列号生成器
    /// </summary>
    public class SequNo
    {
        static ulong _Mask = 100000;
        static ulong _Mask2 = 1000;
        static long _Sequ = 0;

        /// <summary>
        /// 新序列号 长度21
        /// 格式：{0:yyyyMMddHHmmssfff}{1:00000}
        /// </summary>
        static public string NewNo
        {
            get
            {
                ulong sequ = (ulong)Interlocked.Increment(ref _Sequ);
                return string.Format("{0:yyyyMMddHHmmssfff}{1:00000}", DateTime.Now, sequ % _Mask);
            }
        }

        /// <summary>
        /// 新序列号，长度20
        /// 每毫秒1000个订单
        /// </summary>
        static public string NewNo20
        {
            get
            {
                ulong sequ = (ulong)Interlocked.Increment(ref _Sequ);
                return string.Format("{0:yyyyMMddHHmmssfff}{1:000}", DateTime.Now, sequ % _Mask2);
            }
        }

        /// <summary>
        /// 自定义的序列号
        /// </summary>
        /// <param name="timeFormat"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        static public string GetNewNo(string timeFormat, ulong mask)
        {
            ulong sequ = (ulong)Interlocked.Increment(ref _Sequ);
            return string.Format(timeFormat, DateTime.Now, sequ % mask);
        }

        /// <summary>
        /// 下一个序列号
        /// </summary>
        static public ulong Next
        {
            get { return (ulong)Interlocked.Increment(ref _Sequ); }
        }

        static public long NewId
        {
            get
            {
                return DateTime.Now.Ticks + (long)(((ulong)Interlocked.Increment(ref _Sequ)) % _Mask);
            }
        }

        static public int NewIntId { get { return (int)(DateTime.Now.Ticks + (long)(((ulong)Interlocked.Increment(ref _Sequ)) % _Mask)); } }
        private static char[] rDigits = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
            'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 将数字转换为小于36的任意进制字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        static public string Num2Base(long value, int toBase)
        {
            int digitIndex = 0;
            long longPositive = Math.Abs(value);
            int radix = toBase;
            char[] outDigits = new char[63];

            for (digitIndex = 0; digitIndex <= 64; digitIndex++)
            {
                if (longPositive == 0) { break; }

                outDigits[outDigits.Length - digitIndex - 1] =
                    rDigits[longPositive % radix];
                longPositive /= radix;
            }

            return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
        }
    }
}
