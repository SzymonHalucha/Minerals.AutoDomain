namespace Minerals.AutoDomain.Benchmarks
{
    public class AutoDomainBenchmarks
    {
        public BenchmarkGeneration Baseline { get; set; } = default!;
        public BenchmarkGeneration EntityGeneration { get; set; } = default!;
        public BenchmarkGeneration AggregateRootGeneration { get; set; } = default!;
        public BenchmarkGeneration DomainEventGeneration { get; set; } = default!;
        public BenchmarkGeneration BaselineDouble { get; set; } = default!;
        public BenchmarkGeneration EntityGenerationDouble { get; set; } = default!;
        public BenchmarkGeneration AggregateRootGenerationDouble { get; set; } = default!;
        public BenchmarkGeneration DomainEventGenerationDouble { get; set; } = default!;

        private const string _withoutAttribute = """
        namespace Minerals.Examples
        {
            public partial class ExampleClass { }
        }
        """;

        private const string _withEntityAttribute = """
        namespace Minerals.Examples
        {
            [Minerals.AutoDomain.Entity]
            public partial class ExampleClass { }
        }
        """;

        private const string _withAggregateRootAttribute = """
        namespace Minerals.Examples
        {
            [Minerals.AutoDomain.AggregateRoot]
            public partial class ExampleClass { }
        }
        """;

        private const string _withDomainEventAttribute = """
        namespace Minerals.Examples
        {
            [Minerals.AutoDomain.AggregateRoot, Minerals.AutoDomain.NewDomainEvent("ExampleDomainEvent")]
            public partial class ExampleClass { }
        }
        """;

        [GlobalSetup]
        public void Initialize()
        {
            var references = BenchmarkGenerationExtensions.GetAppReferences
            (
                typeof(object),
                typeof(IAggregateRoot),
                typeof(DomainEventGenerator),
                typeof(StringCases.StringExtensions),
                typeof(CodeBuilder)
            );
            Baseline = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withoutAttribute,
                references
            );
            EntityGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withEntityAttribute,
                new EntityGenerator(),
                references
            );
            AggregateRootGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withAggregateRootAttribute,
                new AggregateRootGenerator(),
                references
            );
            DomainEventGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withDomainEventAttribute,
                new DomainEventGenerator(),
                [new AggregateRootGenerator()],
                references
            );
            BaselineDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withoutAttribute,
                references
            );
            EntityGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withEntityAttribute,
                new EntityGenerator(),
                references
            );
            AggregateRootGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withAggregateRootAttribute,
                new AggregateRootGenerator(),
                references
            );
            DomainEventGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withDomainEventAttribute,
                new DomainEventGenerator(),
                [new AggregateRootGenerator()],
                references
            );
            BaselineDouble.RunAndSaveGeneration();
            BaselineDouble.AddSourceCode("// Test Comment");
            EntityGenerationDouble.RunAndSaveGeneration();
            EntityGenerationDouble.AddSourceCode("// Test Comment");
            AggregateRootGenerationDouble.RunAndSaveGeneration();
            AggregateRootGenerationDouble.AddSourceCode("// Test Comment");
            DomainEventGenerationDouble.RunAndSaveGeneration();
            DomainEventGenerationDouble.AddSourceCode("// Test Comment");
        }

        [Benchmark(Baseline = true)]
        public void SingleGeneration_Baseline()
        {
            Baseline.RunGeneration();
        }

        [Benchmark]
        public void SingleGeneration_Entity()
        {
            EntityGeneration.RunGeneration();
        }

        [Benchmark]
        public void SingleGeneration_AggregateRoot()
        {
            AggregateRootGeneration.RunGeneration();
        }

        [Benchmark]
        public void SingleGeneration_DomainEvent()
        {
            DomainEventGeneration.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_Baseline()
        {
            BaselineDouble.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_Entity()
        {
            EntityGenerationDouble.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_AggregateRoot()
        {
            AggregateRootGenerationDouble.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_DomainEvent()
        {
            DomainEventGenerationDouble.RunGeneration();
        }
    }
}