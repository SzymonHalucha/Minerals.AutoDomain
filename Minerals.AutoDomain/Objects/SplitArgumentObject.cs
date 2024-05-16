namespace Minerals.AutoDomain.Objects
{
    public readonly struct SplitArgumentObject(string type, string pascalCaseName, string camelCaseName) : IEquatable<SplitArgumentObject>
    {
        public string Type { get; } = type;
        public string PascalCaseName { get; } = pascalCaseName;
        public string CamelCaseName { get; } = camelCaseName;

        public bool Equals(SplitArgumentObject other)
        {
            return other.Type.Equals(Type)
                && other.PascalCaseName.Equals(PascalCaseName)
                && other.CamelCaseName.Equals(CamelCaseName);
        }

        public override bool Equals(object? obj)
        {
            return obj is SplitArgumentObject other
                && other.Type.Equals(Type)
                && other.PascalCaseName.Equals(PascalCaseName)
                && other.CamelCaseName.Equals(CamelCaseName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, PascalCaseName, CamelCaseName);
        }
    }
}