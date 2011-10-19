using System;

namespace Qi.DataTables.Columns
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TReturnValue"></typeparam>
    public class Column<T, TReturnValue> : AbstractColumn<TReturnValue>
    {
        public Column(string name)
            : base(name)
        {
        }

        public Func<T, TReturnValue> Accessor { get; set; }

        protected override object InvokeObject(object rowObject)
        {
            if (Accessor == null)
                return null;
            TReturnValue result = Accessor.Invoke((T) rowObject);
            return result;
        }
    }
}