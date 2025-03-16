using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTestCodeAnalyserConsoleHost.ExampleModels
{
    public class ExampleModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool Enabled { get; set; }
        public ExampleNestedModel NestedModel { get; set; }
    }
}
