using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace A.WebSite
{
    public class RedisSubscriptionService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private ConnectionMultiplexer _redis;
        private ISubscriber _subscriber;

        public RedisSubscriptionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _redis = ConnectionMultiplexer.Connect("pubsub:6379");
            _subscriber = _redis.GetSubscriber();

            return _subscriber.SubscribeAsync("monitor-reports", (channel, value) =>
            {
                var hubContext = _serviceProvider.GetService<IHubContext<MonitorHub>>();
                hubContext.Clients.All.SendAsync("send", DateTimeOffset.Now, "PubSub", value.ToString());
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.UnsubscribeAll();
            return _redis.CloseAsync();
        }
    }
}