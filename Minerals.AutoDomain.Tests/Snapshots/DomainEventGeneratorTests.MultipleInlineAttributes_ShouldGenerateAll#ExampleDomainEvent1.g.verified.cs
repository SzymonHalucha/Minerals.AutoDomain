﻿//HintName: ExampleDomainEvent1.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoDomain.Generators
// Version: {Removed}
// </auto-generated>
[global::System.Diagnostics.DebuggerNonUserCode]
[global::System.Runtime.CompilerServices.CompilerGenerated]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public readonly partial struct ExampleDomainEvent1 : global::Minerals.AutoDomain.IDomainEvent
{
    public TestClassId TestClassId { get; }

    public ExampleDomainEvent1(TestClassId testClassId)
    {
        TestClassId = testClassId;
    }
}