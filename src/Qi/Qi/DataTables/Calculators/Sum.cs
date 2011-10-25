using System;

namespace Qi.DataTables.Calculators
{
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

    internal abstract class Avg<T> : Sum<T>
    {
        private int _count;

        protected Avg(Func<object, T> convertor, Func<T, T, T> calculate)
            : base(convertor, calculate)
        {
        }

        public override T Result
        {
            get
            {
                if (_count != 0)
                {
                    return getAvag(base.Result, _count);
                }
                return default(T);
            }
        }

        public override string Name
        {
            get { return "Avg"; }
        }

        public override void SetValue(object rowValue)
        {
            base.SetValue(rowValue);
            _count++;
        }

        protected abstract T getAvag(T total, int count);
    }
}