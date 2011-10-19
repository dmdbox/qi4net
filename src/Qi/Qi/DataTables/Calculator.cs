using System;
using Qi.DataTables.Calculators;
using Qi.DataTables.Columns;

namespace Qi.DataTables
{
    public static class Calculator
    {
        public static AbstractColumn<decimal> ForSum(
            this AbstractColumn<decimal> column)
        {
            var result = new SumDecimal();
            column.Add(result);
            return column;
        }

        public static AbstractColumn<decimal?> ForSum(
           this AbstractColumn<decimal?> column)
        {
            var result = new SumDecimalNullable();
            column.Add(result);
            return column;
        }

        public static AbstractColumn<Int32> ForSum(
            this AbstractColumn<Int32> column)
        {
            var result = new SumInt32();
            column.Add(result);
            return column;
        }

        public static AbstractColumn<Int64> ForSum(
            this AbstractColumn<Int64> column)
        {
            var result = new SumInt64();
            column.Add(result);
            return column;
        }
    }
}