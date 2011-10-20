using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt32Nullable : Sum<int?>
    {
        public SumInt32Nullable()
            : base(s => Convert.ToInt32(s), (a, b) => (a ?? 0) + (b ?? 0))
        {
        }
    }
}