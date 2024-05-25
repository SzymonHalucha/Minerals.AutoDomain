namespace Minerals.AutoDomain.Generators
{
    [Generator]
    public sealed class EntityGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var entities = context.SyntaxProvider.ForAttributeWithMetadataName
            (
                "Minerals.AutoDomain.EntityAttribute",
                static (x, _) => x is TypeDeclarationSyntax,
                static (x, _) => new EntityObject(x)
            );

            context.RegisterSourceOutput(entities, static (ctx, element) =>
            {
                ctx.AddSource($"{element.Name}Id.g.cs", IdentifierGeneration.AppendStructFile(element));
                ctx.AddSource($"{element.Name}.g.cs", GeneratePartialEntityObject(element));
            });
        }

        private static SourceText GeneratePartialEntityObject(EntityObject entityObj)
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
            builder.Write(entityObj.Keyword).Write(" ").Write(entityObj.Name);
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
            builder.Write("global::Minerals.AutoDomain.IEntity, global::System.IEquatable<")
                .Write(entityObj.Name)
                .Write(">");
        }
    }
}