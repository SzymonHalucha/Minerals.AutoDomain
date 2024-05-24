namespace Minerals.AutoDomain.Generators.Objects
{
    public readonly struct DomainEventObject : IEquatable<DomainEventObject>
    {
        public string Name { get; }
        public string Namespace { get; }
        public string[] Modifiers { get; }
        public string[] Arguments { get; }

        public DomainEventObject(GeneratorAttributeSyntaxContext context)
        {
            Name = CodeBuilderHelper.GetIdentifierNameOf(context.TargetNode);
            Namespace = CodeBuilderHelper.GetNamespaceOf(context.TargetNode) ?? string.Empty;
            Modifiers = CodeBuilderHelper.GetModifiersOf(context.TargetNode).ToArray();
            Arguments = GetArgumentsOf((TypeDeclarationSyntax)context.TargetNode);
        }

        public bool Equals(DomainEventObject other)
        {
            return other.Name.Equals(Name)
                && other.Namespace.Equals(Namespace)
                && other.Modifiers.SequenceEqual(Modifiers)
                && other.Arguments.SequenceEqual(Arguments);
        }

        public override bool Equals(object? obj)
        {
            return obj is DomainEventObject other
                && other.Name.Equals(Name)
                && other.Namespace.Equals(Namespace)
                && other.Modifiers.SequenceEqual(Modifiers)
                && other.Arguments.SequenceEqual(Arguments);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Namespace, Modifiers, Arguments);
        }

        private static string[] GetArgumentsOf(TypeDeclarationSyntax typeSyntax)
        {
            var fields = typeSyntax.Members.OfType<FieldDeclarationSyntax>()
                .SelectMany(x => x.Declaration.Variables.Select(y => y.Identifier.ValueText));
            var properties = typeSyntax.Members.OfType<PropertyDeclarationSyntax>()
                .Select(x => x.Identifier.ValueText);
            return fields?.Concat(properties)?.ToArray() ?? Array.Empty<string>();
        }
    }
}