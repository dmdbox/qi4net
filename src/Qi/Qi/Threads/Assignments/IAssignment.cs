using System.Collections.Generic;

namespace Qi.Threads.Assignments
{
    public interface IAssignment
    {
        IEnumerable<object[]> Assign(object[] data, int threadNumber);
    }
}