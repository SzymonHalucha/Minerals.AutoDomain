﻿//HintName: ExampleDomainEvent.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoDomain
// Version: {Removed}
// </auto-generated>
namespace Minerals.Tests.Events
{
    [global::System.Diagnostics.DebuggerNonUserCode]
    [global::System.Runtime.CompilerServices.CompilerGenerated]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public sealed record ExampleDomainEvent : global::Minerals.AutoDomain.IDomainEvent
    {
        public TestClassId TestClassId { get; private set; }

        public ExampleDomainEvent(TestClassId testClassId)
        {
            TestClassId = testClassId;
        }
    }
}