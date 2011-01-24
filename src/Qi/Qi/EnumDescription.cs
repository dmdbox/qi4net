using System;

namespace Qi
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        public EnumDescriptionAttribute(string description)
        {
            Description = description;
            IsDefault = false;
        }

        public string Description { get; set; }

        /// <summary>
        /// 设置或获取Language的属性
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 设定或获取是否为默认显示方式
        /// </summary>
        public bool IsDefault { get; set; }
    }
}