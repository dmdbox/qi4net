using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qi.Threads
{
    public static class ThreadQuery
    {
        public static void AvgExecute<T>(int threadCount, IList<T> data,
            ExecutionHandler<T> executeFunction)
        {
            var thread = new MultiThreadAssignment<T>(threadCount, executeFunction);
            thread.Execute(data);

        }
    }
}
