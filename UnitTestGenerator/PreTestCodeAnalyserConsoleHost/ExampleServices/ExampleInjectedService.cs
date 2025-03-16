using PreTestCodeAnalyserConsoleHost.ExampleInterfaces;

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
