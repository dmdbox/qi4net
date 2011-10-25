using System;
using System.Collections.Generic;
using Qi.DataTables.Calculators;
using Qi.DataTables.Calculators.Sums;

namespace Qi.DataTables
{
    public static class Calculator
    {
        private static readonly Dictionary<Type, Func<ICalculator>> SumMap = new Dictionary<Type, Func<ICalculator>>
                                                                                  {
                                                                                      {typeof (int), SumInt32.Create},
                                                                                      {typeof (int?),SumInt32Nullable.Create},
                                                                                      {typeof (long), SumInt64.Create},
                                                                                      {typeof (long?),SumInt64Nullable.Create},
                                                                                      {typeof (decimal),SumDecimal.Create},
                                                                                      {typeof (decimal?),SumDecimalNullable.Create},
                                                                                      {typeof (Single),SumSingle.Create},
                                                                                      {typeof (Single?),SumSingleNullable.Create},
                                                                                      {typeof (double),SumDouble.Create},
                                                                                      {typeof (double?),SumDoubleNullable.Create},
                                                                                  };

        public static IColumn Sum<T>(this IColumn column)
        {
            column.Add(CreateSumCalculator(typeof (T)));
            return column;
        }

        public static ICalculator CreateSumCalculator(Type t)
        {
            return SumMap[t].Invoke();
        }
    }
}