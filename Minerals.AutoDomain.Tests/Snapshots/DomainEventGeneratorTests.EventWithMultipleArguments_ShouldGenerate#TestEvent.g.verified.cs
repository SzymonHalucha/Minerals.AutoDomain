﻿//HintName: TestEvent.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoDomain.Generators
// Version: {Removed}
// </auto-generated>
[global::System.Diagnostics.DebuggerNonUserCode]
[global::System.Runtime.CompilerServices.CompilerGenerated]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public readonly partial struct TestEvent : global::Minerals.AutoDomain.IDomainEvent, global::System.IEquatable<TestEvent>
{
    public bool Equals(TestEvent other)
    {
        return other.Field1.Equals(Field1) && other.Property1.Equals(Property1) && other.Property2.Equals(Property2);
    }

    public override bool Equals(object obj)
    {
        return obj is TestEvent other && other.Field1.Equals(Field1) && other.Property1.Equals(Property1) && other.Property2.Equals(Property2);
    }

    public override int GetHashCode()
    {
        return global::System.HashCode.Combine(Field1, Property1, Property2);
    }

    public static bool operator ==(TestEvent left, TestEvent right)
    {
        return left.Field1.Equals(right.Field1) && left.Property1.Equals(right.Property1) && left.Property2.Equals(right.Property2);
    }

    public static bool operator !=(TestEvent left, TestEvent right)
    {
        return !left.Field1.Equals(right.Field1) || !left.Property1.Equals(right.Property1) || !left.Property2.Equals(right.Property2);
    }
}