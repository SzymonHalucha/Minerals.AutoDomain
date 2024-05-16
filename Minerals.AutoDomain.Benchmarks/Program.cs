namespace Minerals.AutoDomain.Benchmarks
{
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<AutoDomainBenchmarks>
            (
                DefaultConfig.Instance
                    .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest))
                    // .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60))
                    .AddJob(Job.Default.WithRuntime(CoreRuntime.Core80))
                    .AddValidator(JitOptimizationsValidator.FailOnError)
                    .AddDiagnoser(MemoryDiagnoser.Default)
            );
        }
    }
}