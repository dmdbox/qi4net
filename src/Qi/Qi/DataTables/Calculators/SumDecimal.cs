namespace Qi.DataTables.Calculators
{
    internal class SumDecimal : Sum<decimal>
    {
        protected override decimal Calculate(decimal lastData, decimal rowValue)
        {
            return lastData + rowValue;
        }
    }
}