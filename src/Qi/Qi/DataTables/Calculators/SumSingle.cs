using System;

namespace Qi.DataTables.Calculators
{
    internal class SumSingle : Sum<float>
    {
        public SumSingle()
            : base(Convert.ToSingle, (a, b) => a + b)
        {
        }
    }
}