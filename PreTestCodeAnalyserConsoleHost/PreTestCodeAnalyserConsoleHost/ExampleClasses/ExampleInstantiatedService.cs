using PreTestCodeAnalyserConsoleHost.ExampleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTestCodeAnalyserConsoleHost.ExampleClasses
{
    public class ExampleInstantiatedService
    {
        public static ExampleModel CreateExampleModel()
        {
            return new ExampleModel
            {
                Id = 1,
                Name = "Example",
                Description = "Example Description",
                Enabled = true,
                NestedModel = new ExampleNestedModel
                {
                    Id = 1,
                    Deleted = false,
                    ExampleNestedEnum = ExampleEnum.ExampleValue1
                }
            };
        }

        public string NonStaticExampleMethod(string name)
        {
            return $"Hello {name}, greetings from the instantiated service!";
        }
    }
}
