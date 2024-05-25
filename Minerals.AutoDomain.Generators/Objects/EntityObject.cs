namespace Minerals.AutoDomain.Generators.Objects
{
    public readonly struct EntityObject : IEquatable<EntityObject>
    {
        public string[] Modifiers { get; }
        public string[] Bases { get; }
        public string Namespace { get; }
        public string Keyword { get; }
        public string Name { get; }

        public EntityObject(GeneratorAttributeSyntaxContext context)
        {
            Modifiers = CodeBuilderHelper.GetModifiersOf(context.TargetNode).ToArray();
            Bases = CodeBuilderHelper.GetBasesOf(context.TargetNode)?.ToArray() ?? Array.Empty<string>();
            Namespace = CodeBuilderHelper.GetNamespaceOf(context.TargetNode) ?? string.Empty;
            Keyword = GetKeywordOf((TypeDeclarationSyntax)context.TargetNode);
            Name = CodeBuilderHelper.GetIdentifierNameOf(context.TargetNode);
        }

        public bool Equals(EntityObject other)
        {
            return other.Modifiers.SequenceEqual(Modifiers)
                && other.Bases.SequenceEqual(Bases)
                && other.Namespace.Equals(Namespace)
                && other.Keyword.Equals(Keyword)
                && other.Name.Equals(Name);
        }

        public override bool Equals(object? obj)
        {
            return obj is EntityObject other
                && other.Modifiers.SequenceEqual(Modifiers)
                && other.Bases.SequenceEqual(Bases)
                && other.Namespace.Equals(Namespace)
                && other.Keyword.Equals(Keyword)
                && other.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Modifiers, Bases, Namespace, Keyword, Name);
        }

        private static string GetKeywordOf(TypeDeclarationSyntax typeSyntax)
        {
            return typeSyntax.Keyword.ToString();
        }
    }
}