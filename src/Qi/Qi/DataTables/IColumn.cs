namespace Qi.DataTables
{
    public interface IColumn
    {
        string Name { get; set; }

        object GetValue(object data);

        bool HasCaculator(string calculatorName);
        object GetResult(string calculatorName);
        
        object SumResult();

        void Add(ICalculator calculator);

        void Clear();

        /// <summary>
        /// Reset result of the calulators
        /// </summary>
        void Reset();
    }
}