using Microsoft.Extensions.Logging;
using PreTestCodeAnalyserConsoleHost.ExampleInterfaces;

namespace PreTestCodeAnalyserConsoleHost.ExampleClasses
{
    public class ExampleService
    {
        private readonly ILogger<ExampleService> _logger;
        private readonly IExampleInjectedService _exampleInjectedService;

        public ExampleService(ILogger<ExampleService> logger, IExampleInjectedService exampleInjectedService)
        {
            _logger = logger;
            _exampleInjectedService = exampleInjectedService;
        }
        public void ExampleMethodWithoutReturn()
        {
            Console.WriteLine("Hello World!");
        }

        public string ExampleMethodWithReturn()
        {
            return "Hello World!";
        }

        public void ExampleMethodWithParams(string message)
        {
            Console.WriteLine(message);
        }

        public void ExampleMethodWithParamAndReturn(string message)
        {
            Console.WriteLine(message);
        }

        public void ExampleMethodWithParamsAndReturn(string message, string message2)
        {
            Console.WriteLine(message);
            Console.WriteLine(message2);
        }

        public async Task<string> ExampleMethodWithAsync()
        {
            return await Task.FromResult("Hello World!");
        }

        public async void ExampleMethodThatCallsPrivateMethod()
        {
            await ExamplePrivateMethod();
        }

        private async Task ExamplePrivateMethod()
        {
            await Task.Delay(1000);
        }

        public void ExampleMethodThatCallsStaticMethod()
        {
            var exampleModel = ExampleInstantiatedService.CreateExampleModel();
        }

        public string ExampleMethodCallsInstantiatedService()
        {
            var exampleInstantiatedService = new ExampleInstantiatedService();
            var word = "world";
            var sentence = exampleInstantiatedService.NonStaticExampleMethod(word);
            _logger.LogInformation("Successfully retrieved sentence with word {Word}", word);
            return sentence;
        }

        public string ExampleMethodCallsInjectedService(string name)
        {
            return _exampleInjectedService.InjectedExampleMethod(name);
        }
    }
}
