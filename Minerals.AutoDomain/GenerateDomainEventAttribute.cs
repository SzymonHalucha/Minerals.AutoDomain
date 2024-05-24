namespace Minerals.AutoDomain
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class GenerateDomainEventAttribute : Attribute
    {
        public GenerateDomainEventAttribute(string name, bool includeParentId = true) { }
    }
}