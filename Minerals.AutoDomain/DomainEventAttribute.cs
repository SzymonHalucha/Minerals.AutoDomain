namespace Minerals.AutoDomain
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class DomainEventAttribute : Attribute;
}