namespace Minerals.AutoDomain.Generators.Objects
{
    public readonly struct AttributeArgumentsObject(string name, bool includeParentId) : IEquatable<AttributeArgumentsObject>
    {
        public string Name { get; } = name;
        public bool IncludeParentId { get; } = includeParentId;

        public bool Equals(AttributeArgumentsObject other)
        {
            return other.Name.Equals(Name)
                && other.IncludeParentId.Equals(IncludeParentId);
        }

        public override bool Equals(object? obj)
        {
            return obj is AttributeArgumentsObject other
                && other.Name.Equals(Name)
                && other.IncludeParentId.Equals(IncludeParentId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, IncludeParentId);
        }
    }
}