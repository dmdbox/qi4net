using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;

namespace Qi.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="knowTypes"></param>
        /// <returns></returns>
        public static string ToDataContactJson(object obj, params Type[] knowTypes)
        {
            DataContractSerializer dcs = knowTypes == null
                                             ? new DataContractSerializer(obj.GetType())
                                             : new DataContractSerializer(obj.GetType(), knowTypes);
            var memoryStream = new MemoryStream();
            dcs.WriteObject(memoryStream, obj);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        public static string ToJson(Dictionary<string, object> data)
        {
            return ToJson(data, false);
        }

        public static string ToJson(Dictionary<string, object> data, bool format)
        {
            var buffer = new StringBuilder("{");
            if (format)
            {
                buffer.Append("\r\n");
            }
            foreach (string o in data.Keys)
            {
                buffer.Append("{\"");
                if (format)
                {
                    buffer.Append("\r\n");
                }
                buffer.Append(o)
                    .Append("\":")
                    .Append(ToJson(data[o]));
                if (format)
                    buffer.Append("\r\n");
            }
            buffer.Append("};");
            if (format)
            {
                buffer.Append("\r\n");
            }
            return buffer.ToString();
        }
    }
}