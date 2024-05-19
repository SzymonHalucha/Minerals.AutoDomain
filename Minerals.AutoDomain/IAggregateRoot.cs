namespace Minerals.AutoDomain
{
    public partial interface IAggregateRoot : IEntity
    {
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        public void AddDomainEvent(IDomainEvent domainEvent);
        public void RemoveDomainEvent(IDomainEvent domainEvent);
        public void ClearDomainEvents();
    }
}