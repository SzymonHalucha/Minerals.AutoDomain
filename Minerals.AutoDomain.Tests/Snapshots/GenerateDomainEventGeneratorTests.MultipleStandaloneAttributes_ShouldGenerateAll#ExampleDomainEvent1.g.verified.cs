﻿//HintName: ExampleDomainEvent1.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoDomain.Generators
// Version: {Removed}
// </auto-generated>
[global::System.Diagnostics.DebuggerNonUserCode]
[global::System.Runtime.CompilerServices.CompilerGenerated]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public readonly partial struct ExampleDomainEvent1 : global::Minerals.AutoDomain.IDomainEvent, global::System.IEquatable<ExampleDomainEvent1>
{
    public TestClassId TestClassId { get; }

    public ExampleDomainEvent1(TestClassId testClassId)
    {
        TestClassId = testClassId;
    }

    public bool Equals(ExampleDomainEvent1 other)
    {
        return other.TestClassId.Equals(TestClassId);
    }

    public override bool Equals(object obj)
    {
        return obj is ExampleDomainEvent1 other && other.TestClassId.Equals(TestClassId);
    }

    public override int GetHashCode()
    {
        return global::System.HashCode.Combine(TestClassId);
    }

    public static bool operator ==(ExampleDomainEvent1 left, ExampleDomainEvent1 right)
    {
        return left.TestClassId.Equals(right.TestClassId);
    }

    public static bool operator !=(ExampleDomainEvent1 left, ExampleDomainEvent1 right)
    {
        return !left.TestClassId.Equals(right.TestClassId);
    }
}