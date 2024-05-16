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
            return context.TargetNode is BaseTypeDeclarationSyntax typeSyntax
                ? typeSyntax.Identifier.Text
                : context.TargetSymbol.ContainingType.Name;
        }

        private static string[] GetArgumentsOf(GeneratorAttributeSyntaxContext context)
        {
            if (context.TargetNode is TypeDeclarationSyntax typeSyntax)
            {
                return typeSyntax.ParameterList?.Parameters.Select(x => x.ToString()).ToArray() ?? Array.Empty<string>();
            }
            else if (context.TargetNode is MethodDeclarationSyntax methodSyntax)
            {
                return methodSyntax.ParameterList?.Parameters.Select(x => x.ToString()).ToArray() ?? Array.Empty<string>();
            }
            else if (context.TargetNode is ConstructorDeclarationSyntax constructorSyntax)
            {
                return constructorSyntax.ParameterList?.Parameters.Select(x => x.ToString()).ToArray() ?? Array.Empty<string>();
            }
            else if (context.TargetNode is PropertyDeclarationSyntax propertySyntax)
            {
                return [$"{propertySyntax.Type} {propertySyntax.Identifier.Text}"];
            }
            else if (context.TargetNode is FieldDeclarationSyntax fieldSyntax)
            {
                var type = fieldSyntax.Declaration.Type.ToString();
                return fieldSyntax.Declaration.Variables.Select(x => $"{type} {x.Identifier.Text}").ToArray();
            }
            else if (context.TargetNode is EnumDeclarationSyntax enumSyntax)
            {
                return [enumSyntax.Identifier.Text];
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        private static AttributeArgumentsObject[] GetAttributesArgumentsOf(GeneratorAttributeSyntaxContext context)
        {
            var args = new AttributeArgumentsObject[context.Attributes.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = new AttributeArgumentsObject
                (
                    (string)context.Attributes[i].ConstructorArguments.First().Value!,
                    (bool)context.Attributes[i].ConstructorArguments.Skip(1).First().Value!
                );
            }
            return args;
        }
    }
}