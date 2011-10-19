using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt32Nullable : Sum<int?>
    {
        protected override Int32? Calculate(Int32? lastData, Int32? rowValue)
        {
            return (lastData ?? 0) + (rowValue ?? 0);
        }
    }
}