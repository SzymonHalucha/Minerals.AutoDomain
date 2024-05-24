namespace Minerals.AutoDomain.Generators
{
    [Generator]
    public sealed class AggregateRootGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var aggregates = context.SyntaxProvider.ForAttributeWithMetadataName
            (
                "Minerals.AutoDomain.AggregateRootAttribute",
                static (x, _) => x is TypeDeclarationSyntax,
                static (x, _) => new EntityObject(x)
            );

            context.RegisterSourceOutput(aggregates, static (ctx, element) =>
            {
                ctx.AddSource($"{element.Name}Id.g.cs", IdentifierGeneration.AppendStructFile(element));
                ctx.AddSource($"{element.Name}.g.cs", GeneratePartialClass(element));
            });
        }

        private static SourceText GeneratePartialClass(EntityObject entityObj)
        {
            var builder = new CodeBuilder();
            builder.AddAutoGeneratedHeader(Assembly.GetExecutingAssembly());
            AppendNamespace(builder, entityObj);

            builder.AddAutoGeneratedAttributes(typeof(ClassDeclarationSyntax));
            AppendPartialClassHeader(builder, entityObj);
            AppendBases(builder, entityObj);
            AppendInterfaces(builder, entityObj);
            builder.OpenBlock();

            IEquatableGeneration.AppendIdentifier(builder, entityObj.Name);
            AppendDomainEventsProperty(builder);
            AppendDomainEventsField(builder);

            AppendAppendDomainEventMethod(builder);
            AppendClearDomainEventsMethod(builder);

            IEquatableGeneration.AppendEquals(builder, entityObj.Name, "Id.Value");
            IEquatableGeneration.AppendOverrideEquals(builder, entityObj.Name, "Id.Value");
            IEquatableGeneration.AppendOverrideGetHashCode(builder, "Id.Value");
            IEquatableGeneration.AppendOverrideEqualOperator(builder, entityObj.Name, "Id.Value", true);
            IEquatableGeneration.AppendOverrideNotEqualOperator(builder, entityObj.Name, "Id.Value", true);

            builder.CloseAllBlocks();
            return SourceText.From(builder.ToString(), Encoding.UTF8);
        }

        private static void AppendNamespace(CodeBuilder builder, EntityObject entityObj)
        {
            if (entityObj.Namespace != string.Empty)
            {
                builder.WriteLine("namespace ").Write(entityObj.Namespace).OpenBlock();
            }
        }

        private static void AppendPartialClassHeader(CodeBuilder builder, EntityObject entityObj)
        {
            builder.NewLine().WriteIteration(entityObj.Modifiers, (builder1, item, next) =>
            {
                builder1.Write(item).Write(" ");
            });
            builder.Write("class ").Write(entityObj.Name);
        }

        private static void AppendBases(CodeBuilder builder, EntityObject entityObj)
        {
            builder.Write(" : ");
            if (entityObj.Bases.Length > 0)
            {
                builder.WriteIteration(entityObj.Bases, (builder1, item, next) =>
                {
                    builder1.Write(item).Write(", ");
                });
            }
        }

        private static void AppendInterfaces(CodeBuilder builder, EntityObject entityObj)
        {
            builder.Write("global::Minerals.AutoDomain.IAggregateRoot, global::System.IEquatable<")
                .Write(entityObj.Name)
                .Write(">");
        }

        private static void AppendDomainEventsProperty(CodeBuilder builder)
        {
            builder.WriteLine("public global::System.Collections.Generic.IReadOnlyCollection")
                .Write("<global::Minerals.AutoDomain.IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();")
                .NewLine();
        }

        private static void AppendDomainEventsField(CodeBuilder builder)
        {
            builder.WriteLine("private readonly global::System.Collections.Generic.List")
                .Write("<global::Minerals.AutoDomain.IDomainEvent> _domainEvents")
                .Write(" = new global::System.Collections.Generic.List")
                .Write("<global::Minerals.AutoDomain.IDomainEvent>();")
                .NewLine();
        }

        private static void AppendAppendDomainEventMethod(CodeBuilder builder)
        {
            builder.WriteLine("public void AppendDomainEvent(global::Minerals.AutoDomain.IDomainEvent domainEvent)")
                .OpenBlock()
                .WriteLine("_domainEvents.Add(domainEvent);")
                .CloseBlock()
                .NewLine();
        }

        private static void AppendClearDomainEventsMethod(CodeBuilder builder)
        {
            builder.WriteLine("public void ClearDomainEvents()")
                .OpenBlock()
                .WriteLine("_domainEvents.Clear();")
                .CloseBlock()
                .NewLine();
        }
    }
}