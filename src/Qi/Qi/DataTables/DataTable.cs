using System;
using System.Collections.Generic;
using System.Linq;
using Qi.DataTables.Calculators;
using Qi.DataTables.Columns;

namespace Qi.DataTables
{
    public class DataTable<T>
    {
        private ColumnCollection _columns = new ColumnCollection();
        private IEnumerable<T> _data;

        public ColumnCollection Columns
        {
            get
            {
                return _columns ?? (_columns = new ColumnCollection());
            }
        }



        public IEnumerable<object[]> Rows
        {
            get
            {
                var result = new List<object[]>();
                if (_data != null)
                {
                    foreach (T data in _data)
                    {
                        var item = new object[_columns.Count];
                        for (int index = 0; index < Columns.Count; index++)
                        {
                            item[index] = Columns[index].GetValue(data);
                        }
                        result.Add(item);
                    }
                }
                return result;
            }
        }


        public AbstractColumn<TReturnValue> Column<TReturnValue>(string columnName, Func<T, TReturnValue> accessor)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");
            if (accessor == null)
                throw new ArgumentNullException("accessor");
            if (Columns.Contains(columnName))
            {
                throw new ArgumentOutOfRangeException(columnName + " is duplicate column name.");
            }
            var column = new Column<T, TReturnValue>(columnName) { Accessor = accessor };
            Columns.Add(column);
            return column;
        }


        public void SetData(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            _data = items;
        }
        private T[] GetColumns<T>(params string[] columnNames) where T : class
        {
            return columnNames.Select(a => (T)this.Columns[a]).ToArray();
        }

        public AbstractColumn<decimal> SumDecimal(string columnName, params string[] columnNames)
        {
            var columns = GetColumns<AbstractColumn<decimal>>(columnNames);
            return this.Sum(columnName, columns);
        }

        public AbstractColumn<decimal> Sum(string columnName, params AbstractColumn<Decimal>[] columns)
        {
            var column = new CalculatorColumn<decimal>(columnName, new SumDecimal(), columns);
            Columns.Add(column);
            return column;
        }

        public AbstractColumn<decimal?> Sum(string columnName, params AbstractColumn<Decimal?>[] columns)
        {
            var column = new CalculatorColumn<decimal?>(columnName, new SumDecimalNullable(), columns);
            Columns.Add(column);
            return column;
        }

        public AbstractColumn<decimal?> SumDecimalNullable(string columnName, params string[] columnNames)
        {
            var columns = GetColumns<AbstractColumn<decimal?>>(columnNames);
            return this.Sum(columnName, columns);
        }


        public AbstractColumn<Int32> Sum(string columnName, params AbstractColumn<Int32>[] columns)
        {
            var column = new CalculatorColumn<Int32>(columnName, new SumInt32(), columns);
            Columns.Add(column);
            return column;
        }

        public AbstractColumn<Int32?> Sum(string columnName, params AbstractColumn<Int32?>[] columns)
        {
            var column = new CalculatorColumn<Int32?>(columnName, new SumInt32Nullable(), columns);
            Columns.Add(column);
            return column;
        }
    }
}