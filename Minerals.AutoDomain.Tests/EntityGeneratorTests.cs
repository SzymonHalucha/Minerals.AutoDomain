namespace Minerals.AutoDomain.Tests
{
    [TestClass]
    public class EntityGeneratorTests : VerifyBase
    {
        public EntityGeneratorTests()
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
        public Task Entity_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [Entity]
            public partial class TestClass { }
            """;
            return this.VerifyIncrementalGenerators(source, new EntityGenerator());
        }

        [TestMethod]
        public Task EntityWithNamespace_ShouldGenerate()
        {
            const string source = """
            [Minerals.AutoDomain.Entity]
            public partial class TestClass { }
            """;
            return this.VerifyIncrementalGenerators(source, new EntityGenerator());
        }

        [TestMethod]
        public Task EntityInNamespace_ShouldGenerate()
        {
            const string source = """
            namespace Minerals.Tests
            {
                [Minerals.AutoDomain.Entity]
                public partial class TestClass { }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new EntityGenerator());
        }
    }
}