namespace Qi.DataTables.Calculators
{
    internal abstract class Sum<T> : ICalculator<T>
    {
        protected T LastResult;

        #region ICalculator<T> Members

        public string Name
        {
            get { return "Sum"; }
        }

        public T Result
        {
            get { return LastResult; }
        }

        public void SetValue(object rowObject, T rowValue)
        {
            LastResult = Calculate(LastResult, rowValue);
        }

        public void Clear()
        {
            LastResult = default(T);
        }

        #endregion

        protected abstract T Calculate(T lastData, T rowValue);
    }
}