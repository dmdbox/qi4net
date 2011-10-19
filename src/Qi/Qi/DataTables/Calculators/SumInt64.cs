using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt64 : Sum<long>
    {
        protected override Int64 Calculate(Int64 lastData, Int64 rowValue)
        {
            return lastData + rowValue;
        }
    }
}