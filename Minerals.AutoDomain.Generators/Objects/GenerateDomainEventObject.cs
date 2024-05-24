namespace Minerals.AutoDomain.Generators.Objects
{
    public readonly struct GenerateDomainEventObject : IEquatable<GenerateDomainEventObject>
    {
        public string Namespace { get; }
        public string ParentName { get; }
        public string[] Arguments { get; }
        public AttributeArgumentsObject[] Attributes { get; }

        public GenerateDomainEventObject(GeneratorAttributeSyntaxContext context)
        {
            Namespace = CodeBuilderHelper.GetNamespaceOf(context.TargetNode) ?? string.Empty;
            ParentName = GetParentNameOf(context);
            Arguments = GetArgumentsOf(context);
            Attributes = GetAttributesArgumentsOf(context);
        }

        public bool Equals(GenerateDomainEventObject other)
        {
            return other.Namespace.Equals(Namespace)
                && other.ParentName.Equals(ParentName)
                && other.Arguments.SequenceEqual(Arguments)
                && other.Attributes.SequenceEqual(Attributes);
        }

        public override bool Equals(object? obj)
        {
            return obj is GenerateDomainEventObject other
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
                return GetArgumentsFromParameterList(context.SemanticModel, typeSyntax.ParameterList);
            }
            else if (context.TargetNode is MethodDeclarationSyntax methodSyntax)
            {
                return GetArgumentsFromParameterList(context.SemanticModel, methodSyntax.ParameterList);
            }
            else if (context.TargetNode is ConstructorDeclarationSyntax constructorSyntax)
            {
                return GetArgumentsFromParameterList(context.SemanticModel, constructorSyntax.ParameterList);
            }
            else if (context.TargetNode is PropertyDeclarationSyntax propertySyntax)
            {
                var type = context.SemanticModel.GetDeclaredSymbol(propertySyntax)!.Type;
                return
                [
                    type!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    propertySyntax.Identifier.Text
                ];
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        private static string[] GetArgumentsFromParameterList(SemanticModel model, ParameterListSyntax? listSyntax)
        {
            if (listSyntax is null)
            {
                return Array.Empty<string>();
            }
            var args = new string[listSyntax.Parameters.Count * 2];
            for (int i = 0; i < listSyntax.Parameters.Count; i++)
            {
                var type = model.GetDeclaredSymbol(listSyntax.Parameters[i])!.Type;
                args[i * 2] = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                args[i * 2 + 1] = listSyntax.Parameters[i].Identifier.Text;
            }
            return args;
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