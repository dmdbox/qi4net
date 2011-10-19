using System;

namespace Qi.DataTables.Calculators
{
    internal class SumInt32 : Sum<int>
    {
        protected override Int32 Calculate(Int32 lastData, Int32 rowValue)
        {
            return lastData + rowValue;
        }
    }
}