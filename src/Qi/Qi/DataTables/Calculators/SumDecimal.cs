using System;

namespace Qi.DataTables.Calculators
{
    internal class SumDecimal : Sum<decimal>
    {
        public SumDecimal()
            : base(Convert.ToDecimal, (a, b) => a + b)
        {
        }

        internal static SumDecimal Create()
        {
            return new SumDecimal();
        }
    }
}