using PreTestCodeAnalyserConsoleHost.ExampleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTestCodeAnalyserConsoleHost.ExampleClasses
{
    public class ExampleInjectedService : IExampleInjectedService
    {
        public string InjectedExampleMethod(string name)
        {
            return $"Hello {name}, greetings from the injected service!";
        }
    }
}
