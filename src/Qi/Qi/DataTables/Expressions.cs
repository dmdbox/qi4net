using System;
using System.Linq;
using Qi.DataTables.Calculators;
using Qi.DataTables.Columns;

namespace Qi.DataTables
{
    public static class Expressions
    {
        private static T1[] GetColumns<T1, T>(DataTable<T> table, params string[] columnNames) where T1 : class
        {
            return columnNames.Select(a => (T1)table.Columns[a]).ToArray();
        }
       //private static AbstractColumn<T1> Sum<T0,T1>(this DataTable<T0> table,string columnName,
       //                                            params AbstractColumn<T1>[] columns)
       //{
       //    var calcutator = new SumInt32();
       //}

        public static AbstractColumn<Int32> Sum<T>(this DataTable<T> table, string columnName,
                                                 params string[] columnNames)
        {
            var columns = GetColumns<AbstractColumn<Int32>, T>(table, columnNames);
            return table.Sum(columnName, columns);
        }

        public static AbstractColumn<Int32> Sum<T>(this DataTable<T> table, string columnName,
                                                   params AbstractColumn<Int32>[] columns)
        {
            var column = new CalculatorColumn<Int32>(columnName, new SumInt32(), columns);
            table.Columns.Add(column);
            return column;
        }

        public static AbstractColumn<Int32?> Sum<T>(this DataTable<T> table, string columnName,
                                                    params AbstractColumn<Int32?>[] columns)
        {
            var column = new CalculatorColumn<Int32?>(columnName, new SumInt32Nullable(), columns);
            table.Columns.Add(column);
            return column;
        }

        #region decimal

        public static AbstractColumn<decimal> SumDecimal<T>(this DataTable<T> table, string columnName,
                                                            params string[] columnNames)
        {
            AbstractColumn<decimal>[] columns = GetColumns<AbstractColumn<decimal>, T>(table, columnNames);
            return table.Sum(columnName, columns);
        }

        public static AbstractColumn<decimal> Sum<T>(this DataTable<T> table, string columnName,
                                                     params AbstractColumn<Decimal>[] columns)
        {
            var column = new CalculatorColumn<decimal>(columnName, new SumDecimal(), columns);
            table.Columns.Add(column);
            return column;
        }

        public static AbstractColumn<decimal?> Sum<T>(this DataTable<T> table, string columnName,
                                                      params AbstractColumn<Decimal?>[] columns)
        {
            var column = new CalculatorColumn<decimal?>(columnName, new SumDecimalNullable(), columns);
            table.Columns.Add(column);
            return column;
        }

        public static AbstractColumn<decimal?> SumDecimalNullable<T>(this DataTable<T> table, string columnName,
                                                                     params string[] columnNames)
        {
            AbstractColumn<decimal?>[] columns = GetColumns<AbstractColumn<decimal?>, T>(table, columnNames);
            return table.Sum(columnName, columns);
        }

        #endregion
    }
}