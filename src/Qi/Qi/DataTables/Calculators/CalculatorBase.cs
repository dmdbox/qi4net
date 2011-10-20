using System;

namespace Qi.DataTables.Calculators
{
    internal abstract class CalculatorBase<T> : ICalculator
    {
        private readonly Func<T, T, T> _calculate;
        private readonly Func<object, T> _convertor;

        protected CalculatorBase(Func<object, T> convertor, Func<T, T, T> calculate)
        {
            if (convertor == null) throw new ArgumentNullException("convertor");
            if (calculate == null) throw new ArgumentNullException("calculate");
            _convertor = convertor;
            _calculate = calculate;
        }

        #region ICalculator<T> Members

        public abstract string Name { get; }
        object ICalculator.Result { get { return this.Result; } }
        public T Result { get; private set; }

        public void SetValue(object rowValue)
        {
            Result = _calculate(Result, _convertor(rowValue));
        }

        public void Clear()
        {
            Result = default(T);
        }

        #endregion
    }
}