namespace Minerals.AutoDomain.Tests
{
    [TestClass]
    public class AggregateRootGeneratorTests : VerifyBase
    {
        public AggregateRootGeneratorTests()
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
        public Task AggregateRoot_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass { }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
            ];
            return this.VerifyIncrementalGenerators(source, new AggregateRootGenerator(), additional);
        }

        [TestMethod]
        public Task AggregateRootWithNamespace_ShouldGenerate()
        {
            const string source = """
            [Minerals.AutoDomain.AggregateRoot]
            public partial class TestClass { }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
            ];
            return this.VerifyIncrementalGenerators(source, new AggregateRootGenerator(), additional);
        }

        [TestMethod]
        public Task AggregateRootInNamespace_ShouldGenerate()
        {
            const string source = """
            namespace Minerals.Tests
            {
                [Minerals.AutoDomain.AggregateRoot]
                public partial class TestClass { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
            ];
            return this.VerifyIncrementalGenerators(source, new AggregateRootGenerator(), additional);
        }
    }
}