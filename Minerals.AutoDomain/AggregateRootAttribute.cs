namespace Minerals.AutoDomain
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class AggregateRootAttribute : Attribute;
}