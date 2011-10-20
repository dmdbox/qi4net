using System;

namespace Qi.DataTables.Calculators
{
    internal abstract class CalculatorBase<T> : ICalculator<T>
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

    internal abstract class Sum<T> : CalculatorBase<T>
    {
        protected Sum(Func<object, T> convertor, Func<T, T, T> calculate)
            : base(convertor, calculate)
        {
        }

        public override string Name
        {
            get { return "Sum"; }
        }
    }
}