using System.Linq;
using Qi.DataTables.Columns;

namespace Qi.DataTables
{
    public static class Expressions
    {
        public static IColumn[] GetColumns(this IDataTable table, params string[] columnNames)
        {
            return columnNames.Select(name => table.Columns[name]).ToArray();
        }

        public static IColumn Sum<TColumnValue>(this IDataTable table, string columnName,
                                                params IColumn[] columns)
        {
            var column = new CalculatorColumn<TColumnValue>(columnName,
                                                            Calculator.CreateSumCalculator(typeof (TColumnValue)),
                                                            columns);
            table.Columns.Add(column);
            return column;
        }


        public static IColumn Sum<TReturnValue>(this IDataTable table, string columnName,
                                                params string[] columnNames)
        {
            IColumn[] columns = table.GetColumns(columnNames);
            var column = new CalculatorColumn<TReturnValue>(columnName,
                                                            Calculator.CreateSumCalculator(typeof (TReturnValue)),
                                                            columns);
            table.Columns.Add(column);
            return column;
        }
    }
}