namespace Minerals.AutoDomain
{
    public partial interface IAggregateRoot : IEntity
    {
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        public void AppendDomainEvent(IDomainEvent domainEvent);
        public void ClearDomainEvents();
    }
}