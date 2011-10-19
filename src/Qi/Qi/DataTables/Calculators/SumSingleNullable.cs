using System;

namespace Qi.DataTables.Calculators
{
    internal class SumSingleNullable : Sum<float?>
    {
        protected override Single? Calculate(Single? lastData, Single? rowValue)
        {
            return (lastData ?? 0) + (rowValue ?? 0);
        }
    }
}