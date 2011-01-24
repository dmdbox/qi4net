using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Ornament;

namespace Qi
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the objects from enum type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetObjects<T>()
        {
            Array s = Enum.GetValues(typeof(T));
            var result = new T[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                result[i] = (T)s.GetValue(i);
            }
            return result;
        }
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public static string ToString(Enum enumValue)
        {
            FieldInfo fieldinfo = enumValue.GetType().GetField(enumValue.ToString());
            object[] attrs = fieldinfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attrs.Length != 0)
            {
                var attr = (EnumDescriptionAttribute)attrs[0];
                return attr.Description;
            }

            return enumValue.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="descriptionString"></param>
        /// <returns></returns>
        public static T DescriptionToEnum<T>(string descriptionString)
        {
            descriptionString = descriptionString.ToLower().Trim();
            Type enumType = typeof(T);
            FieldInfo[] infos = enumType.GetFields();
            foreach (FieldInfo info in infos)
            {
                object[] descripts = info.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                if (descripts.Length != 0)
                {
                    if (((EnumDescriptionAttribute)descripts[0]).Description.ToLower() == descriptionString)
                    {
                        return (T)Enum.Parse(enumType, info.Name);
                    }
                }
            }

            throw new ArgumentOutOfRangeException(
                    "descriptionString", descriptionString, "can't match enum type " + typeof(T).Name);
        }
        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumExpress"></param>
        /// <returns></returns>
        public static T ToEnum<T>(string enumExpress)
        {
            return (T)Enum.Parse(typeof(T), enumExpress, true);
        }

        public static Dictionary<string, T> GetDescriptionList<T>()
        {
            return GetDescriptionList<T>(Thread.CurrentThread.CurrentUICulture.Name);
        }

        /// <summary>
        /// 获取Key-Value的集合。如果有<see cref="EnumDescription"/>那么就用EnumDescription的描述。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, T> GetDescriptionList<T>(string language)
        {
            return GetDescriptionList<T>(language, typeof(T));
        }

        public static Dictionary<string, Enum> GetDescriptionList(Type enumType)
        {
            return GetDescriptionList<Enum>(Thread.CurrentThread.CurrentUICulture.Name, enumType);
        }

        private static Dictionary<string, T> GetDescriptionList<T>(string language, Type enumType)
        {
            if (typeof(T) != typeof(Enum))
            {
                if (typeof(T).BaseType != typeof(Enum))
                    throw new ArgumentException("Only support Enum");
            }


            FieldInfo[] fields = enumType.GetFields();
            var result = new Dictionary<string, T>();

            foreach (FieldInfo field in fields)
            {
                if (field.IsSpecialName)
                    continue;

                object[] descriptionAttributeList = field.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                var value = (T)field.GetValue(null);

                if (descriptionAttributeList.Length == 0)
                {
                    result.Add(value.ToString(), value);
                }
                else
                {
                    var defaultDescript =
                        (EnumDescriptionAttribute)descriptionAttributeList[descriptionAttributeList.Length - 1];

                    foreach (EnumDescriptionAttribute eda in descriptionAttributeList)
                    {
                        if (eda.IsDefault || eda.Language == language)
                        {
                            defaultDescript = eda;
                        }
                    }
                    result.Add(defaultDescript.Description, value);
                }
            }
            return result;
        }

    }
}