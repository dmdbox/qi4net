using System;

namespace Qi.DataTables.Calculators.Avgs
{
    internal class AvgDecimal : Avg<decimal>
    {
        public AvgDecimal()
            : base(Convert.ToDecimal, (a, b) => a + b)
        {
        }


        internal static AvgDecimal Create()
        {
            return new AvgDecimal();
        }

        protected override decimal getAvag(decimal total, int count)
        {
            return total/count;
        }
    }
}