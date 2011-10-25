using System;

namespace Qi.DataTables
{
    public interface IColumn
    {
        string Name { get; set; }

        object GetValue(object data);

        object SumResult();

        void Add(ICalculator calculator);

        void Clear();
        /// <summary>
        /// Reset result of the calulators
        /// </summary>
        void Reset();


    }
}