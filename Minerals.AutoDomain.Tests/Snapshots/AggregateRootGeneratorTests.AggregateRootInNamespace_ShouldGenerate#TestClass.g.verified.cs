﻿//HintName: TestClass.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoDomain.Generators
// Version: {Removed}
// </auto-generated>
namespace Minerals.Tests
{
    [global::System.Diagnostics.DebuggerNonUserCode]
    [global::System.Runtime.CompilerServices.CompilerGenerated]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class TestClass : global::Minerals.AutoDomain.IAggregateRoot, global::System.IEquatable<TestClass>
    {
        public TestClassId Id { get; private set; }

        public global::System.Collections.Generic.IReadOnlyCollection<global::Minerals.AutoDomain.IDomainEvent> DomainEvents => _domainEvents;

        private readonly global::System.Collections.Generic.List<global::Minerals.AutoDomain.IDomainEvent> _domainEvents = new global::System.Collections.Generic.List<global::Minerals.AutoDomain.IDomainEvent>();

        public void AddDomainEvent(global::Minerals.AutoDomain.IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(global::Minerals.AutoDomain.IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public bool Equals(TestClass other)
        {
            return other.Id.Value.Equals(Id.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is TestClass && ((TestClass)obj).Id.Value.Equals(Id.Value);
        }

        public override int GetHashCode()
        {
            return Id.Value.GetHashCode();
        }

        public static bool operator ==(TestClass left, TestClass right)
        {
            return left != null && right != null && left.Id.Value.Equals(right.Id.Value);
        }

        public static bool operator !=(TestClass left, TestClass right)
        {
            return (left != null && right == null) || (left == null && right != null) || !left.Id.Value.Equals(right.Id.Value);
        }
    }
}