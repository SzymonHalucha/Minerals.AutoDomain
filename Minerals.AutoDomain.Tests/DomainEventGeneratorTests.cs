namespace Minerals.AutoDomain.Tests
{
    [TestClass]
    public class DomainEventGeneratorTests : VerifyBase
    {
        public DomainEventGeneratorTests()
        {
            var references = VerifyExtensions.GetAppReferences
            (
                typeof(object),
                typeof(CodeBuilder),
                typeof(StringCases.StringExtensions),
                typeof(DomainEventGenerator),
                typeof(Assembly)
            );
            VerifyExtensions.Initialize(references);
        }

        [TestMethod]
        public Task ShouldGenerate()
        {
            const string source = """
            using System;
            using Minerals.AutoDomain;

            [Entity]
            public partial class TestClass
            {
                [NewDomainEvent("ExampleDomainEvent")]
                public void TestMethod()
                {

                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator(),
                new EntityGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }
    }
}