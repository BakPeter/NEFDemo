using System.ComponentModel.Composition;
using SimpleCalculator.Interfaces;

namespace SimpleCalculator.Services
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '+')]
    public class Add : IOperation
    {
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }
}
