using System;
using System.Collections.Generic;

namespace Qi.DataTables
{
    
    public interface IDataTable
    {
        ColumnCollection Columns { get; }
        bool HasRows { get; }
        string[] ColumnNames { get; }
        IEnumerable<object[]> GetRows();
        IDataTable SetData(IEnumerable<object> items);
        void Column(string amount1, Func<object, object> func);
    }
}