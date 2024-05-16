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
        public Task InEntityMethod_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [Entity]
            public partial class TestClass
            {
                [NewDomainEvent("ExampleDomainEvent")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new EntityGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
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
                    [NewDomainEvent("ExampleDomainEvent")]
                    public void TestMethod() { }
                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new EntityGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task InAggregateRootMethod_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [NewDomainEvent("ExampleDomainEvent")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task MultipleInlineAttributes_ShouldGenerateAll()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [NewDomainEvent("ExampleDomainEvent1"), NewDomainEvent("ExampleDomainEvent2")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task MultipleStandaloneAttributes_ShouldGenerateAll()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [NewDomainEvent("ExampleDomainEvent1")]
                [NewDomainEvent("ExampleDomainEvent2")]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnMethod_ShouldGenerateWithoutId()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [NewDomainEvent("ExampleDomainEvent", false)]
                public void TestMethod() { }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnProperty_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass
            {
                [NewDomainEvent("ExampleDomainEvent")]
                public int Property1 { get; private set; } = 1;
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnClass_ShouldGenerateWithoutId()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot, NewDomainEvent("ExampleDomainEvent", false)]
            public partial class TestClass { }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnConstructor_ShouldGenerateWithArguments()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass 
            {
                [NewDomainEvent("ExampleDomainEvent")]
                public TestClass(int exampleNumber, string exampleText)
                {

                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }

        [TestMethod]
        public Task OnConstructor_ShouldGenerateWithArgumentsAndWithoutId()
        {
            const string source = """
            using Minerals.AutoDomain;

            [AggregateRoot]
            public partial class TestClass 
            {
                [NewDomainEvent("ExampleDomainEvent", false)]
                public TestClass(int exampleNumber, string exampleText)
                {

                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new AttributesGenerator(),
                new InterfacesGenerator(),
                new AggregateRootGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new DomainEventGenerator(), additional);
        }
    }
}