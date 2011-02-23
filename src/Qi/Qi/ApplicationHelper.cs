using System;
using System.IO;
using System.Linq;

namespace Qi
{
    public class ApplicationHelper
    {
        /// <summary>
        /// 获取物理路径
        /// </summary>
        public static string PhysicalApplicationPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public static bool IsWeb
        {
            get { return false; }
        }

        public static string MapPath(string path)
        {
            if (path.StartsWith("~"))
                path = path.Substring(1);
            return MapPath(new DirectoryInfo(PhysicalApplicationPath), path);
        }

        public static string MapPath(DirectoryInfo currentDirectory, string mapPath)
        {
            if (currentDirectory == null)
                throw new ArgumentNullException("currentDirectory");
            if (mapPath == null)
                throw new ArgumentNullException("mapPath");

            string currentPath = currentDirectory.FullName;

            string[] currentPathAry = currentPath.Split(
                new[]
                    {
                        currentPath.IndexOf('/') != -1 ? '/' : Path.DirectorySeparatorChar
                    },
                StringSplitOptions.RemoveEmptyEntries
                );
            string[] mapPathAry = mapPath.Split(new[]
                                                    {
                                                        mapPath.IndexOf('/') != -1 ? '/' : Path.DirectorySeparatorChar
                                                    },
                                                StringSplitOptions.RemoveEmptyEntries);
            int subFolder = mapPathAry.Count(pathParty => pathParty == "..");

            var resultSeparator = new string(Path.DirectorySeparatorChar, 1);
            currentPath = subFolder >= currentPathAry.Length
                              ? Path.GetPathRoot(currentPath)
                              : String.Join(resultSeparator, currentPathAry, 0, currentPathAry.Length - subFolder);
            return Path.Combine(currentPath ?? "",
                                string.Join(resultSeparator, mapPathAry, subFolder, mapPathAry.Length - subFolder));
        }
    }
}