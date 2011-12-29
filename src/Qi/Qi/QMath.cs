using System;

namespace Qi
{
    public static class QMath
    {
        public static decimal Ceilling(decimal i, int midPointRouting)
        {
            checked
            {
                var vs = Convert.ToDecimal(Math.Pow(10, midPointRouting));
                decimal res = Math.Ceiling(i * (vs));
                return Convert.ToDecimal(res / vs);
            }
        }
    }
}