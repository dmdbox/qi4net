namespace Qi.DataTables.Columns
{
    public class CalculatorColumn<TReturnValue> : AbstractColumn<TReturnValue>
    {
        private readonly ICalculator<TReturnValue> _calculator;
        private readonly IColumn[] _columns;

        public CalculatorColumn(string name, ICalculator<TReturnValue> calculator, IColumn[] columns)
            : base(name)
        {
            _calculator = calculator;
            _columns = columns;
        }

        protected override object InvokeObject(object rowObject)
        {
            foreach (IColumn col in _columns)
            {
                _calculator.SetValue(rowObject, (TReturnValue) col.GetValue(rowObject));
            }
            TReturnValue result = _calculator.Result;
            _calculator.Clear();
            return result;
        }
    }
}