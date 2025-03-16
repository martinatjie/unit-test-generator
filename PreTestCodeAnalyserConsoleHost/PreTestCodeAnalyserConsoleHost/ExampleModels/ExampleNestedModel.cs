using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTestCodeAnalyserConsoleHost.ExampleModels
{
    public class ExampleNestedModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public ExampleEnum ExampleNestedEnum { get; set; }
    }

    public enum ExampleEnum
    {
        ExampleValue1,
        ExampleValue2,
        ExampleValue3
    }
}
