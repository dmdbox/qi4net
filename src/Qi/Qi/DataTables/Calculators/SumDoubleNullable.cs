using System;

namespace Qi.DataTables.Calculators
{
    internal class SumDoubleNullable : Sum<double?>
    {
        protected override Double? Calculate(Double? lastData, Double? rowValue)
        {
            return (lastData ?? 0) + (rowValue ?? 0);
        }
    }
}