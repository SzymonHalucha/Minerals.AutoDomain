namespace Minerals.AutoDomain
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class NewDomainEventAttribute : Attribute
    {
        public NewDomainEventAttribute(string name, bool includeParentId = true) { }
    }
}