using System;
using System.Collections.Generic;

namespace Qi.DataTables.Columns
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">T for colum's Value type.</typeparam>
    public abstract class AbstractColumn<T> : IColumn
    {
        private Dictionary<string, ICalculator<T>> _sets;
        private int _rowObjectHasCode;

        protected AbstractColumn(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            Name = name;
        }

        private Dictionary<string, ICalculator<T>> Sets
        {
            get { return _sets ?? (_sets = new Dictionary<string, ICalculator<T>>()); }
        }

        #region IColumn Members

        public string Name { get; set; }

        object IColumn.GetValue(object data)
        {
            return GetValue(data);
        }

        object IColumn.Sum()
        {
            if (Sets.ContainsKey("Sum"))
            {
                return Sets["Sum"].Result;
            }
            throw new ArgumentException(this.Name + "do not set the Sum function.");
        }

        public void Reset()
        {
            cacheData = null;
        }

        #endregion

        public T GetValue(object data)
        {
            bool sameRowObject = _rowObjectHasCode == data.GetHashCode();

            cacheData = sameRowObject ? cacheData : InvokeObject(data);

            if (_sets != null && !sameRowObject)
            {
                _rowObjectHasCode = data.GetHashCode();
                foreach (var item in Sets.Values)
                {
                    item.SetValue(data, (T)cacheData);
                }
            }
            return (T)cacheData;
        }
        //private object justTest(object rowObject)
        //{
        //    Console.WriteLine(this.Name + " InvokeObject Called");
        //    return InvokeObject(rowObject);
        //}
        protected abstract object InvokeObject(object rowObject);

        public void Add(ICalculator<T> result)
        {
            if (result == null)
                throw new ArgumentNullException("result");
            Sets.Add(result.Name, result);
        }

        private object cacheData;
    }
}