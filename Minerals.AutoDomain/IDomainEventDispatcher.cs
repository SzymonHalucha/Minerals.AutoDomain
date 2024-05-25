namespace Minerals.AutoDomain
{
    public partial interface IDomainEventDispatcher
    {
        public Task Dispatch<TEvent>(TEvent domainEvent, CancellationToken cancellation) where TEvent : IDomainEvent, new();
    }
}