using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt64 : Sum<long>
    {
        public SumInt64()
            : base(Convert.ToInt64, (a, b) => a + b)
        {
     
        }

        internal static SumInt64 Create()
        {
            return new SumInt64();
        }
    }
}