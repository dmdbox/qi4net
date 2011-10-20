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
            get { return _columns ?? (_columns = new ColumnCollection()); }
        }

        public string[] ColumnName
        {
            get { return (from v in Columns select v.Name).ToArray(); }
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
            var column = new Column<T, TReturnValue>(columnName) {Accessor = accessor};
            Columns.Add(column);
            return column;
        }


        public void SetData(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            _data = items;
        }

       

       
    }
}