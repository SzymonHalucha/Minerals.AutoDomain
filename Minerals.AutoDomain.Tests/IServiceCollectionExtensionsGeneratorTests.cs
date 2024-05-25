using Minerals.AutoDomain;
using System.Threading;

namespace Minerals.AutoDomain.Tests
{
    [TestClass]
    public class IServiceCollectionExtensionsGeneratorTests : VerifyBase
    {
        public IServiceCollectionExtensionsGeneratorTests()
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
        public Task SingleDomainEventHandler_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [DomainEvent]
            public readonly partial struct TestDomainEvent
            {
                public int Property1 { get; }
            }

            public class TestDomainEventHandler : IDomainEventHandler<TestDomainEvent>
            {
                public Task Handle(TestDomainEvent domainEvent, CancellationToken cancellation)
                {
                    throw new NotImplementedException();
                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new DomainEventGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator(), additional);
        }

        [TestMethod]
        public Task MultiDomainEventsHandler_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoDomain;

            [DomainEvent]
            public readonly partial struct TestDomainEvent : IDomainEvent
            {
                public int Property1 { get; }
            }

            public class TestDomainEventHandler1 : IDomainEventHandler<TestDomainEvent>
            {
                public Task Handle(TestDomainEvent domainEvent, CancellationToken cancellation)
                {
                    throw new NotImplementedException();
                }
            }

            public class TestDomainEventHandler2 : IDomainEventHandler<TestDomainEvent>
            {
                public Task Handle(TestDomainEvent domainEvent, CancellationToken cancellation)
                {
                    throw new NotImplementedException();
                }
            }
            """;
            IIncrementalGenerator[] additional =
            [
                new DomainEventGenerator()
            ];
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator(), additional);
        }
    }
}