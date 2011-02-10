using System;
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
            var dcs = knowTypes == null ? new DataContractSerializer(obj.GetType()) : new DataContractSerializer(obj.GetType(), knowTypes);
            var memoryStream = new MemoryStream();
            dcs.WriteObject(memoryStream, obj);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}