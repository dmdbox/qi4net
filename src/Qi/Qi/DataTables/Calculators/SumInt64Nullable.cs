using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt64Nullable : Sum<long?>
    {
        public SumInt64Nullable()
            : base(s => Convert.ToInt64(s), (a, b) => (a ?? 0) + (b ?? 0))
        {
        }
    }
}