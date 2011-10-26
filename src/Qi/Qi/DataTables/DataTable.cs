using System;
using System.Collections.Generic;
using System.Linq;
using Qi.DataTables.Columns;

namespace Qi.DataTables
{
    public class DataTable<T> : IDataTable
    {
        private ColumnCollection _columns = new ColumnCollection();

        private IEnumerable<T> _data;

        #region IDataTable Members

        public ColumnCollection Columns
        {
            get { return _columns ?? (_columns = new ColumnCollection()); }
        }


        public string[] ColumnNames
        {
            get { return (from v in Columns select v.Name).ToArray(); }
        }

        /// <summary>
        /// 獲取所有的Rows
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object[]> GetRows()
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
            foreach (IColumn column in Columns)
            {
                column.Clear();
            }
            return result;
        }


        IDataTable IDataTable.SetData(IEnumerable<object> items)
        {
            List<T> a = items.Select(item => (T)item).ToList();
            return SetData(a);
        }


        public bool HasRows
        {
            get { return _data != null && _data.Any(); }
        }

        #endregion

        public object[] GetSummaries(string calculatorName)
        {
            var result = new object[Columns.Count];
            int index = 0;
            foreach (IColumn column in Columns)
            {
                if (column.HasCaculator(calculatorName))
                {
                    result[index] = column.GetResult(calculatorName);
                }
                index++;
            }
            return result;
        }

        public IColumn Column<TReturnValue>(string columnName, Func<T, TReturnValue> accessor)
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

        public DataTable<T> SetData(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            _data = items;
            return this;
        }
    }
}