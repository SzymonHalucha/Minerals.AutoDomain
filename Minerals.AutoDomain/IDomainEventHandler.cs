namespace Minerals.AutoDomain
{
    public partial interface IDomainEventHandler<T> where T : IDomainEvent, new()
    {
        public Task Handle(T domainEvent, CancellationToken cancellation);
    }
}