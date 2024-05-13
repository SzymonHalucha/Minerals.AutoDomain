namespace Minerals.AutoDomain.Utils
{
    public static class IdentifierGeneration
    {
        public static SourceText AppendStructFile(EntityObject entityObj)
        {
            var builder = new CodeBuilder();
            builder.AddAutoGeneratedHeader(Assembly.GetExecutingAssembly());
            AppendNamespace(builder, entityObj);

            builder.AddAutoGeneratedAttributes(typeof(StructDeclarationSyntax));
            AppendStructHeader(builder, entityObj);

            AppendStructValueProperty(builder);
            AppendStructValueField(builder);
            AppendStructConstructor(builder, entityObj);

            var name = $"{entityObj.Name}Id";
            IEquatableGeneration.AppendEquals(builder, name, "Value");
            IEquatableGeneration.AppendOverrideEquals(builder, name, "Value");
            IEquatableGeneration.AppendOverrideGetHashCode(builder, "Value");

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

        private static void AppendStructHeader(CodeBuilder builder, EntityObject entityObj)
        {
            builder.WriteLine("public readonly struct ")
                .Write(entityObj.Name)
                .Write("Id : global::System.IEquatable<")
                .Write(entityObj.Name)
                .Write("Id>")
                .OpenBlock();
        }

        private static void AppendStructValueProperty(CodeBuilder builder)
        {
            builder.WriteLine("public int Value => _value;")
                .NewLine();
        }

        private static void AppendStructValueField(CodeBuilder builder)
        {
            builder.WriteLine("private readonly int _value;")
                .NewLine();
        }

        private static void AppendStructConstructor(CodeBuilder builder, EntityObject entityObj)
        {
            builder.WriteLine("public ")
                .Write(entityObj.Name)
                .Write("Id(int value)")
                .OpenBlock()
                .WriteLine("_value = value;")
                .CloseBlock()
                .NewLine();
        }
    }
}