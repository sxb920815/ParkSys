using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace Utilities
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class Comm
    {
        /// <summary>
        /// 字段空值
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(NameValueCollection coll, ref ResultMsg msg)
        {
            foreach (var field in coll.AllKeys)
            {
                if (string.IsNullOrEmpty(field))
                {
                    msg.SetValue("ReturnMsg", field + "参数错误");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断字段是否完整
        /// </summary>
        /// <param name="coll">请求的参数</param>
        /// <param name="fields">索要匹配的</param>
        /// <param name="msg">返回的错误消息</param>
        /// <returns>字段是否完整</returns>
        public static bool ExistField(NameValueCollection coll, string[] fields, ref ResultMsg msg)
        {
            if (coll == null || coll.Count < 1)
            {
                msg.SetValue("ReturnMsg", "请求参数为空");
                return false;
            }

            foreach (var field in fields)
            {
                var flag = false;
                foreach (var key in coll.AllKeys)
                {
                    if (field == key)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    msg.SetValue("ReturnMsg", field + "不存在");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetEnumDescription(Enum value)
        {
            Type enumType = value.GetType();
            // 获取枚举常数名称。
            string name = Enum.GetName(enumType, value);
            if (name == null) return "";
            // 获取枚举字段。
            FieldInfo fieldInfo = enumType.GetField(name);
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                var attr = Attribute.GetCustomAttribute(fieldInfo,
                    typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
            return "";
        }

        public static string GetEnumDescription(string value, Type enumType)
        {
            // 获取枚举字段。
            FieldInfo fieldInfo = enumType.GetField(value);
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                var attr = Attribute.GetCustomAttribute(fieldInfo,
                    typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
            return "";
        }
        public static string GetEnumDescription(object value, Type enumType)
        {
            if (value == null)
            {
                return "未知";
            }
            // 获取枚举字段。
            FieldInfo fieldInfo = enumType.GetField(value.ToString());
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                var attr = Attribute.GetCustomAttribute(fieldInfo,
                    typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
            return "";
        }


        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static Hashtable EnumToHashtable(Type enumType)
        {
            var ht = new Hashtable();
            foreach (var item in Enum.GetNames(enumType))
            {
                FieldInfo fieldInfo = enumType.GetField(item);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    var attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        ht.Add(item, attr.Description);
                    }
                }
            }
            return ht;
        }
    }
}