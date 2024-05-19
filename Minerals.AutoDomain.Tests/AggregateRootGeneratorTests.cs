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
                typeof(IAggregateRoot),
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
            return this.VerifyIncrementalGenerators(source, new AggregateRootGenerator());
        }

        [TestMethod]
        public Task AggregateRootWithNamespace_ShouldGenerate()
        {
            const string source = """
            [Minerals.AutoDomain.AggregateRoot]
            public partial class TestClass { }
            """;
            return this.VerifyIncrementalGenerators(source, new AggregateRootGenerator());
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
            return this.VerifyIncrementalGenerators(source, new AggregateRootGenerator());
        }
    }
}