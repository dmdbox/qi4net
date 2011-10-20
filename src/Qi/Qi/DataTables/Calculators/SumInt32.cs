using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt32 : Sum<int>
    {
        public SumInt32()
            : base(Convert.ToInt32, (a, b) => a + b)
        {
        }
    }
}