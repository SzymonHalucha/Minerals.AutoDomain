namespace Minerals.AutoDomain.Tests
{
    [TestClass]
    public class GenerateDomainEventGeneratorTests : VerifyBase
    {
        public GenerateDomainEventGeneratorTests()
        {
            var references = VerifyExtensions.GetAppReferences
            (
                typeof(object),
                typeof(CodeBuilder),
                typeof(IAggregateRoot),
                typeof(StringCases.StringExtensions),
                typeof(GenerateDomainEventGenerator),
                typeof(Assembly)
            );
            VerifyExtensions.Initialize(references);
        }

        [TestMethod]
        public Task InEntityMethod_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [Entity]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new EntityGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task InNamespaceAndEntityMethod_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            namespace Minerals.Tests
            {
                [Entity]
                public partial class TestClass
                {
                    [GenerateDomainEvent("ExampleDomainEvent")]
                    public void TestMethod() { }
                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new EntityGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task InAggregateRootMethod_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task MultipleInlineAttributes_ShouldGenerateAll()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent1"), GenerateDomainEvent("ExampleDomainEvent2")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task MultipleStandaloneAttributes_ShouldGenerateAll()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent1")]
                [GenerateDomainEvent("ExampleDomainEvent2")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnMethod_ShouldGenerateWithoutId()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent", false)]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnProperty_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent")]
                public int Property1 { get; private set; } = 1;
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnClass_ShouldGenerateWithoutId()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot, GenerateDomainEvent("ExampleDomainEvent", false)]
            public partial class TestClass { }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnConstructor_ShouldGenerateWithArguments()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent")]
                public TestClass(int exampleNumber, string exampleText)
                {

                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnConstructor_ShouldGenerateWithArgumentsAndWithoutId()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [GenerateDomainEvent("ExampleDomainEvent", false)]
                public TestClass(int exampleNumber, string exampleText)
                {

                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task ArgumentFromOtherNamespace_ShouldGenerateWithUsing()
        {
            const string source = """
            using Minerals.AutoDomain;
            using OtherNamespace;

            namespace Minerals.Examples
            {
                [AggregateRoot]
                public partial class TestClass
                {
                    [GenerateDomainEvent("ExampleDomainEvent")]
                    public TestClass(OtherStruct otherStruct)
                    {

                    }
                }
            }

            namespace OtherNamespace
            {
                public struct OtherStruct { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new GenerateDomainEventGenerator(), additional);
        }
    }
}