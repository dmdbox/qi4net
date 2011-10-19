using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt64Nullable : Sum<long?>
    {
        protected override Int64? Calculate(Int64? lastData, Int64? rowValue)
        {
            return (lastData ?? 0) + (rowValue ?? 0);
        }
    }
}