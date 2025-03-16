using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PreTestCodeAnalyser
{
    public class PreTestAnalyserService
    {
        public static async Task AnalyzeProjectAsync(Project project)
        {
            var compilation = await project.GetCompilationAsync();
            if (compilation == null) return;

            foreach (var document in project.Documents)
            {
                bool allInjected = await AreAllServicesInjectedAsync(document, compilation);
                Console.WriteLine($"{document.Name}: {allInjected}");
            }
        }

        public async void AnalyseDocumentAsync(Document document)
        {
            try
            {
                var allInjected = await AreAllServicesInjectedAsync(document);
                if (allInjected)
                {
                    Console.WriteLine("All services are injected");
                }
                else
                {
                    Console.WriteLine("Not all services are injected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<bool> AreAllServicesInjectedAsync(Document document, Compilation compilation)
        {
            var syntaxTree = await document.GetSyntaxTreeAsync();
            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            var root = await syntaxTree.GetRootAsync();

            foreach (var classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                var fieldDeclarations = classDeclaration.Members.OfType<FieldDeclarationSyntax>();

                foreach (var field in fieldDeclarations)
                {
                    var variable = field.Declaration.Variables.First();
                    var fieldSymbol = semanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                    if (fieldSymbol == null) continue;

                    var assignments = classDeclaration.DescendantNodes().OfType<AssignmentExpressionSyntax>()
                        .Where(a => a.Left is IdentifierNameSyntax id && id.Identifier.Text == fieldSymbol.Name &&
                                    a.Right is ObjectCreationExpressionSyntax);

                    if (assignments.Any())
                    {
                        Console.WriteLine($"Direct instantiation found: {fieldSymbol.Type.Name} in {document.Name}");
                        return false;
                    }
                }
            }

            return true;
        }

        public static async Task<bool> AreAllServicesInjectedAsync(Document document)
        {
            var syntaxTree = await document.GetSyntaxTreeAsync();
            var semanticModel = await document.GetSemanticModelAsync();
            var root = await syntaxTree.GetRootAsync();

            var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
            if (classDeclaration == null)
            {
                Console.WriteLine("No class found.");
                return true;
            }

            // Find all fields that could be services
            var fieldDeclarations = classDeclaration.Members.OfType<FieldDeclarationSyntax>();

            foreach (var field in fieldDeclarations)
            {
                var variable = field.Declaration.Variables.First();
                var fieldSymbol = semanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                if (fieldSymbol == null) continue;

                // Check if the field is assigned via 'new' anywhere in the class
                var assignments = classDeclaration.DescendantNodes().OfType<AssignmentExpressionSyntax>()
                    .Where(a => a.Left is IdentifierNameSyntax id && id.Identifier.Text == fieldSymbol.Name &&
                                a.Right is ObjectCreationExpressionSyntax);

                if (assignments.Any())
                {
                    Console.WriteLine($"Service '{fieldSymbol.Type.Name}' is instantiated directly.");
                    return false; // Found a direct instantiation
                }
            }

            Console.WriteLine("All services are injected.");
            return true; // No direct instantiations found
        }

    }
}
