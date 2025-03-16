using Microsoft.CodeAnalysis;
using PreTestCodeAnalyser;

namespace UnitTestGeneratorUnitTests.PreTestCodeAnalyser
{
    public class PreTestCodeAnalyserServiceUnitTests
    {
        [Fact]
        public async Task AreAllServicesInjectedAsync_ExampleServicesContainInjectedAndNotInjectedServices_PicksUpMixed()
        {
            //arrange
            var workspace = new AdhocWorkspace();
            var project = workspace.AddProject("TestProject", LanguageNames.CSharp);

            var sourceCode = @"
                public class MyClass
                {
                    private readonly MyService _service = new MyService();

                    public MyClass() { }
                }
            ";

            var document = project.AddDocument("TestClass.cs", sourceCode);

            var syntaxTree = await document.GetSyntaxTreeAsync();
            var compilation = await project.GetCompilationAsync();

            if (syntaxTree != null && !compilation.SyntaxTrees.Contains(syntaxTree))
            {
                compilation = compilation.AddSyntaxTrees(syntaxTree);
            }

            //act
            bool result = await PreTestAnalyserService.AreAllServicesInjectedAsync(document, compilation);


            //assert
            Assert.False(result, "AnalyzeProject should return false when a service is instantiated directly.");
        }
    }
}