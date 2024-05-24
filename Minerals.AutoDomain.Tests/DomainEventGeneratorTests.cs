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
                typeof(IAggregateRoot),
                typeof(StringCases.StringExtensions),
                typeof(DomainEventGenerator),
                typeof(Assembly)
            );
            VerifyExtensions.Initialize(references);
        }

        [TestMethod]
        public Task EmptyEvent_ShouldNotGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [DomainEvent]
            public readonly partial struct TestEvent
            {

            }
            """;
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator());
        }

        [TestMethod]
        public Task EventWithSingleArgument_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [DomainEvent]
            public readonly partial struct TestEvent
            {
                public int Property1 { get; set; }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator());
        }

        [TestMethod]
        public Task EventWithMultipleArguments_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [DomainEvent]
            public readonly partial struct TestEvent
            {
                public int Property1 { get; set; }
                public float Property2 { get; set; }

                public string Field1;
            }
            """;
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator());
        }

        [TestMethod]
        public Task EventInNamespace_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            namespace Examples
            {
                [DomainEvent]
                public readonly partial struct TestEvent
                {
                    public int Property1 { get; set; }
                    public float Property2 { get; set; }

                    public string Field1;
                }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator());
        }
    }
}