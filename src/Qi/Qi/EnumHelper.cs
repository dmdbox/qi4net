using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

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
            Array s = Enum.GetValues(typeof (T));
            var result = new T[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                result[i] = (T) s.GetValue(i);
            }
            return result;
        }

        public static string ToString(Enum enumValue)
        {
            return ToString(enumValue, Thread.CurrentThread.CurrentUICulture);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <param name="cultureInfo"></param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public static string ToString(Enum enumValue, CultureInfo cultureInfo)
        {
            FieldInfo fieldinfo = enumValue.GetType().GetField(enumValue.ToString());
            object[] attrs = fieldinfo.GetCustomAttributes(typeof (EnumDescriptionAttribute), false);
            if (attrs.Length != 0)
            {
                var attr = (EnumDescriptionAttribute) attrs[0];
                if (attr.ResourceType != null)
                {
                    var resourceManager = new ResourceManager(attr.ResourceType);

                    string result = resourceManager.GetString(attr.Name, cultureInfo);
                    if (result != null)
                        return result;
                }
                else
                {
                    return attr.Name;
                }
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
            Type enumType = typeof (T);
            FieldInfo[] infos = enumType.GetFields();
            foreach (FieldInfo info in infos)
            {
                object[] descripts = info.GetCustomAttributes(typeof (EnumDescriptionAttribute), false);
                if (descripts.Length != 0)
                {
                    if (((EnumDescriptionAttribute) descripts[0]).Name.ToLower() == descriptionString)
                    {
                        return (T) Enum.Parse(enumType, info.Name);
                    }
                }
            }

            throw new ArgumentOutOfRangeException(
                "descriptionString", descriptionString, "can't match enum type " + typeof (T).Name);
        }

        /// <summary>
        /// ×Ö·û´®×ªÃ¶¾Ù
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumExpress"></param>
        /// <returns></returns>
        public static T ToEnum<T>(string enumExpress)
        {
            return (T) Enum.Parse(typeof (T), enumExpress, true);
        }

        public static Dictionary<string, T> GetDescriptionList<T>(Type enumType)
        {
            return GetDescriptionList<T>(Thread.CurrentThread.CurrentUICulture, enumType);
        }

        public static Dictionary<string, T> GetDescriptionList<T>(CultureInfo cultureInfo, Type enumType)
        {
            if (typeof (T) != typeof (Enum))
            {
                if (typeof (T).BaseType != typeof (Enum))
                    throw new ArgumentException("Only support Enum");
            }


            FieldInfo[] fields = enumType.GetFields();
            var result = new Dictionary<string, T>();

            foreach (FieldInfo field in fields)
            {
                if (field.IsSpecialName)
                    continue;

                object[] descriptionAttributeList = field.GetCustomAttributes(typeof (EnumDescriptionAttribute), false);
                var value = (T) field.GetValue(null);

                if (descriptionAttributeList.Length == 0)
                {
                    result.Add(value.ToString(), value);
                }
                else
                {
                    var attr = (EnumDescriptionAttribute) descriptionAttributeList[0];
                    string name = attr.Name;
                    if (attr.ResourceType != null)
                    {
                        var resourceManager = new ResourceManager(attr.ResourceType);
                        name = resourceManager.GetString(attr.Name, cultureInfo) ?? attr.Name;
                    }
                    result.Add(name, value);
                }
            }
            return result;
        }
    }
}