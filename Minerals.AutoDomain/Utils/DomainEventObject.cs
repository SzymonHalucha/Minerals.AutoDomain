namespace Minerals.AutoDomain.Utils
{
    public readonly struct DomainEventObject : IEquatable<DomainEventObject>
    {
        public string ParentName { get; }
        public string[] Arguments { get; }
        public string Namespace { get; }
        public string Name { get; }

        public DomainEventObject(GeneratorAttributeSyntaxContext context)
        {
            ParentName = GetParentNameOf(context);
            Arguments = GetArgumentsOf(context);
            Namespace = CodeBuilderHelper.GetNamespaceOf(context.TargetNode) ?? string.Empty;
            Name = GetEventName(context);
        }

        public bool Equals(DomainEventObject other)
        {
            return other.ParentName.Equals(ParentName)
                && other.Arguments.SequenceEqual(Arguments)
                && other.Namespace.Equals(Namespace)
                && other.Name.Equals(Name);
        }

        public override bool Equals(object? obj)
        {
            return obj is DomainEventObject other
                && other.ParentName.Equals(ParentName)
                && other.Arguments.SequenceEqual(Arguments)
                && other.Namespace.Equals(Namespace)
                && other.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ParentName, Arguments, Namespace, Name);
        }

        private static string GetParentNameOf(GeneratorAttributeSyntaxContext context)
        {
            return context.TargetSymbol.ContainingType.Name;
        }

        private static string GetEventName(GeneratorAttributeSyntaxContext context)
        {
            return context.Attributes.First().ConstructorArguments.First().Value!.ToString();
        }

        private static string[] GetArgumentsOf(GeneratorAttributeSyntaxContext context)
        {
            return ((MethodDeclarationSyntax)context.TargetNode).ParameterList.Parameters.Select(x => x.ToString()).ToArray();
        }
    }
}