using System;

namespace Qi.DataTables.Calculators
{
    internal class SumDoubleNullable : Sum<double?>
    {
        public SumDoubleNullable():base(s=>Convert.ToDouble(s),(a,b)=>(a ?? 0) + (b ?? 0))
        {
        }

        
    }
}