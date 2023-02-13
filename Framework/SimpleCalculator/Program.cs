using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using SimpleCalculator.Interfaces;

namespace SimpleCalculator
{
    internal class Program
    {
        private CompositionContainer _container;

        [Import(typeof(ICalculator))]
        public ICalculator calculator;

        public Program()
        {
            try
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
                catalog.Catalogs.Add(new DirectoryCatalog("C:\\Workspaces\\NEFDemo\\Framework\\SimpleCalculator\\Extensions"));

                _container = new CompositionContainer(catalog);
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
                throw;
            }
        }

        static void Main(string[] args)
        {
            var p = new Program();

            while (true)
            {
                Console.WriteLine("Enter command:");
                string s = Console.ReadLine();
                Console.WriteLine(p.calculator.Calculate(s));
                Console.WriteLine();
            }
        }
    }
}