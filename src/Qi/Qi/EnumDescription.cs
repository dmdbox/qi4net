using System;

namespace Qi
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        public EnumDescriptionAttribute(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            Name = name;
        }
       
        public Type ResourceType { get; set; }
        /// <summary>
        /// Gets or sets the Name of this descirption.
        /// </summary>
        public string Name { get; set; }
    }
}