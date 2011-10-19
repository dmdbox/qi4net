using System;

namespace Qi.DataTables.Calculators
{
    internal class SumDouble : Sum<double>
    {
        protected override Double Calculate(Double lastData, Double rowValue)
        {
            return lastData + rowValue;
        }
    }
}