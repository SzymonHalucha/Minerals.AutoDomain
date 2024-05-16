namespace Minerals.AutoDomain.Objects
{
    public readonly struct DomainEventObject : IEquatable<DomainEventObject>
    {
        public string Namespace { get; }
        public string ParentName { get; }
        public string[] Arguments { get; }
        public AttributeArgumentsObject[] Attributes { get; }

        public DomainEventObject(GeneratorAttributeSyntaxContext context)
        {
            Namespace = CodeBuilderHelper.GetNamespaceOf(context.TargetNode) ?? string.Empty;
            ParentName = GetParentNameOf(context);
            Arguments = GetArgumentsOf(context);
            Attributes = GetAttributesArgumentsOf(context);
        }

        public bool Equals(DomainEventObject other)
        {
            return other.Namespace.Equals(Namespace)
                && other.ParentName.Equals(ParentName)
                && other.Arguments.SequenceEqual(Arguments)
                && other.Attributes.SequenceEqual(Attributes);
        }

        public override bool Equals(object? obj)
        {
            return obj is DomainEventObject other
                && other.Namespace.Equals(Namespace)
                && other.ParentName.Equals(ParentName)
                && other.Arguments.SequenceEqual(Arguments)
                && other.Attributes.SequenceEqual(Attributes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Namespace, ParentName, Arguments, Attributes);
        }

        private static string GetParentNameOf(GeneratorAttributeSyntaxContext context)
        {
            return context.TargetSymbol.ContainingType.Name;
        }

        //TODO: Add Multiple Declaration Syntax Support
        private static string[] GetArgumentsOf(GeneratorAttributeSyntaxContext context)
        {
            
            return ((MethodDeclarationSyntax)context.TargetNode).ParameterList.Parameters.Select(x => x.ToString()).ToArray();
        }

        private static AttributeArgumentsObject[] GetAttributesArgumentsOf(GeneratorAttributeSyntaxContext context)
        {
            var args = new AttributeArgumentsObject[context.Attributes.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = new
                (
                    (string)context.Attributes[i].ConstructorArguments.First().Value!,
                    (bool)context.Attributes[i].ConstructorArguments.Skip(1).First().Value!
                );
            }
            return args;
        }
    }
}