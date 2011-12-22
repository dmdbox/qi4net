using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qi.Threads
{
    public static class ThreadQuery
    {
        /// <summary>
        /// 以同步的方式自动分配数据到多个线程中区
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="threadCount"></param>
        /// <param name="data"></param>
        /// <param name="executeFunction"></param>
        public static void AvgExecute<T>(int threadCount, IList<T> data,
            ExecutionHandler<T> executeFunction)
        {
            var thread = new MultiThreadAssignment<T>(threadCount, executeFunction);
            thread.Execute(data);
        }
    }
}
