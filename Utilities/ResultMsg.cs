using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Utilities
{
    /// <summary>
    /// 结果消息
    /// </summary>
    public class ResultMsg
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public enum ResultCode
        {
            Success = 0,
            Fail = -1,
        }

        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /// <summary>
        /// 设置某个字段的值
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }

        /// <summary>
        /// 根据字段名获取某个字段的值
        /// </summary>
        /// <param name="key">字段名</param>
        /// <returns>对应的字段值</returns>
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }

        /// <summary>
        /// 判断某个字段是否已设置
        /// </summary>
        /// <param name="key">字段名</param>
        /// <returns>若字段key已被设置，则返回true，否则返回false</returns>
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 将Dictionary转成xml
        /// </summary>
        /// <returns>return 经转换得到的xml串</returns>
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                throw new Exception("ResultMsg数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    throw new Exception("ResultMsg内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new Exception("ResultMsg字段数据类型错误!");
                }
            }
            xml += "</xml>";

            return xml;
        }

        /// <summary>
        /// 将Dictionary转成json
        /// </summary>
        /// <returns>return 经转换得到的json串</returns>
        public string ToJson()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                throw new Exception("ResultMsg数据为空!");
            }
            string json = "{";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    throw new Exception("ResultMsg内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    json += string.Format("\"{0}\":{1},", pair.Key, pair.Value);
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    json += string.Format("\"{0}\":\"{1}\",", pair.Key, pair.Value);
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    json += string.Format("\"{0}\":\"{1}\",", pair.Key, pair.Value);
                }
            }
            json = json.Remove(json.LastIndexOf(","), 1);
            json += "}";

            return json;
        }

        public Hashtable ToHashtable()
        {
            if (0 == m_values.Count)
            {
                throw new Exception("ResultMsg数据为空!");
            }

            var ht = new Hashtable();

            foreach (KeyValuePair<string, object> pair in m_values)
            {
                ht.Add(pair.Key, pair.Value);
            }

            return ht;
        }

        public string ToJsonNet()
        {
            if (0 == m_values.Count)
            {
                throw new Exception("ResultMsg数据为空!");
            }

            var ht = new Hashtable();

            foreach (KeyValuePair<string, object> pair in m_values)
            {
                ht.Add(pair.Key, pair.Value);
            }

            Newtonsoft.Json.Converters.IsoDateTimeConverter timeFormat = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            return Newtonsoft.Json.JsonConvert.SerializeObject(ht, Newtonsoft.Json.Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// 将xml转为WxPayData对象并返回对象内部的数据
        /// </summary>
        /// <param name="xml">待转换的xml串</param>
        /// <returns>经转换得到的Dictionary</returns>
        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换为ResultMsg不合法!");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            var nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            return m_values;
        }
    }
}