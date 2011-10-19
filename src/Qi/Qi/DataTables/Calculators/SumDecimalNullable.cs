using System;

namespace Qi.DataTables.Calculators
{
    internal class SumDecimalNullable : Sum<decimal?>
    {
        protected override Decimal? Calculate(Decimal? lastData, Decimal? rowValue)
        {
            return (lastData ?? 0) + (rowValue ?? 0);
        }
    }
}