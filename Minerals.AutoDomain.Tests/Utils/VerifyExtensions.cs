namespace Minerals.AutoDomain.Tests.Utils
{
    public static class VerifyExtensions
    {
        private static IEnumerable<MetadataReference> s_globalReferences = Array.Empty<MetadataReference>();
        private static bool s_scrubVersionInfo = true;
        private static bool s_scrubCommentLines;
        private static bool s_isInitialized;

        public static void Initialize
        (
            IEnumerable<MetadataReference> globalReferences,
            bool removeVersionInfo = true,
            bool removeCommentLines = false,
            IEnumerable<DiffTool>? order = null
        )
        {
            order ??= new DiffTool[] { DiffTool.VisualStudioCode, DiffTool.VisualStudio, DiffTool.Rider, DiffTool.Neovim, DiffTool.Vim };
            if (s_isInitialized)
            {
                return;
            }

            DiffTools.UseOrder(order.ToArray());
            VerifyBase.UseProjectRelativeDirectory("Snapshots");
            VerifierSettings.UseEncoding(System.Text.Encoding.UTF8);
            VerifySourceGenerators.Initialize();
            s_globalReferences = globalReferences;
            s_scrubCommentLines = removeCommentLines;
            s_scrubVersionInfo = removeVersionInfo;
            s_isInitialized = true;
        }

        public static IEnumerable<MetadataReference> GetAppReferences(params Type[] additionalReferences)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetReferencedAssemblies());
            assemblies = assemblies.Concat(additionalReferences.Select(x => x.Assembly.GetName()));
            return assemblies.Select(x => MetadataReference.CreateFromFile(Assembly.Load(x).Location));
        }

        public static Task VerifyIncrementalGenerators
        (
            this VerifyBase instance,
            IIncrementalGenerator target
        )
        {
            var targets = new IIncrementalGenerator[] { target };
            var additional = Array.Empty<IIncrementalGenerator>();
            return VerifyIncrementalGenerators(instance, targets, additional);
        }

        public static Task VerifyIncrementalGenerators
        (
            this VerifyBase instance,
            IIncrementalGenerator target,
            IEnumerable<IIncrementalGenerator> additional
        )
        {
            var targets = new IIncrementalGenerator[] { target };
            return VerifyIncrementalGenerators(instance, targets, additional);
        }

        public static Task VerifyIncrementalGenerators
        (
            this VerifyBase instance,
            string source,
            IIncrementalGenerator target
        )
        {
            var targets = new IIncrementalGenerator[] { target };
            var additional = Array.Empty<IIncrementalGenerator>();
            return VerifyIncrementalGenerators(instance, source, targets, additional);
        }

        public static Task VerifyIncrementalGenerators
        (
            this VerifyBase instance,
            string source,
            IEnumerable<IIncrementalGenerator> targets
        )
        {
            var additional = Array.Empty<IIncrementalGenerator>();
            return VerifyIncrementalGenerators(instance, source, targets, additional);
        }

        public static Task VerifyIncrementalGenerators
        (
            this VerifyBase instance,
            string source,
            IIncrementalGenerator target,
            IEnumerable<IIncrementalGenerator> additional
        )
        {
            var targets = new IIncrementalGenerator[] { target };
            return VerifyIncrementalGenerators(instance, source, targets, additional);
        }

        public static Task VerifyIncrementalGenerators
        (
            this VerifyBase instance,
            IEnumerable<IIncrementalGenerator> targets,
            IEnumerable<IIncrementalGenerator> additional
        )
        {
            var cSharpCmp = CSharpCompilation.Create("Tests")
                .AddReferences(s_globalReferences)
                .WithOptions(new CSharpCompilationOptions
                (
                    OutputKind.DynamicallyLinkedLibrary,
                    nullableContextOptions: NullableContextOptions.Enable,
                    optimizationLevel: OptimizationLevel.Release
                ));

            CSharpGeneratorDriver.Create(additional.ToArray())
                .RunGeneratorsAndUpdateCompilation(cSharpCmp, out var cmp, out _);

            var driver = CSharpGeneratorDriver.Create(targets.ToArray())
                .RunGenerators(cmp);

            foreach (var diag in cmp.GetDiagnostics())
            {
                var path = diag.Location.GetLineSpan().Path;
                var start = diag.Location.GetLineSpan().StartLinePosition.Line;
                var end = diag.Location.GetLineSpan().EndLinePosition.Line;
                Console.WriteLine($"({path}) [{start}-{end}]: {diag.GetMessage()}.\n");
            }

            return ApplyDynamicSettings(instance.Verify(driver.GetRunResult()));
        }

        public static Task VerifyIncrementalGenerators
        (
            this VerifyBase instance,
            string source,
            IEnumerable<IIncrementalGenerator> targets,
            IEnumerable<IIncrementalGenerator> additional
        )
        {
            var tree = CSharpSyntaxTree.ParseText(source);
            var cSharpCmp = CSharpCompilation.Create("Tests")
                .AddReferences(MetadataReference.CreateFromFile(tree.GetType().Assembly.Location))
                .AddReferences(s_globalReferences)
                .AddSyntaxTrees(tree)
                .WithOptions(new CSharpCompilationOptions
                (
                    OutputKind.DynamicallyLinkedLibrary,
                    nullableContextOptions: NullableContextOptions.Enable,
                    optimizationLevel: OptimizationLevel.Release
                ));

            CSharpGeneratorDriver.Create(additional.ToArray())
                .RunGeneratorsAndUpdateCompilation(cSharpCmp, out var cmp, out _);

            var driver = CSharpGeneratorDriver.Create(targets.ToArray())
                .RunGenerators(cmp);

            foreach (var diag in cmp.GetDiagnostics())
            {
                var path = diag.Location.GetLineSpan().Path;
                var start = diag.Location.GetLineSpan().StartLinePosition.Line;
                var end = diag.Location.GetLineSpan().EndLinePosition.Line;
                Console.WriteLine($"({path}) [{start}-{end}]: {diag.GetMessage()}.\n");
            }

            return ApplyDynamicSettings(instance.Verify(driver.GetRunResult()));
        }

        private static SettingsTask ApplyDynamicSettings(SettingsTask task)
        {
            if (s_scrubCommentLines)
            {
                var isBlockComment = false;
                task.ScrubLines("cs", x =>
                {
                    if (isBlockComment && x.Contains("*/"))
                    {
                        isBlockComment = false;
                        return true;
                    }
                    else if (isBlockComment)
                    {
                        return true;
                    }
                    else if (x.Contains("/*"))
                    {
                        isBlockComment = true;
                        return true;
                    }
                    else
                    {
                        return x.Replace(" ", "").StartsWith("//");
                    }
                });
            }
            else if (s_scrubVersionInfo)
            {
                task.ScrubLinesWithReplace("cs", x =>
                {
                    return x.Replace(" ", "").StartsWith("//Version:") ? "// Version: {Removed}" : x;
                });
            }
            return task;
        }
    }
}