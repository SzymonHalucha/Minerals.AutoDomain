namespace Minerals.AutoDomain.Generators.Objects
{
    public readonly struct DomainEventHandlerObject : IEquatable<DomainEventHandlerObject>
    {
        public string HandlerTypeName { get; }
        public string EventTypeName { get; }

        public DomainEventHandlerObject(ISymbol symbol)
        {
            HandlerTypeName = GetHandlerTypeName(symbol);
            EventTypeName = GetHandledEventTypeName(symbol);
        }

        public bool Equals(DomainEventHandlerObject other)
        {
            return other.HandlerTypeName.Equals(HandlerTypeName)
                && other.EventTypeName.Equals(EventTypeName);
        }

        public override bool Equals(object? obj)
        {
            return obj is DomainEventHandlerObject other
                && other.HandlerTypeName.Equals(HandlerTypeName)
                && other.EventTypeName.Equals(EventTypeName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HandlerTypeName, EventTypeName);
        }

        private static string GetHandlerTypeName(ISymbol symbol)
        {
            return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        private static string GetHandledEventTypeName(ISymbol symbol)
        {
            var interfaces = ((ITypeSymbol)symbol).Interfaces.Where(x =>
            {
                return x.Name.Equals("IDomainEventHandler")
                    && x.ContainingNamespace.Name.Equals(nameof(AutoDomain))
                    && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
            });

            return interfaces.First()
                .TypeArguments[0]
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }
    }
}