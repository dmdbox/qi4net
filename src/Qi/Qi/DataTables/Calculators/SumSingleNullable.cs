using System;

namespace Qi.DataTables.Calculators
{
    internal class SumSingleNullable : Sum<float?>
    {
        public SumSingleNullable()
            : base(s => Convert.ToInt64(s), (a, b) => (a ?? 0) + (b ?? 0))
        {
        }
    }
}