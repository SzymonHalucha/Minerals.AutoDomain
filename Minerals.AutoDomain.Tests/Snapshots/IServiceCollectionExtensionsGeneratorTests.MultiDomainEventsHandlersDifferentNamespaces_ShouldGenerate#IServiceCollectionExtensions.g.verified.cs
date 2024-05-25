﻿//HintName: IServiceCollectionExtensions.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoDomain.Generators
// Version: {Removed}
// </auto-generated>
namespace Minerals.AutoDomain
{
    using global::Microsoft.Extensions.DependencyInjection.Extensions;
    using global::Microsoft.Extensions.DependencyInjection;
    [global::System.Diagnostics.DebuggerNonUserCode]
    [global::System.Runtime.CompilerServices.CompilerGenerated]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddDomainEventDispatcher(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection, global::System.Action<global::Microsoft.Extensions.DependencyInjection.IServiceCollection, global::System.Type, global::System.Type> injectPolicy = null)
        {
            collection.TryAddSingleton<global::Minerals.AutoDomain.IDomainEventDispatcher, global::Minerals.AutoDomain.DomainEventDispatcher>();
            if (injectPolicy is null)
            {
                injectPolicy = DefaultInjectPolicy;
            }
            injectPolicy.Invoke(collection, typeof(global::Minerals.AutoDomain.IDomainEventHandler<global::Examples1.TestDomainEvent>), typeof(global::Handlers1.TestDomainEventHandler1));
            injectPolicy.Invoke(collection, typeof(global::Minerals.AutoDomain.IDomainEventHandler<global::Examples1.TestDomainEvent>), typeof(global::Handlers1.TestDomainEventHandler2));
            return collection;
        }

        private static void DefaultInjectPolicy(global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection, global::System.Type handlerInterfaceType, global::System.Type handlerType)
        {
            collection.AddSingleton(handlerInterfaceType, handlerType);
        }
    }
}