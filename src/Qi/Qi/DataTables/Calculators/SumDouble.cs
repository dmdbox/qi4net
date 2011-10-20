using System;

namespace Qi.DataTables.Calculators
{
    internal class SumDouble : Sum<double>
    {
        public SumDouble()
            : base(Convert.ToDouble, (a, b) => a + b)
        {
        }
    }
}