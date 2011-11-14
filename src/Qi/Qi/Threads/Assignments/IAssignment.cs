using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qi.Threads.Assignments
{
    public interface IAssignment
    {
        IEnumerable<object[]> Assign(object[] data, int threadNumber);
    }
}
