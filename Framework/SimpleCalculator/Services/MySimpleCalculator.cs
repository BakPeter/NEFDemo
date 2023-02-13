using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SimpleCalculator.Interfaces;

namespace SimpleCalculator.Services
{

    [Export(typeof(ICalculator))]
    public class MySimpleCalculator : ICalculator
    {
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> operations;

        public string Calculate(string input)
        {
            int left, right;

            var fn = FindFirstNonDigit(input);
            if (fn < 0) return "Could not parse command";

            try
            {
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            }
            catch
            {
                return "could not parse command";
            }

            var operation = input[fn];

            foreach (Lazy<IOperation, IOperationData> i in operations)
            {
                if (i.Metadata.Symbol.Equals(operation))
                {
                    return i.Value.Operate(left, right).ToString();
                }
            }

            return "Operation Not Found!";
        }

        private int FindFirstNonDigit(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i])) return i;
            }
            return -1;
        }
    }
}
