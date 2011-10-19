using System;

namespace Qi.DataTables.Calculators
{
    internal class SumSingle : Sum<float>
    {
        protected override Single Calculate(Single lastData, Single rowValue)
        {
            return lastData + rowValue;
        }
    }
}