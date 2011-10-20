namespace Qi.DataTables
{
    public interface ICalculator
    {
        string Name { get; }

        object Result { get; }

        /// <summary>
        /// set value to this object for calculate;
        /// </summary>
        /// <param name="rowValue"></param>
        void SetValue(object rowValue);

        /// <summary>
        /// 
        /// </summary>
        void Clear();
    }
}