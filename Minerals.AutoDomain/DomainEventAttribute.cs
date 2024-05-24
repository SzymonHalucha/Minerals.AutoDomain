namespace Minerals.AutoDomain
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class DomainEventAttribute : Attribute;
}