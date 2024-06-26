﻿//HintName: ExampleDomainEvent2.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoDomain.Generators
// Version: {Removed}
// </auto-generated>
[global::System.Diagnostics.DebuggerNonUserCode]
[global::System.Runtime.CompilerServices.CompilerGenerated]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public readonly partial struct ExampleDomainEvent2 : global::Minerals.AutoDomain.IDomainEvent, global::System.IEquatable<ExampleDomainEvent2>
{
    public TestClassId TestClassId { get; }

    public ExampleDomainEvent2(TestClassId testClassId)
    {
        TestClassId = testClassId;
    }

    public bool Equals(ExampleDomainEvent2 other)
    {
        return other.TestClassId.Equals(TestClassId);
    }

    public override bool Equals(object obj)
    {
        return obj is ExampleDomainEvent2 other && other.TestClassId.Equals(TestClassId);
    }

    public override int GetHashCode()
    {
        return global::System.HashCode.Combine(TestClassId);
    }

    public static bool operator ==(ExampleDomainEvent2 left, ExampleDomainEvent2 right)
    {
        return left.TestClassId.Equals(right.TestClassId);
    }

    public static bool operator !=(ExampleDomainEvent2 left, ExampleDomainEvent2 right)
    {
        return !left.TestClassId.Equals(right.TestClassId);
    }
}