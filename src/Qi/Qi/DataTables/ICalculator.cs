namespace Qi.DataTables
{
    public interface ICalculator<T>
    {
        string Name { get; }

        T Result { get; }
        /// <summary>
        /// set value to this object for calculate;
        /// </summary>
        /// <param name="rowObject"></param>
        /// <param name="rowValue"></param>
        void SetValue(object rowObject, T rowValue);
        /// <summary>
        /// 
        /// </summary>
        void Clear();
    }
}