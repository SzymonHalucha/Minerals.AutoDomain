namespace Minerals.AutoDomain
{
    public partial class DomainEventDispatcher(IServiceProvider provider) : IDomainEventDispatcher
    {
        private readonly IServiceProvider _provider = provider;

        public async Task Dispatch<TEvent>(TEvent domainEvent, CancellationToken cancellation) where TEvent : IDomainEvent, new()
        {
            var services = _provider.GetServices<IDomainEventHandler<TEvent>>();
            foreach (var service in services)
            {
                await service.Handle(domainEvent, cancellation);
            }
        }
    }
}