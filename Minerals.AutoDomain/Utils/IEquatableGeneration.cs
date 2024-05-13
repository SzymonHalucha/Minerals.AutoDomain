namespace Minerals.AutoDomain.Utils
{
    public static class IEquatableGeneration
    {
        public static void AppendIdentifier(CodeBuilder builder, string identifierName)
        {
            builder.WriteLine("public ")
                .Write(identifierName)
                .Write("Id Id { get; private set; }")
                .NewLine();
        }

        public static void AppendEquals(CodeBuilder builder, string objectName, string valueSyntax)
        {
            builder.WriteLine("public bool Equals(")
                .Write(objectName)
                .Write(" other)")
                .OpenBlock()
                .WriteLine("return other.")
                .Write(valueSyntax)
                .Write(".Equals(")
                .Write(valueSyntax)
                .Write(");")
                .CloseBlock()
                .NewLine();
        }

        public static void AppendOverrideEquals(CodeBuilder builder, string objectName, string valueSyntax)
        {
            builder.WriteLine("public override bool Equals(object obj)")
                .OpenBlock()
                .WriteLine("return obj is ")
                .Write(objectName)
                .Write(" && ((")
                .Write(objectName)
                .Write(")obj).")
                .Write(valueSyntax)
                .Write(".Equals(")
                .Write(valueSyntax)
                .Write(");")
                .CloseBlock()
                .NewLine();
        }

        public static void AppendOverrideGetHashCode(CodeBuilder builder, string valueSyntax)
        {
            builder.WriteLine("public override int GetHashCode()")
                .OpenBlock()
                .WriteLine("return ")
                .Write(valueSyntax)
                .Write(".GetHashCode();")
                .CloseBlock();
        }
    }
}